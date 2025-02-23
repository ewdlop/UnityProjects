// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// 
// ==--==
/*============================================================
**
** Class:  MethodToken
** 
** <OWNER>Microsoft</OWNER>
**
**
** Purpose: Represents a Method to the ILGenerator class.
**
** 
===========================================================*/
namespace System.Reflection.Emit {
    
    using System;
    using System.Reflection;
    using System.Security.Permissions;

    [Serializable]
    [System.Runtime.InteropServices.ComVisible(true)]
    public struct MethodToken
    {
        public static readonly MethodToken Empty = new MethodToken();
        internal int m_method;
            
        internal MethodToken(int str) {
            m_method=str;
        }
    
        public int Token {
            get { return m_method; }
        }
        
        public override int GetHashCode()
        {
            return m_method;
        }

        public override bool Equals(Object obj)
        {
            if (obj is MethodToken)
                return Equals((MethodToken)obj);
            else
                return false;
        }
        
        public bool Eq