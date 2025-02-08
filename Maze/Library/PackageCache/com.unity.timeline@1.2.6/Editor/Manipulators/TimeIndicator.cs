//------------------------------------------------------------------------------
// <copyright file="InvalidEnumArgumentException.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.ComponentModel {
    using Microsoft.Win32;
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Runtime.Serialization;
#if MONO_FEATURE_CAS
    using System.Security.Permissions;
#endif
    
    /// <devdoc>
    ///    <para>The exception that is thrown when using invalid arguments that are enumerators.</para>
    /// </devdoc>
#if MONO_FEATURE_CAS
    [HostProtection(SharedState = true)]
#endif
    [Serializable]
    public class InvalidEnumArgumentException : ArgumentException {

        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.ComponentModel.InvalidEnumArgumentException'/> class without a message.</para>
        /// </devdoc>
        public InvalidEnumArgumentException() : this(null) {
        }

        /// <devdoc>
        /// <para>Initializes a new instance of the <see cref='System.ComponentModel.InvalidEnumArgumentException'/> class with 
        ///    the specified message.</para>
        /// </devdoc>
        public InvalidEnumArgumentException(string message)
            : base(message) {
        }

        /// <devdoc>
        ///     Initializes a new