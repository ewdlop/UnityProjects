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
    _ARCHIVE_FORMATS['gztar'] = (_ma�PNG

   IHDR         ��a   	pHYs     ��  
OiCCPPhotoshop ICC profile  xڝSgTS�=���BK���KoR RB���&*!	J�!��Q�EEȠ�����Q,�
��!���������{�kּ������>�����H3Q5��B�������.@�
$p �d!s�# �~<<+"�� x� �M��0���B�\���t�8K� @z�B� @F���&S � `�cb� P- `'�� ����{ [�!��  e�D h; ��V�E X0 fK�9 �- 0IWfH �� ���  0Q��) { `�##x �� F�W<�+��*  x��<�$9E�[-qWW.(�I+6aa�@.�y�2�4���  ������x����6��_-��"bb���ϫp@  �t~��,/��;�m��%�h^�u��f�@� ���W�p�~<<E���������J�B[a�W}�g�_�W�l�~<�����$�2]�G�����L�ϒ	�b��G�����"�Ib�X*�Qq�D���2�"�B�)�%��d��,�>�5 �j>{�-�]c�K'Xt���  �o��(�h���w��?�G�% �fI�q  ^D$.Tʳ?�  D��*�A��,�����`6�B$��BB
d�r`)��B(�Ͱ*`/�@4�Qh��p.�U�=p�a��(��	A�a!ڈb�X#����!�H�$ ɈQ"K�5H1R�T UH�=r9�\F��;� 2����G1���Q=��C��7�F��dt1�����r�=�6��Ыhڏ>C�0��3�l0.��B�8,	�c˱"����V����cϱw�E�	6wB aAHXLXN�H� $4�	7	�Q�'"��K�&���b21�XH,#��/{�C�7$�C2'��I��T��F�nR#�,��4H#���dk�9�, +ȅ����3��!�[
�b@q��S�(R�jJ��4�e�2AU��Rݨ�T5�ZB���R�Q��4u�9̓IK�����hh�i��t�ݕN��W���G���w��ǈg(�gw��L�Ӌ�T071���oUX*�*|��
�J�&�*/T����ުU�U�T��^S}�FU3S�	Ԗ�U��P�SSg�;���g�oT?�~Y��Y�L�OC�Q��_�� c�x,!k��u�5�&���|v*�����=���9C3J3W�R�f?�q��tN	�(���~���)�)�4L�1e\k����X�H�Q�G�6����E�Y��A�J'\'Gg����S�Sݧ
�M=:��.�k���Dw�n��^��Lo��y���}/�T�m���GX�$��<�5qo</���QC]�@C�a�a�ᄑ��<��F�F�i�\�$�m�mƣ&&!&KM�M�RM��)�;L;L���͢�֙5�=1�2��כ߷`ZxZ,����eI��Z�Yn�Z9Y�XUZ]�F���%ֻ�����N�N���gð�ɶ�����ۮ�m�}agbg�Ů��}�}��=���Z~s�r:V:ޚΜ�?}����/gX���3��)�i�S��Ggg�s�󈋉K��.�>.���Ƚ�Jt�q]�z��������ۯ�6�i�ܟ�4�)�Y3s���C�Q��?��0k߬~OCO�g��#/c/�W�װ��w��a�>�>r��>�<7�2�Y_�7��ȷ�O�o�_��C#�d�z�� ��%g��A�[��z|!��?:�e����A���AA�������!h�쐭!��Α�i�P~���a�a��~'���W�?�p�X�1�5w��Cs�D�D�Dޛg1O9�-J5*>�.j<�7�4�?�.fY��X�XIlK9.*�6nl��������{�/�]py�����.,:�@L�N8��A*��%�w%�
y��g"/�6ш�C\*N�H*Mz�쑼5y$�3�,幄'���LLݛ:��v m2=:�1����qB�!M��g�g�fvˬe����n��/��k���Y-
�B��TZ(�*�geWf�͉�9���+��̳�ې7�����ᒶ��KW-X潬j9�<qy�
�+�V�<���*m�O��W��~�&zMk�^�ʂ��k�U
�}����]OX/Yߵa���>������(�x��oʿ�ܔ���Ĺd�f�f���-�[����n�ڴ�V����E�/��(ۻ��C���<��e����;?T�T�T�T6��ݵa��n��{��4���[���>ɾ�UUM�f�e�I���?�������m]�Nmq����#�׹���=TR��+�G�����w-6U����#pDy���	��:�v�{���vg/jB��F�S��[b[�O�>����z�G��4<YyJ�T�i��ӓg�ό���}~.��`ۢ�{�c��jo�t��E���;�;�\�t���W�W��:_m�t�<���Oǻ�����\k��z��{f���7����y���՞9=ݽ�zo������~r'��˻�w'O�_�@�A�C݇�?[�����j�w����G��������C���ˆ��8>99�?r����C�d�&����ˮ/~�����јѡ�򗓿m|������������x31^�V���w�w��O�| (�h���SЧ��������c3-�    cHRM  z%  ��  ��  ��  u0  �`  :�  o�_�F   7IDATx�b��������p��uR��۷�(��0j �`����O�    �� �fף��p    IEND�B`�                                                                                                                                                                                                                                                    #"��                            @SFA  �      �  �         �@���0�TU.�� #��"f����� ,@�&>�$u��ڐ3� #CfQ��!�����0�1 +$���]�CۮE?�0�� -R-����334%0���� &�ڱATP�3���.� (��U �u����E�V ��� ���2gR '�����$51� ����WR��&@.� %�.��t=�U����   S.��31�!.�5� +�A?ɯ2�W0�� A����t�33� ���B�D��4Bf ���S!5f ����� -�#r��21&@�� �T@�/���E� +5*�P�4��!��7. "�g-�D�#��3�$ *��1��"��u! !����.��D �v��� *�����P /��C��3 �6/�0��5����O  0��C=�"��/�Gp "���1���T@�/� .�K�6�4N�f5� 4�#���"�6p� ��! 3�%?�%�'L�%A� b�6,�3�6>��"�� �b�F+����u0 b��?�3S��S��V c������/�_ &} *�����2@�� �E'A��D����@� �g=�4����'2� -%�_��,�EOV 8 ���#�2!��� !4V��P���RuA޾�� ��O�4/�^���M�#4 ���t����� @ ?� ��C�C2"�@�E  4��.�?�7b��� �����0 �/#�� "�24�$#��� ��� qC1ܱ+�1d��� $R�A��3��#A �$R��0�����2C7 (cɼ����2 �#"��                            @SFA  �      �  �        //------------------------------------------------------------------------------
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
     