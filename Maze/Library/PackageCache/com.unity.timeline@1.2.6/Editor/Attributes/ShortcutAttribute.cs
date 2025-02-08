//------------------------------------------------------------------------------
// <copyright file="EventDescriptor.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

/*
 */
namespace System.ComponentModel {

    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Security.Permissions;

    /// <devdoc>
    ///    <para>
    ///       Provides a description
    ///       of an event.
    ///    </para>
    /// </devdoc>
    [HostProtection(SharedState = true)]
    [System.Runtime.InteropServices.ComVisible(true)]
    public abstract class EventDescriptor : MemberDescriptor {
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the <see cref='System.ComponentModel.EventDescriptor'/> class with the
        ///       specified name and attribute
        ///       array.
        ///    </para>
        /// </devdoc>
        protected EventDescriptor(string name, Attribute[] attrs)
            : base(name, attrs) {
        }
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the <see cref='System.ComponentModel.EventDescriptor'/> class with the name and attributes in
        ///       the specified <see cref='System.ComponentModel.MemberDescriptor'/>
        ///       .
        ///    </para>
        /// </devdoc>
        protected EventDescriptor(MemberDescriptor descr)
            : base(descr) {
        }
        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the <see cref='System.ComponentModel.EventDescriptor'/> class with
        ///       the name in the specified <see cref='System.ComponentModel.MemberDescriptor'/> and the
        ///       attributes in both the <see cref='System.ComponentModel.MemberDescriptor'/> and the <see cref='System.Attribute'/>
        ///       array.
        ///    </para>
        /// </devdoc>
        protected EventDescriptor(MemberDescriptor descr, Attribute[] attrs)
  