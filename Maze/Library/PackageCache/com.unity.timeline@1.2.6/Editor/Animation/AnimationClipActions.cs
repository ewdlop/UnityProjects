//------------------------------------------------------------------------------
// <copyright file="IBindingList.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------
[assembly: System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Scope="type", Target="System.ComponentModel.IBindingList")]

namespace System.ComponentModel {
    using System.Collections;

    /// <devdoc>
    ///    <para>[To be supplied.]</para>
    /// </devdoc>
    public interface IBindingList : IList {
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        bool AllowNew { get;}
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        object AddNew();
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>

        bool AllowEdit { get; }
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        bool AllowRemove { get; }
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>

        bool SupportsChangeNotification { get; }
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>

        bool SupportsSearching { get; }
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>

        bool SupportsSorting { get; }
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>

        bool IsSorted { get; }
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        PropertyDescriptor SortProperty { get; }
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        ListSortDirection SortDirection { get; }
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>

        event ListChangedEventHandler ListChanged;
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>

        void AddIndex(PropertyDescriptor property);
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        void ApplySort(PropertyDescriptor property, ListSortDirection direction);
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        int Find(PropertyDescriptor property, object key);
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        void RemoveIndex(PropertyDescriptor property);
        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        void RemoveSort();
    }
}

                                                                                                                                                                                                ebugFrame5parseENS_18DWARFDataExtractorE .rela.text._ZL11ReportErrorjPKc .rela.text._ZN4llvm11make_uniqueINS_5dwarf3CIEEJRjRmRhRNS_9StringRefES5_S5_S4_RlS4_S7_S3_S3_RNS_8OptionalImEERNS9_IjEEEEENSt9enable_ifIXntsr3std8is_arrayIT_EE5valueESt10unique_ptrISF_St14default_deleteISF_EEE4typeEDpOT0_ .rela.text._ZN4llvm8toStringB5cxx11ENS_5ErrorE .text._ZNK4llvm15DWARFDebugFrame16getEntryAtOffsetEm .rela.text._ZNK4llvm15DWARFDebugFrame4dumpERNS_11raw_ostreamEPKNS_14MCRegisterInfoENS_8OptionalImEE .rela.text._ZN4llvm5dwarf3CIED2Ev .rela.text._ZN4llvm5dwarf3CIED0Ev .rela.text._ZN4llvm5dwarf3FDED2Ev .rela.text._ZN4llvm5dwarf3FDED0Ev .rela.text._ZNSt6vectorIN4llvm5dwarf10CFIProgram11InstructionESaIS3_EE17_M_realloc_insertIJS3_EEEvN9__gnu_cxx17__normal_iteratorIPS3_S5_EEDpOT_ .rela.