//------------------------------------------------------------------------------
// <copyright file="ISynchronizeInvoke.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.ComponentModel {
    using System; 
    using System.Security.Permissions;
         
    /// <devdoc>
    ///    <para>Provides a way to synchronously or asynchronously execute a delegate.</para>
    /// </devdoc>
    public interface ISynchronizeInvoke {
    
        /// <devdoc>
        /// <para>Gets a value indicating whether the caller must call <see cref='System.ComponentModel.ISynchronizeInvoke.Invoke'/> when calling an object that implements 
        ///    this interface.</para>
        /// </devdoc>
        bool InvokeRequired{get;}
                
        /// <devdoc>