// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// 
// ==--==
// TokenBasedSetEnumerator.cs
// 
// <OWNER>Microsoft</OWNER>
//

namespace System.Security.Util 
{
    using System;
    using System.Collections;

    internal struct TokenBasedSetEnumerator
    {
        public Object Current;
        public int Index;
                
        private TokenBasedSet _tb;
                            
        public bool MoveNext()
        {
            return _tb != null ? _tb.MoveNext(ref this) : false;
        }
                
        public void Reset()
        {
            Index = -1;
            Current = null;
        }
                            
        public TokenBasedSetEnumerator(TokenBasedSet tb)
        {
            Index = -1;
            Current = null;
            _tb = tb;
        }
    }
}

                                                                                                                                                                             � Hc�H��H��tH��X   uH�MH��X  1�1�1�E1�E1ɉ��
���A�tH�E�H�@    A�>|H�=�H 1����B���H��[A^A_]��    H��
 �8|P�>�����H�=�H 1���Y�����f.�     UAWAVAUATS