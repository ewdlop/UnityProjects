//===-- ARMCallingConv.td - Calling Conventions for ARM ----*- tablegen -*-===//
//
//                     The LLVM Compiler Infrastructure
//
// This file is distributed under the University of Illinois Open Source
// License. See LICENSE.TXT for details.
//
//===----------------------------------------------------------------------===//
// This describes the calling conventions for ARM architecture.
//===----------------------------------------------------------------------===//

/// CCIfAlign - Match of the original alignment of the arg
class CCIfAlign<string Align, CCAction A>:
  CCIf<!strconcat("ArgFlags.getOrigAlign() == ", Align), A>;

//===----------------------------------------------------------------------===//
// ARM APCS Calling Convention
//===----------------------------------------------------------------------===//
def CC_ARM_APCS : CallingConv<[

  // Handles byval parameters.
  CCIfByVal<CCPassByVal<4, 4>>,
    
  CCIfType<[i1, i8, i16], CCPromoteToType<i32>>,

  // Pass SwiftSelf in a callee saved register.
  CCIfSwiftSelf<CCIfType<[i32], CCAssignToReg<[R10]>>>,

  // A SwiftError is passed in R8.
  CCIfSwiftError<CCIfType<[i32], CCAssignToReg<[R8]>>>,

  // Handle all vector types as either f64 or v2f64.
  CCIfType<[v1i64, v2i32, v4i16, v8i8, v2f32], CCBitConvertToType<f64>>,
  CCIfType<[v2i64, v4i32, v8i16, v16i8, v4f32], CCBitConvertToType<v2f64>>,

  // f64 and v2f64 are passed in adjacent GPRs, possibly split onto the stack
  CCIfType<[f64, v2f64], CCCustom<"CC_ARM_APCS_Custom_f64">>,

  CCIfType<[f32], CCBitConvertToType<i32>>,
  CCIfType<[i32], CCAssignToReg<[R0, R1, R2, R3]>>,

  CCIfType<[i32], CCAssignToStack<4, 4>>,
  CCIfType<[f64], CCAssignToStack<8, 4>>,
  CCIfType<[v2f64], CCAssignToStack<16, 4>>
]>;

def RetCC_ARM_APCS : CallingConv<[
  CCIfType<[i1, i8, i16], CCPromoteToType<i32>>,
  CCIfType<[f32], CCBitConvertToType<i32>>,

  // Pass SwiftSelf in a callee saved register.
  CCIfSwiftSelf<CCIfType<[i32], CCAssignToReg<[R10]>>>,

  // A SwiftError is returned in R8.
  CCIfSwiftError<CCIfType<[i32], CCAssignToReg<[R8]>>>,

  // Handle all vector types as either f64 or v2f64.
  CCIfType<[v1i64, v2i32, v4i16, v8i8, v2f32], CCBitConvertToType<f64>>,
  CCIfType<[v2i64, v4i32, v8i16, v16i8, v4f32], CCBitConvertToType<v2f64>>,

  CCIfType<[f64, v2f64], CCCustom<"RetCC_ARM_APCS_Custom_f64">>,

  CCIfType<[i32], CCAssignToReg<[R0, R1, R2, R3]>>,
  CCIfType<[i64], CCAssignToRegWithShadow<[R0, R2], [R1, R3]>>
]>;

//===----------------------------------------------------------------------===//
// ARM APCS Calling Convention for FastCC (when VFP2 or later is available)
//===----------------------------------------------------------------------===//
def FastCC_ARM_APCS : CallingConv<[
  // Handle all vector types as either f64 or v2f64.
  CCIfType<[v1i64, v2i32, v4i16, v8i8, v2f32], CCBitConvertToType<f64>>,
  CCIfType<[v2i64, v4i32, v8i16, v16i8, v4f32], CCBitConvertToType<v2f64>>,

  CCIfType<[v2f64], CCAssignToReg<[Q0, Q1, Q2, Q3]>>,
  CCIfType<[f64], CCAssignToReg<[D0, D1, D2, D3, D4, D5, D6, D7]>>,
  CCIfType<[f32], CCAssignToReg<[S0, S1, S2, S3, S4, S5, S6, S7, S8,
                                 S9, S10, S11, S12, S13, S14, S15]>>,

  // CPRCs may be allocated to co-processor registers or the stack - they
  // may never be allocated to core registers. 
  CCIfType<[f32], CCAssignToStackWithShadow<4, 4, [Q0, Q1, Q2, Q3]>>,
  CCIfType<[f64], CCAssignToStackWithShadow<8, 4, [Q0, Q1, Q2, Q3]>>,
  CCIfType<[v2f64], CCAssignToStackWithShadow<16, 4, [Q0, Q1, Q2, Q3]>>,

  CCDelegateTo<CC_ARM_APCS>
]>;

