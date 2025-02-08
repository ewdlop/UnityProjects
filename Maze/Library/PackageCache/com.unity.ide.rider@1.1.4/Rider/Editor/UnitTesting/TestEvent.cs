//------------------------------------------------------------------------------
// <copyright file="PerformanceCounterManager.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Diagnostics {
    using System.Runtime.InteropServices;    
    using System;    
    using System.Security.Permissions;    
    using System.Security;
    using Microsoft.Win32;

    // All of this code was ported to native and this implementation is no longer used.  It is not meant to be accessed directly.  
    // This code was no longer maintained, and it accessed the same shared memory that the new code accessed.  To 