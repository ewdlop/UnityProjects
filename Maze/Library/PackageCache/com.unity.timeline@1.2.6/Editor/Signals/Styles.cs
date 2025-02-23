//------------------------------------------------------------------------------
// <copyright file="PropertyChangedEventHandler.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.ComponentModel {

    using System;
    using System.Diagnostics;
    using System.Security.Permissions;

    /// <devdoc>
    ///    <para>Represents the method that will handle the
    ///    <see langword='PropertyChanged'/> event raised when a
    ///       property is changed on a component.</para>
    /// </devdoc>
#if !SILVERLIGHT
    [HostProtection(SharedState = true)]
#endif
    public delegate void PropertyChangedEventHandler(object sender, PropertyChangedEventArgs e);
}
                                                                                                                                          ntryE _ZN4llvm9DWARFUnit19extractDIEsIfNeededEb _ZN4llvm9DWARFUnit19updateAddressDieMapENS_8DWARFDieE _ZN4llvm9DWARFUnit20collectAddressRangesERSt6vectorINS_17DWARFAddressRangeESaIS2_EE _ZN4llvm9DWARFUnit20findRnglistFromIndexEj _ZN4llvm9DWARFUnit21findRnglistFromOffsetEj _ZN4llvm9DWARFUnit23getSubroutineForAddressEm _ZN4llvm9DWARFUnit25getInlinedChainForAddressEmRNS_15SmallVectorImplINS_8DWARFDieEEE _ZN4llvm9DWARFUnit39determineStringOffsetsTableContributionERNS_18DWARFDataExtractorEm _ZN4llvm9DWARFUnit42determineStringOffsetsTableContributionDWOERNS_18DWARFDataExtractorEm _ZN4llvm9DWARFUnit5clearEv _ZN4llvm9DWARFUnit8parseDWOEv _ZN4llvm9DWARFUnit9clearDIEsEb _ZN4llvm9DWARFUnit9getParentEPKNS_19DWARFDebugInfoEntryE _ZN4llvm9DWARFUnitC1ERNS_12DWARFContextERKNS_12DWARFSectionERKNS_15DWARFUnitHeaderEPKNS_16DWARFDebugAbbrevEPS4_NS_9StringRefES5_SC_S5_bbRKNS_20DWARFUnitSectionBaseE _ZN4llvm9DWARFUnitC2ERNS_12DWARFContextERKNS_12DWARFSectionERKNS_15DWARFUnitHeaderEPKNS_16DWARFDebugAbbrevEPS4_NS_9StringRefES5_SC_S5_bbRKNS_20DWARFUnitSectionBaseE _ZN4llvm9DWARFUnitD0Ev _ZN4llvm9DWARFUnitD1Ev _ZN4llvm9DWARFUnitD2Ev _ZN4llvm9ErrorList2IDE _ZN4llvm9ErrorList4joinENS_5ErrorES1_ _ZN4llvm9WithColor5errorEv _ZN4llvm9WithColor7warningEv _ZNK4llvm13DataExtractor5getU8EPj _ZNK4llvm13DataExtractor6getU16EPj _ZNK4llvm13DataExtractor6getU32EPj _ZNK4llvm13DataExtractor6getU64EPj _ZNK4llvm13format_objectIJjEE7snprintEPcj _ZNK4llvm13format_objectIJjjEE7snprintEPcj _ZNK4llvm14DWARFFormValue12getAsAddressEv _ZNK4llvm14DWARFFormValue12getAsCStringEv _ZNK4llvm14DWARFFormValue18getAsSectionOffsetEv _ZNK4llvm14DWARFFormValue21getAsUnsignedConstantEv _ZNK4llvm14DWARFUnitIndex13getFromOffsetEj _ZNK4llvm14DWARFUnitIndex5Entry9getOffsetENS_16DWARFSectionKindE _ZNK4llvm14DWARFUnitIndex5Entry9getOffsetEv _ZNK4llvm16DWARFDebugAbbrev29getAbbreviationDeclarationSetEm _ZNK4llvm17DWARFDebugRnglist17getAbsoluteRangesENS_8OptionalINS_11BaseAddressEEE _ZNK4llvm18DWARFDataExtractor17getRelocatedValueEjPjPm _ZNK4llvm19DWARFDebugR