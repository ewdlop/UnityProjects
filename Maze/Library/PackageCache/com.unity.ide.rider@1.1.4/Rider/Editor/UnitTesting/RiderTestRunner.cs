//------------------------------------------------------------------------------
// <copyright file="InstanceDataCollection.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Diagnostics {
    using System;
    using System.Diagnostics;
    using System.Collections;
    using System.Globalization;
    
    /// <devdoc>
    ///     A collection containing all the instance data for a counter.  This collection is contained in the 
    ///     <see cref='System.Diagnostics.InstanceDataCollectionCollection'/> when using the 
    ///     <see cref='System.Diagnostics.PerformanceCounterCategory.ReadCategory'/> method.  
    /// </devdoc>    
    public class InstanceDataCollection : DictionaryBase {
        private string counterName;

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        [Obsolete("This constructor has been deprecated.  Please use System.Diagnostics.InstanceDataCollectionCollection.get_Item to get an instance of this collection instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
        public InstanceDataCollection(string counterName) {
            if (counterName == null)
                throw new ArgumentNullException("counterName");
            this.counterName = counterName;
        }

        /// <devdoc>
        ///    <para>[To be supplied.]</para>
        /// </devdoc>
        public string CounterName {
            get {
                return counterName;
      