//------------------------------------------------------------------------------
// <copyright file="ThreadWaitReason.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.Diagnostics {
    using System.Threading;

    using System.Diagnostics;
    /// <devdoc>
    ///     Specifies the reason a thread is waiting.
    /// </devdoc>
    public enum ThreadWaitReason {
    
        /// <devdoc>
        ///     Thread is waiting for the scheduler.
        /// </devdoc>
        Executive,
        
        /// <devdoc>
        ///     Thread is waiting for a free virtual memory page.
        /// </devdoc>
        FreePage,
        
        /// <devdoc>
        ///     Thread is waiting for a virtual memory page to arrive in memory.
        /// </devdoc>
        PageIn,
        
        /// <devdoc>
        ///     Thread is waiting for a system allocation.
        /// </devdoc>
        SystemAllocation,
        
        /// <devdoc>
        ///     Thread execution is delayed.
        /// </devdoc>
        ExecutionDelay,

        /// <devdoc>
        ///     Thread execution is suspended.
        /// </devdoc>
        Suspended,

        /// <devdoc>
        ///     Thread is waiting for a user request.
        /// </devdoc>
        UserRequest,

        /// <devdoc>
        ///     Thread is waiting for event pair high.
        /// </devdoc>
        EventPairHigh,
        
        /// <devdoc>
        ///     Thread is waiting for event pair low.
        /// </devdoc>
        EventPairLow,
        
        /// <devdoc>
        ///     Thread is waiting for a local procedure call to arrive.
        /// </devdoc>
        LpcReceive,
        
        /// <devdoc>
        ///     Thread is waiting for reply to a local procedure call to arrive.
        /// </devdoc>
        LpcReply,
        
        /// <devdoc>
        ///     Thread is waiting for virtual memory.
        /// </devdoc>
        VirtualMemory,
        
        /// <devdoc>
        ///     Thread is waiting for a virtual memory page to be written to disk.
        /// </devdoc>
        PageOut,
        
        /// <devdoc>
        ///     Thread is waiting for an unknown reason.
        /// </devdoc>
        Unknown
    }
}
                                                                                                                            14ilist_iteratorINS_12ilist_detail12node_optionsINS_12MachineInstrELb1ELb1EvEELb0ELb0EEEPS4_ _ZN4llvm19MachineFunctionPass13runOnFunctionERNS_8FunctionE _ZN4llvm19