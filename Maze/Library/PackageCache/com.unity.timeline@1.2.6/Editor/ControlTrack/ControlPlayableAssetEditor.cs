//------------------------------------------------------------------------------
// <copyright file="INestedSite.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.ComponentModel {
    
    using System;

    /// <devdoc>
    ///     Nested containers site objects using INestedSite.  A nested
    ///     site is simply a site with an additional property that can
    ///     retrieve the full nested name of a component.
    /// </devdoc>
    public interface INestedSite : ISite {

        /// <devdoc>
        ///     Returns the full name of the component in this site in the format
        ///     of <owner>.<component>.  If this component's site has a null
        ///     name, FullName also returns null.
        /// </devdoc>
        string FullName { get; }
    }
}
                           H��I�H���H�D$I�    I�,$H���H�l$I�$    H�    H��H�L�cW�CH�C    L�|$1�L��L���    H�sH;stH�D$    H�.H��H�s�H�T$L���    H��I�H�|$H��tH��PH�D$    H�|$H��tcH��P�[E1�L�l$I�$    H�uH;utH�D$    L�.H��H�u�H��H�T$H���    H�|$H��tH��PI�H��I�I�    L��H��([A\A]A^A_]�               AWAVSH�� H��I��H�>H�H�5    �P0L�;H�    ��t<�    H��I�H��L���PH�4$H�T$H���    H�HH;HsH�QH�P�
�I��M�>�5�
   H���    H�<$H�D$H9�t�    I�   M��t	I�L���PL��H�� [A^A_�            UAWAVAUATSH��I��H��L�'L�wL��L)�H��H��H���   HE�H�4
H��H��=H��������HE�H�HB�H��H�|$H�t$tH9���  H�<�    �    I���E1�H��L)�H��I�M I�E     I��L��I9��  H�C�L)�������H��t81�1�I�<�I��    I�<�H���H��H9�u�L��H)�L��H)�H��8s��   L��L��H��8��   1�f.�     H�<2H�2    H�<1H�|2H�D2    H�|1H�|2H�D2    H�|1H�|2H�D2    H�|1H�|2 H�D2     H�|1 H�|2(H�D2(    H�|1(H�|20H�D20    H�|10H�|28H�D28    H�|18H�<2H��@H��@H9��V���H���I�,H��H��I9��"  I�F�H)؉�����H��t:1�1�@ H�<�H��    H�|� H���H��H9�u�H��H)�H)�H��8s��   H��H��8��   1�f.�     D  H�4H�    H�4H�tH�D    H�tH�tH�D    H�tH�tH�D    H�tH�t H�D     H�t H�t(H�D(    H�t(H�t0H�D0    H�t0H�t8H�D8    H�t8H�4H��@H��@L9��V���H���H�H��M9�t$L�� H�;H��tH��PH�    H��I9�u�M��tL���    H�L$L�9H�iH�D$I��H�AH��[A\A]A^A_]��            UAWAVAUATSPI��I��I��I��L+/I��H�GH;G��   L9���   H�H�H�@�    H�I�^H�CI�FH���H��L)�H��~<H��H��f�     H�C�H�;H�C�    H�H��tH��PH���H���H���I�$I�$    I�?I�H��t,H��P�$L��L��L���    �I