// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// 
// ==--==
/*=============================================================================
**
** Class: InsufficientExecutionStackException
**
**
** Purpose: The exception class used when there is insufficient execution stack
**          to allow most Framework methods to execute.
**
**
=============================================================================*/

namespace System {
    
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public sealed class InsufficientExecutionStackException : SystemException 
    {
        public InsufficientExecutionStackException()
            : base