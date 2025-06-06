//------------------------------------------------------------------------------
// <copyright file="ListSortDescription.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------
namespace System.ComponentModel {
    using System.Collections;
    using System.Security.Permissions;
    /// <devdoc>
    ///    <para>[To be supplied.]</para>
    /// </devdoc>
    [HostProtection(SharedState = true)]
    public class ListSortDescription {
        PropertyDescriptor property;
        ListSortDirection sortDirection;
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public ListSortDescription(PropertyDescriptor property, ListSortDirection direction) {
            this.property = property;
            this.sortDirection = direction;
        }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public PropertyDescriptor PropertyDescriptor {
            get {
                return this.property;
            }
            set {
                this.property = value;
            }
        }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public ListSortDirection SortDirection {
            get {
                return this.sortDirection;
            }
            set {
                this.sortDirection = value;
            }
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                         I�EH��[A\A]A^A_]�H��K�dH��I�EL)�t6H�UUUUUUU�H��H��H��H��?H��H�H�RH�<�L��H���    L�D$M��H�4$t�L��L��H��[A\A]A^A_]�    K�dH��I�E�t���H�=    �    �                   A��H�W�O1�H��L���             UAWAVAUATSPI��H��H�7H�WW�GH�G     H�(�    L�k L;kuQH�kL�{M)�   �    I��M��tL��L��L���    I��H�} H��t�    L�cK��H�CI��L�c H�k@H�    H��H�C@A�Ff�CH�   �    H�(H�{H�CH�@H�CH�C H��H��t[A\A]A^A_]�    [A\A]A^A_]�     �               H���           AWAVATSH��I��I���?�    M�gI�OI�H)�H��tH��H9�s>L��H��H���    �  H��w9H�5    �   L���    I��H�HL�`H���-H��H���    I$��   I�G�G_f�DWI�$H��I�$I�WH)�H��w%H�5    �   L���    I��H�HL�`H���H�    �Z�Y�f�I�$H��I�$H� H)�H��wH�5    �	   L���    I���H�_unknownH��A_I�$	H�    H�D$H�    H��H�$A�f�D$H��L���    H��[A\A^A_�         A��H�W�O1�H��L���            A��H�W�OD�G1�H��L���                
0x%8.8x:  {0}  [%u] %c Abbreviation code not found in 'debug