//===- ARCDisassembler.cpp - Disassembler for ARC ---------------*- C++ -*-===//
//
//                     The LLVM Compiler Infrastructure
//
// This file is distributed under the University of Illinois Open Source
// License. See LICENSE.TXT for details.
//
//===----------------------------------------------------------------------===//
///
/// \file
/// \brief This file is part of the ARC Disassembler.
///
//===----------------------------------------------------------------------===//

#include "ARC.h"
#include "ARCRegisterInfo.h"
#include "MCTargetDesc/ARCMCTargetDesc.h"
#include "llvm/MC/MCContext.h"
#include "llvm/MC/MCDisassembler/MCDisassembler.h"
#include "llvm/MC/MCFixedLenDisassembler.h"
#include "llvm/MC/MCInst.h"
#include "llvm/MC/MCInstrInfo.h"
#include "llvm/MC/MCSubtargetInfo.h"
#include "llvm/Support/TargetRegistry.h"

using namespace llvm;

#define DEBUG_TYPE "arc-disassembler"

using DecodeStatus = MCDisassembler::DecodeStatus;

namespace {

/// \brief A disassembler class for ARC.
class ARCDisassembler : public MCDisassembler {
public:
  std::unique_ptr<MCInstrInfo const> const MCII;

  ARCDisassembler(const MCSubtargetInfo &STI, MCContext &Ctx,
                  MCInstrInfo const *MCII)
      : MCDisassembler(STI, Ctx), MCII(MCII) {}

  DecodeStatus getInstruction(MCInst &Instr, uint64_t &Size,
                              ArrayRef<uint8_t> Bytes, uint64_t Address,
                              raw_ostream &VStream,
                              raw_ostream &CStream) const override;
};

} // end anonymous namespace

static bool readInstruction32(ArrayRef<uint8_t> Bytes, uint64_t Address,
                              uint64_t &Size, uint32_t &Insn) {
  Size = 4;
  // Read 2 16-bit values, but swap hi/lo parts.
  Insn =
      (Bytes[0] << 16) | (Bytes[1] << 24) | (Bytes[2] << 0) | (Bytes[3] << 8);
  return true;
}

static bool readInstruction64(ArrayRef<uint8_t> Bytes, uint64_t Address,
                              uint64_t &Size, uint64_t &Insn) {
  Size = 8;
  Insn = ((uint64_t)Bytes[0] << 16) | ((uint64_t)Bytes[1] << 24) |
         ((uint64_t)Bytes[2] << 0) | ((uint64_t)Bytes[3] << 8) |
         ((uint64_t)Bytes[4] << 48) | ((uint64_t)Bytes[5] << 56) |
         ((uint64_t)Bytes[6] << 32) | ((uint64_t)Bytes[7] << 40);
  return true;
}

static bool readInstruction48(ArrayRef<uint8_t> Bytes, uint64_t Address,
                              uint64_t &Size, uint64_t &Insn) {
  Size = 6;
  Insn = ((uint64_t)Bytes[0] << 0) | ((uint64_t)Bytes[1] << 8) |
         ((uint64_t)Bytes[2] << 32) | ((uint64_t)Bytes[3] << 40) |
         ((uint64_t)Bytes[4] << 16) | ((uint64_t)Bytes[5] << 24);
  return true;
}

static bool readInstruction16(ArrayRef<uint8_t> Bytes, uint64_t Address,
                              uint64_t &Size, uint32_t &Insn) {
  Size = 2;
  Insn = (Bytes[0] << 0) | (Bytes[1] << 8);
  return true;
}

template <unsigned B>
static DecodeStatus DecodeSignedOperand(MCInst &Inst, unsigned InsnS,
                                        uint64_t Address = 0,
                                        const void *Decoder = nullptr);

template <unsigned B>
static DecodeStatus DecodeFromCyclicRange(MCInst &Inst, unsigned InsnS,
                                        uint64_t Address = 0,
                                        const void *Decoder = nullptr);

template <unsigned B>
static DecodeStatus DecodeBranchTargetS(MCInst &Inst, unsigned InsnS,
                                        uint64_t Address, const void *Decoder);

