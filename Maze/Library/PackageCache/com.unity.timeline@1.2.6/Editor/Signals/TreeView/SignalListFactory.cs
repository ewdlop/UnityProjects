// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// 
// ==--==
////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////
//
// BindingFlags are a set of flags that control the binding and invocation process
// 
// <OWNER>WESU</OWNER>
//    in Reflection.  There are two processes.  The first is selection of a Member which
//    is the binding phase.  The second, is invocation.  These flags control how this
//    process works.
//
// <EMAIL>Author: darylo</EMAIL>
// Date: June 99
//
namespace System.Reflection {
    
    using System;
    [Serializable]
    [Flags]
    [System.Runtime.InteropServices.ComVisible(true)]
    public enum BindingFlags
    {

        // NOTES: We have lookup masks defined in RuntimeType and Activator.  If we
        //    change the lookup values then these masks may need to change also.

        // a place hold