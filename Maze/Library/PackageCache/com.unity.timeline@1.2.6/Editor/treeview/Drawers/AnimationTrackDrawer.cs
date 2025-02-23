// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// 
// ==--==
namespace System {
 
    using System;
    using System.Runtime.Serialization;
    [Serializable]
[System.Runtime.InteropServices.ComVisible(true)]
    public class SystemException : Exception
    {
        public SystemException() 
            : base(Environment.GetResourceString("Arg_SystemException")) {
            SetErrorCode(__HResults.COR_E_SYSTEM);
        }
        
        public SystemException(String message) 
            : base(message) {
            SetErrorCode(__HResults.COR_E_SYSTEM);
        }
        
        public SystemException(String message, Exception innerException) 
            : base(message, innerException) {
            SetErrorCode(__HResults.COR_E_SYSTEM);
        }

        protected SystemException(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}
                                                                                                  �����u��C ��L�5S_ A�Ft%H�M_ L�D$XH�Q� �   1��   ���   H�=8� ���Ay��A�FtH�_ H�T$XH�5� �   ���   (T$ (�����fnK�4 �����CH�=�� ���W A�F��   H��^ H���   H�T$XH�5�� �   H��@[A^]��(�H�D$H�D$H��H�D$H�H�D$fn�H�� fn�(����)\$0�/f.�     f��H�H�L$H��D$fnL$(T$ (\$0��3 f~$����D$H�D$H� H�L$H�	�H�u�H��@[A^]�f�     AWAVSH�� )$)D$I�։�H�j� �8��   ���u�B ��L�=�] A�Gt%H��] L�D$8H��� �   1��   ���   H�=�� ���w��A�GtH�n] H�T$8H�5r� �   ���   fAf(L$f$fX�fAH�=G� ���pU A�G��   H�] H���   H�T$8H�5� �   L�=�\ A�Gt%H��\ L�D$8H�u� �   1��   ���   H�=\� ����v��A�GtH��\ H�T$8H�5;� �   ���   fAf(L$f$fX�fAH�=� ���T A�Gu
H�� [A^A_�H�a\ H���   H�T$8H�5�� �   H�� [A^A_��fffff.�     AWAVSH�� )$)D$I�։�H��� �8��   ���u�_@ ��L�=�[ A�Gt%H��[ L�D$8H��� �   1��   ���   H�=�� ����u��A�GtH��[ H�T$8H�5�� �   ���   fAf(L$f$f\�fAH�=�� ���S A�G��   H�^[ H���   H�T$8H�5[� �   L�=7[ A�Gt%H�1[ L�D$8H��� �   1��   ���   H�=�� ���%u��A�GtH��Z H�T$8H�5{� �   ���   fAf(L$f$f\�fAH�=P� ����R A�Gu
H�� [A^A_�H��Z H���   H�T$8H�5� �   H�� [A^A_��fffff.�     UAVSH��H�Ӊ�H��� �8�L$�$��   ���u�> ��L�5-Z A�Ft%H�'Z L�D$(H�+� �   1��   ���   H�=� ���t��A�FtH��Y H�T$(H�5�� �   ���   �3�kf(��$�Y�f(��\$�Y��\�f(��Y�f(��Y��X�f.�{
f.��?  ��KH�=�� ���Q A�F��   H�aY H���   H�T$(H�5^� ��   L�5:Y A�Ft%H�4Y L�D$(H��� �   1��   ���   H�=�� ���(s��A�FtH��X H�T$(H�5~� �   ���   �3�kf(��$�Y�f(��\$�Y��\�f(��Y�f(��Y��X�f.�{f.�zb��KH�=� ����P A�Fu	H��[A^]�H�mX H���   H�T$(H�5�� �   H��[A^]��f(�f(��% ����f(�f(���$ �f.�     UAVSH���L$�$H�Ӊ�H��� �8��   ���u�N< ��L�5�W A�Ft%H��W L�D$(H��� �   1��   ���   H�=�� ����q��A�FtH��W H�T$(H�5�� �   ���   ��K�$�\$�}0 ��KH�=m� ���O A�F��   H�DW H���   H�T$(H�5A� ��   L�5W A�Ft%H�W L�D$(H��� �   1��   ���   H�=�� ���q��A�FtH��V H�T$(H�5a� �   ���   ��K�$�\$�/ ��KH�=-� ����N A�Fu	H��[A^]�H�V H���   H�T$(H�5�� �   H��[A^]��ffff.