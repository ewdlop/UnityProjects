//------------------------------------------------------------------------------
// <copyright file="TextWriterTraceListener.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

/*
 */
namespace System.Diagnostics {
    using System;
    using System.IO;
    using System.Security.Permissions;
    using Microsoft.Win32;

    [HostProtection(Synchronization=true)]
    public class ConsoleTraceListener : TextWriterTraceListener {

        public ConsoleTraceListener() : base (Console.Out) {
        }

        public ConsoleTraceListener(bool useErrorStream) : base (useErrorStream ? Console.Error : Console.Out) {
        }

        public override void Close() {
            // No resources to clean up.
        }
    }
}
 

        
                                                                                      _15AnalysisManagerIS2_JEEEJEE3runERS2_RS6_ _ZN4llvm6detail9PassModelINS_8FunctionENS_17JumpThreadingPassENS_17PreservedAnalysesENS_15AnalysisManagerIS2_JEEEJEE4nameEv _ZN4llvm6detail9PassModelINS_8FunctionENS_17JumpThreadingPassENS_17PreservedAnalysesENS_15AnalysisManagerIS2_JEEEJEED0Ev _ZN4llvm6detail9PassModelINS_8FunctionENS_17JumpThreadingPassENS_17PreservedAnalysesENS_15AnalysisManagerIS2_JEEEJEED2Ev _ZN4llvm6detail9PassModelINS_8FunctionENS_17LoopVectorizePassENS_17Preserv