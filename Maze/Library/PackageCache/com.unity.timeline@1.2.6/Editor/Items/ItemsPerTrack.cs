//------------------------------------------------------------------------------
// <copyright file="ITypeDescriptorContext.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

/*
 */
namespace System.ComponentModel {
    using System.Runtime.Remoting;
    using System.Runtime.InteropServices;
    

    using System.Diagnostics;

    /// <devdoc>
    ///    <para> 
    ///       Provides information about a context to a type converter or a value editor,
    ///       so that the type converter or editor can perform a conversion.</para>
    /// </devdoc>
    [System.Runtime.InteropServices.ComVisible(true)]
    public interface ITypeDescriptorContext : IServiceProvider {
    
        /// <devdoc>
        ///    <para>Gets the container with the set of objects for this formatter.</para>
        /// </devdoc>
        IContainer Container { get; }
        
        /// <devdoc>
        ///    <para>Gets the instance that is invok