def RetFastCC_ARM_APCS : CallingConv<[
  // Handle all vector types as either f64 or v2f64.
  CCIfType<[v1i64, v2i32, v4i16, v8i8, v2f32], CCBitConvertToType<f64>>,
  CCIfType<[v2i64, v4i32, v8i16, v16i8, v4f32], CCBitConvertToType<v2f64>>,

  CCIfType<[v2f64], CCAssignToReg<[Q0, Q1, Q2, Q3]>>,
  CCIfType<[f64], CCAssignToReg<[D0, D1, D2, D3, D4, D5, D6, D7]>>,
  CCIfType<[f32], CCAssignToReg<[S0, S1, S2, S3, S4, S5, S6, S7, S8,
                                 S9, S10, S11, S12, S13, S14, S15]>>,
  CCDelegateTo<RetCC_ARM_APCS>
]>;

//===----------------------------------------------------------------------===//
// ARM APCS Calling Convention for GHC
//===----------------------------------------------------------------------===//

def CC_ARM_APCS_GHC : CallingConv<[
  // Handle all vector types as either f64 or v2f64.
  CCIfType<[v1i64, v2i32, v4i16, v8i8, v2f32], CCBitConvertToType<f64>>,
  CCIfType<[v2i64, v4i32, v8i16, v16i8, v4f32], CCBitConvertToType<v2f64>>,

  CCIfType<[v2f64], CCAssignToReg<[Q4, Q5]>>,
  CCIfType<[f64], CCAssignToReg<[D8, D9, D10, D11]>>,
  CCIfType<[f32], CCAssignToReg<[S16, S17, S18, S19, S20, S21, S22, S23]>>,

  // Promote i8/i16 arguments to i32.
  CCIfType<[i8, i16], CCPromoteToType<i32>>,

  // Pass in STG registers: Base, Sp, Hp, R1, R2, R3, R4, SpLim
  CCIfType<[i32], CCAssignToReg<[R4, R5, R6, R7, R8, R9, R10, R11]>>
]>;

//===----------------------------------------------------------------------===//
// ARM AAPCS (EABI) Calling Convention, common parts
//===----------------------------------------------------------------------===//

def CC_ARM_AAPCS_Common : CallingConv<[

  CCIfType<[i1, i8, i16], CCPromoteToType<i32>>,

  // i64/f64 is passed in even pairs of GPRs
  // i64 is 8-aligned i32 here, so we may need to eat R1 as a pad register
  // (and the same is true for f64 if VFP is not enabled)
  CCIfType<[i32], CCIfAlign<"8", CCAssignToRegWithShadow<[R0, R2], [R0, R1]>>>,
  CCIfType<[i32], CCIf<"ArgFlags.getOrigAlign() != 8",
                       CCAssignToReg<[R0, R1, R2, R3]>>>,

  CCIfType<[i32], CCIfAlign<"8", CCAssignToStackWithShadow<4, 8, [R0, R1, R2, R3]>>>,
  CCIfType<[i32], CCAssignToStackWithShadow<4, 4, [R0, R1, R2, R3]>>,
  CCIfType<[f32], CCAssignToStackWithShadow<4, 4, [Q0, Q1, Q2, Q3]>>,
  CCIfType<[f64], CCAssignToStackWithShadow<8, 8, [Q0, Q1, Q2, Q3]>>,
  CCIfType<[v2f64], CCIfAlign<"16",
           CCAssignToStackWithShadow<16, 16, [Q0, Q1, Q2, Q3]>>>,
  CCIfType<[v2f64], CCAssignToStackWithShadow<16, 8, [Q0, Q1, Q2, Q3]>>
]>;

def RetCC_ARM_AAPCS_Common : CallingConv<[
  CCIfType<[i1, i8, i16], CCPromoteToType<i32>>,
  CCIfType<[i32], CCAssignToReg<[R0, R1, R2, R3]>>,
  CCIfType<[i64], CCAssignToRegWithShadow<[R0, R2], [R1, R3]>>
]>;

//===----------------------------------------------------------------------===//
// ARM AAPCS (EABI) Calling Convention
//===----------------------------------------------------------------------===//