static DecodeStatus DecodeMEMrs9(MCInst &, unsigned, uint64_t, const void *);

static DecodeStatus DecodeLdLImmInstruction(MCInst &, uint64_t, uint64_t,
                                            const void *);

static DecodeStatus DecodeStLImmInstruction(MCInst &, uint64_t, uint64_t,
                                            const void *);

static DecodeStatus DecodeLdRLImmInstruction(MCInst &, uint64_t, uint64_t,
                                             const void *);

static DecodeStatus DecodeMoveHRegInstruction(MCInst &Inst, uint64_t, uint64_t,
                                              const void *);

static const uint16_t GPR32DecoderTable[] = {
    ARC::R0,  ARC::R1,    ARC::R2,  ARC::R3,   ARC::R4,  ARC::R5,  ARC::R6,
    ARC::R7,  ARC::R8,    ARC::R9,  ARC::R10,  ARC::R11, ARC::R12, ARC::R13,
    ARC::R14, ARC::R15,   ARC::R16, ARC::R17,  ARC::R18, ARC::R19, ARC::R20,
    ARC::R21, ARC::R22,   ARC::R23, ARC::R24,  ARC::R25, ARC::GP,  ARC::FP,
    ARC::SP,  ARC::ILINK, ARC::R30, ARC::BLINK};

static DecodeStatus DecodeGPR32RegisterClass(MCInst &Inst, unsigned RegNo,
                                             uint64_t Address,
                                             const void *Decoder) {
  if (RegNo >= 32) {
    DEBUG(dbgs() << "Not a GPR32 register.");
    return MCDisassembler::Fail;
  }

  unsigned Reg = GPR32DecoderTable[RegNo];
  Inst.addOperand(MCOperand::createReg(Reg));
  return MCDisassembler::Success;
}

static DecodeStatus DecodeGBR32ShortRegister(MCInst &Inst, unsigned RegNo,
                                               uint64_t Address,
                                               const void *Decoder) {
  // Enumerates registers from ranges [r0-r3],[r12-r15].
  if (RegNo > 3)
    RegNo += 8; // 4 for r12, etc...

  return DecodeGPR32RegisterClass(Inst, RegNo, Address, Decoder);
}

#include "ARCGenDisassemblerTables.inc"

static unsigned decodeCField(unsigned Insn) {
  return fieldFromInstruction(Insn, 6, 6);
}

static unsigned decodeBField(unsigned Insn) {
  return (fieldFromInstruction(Insn, 12, 3) << 3) |
         fieldFromInstruction(Insn, 24, 3);
}

static unsigned decodeAField(unsigned Insn) {
  return fieldFromInstruction(Insn, 0, 6);
}

static DecodeStatus DecodeMEMrs9(MCInst &Inst, unsigned Insn, uint64_t Address,
                                 const void *Dec) {
  // We have the 9-bit immediate in the low bits, 6-bit register in high bits.
  unsigned S9 = Insn & 0x1ff;
  unsigned R = (Insn & (0x7fff & ~0x1ff)) >> 9;
  DecodeGPR32RegisterClass(Inst, R, Address, Dec);
  Inst.addOperand(MCOperand::createImm(SignExtend32<9>(S9)));
  return MCDisassembler::Success;
}

static bool DecodeSymbolicOperand(MCInst &Inst, uint64_t Address,
                                  uint64_t Value, const void *Decoder) {
  static const uint64_t atLeast = 2;
  // TODO: Try to force emitter to use MCDisassembler* instead of void*.
  auto Disassembler = static_cast<const MCDisassembler *>(Decoder);
  return (nullptr != Disassembler &&
          Disassembler->tryAddingSymbolicOperand(Inst, Value, Address, true, 0,
                                                 atLeast));
}

static void DecodeSymbolicOperandOff(MCInst &Inst, uint64_t Address,
                                     uint64_t Offset, const void *Decoder) {
  uint64_t nextAddress = Address + Offset;

  if (!DecodeSymbolicOperand(Inst, Address, nextAddress, Decoder))
    Inst.addOperand(MCOperand::createImm(Offset));
}

