//------------------------------------------------------------------------------
// <copyright file="IIntellisenseBuilder.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

namespace System.ComponentModel {

    /// <devdoc>
    /// 
    /// </devdoc>
    public interface IIntellisenseBuilder {

        /// <devdoc>
        /// Return a localized name.
        /// </devdoc>
        string Name { get; }

        /// <devdoc>
        /// Show the builder and return a boolean indicating whether value should be replaced with newValue
        /// - false if the user cancels for example
        ///
        /// language - indicates which language service is calling the builder
        /// value - expression being edited
        /// newValue - return the new value
        /// </devdoc> 
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1045:DoNotPassTypesByReference")]
        bool Show(string language, string value, ref string newValue);       
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                        H��H�I�I�� H��H���M9���   I92��   I�JM�BI��H��H9���   H91��   SE� E�H�GxK�@L��K�RL��L��L)�L��H��~BH���������H��H��L��H���     H��H�@H94�sH��H�H��H��H��H���I9�[tH��L)�H��DiЫ���E�H91v	I9�tVA���D���D�D���I�Q�I�I�M�A�M�Q�H9��@����M9�t I�R�M�B�I�J�I���H9�� ���D�D���D�D���D�D���            UAWAVAUATSH��XL���   L���   M9��K  L��L)�M��H��H�<$~>H��M��H��f.�     D  H��H��H��I94:sH��H�I�I�� H��H���M9�tI92uH��I9rw)��  M9���  I�j�H9���  I���I9r��  �M9���  H�H9���  H�t$ H�B�H�D$I��������?L�T$L��H�L$@H�T$0L�L$8f�     D�PE��H�D$H;D$tH;l$�  ��  f.�      H�D$ H9���   H�T$H9B��   H�$H�@xK�RL��H�T$D�zK�L��L��L)�L��H��~LH���������H��H��L��H��f.�      H��H�@H�t$ H94�sH��H�H�<�H��H��H���I9�t'H��L)�H��Di�����E�H�D$ H9�$���I9�tA���H;l$v#�  f.�     f�H�$D�8H;l$��   H�D$H�T$H9B��   H�$H�@xK�RH�4�H�T$D�BK�@L��L��H)�H��H��~CH���������H��H��H��H��@ H��H�@H�l$H9,�sH��H�H�<�H��H��H���I9���  H��H)�H��i�����A�H�D$H9H�$vH9�t=A���D;u<�f.�     @ D;u'H�D$D�PA���E9��&  � H�$D�D;t�E9��  H�iL�T$Hf�H;itD�} H��H�i��   f�     H�H�D$PH)�H��H��H��tH����   H�L��L9�wH��M��H�rI��M��t#M9���   J�<�    �    I��H��H�D$(�
1�H�D$(E1�L�4�    M�E�|� H��H�\$PtH�|$(H��H���    I��H��tH���    H�L$@H�D$(H�L�qK�D� H�AL��I��������?L�T$HA��E9������H�D$H�� L�L$8L9�t=H�(H�T$0H9�������*A���E��H�$D;���������1�H��X[A\A]A^A_]ð���          H��t!H�GhH+G`H��H���������H��H9����1��       AVSH��H��H��t^��tZH�F`H�NhH)�H��H���������H��H9�r8H��H��H��H�4H���H�|$�    �|$ tL�t$M��tL���    ��C H��H��[A^�1��CL�3H�C��     UAWAVAUATSH��   1�H����  �ͅ���  H��L�o`H�GhL)�H��H���������H��H9�s1���  I��L�D$@L�v�M��I��I�4�H���L�H���    H�<$