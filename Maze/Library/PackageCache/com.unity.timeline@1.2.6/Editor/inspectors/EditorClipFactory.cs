//------------------------------------------------------------------------------
// <copyright file="RefreshEventHandler.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------
namespace System.ComponentModel {
    using System.Diagnostics;
    using System.Security.Permissions;

    /// <devdoc>
    /// <para>Represents the method that will handle the <see cref='System.ComponentModel.TypeDescriptor.Refresh'/> event
    ///    raised when a <see cref='System.Type'/> or component is changed during design time.</para>
    /// </devdoc>
    [HostProtection(SharedState = true)]
    public delegate void RefreshEventHandler(RefreshEventArgs e);
}
                                                                                                                                                                                          K1                   ��                                    F1     @              �            �  �                �1                   ��      	                              �1     @               �            �  �                2                   ��      d                              2     @              8�     8      �  �                j2                   @�                                    e2     @              p�            �  �                �2                  