template <unsigned B>
static DecodeStatus DecodeBranchTargetS(MCInst &Inst, unsigned InsnS,
                                        uint64_t Address, const void *Decoder) {

  static_assert(B > 0, "field is empty");
  DecodeSymbolicOperandOff(Inst, Address, SignExtend32<B>(InsnS), Decoder);
  return MCDisassembler::Success;
}

template <unsigned B>
static DecodeStatus DecodeSignedOperand(MCInst &Inst, unsigned InsnS,
                                        uint64_t /*Address*/,
                                        const void * /*Decoder*/) {

  static_assert(B > 0, "field is empty");
  Inst.addOperand(MCOperand::createImm(
      SignExtend32<B>(maskTrailingOnes<decltype(InsnS)>(B) & InsnS)));
  return MCDisassembler::Success;
}

template <unsigned B>
static DecodeStatus DecodeFromCyclicRange(MCInst &Inst, unsigned InsnS,
                                          uint64_t /*Address*/,
                                          const void * /*Decoder*/) {

  static_assert(B > 0, "field is empty");
  const unsigned max = (1u << B) - 1;
  Inst.addOperand(
      MCOperand::createImm(InsnS < max ? static_cast<int>(InsnS) : -1));
  return MCDisassembler::Success;
}

static DecodeStatus DecodeStLImmInstruction(MCInst &Inst, uint64_t Insn,
                                            uint64_t Address,
                                            const void *Decoder) {
  unsigned SrcC, DstB, LImm;
  DstB = decodeBField(Insn);
  if (DstB != 62) {
    DEBUG(dbgs() << "Decoding StLImm found non-limm register.");
    return MCDisassembler::Fail;
  }
  SrcC = decodeCField(Insn);
  DecodeGPR32RegisterClass(Inst, SrcC, Address, Decoder);
  LImm = (Insn >> 32);
  Inst.addOperand(MCOperand::createImm(LImm));
  Inst.addOperand(MCOperand::createImm(0));
  return MCDisassembler::Success;
}

static DecodeStatus DecodeLdLImmInstruction(MCInst &Inst, uint64_t Insn,
                                            uint64_t Address,
                                            const void *Decoder) {
  unsigned DstA, SrcB, LImm;
  DEBUG(dbgs() << "Decoding LdLImm:\n");
  SrcB = decodeBField(Insn);
  if (SrcB != 62) {
    DEBUG(dbgs() << "Decoding LdLImm found non-limm register.");
    return MCDisassembler::Fail;
  }
  DstA = decodeAField(Insn);
  DecodeGPR32RegisterClass(Inst, DstA, Address, Decoder);
  LImm = (Insn >> 32);
  Inst.addOperand(MCOperand::createImm(LImm));
  Inst.addOperand(MCOperand::createImm(0));
  return MCDisassembler::Success;
}

static DecodeStatus DecodeLdRLImmInstruction(MCInst &Inst, uint64_t Insn,
                                             uint64_t Address,
                                             const void *Decoder) {
  unsigned DstA, SrcB;
  DEBUG(dbgs() << "Decoding LdRLimm\n");
  DstA = decodeAField(Insn);
  DecodeGPR32RegisterClass(Inst, DstA, Address, Decoder);
  SrcB = decodeBField(Insn);
  DecodeGPR32RegisterClass(Inst, SrcB, Address, Decoder);
  if (decodeCField(Insn) != 62) {
    DEBUG(dbgs() << "Decoding LdRLimm found non-limm register.");
    return MCDisassembler::Fail;
  }
  Inst.addOperand(MCOperand::createImm((uint32_t)(Insn >> 32)));
  return MCDisassembler::Success;
}