def CC_ARM_AAPCS : CallingConv<[
  // Handles byval parameters.
  CCIfByVal<CCPassByVal<4, 4>>,

  // The 'nest' parameter, if any, is passed in R12.
  CCIfNest<CCAssignToReg<[R12]>>,

  // Handle all vector types as either f64 or v2f64.
  CCIfType<[v1i64, v2i32, v4i16, v8i8, v2f32], CCBitConvertToType<f64>>,
  CCIfType<[v2i64, v4i32, v8i16, v16i8, v4f32], CCBitConvertToType<v2f64>>,

  // Pass SwiftSelf in a callee saved register.
  CCIfSwiftSelf<CCIfType<[i32], CCAssignToReg<[R10]>>>,

  // A SwiftError is passed in R8.
  CCIfSwiftError<CCIfType<[i32], CCAssignToReg<[R8]>>>,

  CCIfType<[f64, v2f64], CCCustom<"CC_ARM_AAPCS_Custom_f64">>,
  CCIfType<[f32], CCBitConvertToType<i32>>,
  CCDelegateTo<CC_ARM_AAPCS_Common>
]>;

def RetCC_ARM_AAPCS : CallingConv<[
  // Handle all vector types as either f64 or v2f64.
  CCIfType<[v1i64, v2i32, v4i16, v8i8, v2f32], CCBitConvertToType<f64>>,
  CCIfType<[v2i64, v4i32, v8i16, v16i8, v4f32], CCBitConvertToType<v2f64>>,

  // Pass SwiftSelf in a callee saved register.
  CCIfSwiftSelf<CCIfType<[i32], CCAssignToReg<[R10]>>>,

  // A SwiftError is returned in R8.
  CCIfSwiftError<CCIfType<[i32], CCAssignToReg<[R8]>>>,

  CCIfType<[f64, v2f64], CCCustom<"RetCC_ARM_AAPCS_Custom_f64">>,
  CCIfType<[f32], CCBitConvertToType<i32>>,
  CCDelegateTo<RetCC_ARM_AAPCS_Common>
]>;

//===----------------------------------------------------------------------===//
// ARM AAPCS-VFP (EABI) Calling Convention
// Also used for FastCC (when VFP2 or later is available)
//===----------------------------------------------------------------------===//

def CC_ARM_AAPCS_VFP : CallingConv<[
  // Handles byval parameters.
  CCIfByVal<CCPassByVal<4, 4>>,

  // Handle all vector types as either f64 or v2f64.
  CCIfType<[v1i64, v2i32, v4i16, v8i8, v2f32], CCBitConvertToType<f64>>,
  CCIfType<[v2i64, v4i32, v8i16, v16i8, v4f32], CCBitConvertToType<v2f64>>,

  // Pass SwiftSelf in a callee saved register.
  CCIfSwiftSelf<CCIfType<[i32], CCAssignToReg<[R10]>>>,

  // A SwiftError is passed in R8.
  CCIfSwiftError<CCIfType<[i32], CCAssignToReg<[R8]>>>,

  // HFAs are passed in a contiguous block of registers, or on the stack
  CCIfConsecutiveRegs<CCCustom<"CC_ARM_AAPCS_Custom_Aggregate">>,

  CCIfType<[v2f64], CCAssignToReg<[Q0, Q1, Q2, Q3]>>,
  CCIfType<[f64], CCAssignToReg<[D0, D1, D2, D3, D4, D5, D6, D7]>>,
  CCIfType<[f32], CCAssignToReg<[S0, S1, S2, S3, S4, S5, S6, S7, S8,
                                 S9, S10, S11, S12, S13, S14, S15]>>,
  CCDelegateTo<CC_ARM_AAPCS_Common>
]>;

def RetCC_ARM_AAPCS_VFP : CallingConv<[
  // Handle all vector types as either f64 or v2f64.
  CCIfType<[v1i64, v2i32, v4i16, v8i8, v2f32], CCBitConvertToType<f64>>,
  CCIfType<[v2i64, v4i32, v8i16, v16i8, v4f32], CCBitConvertToType<v2f64>>,

  // Pass SwiftSelf in a callee saved register.
  CCIfSwiftSelf<CCIfType<[i32], CCAssignToReg<[R10]>>>,

  // A SwiftError is returned in R8.
  CCIfSwiftError<CCIfType<[i32], CCAssignToReg<[R8]>>>,

  CCIfType<[v2f64], CCAssignToReg<[Q0, Q1, Q2, Q3]>>,
  CCIfType<[f64], CCAssignToReg<[D0, D1, D2, D3, D4, D5, D6, D7]>>,
  CCIfType<[f32], CCAssignToReg<[S0, S1, S2, S3, S4, S5, S6, S7, S8,
                                 S9, S10, S11, S12, S13, S14, S15]>>,
  CCDelegateTo<RetCC_ARM_AAPCS_Common>
]>;

//===----------------------------------------------------------------------===//
// Callee-saved register lists.
//===----------------------------------------------------------------------===//

def CSR_NoRegs : CalleeSavedRegs<(add)>;
def CSR_FPRegs : CalleeSavedRegs<(add (sequence "D%u", 0, 31))>;

