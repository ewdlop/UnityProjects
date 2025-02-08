           const ARMBaseInstrInfo &TII) {
  unsigned Opcode = MI.getOpcode();
  const MCInstrDesc &Desc = MI.getDesc();
  unsigned AddrMode = (Desc.TSFlags & ARMII::AddrModeMask);
  bool isSub = false;

  // Memory operands in inline assembly always use AddrModeT2_i12.
  if (Opcode == ARM::INLINEASM)
    AddrMode = ARMII::AddrModeT2_i12; // FIXME. mode for thumb2?

  if (Opcode == ARM::t2ADDri || Opcode == ARM::t2ADDri12) {
    Offset += MI.getOperand(FrameRegIdx+1).getImm();

    unsigned PredReg;
    if (Offset == 0 && getInstrPredicate(MI, PredReg) == ARMCC::AL) {
      // Turn it into a move.
      MI.setDesc(TII.get(ARM::tMOVr));
      MI.getOperand(FrameRegIdx).ChangeToRegister(FrameReg, false);
      // Remove offset and remaining explicit predicate operands.
      do MI.RemoveOperand(FrameRegIdx+1);
      while (MI.getNumOperands() > FrameRegIdx+1);
      MachineInstrBuilder MIB(*MI.getParent()->getParent(), &MI);
      MIB.add(predOps(ARMCC::AL));
      return true;
    }

    bool HasCCOut = Opcode != ARM::t2ADDri12;

    if (Offset < 0) {
      Offset = -Offset;
      isSub = true;
      MI.setDesc(TII.get(ARM::t2SUBri));
    } else {
      MI.setDesc(TII.get(ARM::t2ADDri));
    }

    // Common case: small offset, fits into instruction.
    if (ARM_AM::getT2SOImmVal(Offset) != -1) {
      MI.getOperand(FrameRegIdx).ChangeToRegister(FrameReg, false);
      MI.getOperand(FrameRegIdx+1).ChangeToImmediate(Offset);
      // Add cc_out operand if the original instruction did not have one.
      if (!HasCCOut)
        MI.addOperand(MachineOperand::CreateReg(0, false));
      Offset = 0;
      return true;
    }
    // Another common case: imm12.
    if (Offset < 4096 &&
        (!HasCCOut || MI.getOperand(MI.getNumOperands()-1).getReg() == 0)) {
      unsigned NewOpc = isSub ? ARM::t2SUBri12 : ARM::t2ADDri12;
      MI.setDesc(TII.get(NewOpc));
      MI.getOperand(FrameRegIdx).ChangeToRegister(FrameReg, false);
      MI.getOperand(FrameRegIdx+1).ChangeToImmediate(Offset);
      // Remove the cc_out operand.
      if (HasCCOut)
        MI.RemoveOperand(MI.getNumOperands()-1);
      Offset = 0;
      return true;
    }

    // Otherwise, extract 8 adjacent bits from the immediate into this
    // t2ADDri/t2SUBri.
    unsigned RotAmt = countLeadingZeros<unsigned>(Offset);
    unsigned ThisImmVal = Offset & ARM_AM::rotr32(0xff000000U, RotAmt);

    // We will handle these bits from offset, clear them.
    Offset &= ~ThisImmVal;

    assert(ARM_AM::getT2SOImmVal(ThisImmVal) != -1 &&
           "Bit extraction didn't work?");
    MI.getOperand(FrameRegIdx+1).ChangeToImmediate(ThisImmVal);
    // Add cc_out operand if the original instruction did not have one.
    if (!HasCCOut)
      MI.addOperand(MachineOperand::CreateReg(0, false));
  } else {
    // AddrMode4 and AddrMode6 cannot handle any offset.
    if (AddrMode == ARMII::AddrMode4 || AddrMode == ARMII::AddrMode6)
      return false;

    // AddrModeT2_so cannot handle any offset. If there is no offset
    // register then we change to an immediate version.
    unsigned NewOpc = Opcode;
    if (AddrMode == ARMII::AddrModeT2_so) {
      unsigned OffsetReg = MI.getOperand(FrameRegIdx+1).getReg();
      if (OffsetReg != 0) {
        MI.getOperand(FrameRegIdx).ChangeToRegister(FrameReg, false);
        return Offset == 0;
      }

      MI.RemoveOperand(FrameRegIdx+1);
      MI.getOperand(FrameRegIdx+1).ChangeToImmediate(0);
      NewOpc = immediateOffsetOpcode(Opcode);
      AddrMode = ARMII::AddrModeT2_i12;
    }

    unsigned NumBits = 0;
    unsigned Scale = 1;
    if (AddrMode == ARMII::AddrModeT2_i8 || AddrMode == ARMII::AddrModeT2_i12) {
      // i8 supports only negative, and i12 supports only positive, so
      // based on Offset sign convert Opcode to the appropriate
      // instruction
      Offset += MI.getOperand(FrameRegIdx+1).getImm();
      if (Offset < 0) {
        NewOpc = negativeOffsetOpcode(Opcode);
        NumBits = 8;
        isSub = true;
        Offset = -Offset;
      } else {
        NewOpc = positiveOffsetOpcode(Opcode);
        NumBits = 12;
      }
    } else if (AddrMode == ARMII::AddrMode5) {
      // VFP address mode.
      const MachineOperand &OffOp = MI.getOperand(FrameRegIdx+1);
      int InstrOffs = ARM_AM::getAM5Offset(OffOp.getImm());
      if (ARM_AM::getAM5Op(OffOp.getImm()) == ARM_AM::sub)
        InstrOffs *= -1;
      NumBits = 8;
      Scale = 4;
      Offset += InstrOffs * 4;
      assert((Offset & (Scale-1)) == 0 && "Can't encode this offset!");
      if (Offset < 0) {
        Offset = -Offset;
        isSub = true;
      }
    } else if (AddrMode == ARMII::AddrModeT2_i8s4) {
      Offset += MI.getOperand(FrameRegIdx + 1).getImm() * 4;
      NumBits = 10; // 8 bits scaled by 4
      // MCInst operand expects already scaled value.
      Scale = 1;
      assert((Offset & 3) == 0 && "Can't encode this offset!");
    } else {
      llvm_unreachable("Unsupported addressing mode!");
    }

    if (NewOpc != Opcode)
      MI.setDesc(TII.get(NewOpc));

    MachineOperand &ImmOp = MI.getOperand(FrameRegIdx+1);

    // Attempt to fold address computation
    // Common case: small offset, fits into instruction.
    int ImmedOffset = Offset / Scale;
    unsigned Mask = (1 << NumBits) - 1;
    if ((unsigned)Offset <= Mask * Scale) {
      // Replace the FrameIndex with fp/sp
      MI.getOperand(FrameRegIdx).ChangeToRegister(FrameReg, false);
      if (isSub) {
        if (AddrMode == ARMII::AddrMode5)
          // FIXME: Not consistent.
          ImmedOffset |= 1 << NumBits;
        else
          ImmedOffset = -ImmedOffset;
      }
      ImmOp.ChangeToImmediate(ImmedOffset);
      Offset = 0;