static DecodeStatus DecodeMoveHRegInstruction(MCInst &Inst, uint64_t Insn,
                                              uint64_t Address,
                                              const void *Decoder) {
  DEBUG(dbgs() << "Decoding MOV_S h-register\n");
  using Field = decltype(Insn);
  Field h = fieldFromInstruction(Insn, 5, 3) |
            (fieldFromInstruction(Insn, 0, 2) << 3);
  Field g = fieldFromInstruction(Insn, 8, 3) |
            (fieldFromInstruction(Insn, 3, 2) << 3);

  auto DecodeRegisterOrImm = [&Inst, Address, Decoder](Field RegNum,
                                                       Field Value) {
    if (30 == RegNum) {
      Inst.addOperand(MCOperand::createImm(Value));
      return MCDisassembler::Success;
    }

    return DecodeGPR32RegisterClass(Inst, RegNum, Address, Decoder);
  };

  if (MCDisassembler::Success != DecodeRegisterOrImm(g, 0))
    return MCDisassembler::Fail;

  return DecodeRegisterOrImm(h, Insn >> 16u);
}

DecodeStatus ARCDisassembler::getInstruction(MCInst &Instr, uint64_t &Size,
                                             ArrayRef<uint8_t> Bytes,
                                             uint64_t Address,
                                             raw_ostream &vStream,
                                             raw_ostream &cStream) const {
  MCDisassembler::DecodeStatus Result;
  if (Bytes.size() < 2) {
    Size = 0;
    return Fail;
  }
  uint8_t DecodeByte = (Bytes[1] & 0xF7) >> 3;
  // 0x00 -> 0x07 are 32-bit instructions.
  // 0x08 -> 0x1F are 16-bit instructions.
  if (DecodeByte < 0x08) {
    // 32-bit instruction.
    if (Bytes.size() < 4) {
      // Did we decode garbage?
      Size = 0;
      return Fail;
    }
    if (Bytes.size() >= 8) {
      // Attempt to decode 64-bit instruction.
      uint64_t Insn64;
      if (!readInstruction64(Bytes, Address, Size, Insn64))
        return Fail;
      Result =
          decodeInstruction(DecoderTable64, Instr, Insn64, Address, this, STI);
      if (Success == Result) {
        DEBUG(dbgs() << "Successfully decoded 64-bit instruction.");
        return Result;
      }
      DEBUG(dbgs() << "Not a 64-bit instruction, falling back to 32-bit.");
    }
    uint32_t Insn32;
    if (!readInstruction32(Bytes, Address, Size, Insn32)) {
      return Fail;
    }
    // Calling the auto-generated decoder function.
    return decodeInstruction(DecoderTable32, Instr, Insn32, Address, this, STI);
  } else {
    if (Bytes.size() >= 6) {
      // Attempt to treat as instr. with limm data.
      uint64_t Insn48;
      if (!readInstruction48(Bytes, Address, Size, Insn48))
        return Fail;
      Result =
          decodeInstruction(DecoderTable48, Instr, Insn48, Address, this, STI);
      if (Success == Result) {
        DEBUG(dbgs() << "Successfully decoded 16-bit instruction with limm.");
        return Result;
      }
      DEBUG(dbgs() << "Not a 16-bit instruction with limm, try without it.");
    }

    uint32_t Insn16;
    if (!readInstruction16(Bytes, Address, Size, Insn16))
      return Fail;

    // Calling the auto-generated decoder function.
    return decodeInstruction(DecoderTable16, Instr, Insn16, Address, this, STI);
  }
}

static MCDisassembler *createARCDisassembler(const Target &T,
                                             const MCSubtargetInfo &STI,
                                             MCContext &Ctx) {
  return new ARCDisassembler(STI, Ctx, T.createMCInstrInfo());
}