def CSR_AAPCS : CalleeSavedRegs<(add LR, R11, R10, R9, R8, R7, R6, R5, R4,
                                     (sequence "D%u", 15, 8))>;

// R8 is used to pass swifterror, remove it from CSR.
def CSR_AAPCS_SwiftError : CalleeSavedRegs<(sub CSR_AAPCS, R8)>;

// The order of callee-saved registers needs to match the order we actually push
// them in FrameLowering, because this order is what's used by
// PrologEpilogInserter to allocate frame index slots. So when R7 is the frame
// pointer, we use this AAPCS alternative.
def CSR_AAPCS_SplitPush : CalleeSavedRegs<(add LR, R7, R6, R5, R4,
                                               R11, R10, R9, R8,
                                               (sequence "D%u", 15, 8))>;

// R8 is used to pass swifterror, remove it from CSR.
def CSR_AAPCS_SplitPush_SwiftError : CalleeSavedRegs<(sub CSR_AAPCS_SplitPush,
                                                      R8)>;

// Constructors and destructors return 'this' in the ARM C++ ABI; since 'this'
// and the pointer return value are both passed in R0 in these cases, this can
// be partially modelled by treating R0 as a callee-saved register
// Only the resulting RegMask is used; the SaveList is ignored
def CSR_AAPCS_ThisReturn : CalleeSavedRegs<(add LR, R11, R10, R9, R8, R7, R6,
                                            R5, R4, (sequence "D%u", 15, 8),
                                            R0)>;

// iOS ABI deviates from ARM standard ABI. R9 is not a callee-saved register.
// Also save R7-R4 first to match the stack frame fixed spill areas.
def CSR_iOS : CalleeSavedRegs<(add LR, R7, R6, R5, R4, (sub CSR_AAPCS, R9))>;

// R8 is used to pass swifterror, remove it from CSR.
def CSR_iOS_SwiftError : CalleeSavedRegs<(sub CSR_iOS, R8)>;

def CSR_iOS_ThisReturn : CalleeSavedRegs<(add LR, R7, R6, R5, R4,
                                         (sub CSR_AAPCS_ThisReturn, R9))>;

def CSR_iOS_TLSCall
    : CalleeSavedRegs<(add LR, SP, (sub(sequence "R%u", 12, 1), R9, R12),
                      (sequence "D%u", 31, 0))>;

// C++ TLS access function saves all registers except SP. Try to match
// the order of CSRs in CSR_iOS.
def CSR_iOS_CXX_TLS : CalleeSavedRegs<(add CSR_iOS, (sequence "R%u", 12, 1),
                                           (sequence "D%u", 31, 0))>;

// CSRs that are handled by prologue, epilogue.
def CSR_iOS_CXX_TLS_PE : CalleeSavedRegs<(add LR, R12, R11, R7, R5, R4)>;

// CSRs that are handled explicitly via copies.
def CSR_iOS_CXX_TLS_ViaCopy : CalleeSavedRegs<(sub CSR_iOS_CXX_TLS,
                                                   CSR_iOS_CXX_TLS_PE)>;

// The "interrupt" attribute is used to generate code that is acceptable in
// exception-handlers of various kinds. It makes us use a different return
// instruction (handled elsewhere) and affects which registers we must return to
// our "caller" in the same state as we receive them.

// For most interrupts, all registers except SP and LR are shared with
// user-space. We mark LR to be saved anyway, since this is what the ARM backend
// generally does rather than tracking its liveness as a normal register.
def CSR_GenericInt : CalleeSavedRegs<(add LR, (sequence "R%u", 12, 0))>;

// The fast interrupt handlers have more private state and get their own copies
// of R8-R12, in addition to SP and LR. As before, mark LR for saving too.

// FIXME: we mark R11 as callee-saved since it's often the frame-pointer, and
// current frame lowering expects to encounter it while processing callee-saved
// registers.
def CSR_FIQ : CalleeSavedRegs<(add LR, R11, (sequence "R%u", 7, 0))>;

//===----------------------------------------------------------------------===//
// ARM Mono calling conventions
//===----------------------------------------------------------------------===//

def CC_ARM_Mono_APCS : CallingConv<[
  // Mono marks the parameter it wants to pass in this non-abi register with
  // the 'inreg' attribute.
  CCIfInReg<CCAssignToReg<[R8]>>,

  CCDelegateTo<CC_ARM_APCS>
]>;

def CC_ARM_Mono_AAPCS : CallingConv<[
  // Mono marks the parameter it wants to pass in this non-abi register with
  // the 'inreg' attribute.
  CCIfInReg<CCAssignToReg<[R8]>>,

  CCDelegateTo<CC_ARM_AAPCS>
]>;

