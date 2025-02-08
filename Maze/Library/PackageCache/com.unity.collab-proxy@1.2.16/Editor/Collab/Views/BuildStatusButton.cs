//----------------------------------------------------
// <copyright file="PerformanceCounterPermissionEntryCollection.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Diagnostics {
    using System.Security.Permissions;
    using System.Collections;
    
    [
    Serializable()
    ]
    public class PerformanceCounterPermissionEntryCollection : CollectionBase {        
        PerformanceCounterPermission owner;
        
        ///<internalonly/>   
        internal PerformanceCounterPermissionEntryCollection(PerformanceCounterPermission owner, ResourcePermissionBaseEntry[] entries) {
            this.owner = owner;
            for (int index = 0; index < entries.Length; ++index)
                this.InnerList.Add(new PerformanceCounterPermissionEntry(entries[index]));
        }                                                                                                              
                                                                                                            
        public PerformanceCounterPermissionEntry this[int index] {
            get {
                return (PerformanceCounterPermissionEntry)List[index];
            }
            set {
                List[index] = value;
            }
            
        }
        
        public int Add(PerformanceCounterPermissionEntry value) {   
            return List.Add(value);
        }
        
        public void AddRange(PerformanceCounterPermissionEntry[] value) {            
            if (value == null) {
                throw new ArgumentNu