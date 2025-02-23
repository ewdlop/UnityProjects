// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// 
// ==--==
/*============================================================
**
** File:    AssemblyNameProxy
** 
** <OWNER>Microsoft</OWNER>
** <OWNER>Microsoft</OWNER>
**
**
** Purpose: Remotable version the AssemblyName
**
**
===========================================================*/
namespace System.Reflection {
    using System;
    using System.Runtime.Versioning;

    [System.Runtime.InteropServices.ComVisible(true)]
    public class AssemblyNameProxy : MarshalByRefObject 
    {
        [ResourceExposure(ResourceScope.Machine)]
        [ResourceConsumption(ResourceScope.Machine)]
        public AssemblyName GetAssemblyName(String assemblyFile)
        {
            return AssemblyName.GetAssemblyName(assemblyFile);
        }
    }
}
                                                                                                                                                                                                       �      6                              �
     @              �3      0       e   R                 5                    �                                    0     @               �3             e   T                 j                    �      H                             e     @               4      �      e   V                 �     2                                                  �                    8      P                             �     @               �7      �      e   Y                 �                    �	                                                        �	                                         @               �;      H       e   \                 7                    �	                                    `                    �	                                    [     @               �;      H       e   _                 �                     
                                     �    p               
      �                             �     @               0<      �      e   b                 �  L�o   �            �             e                                            �      �      f   X                 	                      �      �                                                   �?      �                             /253            0           0     0     644     163784    `
ELF          >                             @     @ ��   �      �      �      �      �      �      �      �      �   �      �      �      �      �      �      �      �      �      �      �      �      �      �      �      �      �      �      �      �      �   �      �   �      �      �   �      �   �      �   �      �      �   �      �   �      �      �   �      �   �      �   �      �   �      �      �      �      �      �                                              	     
                                                                                                           !     "     #     $     %  &  '  (     )     *  +     ,     -  .     /     0     1     2     3     4     5     6     7     8     9     :     ;     <     =     >     ?     @  A     B  C     D  E     F  G     H     I  J     K  L     M  N     O     P     Q  R     S  T     U  V     W  X     Y  Z     [     \  ]     ^     _     `     a     b     c  d     e  f     g  h     i  j     k  l     m     n     o     p     q     r     s     t     u     v  w     x  y  z  {     |  }  ~       �  �     �  �     �  �     �  �     �  �  �  �     �  �     �  �  �  �     �  �  