def CC_ARM_Mono_AAPCS_VFP : CallingConv<[
  // Mono marks the parameter it wants to pass in this non-abi register with
  // the 'inreg' attribute.
  CCIfInReg<CCAssignToReg<[R8]>>,

  CCDelegateTo<CC_ARM_AAPCS_VFP>
]>;

                                                                                                                                                                                                                                                                                                                                                                                                                                                                            ath, path)
                    if logger is not None:
                        logger.info("adding '%s'", path)
                for dirpath, dirnames, filenames in os.walk(base_dir):
                    for name in sorted(dirnames):
                        path = os.path.normpath(os.path.join(dirpath, name))
                        zf.write(path, path)
                        if logger is not None:
                            logger.info("adding '%s'", path)
                    for name in filenames:
                        path = os.path.normpath(os.path.join(dirpath, name))
                        if os.path.isfile(path):
                            zf.write(path, path)
                            if logger is not None:
                                logger.info("adding '%s'", path)

    return zip_filename

_ARCHIVE_FORMATS = {
    'tar':   (_make_tarball, [('compress', None)], "uncompressed tar file"),
    'zip':   (_make_zipfile, [], "ZIP file")
}

if _ZLIB_SUPPORTED:
    _ARCHIVE_FORMATS['gztar'] = (_maâPNG

   IHDR         Ûˇa   	pHYs     öú  
OiCCPPhotoshop ICC profile  x⁄ùSgTSÈ=˜ﬁÙBKàÄîKoR RBãÄë&*!	Jà!°ŸQ¡EE»†àééÄåQ,ä
ÿ‰!¢éÉ£àä ˚·{£k÷º˜ÊÕ˛µ◊>Á¨Ûù≥œ¿ñH3Q5Ä©B‡É«ƒ∆·‰.@Å
$p ≥d!s˝# ¯~<<+"¿æ x” ¿Mõ¿0áˇÍBô\ÄÑ¿të8KÄ @zéB¶ @FÄùò&S † `Àcb„ P- `'Ê” Äù¯ô{ [î!†ë  eàD h; ¨œVäE X0 fKƒ9 ÿ- 0IWfH ∞∑ ¿Œ≤  0QàÖ) { `»##x Ñô FÚW<Ò+ÆÁ*  xô≤<π$9EÅ[-qWW.(ŒI+6aaö@.¬yô2Å4‡ÛÃ  †ë‡ÉÛ˝xŒÆŒŒ6é∂_-Íøˇ"bb„˛Âœ´p@  ·t~—˛,/≥Ä;Äm˛¢%Óh^†u˜ãf≤@µ †È⁄WÛp¯~<<E°êπŸŸÂ‰‰ÿJƒB[a W}˛g¬_¿W˝l˘~<¸˜ı‡æ‚$Å2]ÅG¯‡¬ÃÙL•œí	Ñb‹ÊèG¸∑ˇ¸”"ƒIbπX*„QqéDöåÛ2•"âBí)≈%“ˇd‚ﬂ,˚>ﬂ5 ∞j>{ë-®]cˆK'Xt¿‚˜  Úªo¡‘(ÄhÉ·œwˇÔ?˝G†% ÄfIíq  ^D$.T ≥?«  D†Å*∞AÙ¡,¿¡‹¡¸`6ÑB$ƒ¬BB
dÄr`)¨ÇB(ÜÕ∞*`/‘@4¿QhÜìp.¬U∏=p˙aû¡(ºÅ	A»a!⁄àbäX#éôÖ¯!¡Hã$ …àQ"Kë5H1RäT UHÚ=r9á\F∫ë;» 2Ç¸ÜºG1îÅ≤Q=‘µCπ®7ÑF¢–dt1öè†õ–r¥=å6°Á–´h⁄è>C«0¿Ë3ƒl0.∆√B±8,	ìcÀ±"¨´∆∞V¨ªâıcœ±wÅE¿	6wB aAHXLXNÿH® $4⁄	7	ÑQ¬'"ì®K¥&∫˘ƒb21áXH,#÷è/{àCƒ7$âC2'πêI±§T““F“nR#È,©õ4H#ì…⁄dk≤9î, +»Ö‰ù‰√‰3‰‰!Ú[
ùb@q§¯S‚(R jJÂÂ4Âeò2AU£öR›®°T5èZB≠°∂RØQá®4uö9ÕÉIK•≠¢ï”hh˜iØËt∫›ïNó–W“ÀÈGËóËÙwÜÉ«àg(õgwØòL¶”ã«T071ÎòÁôôoUX*∂*|ë 
ïJï&ï*/T©™¶™ﬁ™UÛUÀTè©^S}ÆFU3S„©	‘ñ´U™ùPÎSSg©;®á™g®oT?§~Y˝âY√L√OC§Q†±_„º∆ c≥x,!k´ÜuÅ5ƒ&±ÕŸ|v*ªò˝ªã=™©°9C3J3W≥RÛîf?„òq¯útN	Á(ßóÛ~äﬁÔ)‚)¶4Lπ1e\k™ñóñX´H´Q´GÎΩ6ÆÌßù¶ΩEªY˚ÅA«J'\'GgèŒùÁSŸS›ß
ßM=:ıÆ.™k•°ªDwønßÓòûæ^ÄûLoßﬁyΩÁ˙}/˝T˝m˙ßıGX≥$€Œ<≈5qo</«€ÒQC]√@C•aïaó·Ñëπ—<£’FçFåi∆\„$„m∆m∆£&&!&KMÍMÓöRMπ¶)¶;L;L«ÕÃÕ¢Õ÷ô5õ=1◊2ÁõÁõ◊õﬂ∑`ZxZ,∂®∂∏eI≤‰Z¶YÓ∂ºnÖZ9Y•XUZ]≥F≠ù≠%÷ª≠ªßßπNìN´û÷g√∞Ò∂…∂©∑∞Âÿ€Æ∂m∂}agbg∑≈Æ√ÓìΩì}∫}ç˝=áŸ´Z~s¥r:V:ﬁöŒúÓ?}≈ÙñÈ/gXœœÿ3„∂À)ƒiùSõ”GggπsÉÛàãâKÇÀ.ó>.õ∆›»Ω‰Jtıq]·z“ıùõ≥õ¬Ì®€ØÓ6ÓiÓá‹üÃ4ü)ûY3s–√»C‡QÂ—?üï0kﬂ¨~OCOÅgµÁ#/c/ëW≠◊∞∑•w™˜aÔ>ˆ>rü„>„<7ﬁ2ﬁY_Ã7¿∑»∑ÀO√oû_ÖﬂC#ˇdˇzˇ— ßÄ%gâÅAÅ[˚¯z|!øé?:€eˆ≤ŸÌAå†πAAèÇ≠ÇÂ¡≠!h»Ïê≠!˜ÁòŒëŒiÖP~Ë÷–aÊaã√~'ÖáÖWÜ?épàX—1ó5w—‹CsﬂD˙DñDﬁõg1O9Ø-J5*>™.j<⁄7∫4∫?∆.fYÃ’XùXIlK9.*Æ6nlæﬂ¸ÌÛá‚ù‚„{ò/»]py°Œ¬ÙÖß©.,:ñ@LàN8îA*®å%Úw%é
y¬¬g"/—6—àÿC\*NÚH*MzíÏëº5y$≈3•,ÂπÑ'©êºLL›õ:ûöv m2=:Ω1ÉíëêqB™!Mì∂gÍgÊfvÀ¨eÖ≤˛≈nã∑/ï…k≥ê¨Y-
∂B¶ËTZ(◊*≤geWføÕâ 9ñ´û+ÕÌÃ≥ €ê7úÔüˇÌ¬·í∂•ÜKW-XÊΩ¨j9≤<qy€
„+ÜV¨<∏ä∂*m’O´ÌWóÆ~Ω&zMkÅ^¡ Ç¡µkÎU
ÂÖ}Î‹◊Ì]OX/Yﬂµa˙Üù>âäÆ€óÿ(‹xÂáo øô‹î¥©´ƒπdœf“fÈÊﬁ-û[ñ™óÊónŸ⁄¥ﬂV¥ÌıˆE€/óÕ(€ªÉ∂Cπ£ø<∏ºeß…ŒÕ;?T§TÙT˙T6Ó“›µa◊¯n—Ó{ºˆ4Ï’€[º˜˝>…æ€UUM’f’e˚I˚≥˜?Æâ™È¯ñ˚m]≠NmqÌ«“˝#∂◊π‘’“=TRè÷+ÎG«æ˛ùÔw-6Uçú∆‚#pDy‰È˜	ﬂ˜:⁄vå{¨·”vg/jBöÚöFõSö˚[b[∫OÃ>—÷Íﬁz¸G€ú4<YyJÛT…i⁄ÈÇ”ìgÚœåùïù}~.˘‹`€¢∂{ÁcŒﬂjoÔ∫t·“EˇãÁ;º;Œ\Ú∏tÚ≤€ÂW∏WöØ:_mÍtÍ<˛ì”O«ªúªöÆπ\kπÓzΩµ{f˜Èû7Œ›ÙΩyÒˇ÷’û9=›ΩÛzo˜≈˜ıﬂ›~r'˝ŒÀªŸw'Ó≠ºOº_Ù@ÌAŸC›á’?[˛‹ÿÔ‹j¿w†Û—‹G˜ÖÉœ˛ëıèCèôèÀÜÜÎû8>99‚?r˝È¸ßCœdœ&û˛¢˛ÀÆ/~¯’Î◊Œ—ò—°óÚóìøm|•˝Í¿ÎØ€∆¬∆æ…x31^ÙV˚Ì¡w‹wÔ£ﬂO‰| (ˇh˘±ıS–ß˚ìììˇòÛ¸c3-€    cHRM  z%  ÄÉ  ˘ˇ  ÄÈ  u0  Í`  :ò  oí_≈F   7IDATx⁄bîóóˇÔÁÁ«p˝˙uR¡Ì€∑ò(£å0j ï`îóóˇOâ    ˇˇ Ωf◊£ÂÎp    IENDÆB`Ç                                                                                                                                                                                                                                                    #"ÌÌ                            @SFA  ∏      Æ  µ         ‚@Ú‡Ã0ŒTU.öÓ #·Û"fÌÒ…ﬁ ,@Õ&>ª$u˛Î⁄ê3Û #CfQ€Ì!øÓÌÒ—0Ù1 +$⁄›Ù]≤C€ÆE?ﬁ0˛Ó -R- ˇπ–334%0Ô˝ﬂÓ &ˇ⁄±ATP·3ÏŒÒ.¨ (ÕıU ıuöÒÕﬁEıV Ôê¸  ‹Ù2gR 'ˇôœÔÓ$51‹ ﬂ›ﬂÒWR—·Ω&@.™ %‰.ŸÂt=—UÔ˛Õ¡   S.ﬁæ31—!.ú5ø +˝A?…Ø2¿W0Ó AÕÏÚ’t33‹ –⁄¡BˇDÔÏ´4Bf ΩÌôÛS!5f ˙ÚÏ¿ -Ê#r‡ê21&@ö¬ üT@‚/¸ü¿EŒ +5*≠P”4ˇ›!ˇº7. "∞g-ﬁDÏ¨#Ô–3‡$ *™1È†"ÒÚu! !˙Ú©Ú.Ú˛D ﬂvÎ–ﬂ *‡ˇ‹·ÂP /‹C˘≥3 ù6/ˇ0ˇÒ5·˝Ó¿O  0„ÚC=ﬂ"ÏõÛ/—Gp "ˇ1æÒπT@¿/˛ .ÓKú6û4N¬f5˙ 4Ò# ‰›"›6p˛ ˝ﬁ! 3ﬁ%?Ô%¨'Lù%AΩ b´6,ø3Ó6>¿‚"ÕÓ ÙbF+æ´Ÿƒu0 b›Íæ?œ3S ƒS˚ÆV cºÚ˝œ·Ú/ﬁ_ &} *π∞Û–‡2@›º üE'A›DÛ˛™Ù@⁄ –g=Ò4øÍÓﬂ'2Æ -%Œ_›‡,ùEOV 8 ﬁõÒ#Û2!Ï¿‡ !4V˘‘P˚ú‚RuAﬁæÌ– ÏıO¡4/ˇ^π∞ÚM”#4 ∫—ÒtﬁÌﬂœÌ @ ?Ì ŸﬂC˙C2"ﬂ@πE  4˝‘.™?Ø7b „ €·Òˇ0 „/#ˇ˘ "ü24˛$#“ˇ› Ú¸œ qC1‹±+„1d¸Ïø $R‹AÈŒ3õÒ#A Ó$R˛≤0Ô˛Ó—2C7 (c…ºÛÚ„2 Ò#"ÌÌ                            @SFA  ∏      Æ  µ        //------------------------------------------------------------------------------
// <copyright file="DiagnosticsConfigurationHandler.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

#if !LIB
#define TRACE
#define DEBUG
namespace System.Diagnostics {

    using System;
    using System.Collections;
    using System.Diagnostics;    
    using System.Xml;
    using System.Configuration;
    using System.Reflection;
    using System.Globalization;
    
    /// <devdoc>
    ///    The configuration section handler for the diagnostics section of the configuration
    ///    file. The section handler participates in the resolution of configuration settings 
    ///    between the &lt;diagnostics&gt; and &lt;/diagnostics&gt;portion of the .config file.
    /// </devdoc>
    /// <internalonly/>
    [Obsolete("This class has been deprecated.  http://go.microsoft.com/fwlink/?linkid=14202")]
    public class DiagnosticsConfigurationHandler : IConfigurationSectionHandler {

        /// <devdoc>
        ///    <para>Parses the configuration settings between the 
        ///       &lt;diagnostics&gt; and &lt;/diagnostics&gt; portion of the .config file to populate
        ///       the values of 'WebServicesConfiguration' object and returning it.
        ///    </para>
        /// </devdoc>
        /// <internalonly/>
        public virtual object Create(object parent, object configContext, XmlNode section) {
            bool foundSwitches = false;
            bool foundAssert = false;
            bool foundTrace = false;
            bool foundCounters = false;

            HandlerBase.CheckForUnrecognizedAttributes(section);

            // Since the tracing and switch code lives in System.Dll and config is in System.Configuration.dll
            // the settings just go into a hashtable to communicate to the values to the diagnostics code in System.dll
            Hashtable parentConfig = (Hashtable)parent;
            Hashtable config;
            if (parentConfig == null)
                config = new Hashtable();
            else
                config = (Hashtable)parentConfig.Clone();

            foreach (XmlNode child in section.ChildNodes) {
                if (HandlerBase.IsIgnorableAlsoCheckForNonElement(child))
                    continue;

                switch (child.Name) {
                    case "switches":                        
                        if (foundSwitches)
                            throw new ConfigurationErrorsException(SR.GetString(SR.ConfigSectionsUnique, "switches"));
                        foundSwitches = true;
            
                        HandleSwitches(config, child, configContext);
                        break;
                    case "assert":
                        if (foundAssert)
                            throw new ConfigurationErrorsException(SR.GetString(SR.ConfigSectionsUnique, "assert"));
                        foundAssert = true;

                        HandleAssert(config, child, configContext);
                        break;
                    case "trace":
                        if (foundTrace)
                            throw new ConfigurationErrorsException(SR.GetString(SR.ConfigSectionsUnique, "trace"));
                        foundTrace = true;

                        HandleTrace(config, child, configContext);
                        break;
                    case "performanceCounters":                      
                        if (foundCounters)
                            throw new ConfigurationErrorsException(SR.GetString(SR.ConfigSectionsUnique, "performanceCounters"));
                        foundCounters = true;

                        HandleCounters((Hashtable)parent, config, child, configContext);                                                    
                        break;
                    default:
                        HandlerBase.ThrowUnrecognizedElement(child);
                        break;
                } // switch(child.Name)
                
                HandlerBase.CheckForUnrecognizedAttributes(child);
            }
            return config;
        }

        private static void HandleSwitches(Hashtable config, XmlNode switchesNode, object context) {
            Hashtable switches = (Hashtable) new SwitchesDictionarySectionHandler().Create(config["switches"], context, switchesNode);
            IDictionaryEnumerator en = switches.GetEnumerator();
            while (en.MoveNext()) {
                try {
                    Int32.Parse((string) en.Value, CultureInfo.InvariantCulture);
                }
                catch {
                    throw new ConfigurationErrorsException(SR.GetString(SR.Value_must_be_numeric, en.Key));
                }
            }

            config["switches"] = switches;
        }

        private static void HandleAssert(Hashtable config, XmlNode assertNode, object context) {
            bool assertuienabled = false;
            if (HandlerBase.GetAndRemoveBooleanAttribute(assertNode, "assertuienabled", ref assertuienabled) != null)
                config["assertuienabled"] = assertuienabled;

            string logfilename = null;
            if (HandlerBase.GetAndRemoveStringAttribute(assertNode, "logfilename", ref logfilename) != null)
                config["logfilename"] = logfilename;

            HandlerBase.CheckForChildNodes(assertNode);
        }

        private static void HandleCounters(Hashtable parent, Hashtable config, XmlNode countersNode, object context) {            
            int filemappingsize = 0;
            if (HandlerBase.GetAndRemoveIntegerAttribute(countersNode, "filemappingsize", ref filemappingsize) != null) {
                //Should only be handled at machine config level
                if (parent == null)
                    config["filemappingsize"] = filemappingsize;
            }                

            HandlerBase.CheckForChildNodes(countersNode);
        }
        
        private static void HandleTrace(Hashtable config, XmlNode traceNode, object context) {
            bool foundListeners = false;
            bool autoflush = false;
            if (HandlerBase.GetAndRemoveBooleanAttribute(traceNode, "autoflush", ref autoflush) != null)
                config["autoflush"] = autoflush;
                                       
            int indentsize = 0;
            if (HandlerBase.GetAndRemoveIntegerAttribute(traceNode, "indentsize", ref indentsize) != null)
                config["indentsize"] = indentsize;

            foreach (XmlNode traceChild in traceNode.ChildNodes) {
                if (HandlerBase.IsIgnorableAlsoCheckForNonElement(traceChild))
                    continue;
                
                if (traceChild.Name == "listeners") {
                    if (foundListeners) 
     