extern "C" void LLVMInitializeARCDisassembler() {
  // Register the disassembler.
  TargetRegistry::RegisterMCDisassembler(getTheARCTarget(),
                                         createARCDisassembler);
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     # created by tools/tclZIC.tcl - do not edit

set TZData(:Australia/Melbourne) {
    {-9223372036854775808 34792 0 LMT}
    {-2364111592 36000 0 AEST}
    {-1672567140 39600 1 AEDT}
    {-1665392400 36000 0 AEST}
    {-883641600 39600 1 AEDT}
    {-876128400 36000 0 AEST}
    {-860400000 39600 1 AEDT}
    {-844678800 36000 0 AEST}
    {-828345600 39600 1 AEDT}
    {-813229200 36000 0 AEST}
    {31500000 36000 0 AEST}
    {57686400 39600 1 AEDT}
    {67968000 36000 0 AEST}
    {89136000 39600 1 AEDT}
    {100022400 36000 0 AEST}
    {120585600 39600 1 AEDT}
    {131472000 36000 0 AEST}
    {152035200 39600 1 AEDT}
    {162921600 36000 0 AEST}
    {183484800 39600 1 AEDT}
    {194976000 36000 0 AEST}
    {215539200 39600 1 AEDT}
    {226425600 36000 0 AEST}
    {246988800 39600 1 AEDT}
    {257875200 36000 0 AEST}
    {278438400 39600 1 AEDT}
    {289324800 36000 0 AEST}
    {309888000 39600 1 AEDT}
    {320774400 36000 0 AEST}
    {341337600 39600 1 AEDT}
    {352224000 36000 0 AEST}
    {372787200 39600 1 AEDT}
    {384278400 36000 0 AEST}
    {404841600 39600 1 AEDT}
    {415728000 36000 0 AEST}
    {436291200 39600 1 AEDT}
    {447177600 36000 0 AEST}
    {467740800 39600 1 AEDT}
    {478627200 36000 0 AEST}
    {499190400 39600 1 AEDT}
    {511286400 36000 0 AEST}
    {530035200 39600 1 AEDT}
    {542736000 36000 0 AEST}
    {561484800 39600 1 AEDT}
    {574790400 36000 0 AEST}
    {594144000 39600 1 AEDT}
    {606240000 36000 0 AEST}
    {625593600 39600 1 AEDT}
    {637689600 36000 0 AEST}
    {657043200 39600 1 AEDT}
    {667929600 36000 0 AEST}
    {688492800 39600 1 AEDT}
    {699379200 36000 0 AEST}
    {719942400 39600 1 AEDT}
    {731433600 36000 0 AEST}
    {751996800 39600 1 AEDT}
    {762883200 36000 0 AEST}
    {783446400 39600 1 AEDT}
    {796147200 36000 0 AEST}
    {814896000 39600 1 AEDT}
    {828201600 36000 0 AEST}
    {846345600 39600 1 AEDT}
    {859651200 36000 0 AEST}
    {877795200 39600 1 AEDT}
    {891100800 36000 0 AEST}
    {909244800 39600 1 AEDT}
    {922550400 36000 0 AEST}
    {941299200 39600 1 AEDT}
    {954000000 36000 0 AEST}
    {967305600 39600 1 AEDT}
    {985449600 36000 0 AEST}
    {1004198400 39600 1 AEDT}
    {1017504000 36000 0 AEST}
    {1035648000 39600 1 AEDT}
    {1048953600 36000 0 AEST}
    {1067097600 39600 1 AEDT}
    {1080403200 36000 0 AEST}
    {1099152000 39600 1 AEDT}
    {1111852800 36000 0 AEST}
    {1130601600 39600 1 AEDT}
    {1143907200 36000 0 AEST}
    {1162051200 39600 1 AEDT}
    {1174752000 36000 0 AEST}
    {1193500800 39600 1 AEDT}
    {1207411200 36000 0 AEST}
    {1223136000 39600 1 AEDT}
    {1238860800 36000 0 AEST}
    {1254585600 39600 1 AEDT}
    {1270310400 36000 0 AEST}
    {1286035200 39600 1 AEDT}
    {1301760000 36000 0 AEST}
    {1317484800 39600 1 AEDT}
    {1333209600 36000 0 AEST}
    {1349539200 39600 1 AEDT}
    {1365264000 36000 0 AEST}
    {1380988800 39600 1 AEDT}
    {1396713600 36000 0 AEST}
    {1412438400 39600 1 AEDT}
    {1428163200 36000 0 AEST}
    {1443888000 39600 1 AEDT}
    {1459612800 36000 0 AEST}
    {1475337600 39600 1 AEDT}
    {1491062400 36000 0 AEST}
    {1506787200 39600 1 AEDT}
    {1522512000 36000 0 AEST}
    {1538841600 39600 1 AEDT}
    {1554566400 36000 0 AEST}
    {1570291200 39600 1 AEDT}
    {1586016000 36000 0 AEST}
    {1601740800 39600 1 AEDT}
    {1617465600 36000 0 AEST}
    {1633190400 39600 1 AEDT}
    {1648915200 36000 0 AEST}
    {1664640000 39600 1 AEDT}
    {1680364800 36000 0 AEST}
    {1696089600 39600 1 AEDT}
    {1712419200 36000 0 AEST}
    {1728144000 39600 1 AEDT}
    {1743868800 36000 0 AEST}
    {1759593600 39600 1 AEDT}
    {1775318400 36000 0 AEST}
    {1791043200 39600 1 AEDT}
    {1806768000 36000 0 AEST}
    {1822492800 39600 1 AEDT}
    {1838217600 36000 0 AEST}
    {1853942400 39600 1 AEDT}
    {1869667200 36000 0 AEST}
    {1885996800 39600 1 AEDT}
    {1901721600 36000 0 AEST}
    {1917446400 39600 1 AEDT}
    {1933171200 36000 0 AEST}
    {1948896000 39600 1 AEDT}
    {1964620800 36000 0 AEST}
    {1980345600 39600 1 AEDT}
    {1996070400 36000 0 AEST}
    {2011795200 39600 1 AEDT}
    {2027520000 36000 0 AEST}
    {2043244800 39600 1 AEDT}
    {2058969600 36000 0 AEST}
    {2075299200 39600 1 AEDT}
    {2091024000 36000 0 AEST}
    {2106748800 39600 1 AEDT}
    {2122473600 36000 0 AEST}
    {2138198400 39600 1 AEDT}
    {2153923200 36000 0 AEST}
    {2169648000 39600 1 AEDT}
    {2185372800 36000 0 AEST}
    {2201097600 39600 1 AEDT}
    {2216822400 36000 0 AEST}
    {2233152000 39600 1 AEDT}
    {2248876800 36000 0 AEST}
    {2264601600 39600 1 AEDT}
    {2280326400 36000 0 AEST}
    {2296051200 39600 1 AEDT}
    {2311776000 36000 0 AEST}
    {2327500800 39600 1 AEDT}
    {2343225600 36000 0 AEST}
    {2358950400 39600 1 AEDT}
    {2374675200 36000 0 AEST}
    {2390400000 39600 1 AEDT}
    {2406124800 36000 0 AEST}
    {2422454400 39600 1 AEDT}
    {2438179200 36000 0 AEST}
    {2453904000 39600 1 AEDT}
    {2469628800 36000 0 AEST}
    {2485353600 39600 1 AEDT}
    {2501078400 36000 0 AEST}
    {2516803200 39600 1 AEDT}
    {2532528000 36000 0 AEST}
    {2548252800 39600 1 AEDT}
    {2563977600 36000 0 AEST}
    {2579702400 39600 1 AEDT}
    {2596032000 36000 0 AEST}
    {2611756800 39600 1 AEDT}
    {2627481600 36000 0 AEST}
    {2643206400 39600 1 AEDT}
    {2658931200 36000 0 AEST}
    {2674656000 39600 1 AEDT}
    {2690380800 36000 0 AEST}
    {2706105600 39600 1 AEDT}
    {2721830400 36000 0 AEST}
    {2737555200 39600 1 AEDT}
    {2753280000 36000 0 AEST}
    {2769609600 39600 1 AEDT}
    {2785334400 36000 0 AEST}
    {2801059200 39600 1 AEDT}
    {2816784000 36000 0 AEST}
    {2832508800 39600 1 AEDT}
    {2848233600 36000 0 AEST}
    {2863958400 39600 1 AEDT}
    {2879683200 36000 0 AEST}
    {2895408000 39600 1 AEDT}
    {2911132800 36000 0 AEST}
    {2926857600 39600 1 AEDT}
    {2942582400 36000 0 AEST}
    {2958912000 39600 1 AEDT}
    {2974636800 36000 0 AEST}
    {2990361600 39600 1 AEDT}
    {3006086400 36000 0 AEST}
    {3021811200 39600 1 AEDT}
    {3037536000 36000 0 AEST}
    {3053260800 39600 1 AEDT}
    {3068985600 36000 0 AEST}
    {3084710400 39600 1 AEDT}
    {3100435200 36000 0 AEST}
    {3116764800 39600 1 AEDT}
    {3132489600 36000 0 AEST}
    {3148214400 39600 1 AEDT}
    {3163939200 36000 0 AEST}
    {3179664000 39600 1 AEDT}
    {3195388800 36000 0 AEST}
    {3211113600 39600 1 AEDT}
    {3226838400 36000 0 AEST}
    {3242563200 39600 1 AEDT}
    {3258288000 36000 0 AEST}
    {3274012800 39600 1 AEDT}
    {3289737600 36000 0 AEST}
    {3306067200 39600 1 AEDT}
    {3321792000 36000 0 AEST}
    {3337516800 39600 1 AEDT}
    {3353241600 36000 0 AEST}
    {3368966400 39600 1 AEDT}
    {3384691200 36000 0 AEST}
    {3400416000 39600 1 AEDT}
    {3416140800 36000 0 AEST}
    {3431865600 39600 1 AEDT}
    {3447590400 36000 0 AEST}
    {3463315200 39600 1 AEDT}
    {3479644800 36000 0 AEST}
    {3495369600 39600 1 AEDT}
    {3511094400 36000 0 AEST}
    {3526819200 39600 1 AEDT}
    {3542544000 36000 0 AEST}
    {3558268800 39600 1 AEDT}
    {3573993600 36000 0 AEST}
    {3589718400 39600 1 AEDT}
    {3605443200 36000 0 AEST}
    {3621168000 39600 1 AEDT}
    {3636892800 36000 0 AEST}
    {3653222400 39600 1 AEDT}
    {3668947200 36000 0 AEST}
    {3684672000 39600 1 AEDT}
    {3700396800 36000 0 AEST}
    {3716121600 39600 1 AEDT}
    {3731846400 36000 0 AEST}
    {3747571200 39600 1 AEDT}
    {3763296000 36000 0 AEST}
    {3779020800 39600 1 AEDT}
    {3794745600 36000 0 AEST}
    {3810470400 39600 1 AEDT}
    {3826195200 36000 0 AEST}
    {3842524800 39600 1 AEDT}
    {3858249600 36000 0 AEST}
    {3873974400 39600 1 AEDT}
    {3889699200 36000 0 AEST}
    {3905424000 39600 1 AEDT}
    {3921148800 36000 0 AEST}
    {3936873600 39600 1 AEDT}
    {3952598400 36000 0 AEST}
    {3968323200 39600 1 AEDT}
    {3984048000 36000 0 AEST}
    {4000377600 39600 1 AEDT}
    {4016102400 36000 0 AEST}
    {4031827200 39600 1 AEDT}
    {4047552000 36000 0 AEST}
    {4063276800 39600 1 AEDT}
    {4079001600 36000 0 AEST}
    {4094726400 39600 1 AEDT}
}
                                                                                                                                                                                                                                                                                                                                                                           D'`¦j¥Ù6bo·MDUÓ÷×öªL›û°}“$Àh‰à™ Æé åÿ·ü¿ó6Îk]2˜ÔÀÒø"Ã¨‹â
iàìš"z|²uüY*’úÞþJ[ÅãÁÛþU›:ž„TGÒP½*j"l*¡€Ð& § @íÿ?]}ocH"í"O’a·h?ïö©ÿI¢#‚Í‚Šhiô¿DZ%T‚$V‚ú"(4‚¾ ^ˆc0àqÿ†  |ëkûÜnÝÃÁ·þ±€GÑ5K¤É$Éý6ŠõEï¯DÁ•™=ä!dêÔ¥î`mb¸*âeÛF ¶ÍD`w«ESÃdú÷†ß É©ƒv‘p~šUÇH¼È’i•N^mÊºöCkjã¼U¢µQê†Úáµ®N%£E¢