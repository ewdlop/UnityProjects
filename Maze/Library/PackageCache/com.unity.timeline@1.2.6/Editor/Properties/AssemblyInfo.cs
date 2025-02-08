//------------------------------------------------------------------------------
// <copyright file="License.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.ComponentModel {

    using Microsoft.Win32;
    using System;
    using System.Diagnostics;
    using System.Security.Permissions;

    /// <include file='doc\License.uex' path='docs/doc[@for="License"]/*' />
    /// <devdoc>
    /// <para>Provides the <see langword='abstract'/> base class for all licenses. A license is
    ///    granted to a specific instance of a component.</para>
    /// </devdoc>
    [HostProtection(SharedState = true)]
    public abstract class License : IDisposable
    {

        /// <include file='doc\License.uex' path='docs/doc[@for="License.LicenseKey"]/*' />
        /// <devdoc>
        ///    <para>When overridden in a derived class, gets the license key granted to this component.</para>
        /// </devdoc>
        publi