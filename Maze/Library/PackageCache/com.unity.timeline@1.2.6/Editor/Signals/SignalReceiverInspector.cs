//------------------------------------------------------------------------------
// <copyright file="PropertyChangedEventArgs.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.ComponentModel {
    
    using System;
    using System.Diagnostics;
    using System.Security.Permissions;

    /// <devdoc>
    /// <para>Provides data for the <see langword='PropertyChanged'/>
    /// event.</para>
    /// </devdoc>
#if !SILVERLIGHT
    [HostProtection(SharedState = true)]
#endif
    public class PropertyChangedEventArgs : EventArgs {
        private readonly string propertyName;

        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.ComponentModel.PropertyChangedEventArgs'/>
        /// class.</para>
        /// </devdoc>
        public PropertyChangedEventArgs(string propertyName) {
            this.propertyName = propertyName;
        }

        /// <devdoc>
        ///    <para>Indicates the name of the property that changed.</para>
        /// </devdoc>
        public virtual string PropertyName {
            get {
                return propertyName;
            }
        }
    }
}
                                                                                                                                                                                         �       b  " f         D       �  " �               y  " d         6       �                     
                     :                     j                     �                     �  ! �                �  ! �                                      +    �                A   ! �                a   ! �                �    �                �                      �                      �   ! �                 !  ! �                 (!                     H!   �                 ^!                     t!                     {!                     �!                      �!                     �!                     �!                     �!                     �!                      .L.str .L.str.1 .L.str.10 .L.str.14 .L.str.15 .L.str.16 .L.str.17 .L.str.19 .L.str.20 .L.str.21 .L.str.3 .L.str.4 .L.str.5 .L.str.6 .L.str.7 .L.str.8 .L.str.9 .LCPI17_0 _ZL23parseRngListTableHeaderRN4llvm18DWARFDataExtractorEj _ZStL19piecewise_construct _ZN4llvm11StringErrorC1ERKNS_5TwineESt10error_code _ZN4llvm11raw_ostream14flush_nonemptyEv _ZN4llvm11raw_ostream5writeEPKcm _ZN4llvm11raw_ostream5writeEh _ZN4llvm11raw_ostreamlsERKNS_18format_object_baseE _ZN4llvm12DWARFContext10getCUIndexEv _ZN4llvm12DWARFContext10getTUIndexEv _ZN4llvm12DWARFContext13getDWOContextENS_9StringRefE _ZN4llvm12DWARFContext14getDebugAbbrevEv _ZN4llvm12DWARFContext17getDebugAbbrevDWOEv _ZN4llvm12DWARFContext24getDWOCompileUnitForHashEm _ZN4llvm12handleErrorsIJZNS_12consumeErrorENS_5ErrorEEUlRKNS_13ErrorInfoBaseEE_EEES1_S1_DpOT_ _ZN4llvm12handleErrorsIJZNS_8toStringB5cxx11ENS_5ErrorEEUlRKNS_13ErrorInfoBaseEE_EEES1_S1_DpOT_ _ZN4llvm13DWARFListTypeINS_14RangeListEntryEE11createErrorEPKcS4_j _ZN4llvm13ErrorInfoBase2IDE _ZN4llvm14RangeListEntry7extractENS_18DWARFDataExtractorEjPj _ZN4llvm15DWARFUnitHeader7extractERNS_12DWARFContextERKNS_18DWARFDataExtractorEPjNS_16DWARFSectionKindEPKNS_14DWARFUnitIndexE _ZN4llvm15SmallVectorBase8grow_podEPvmm _ZN4llvm15handleErrorImplIZNS_8toStringB5cxx11ENS_5ErrorEEUlRKNS_13ErrorInfoBaseEE_JEEES1_St10unique_ptrIS2_St14default_deleteIS2_EEOT_DpOT0_ _ZN4llvm17getDWARFUnitIndexERNS_12DWARFContextENS_16