//------------------------------------------------------------------------------
// <copyright file="LocalizableAttribute.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

/*
 */
namespace System.ComponentModel {

    using System;
    using System.Diagnostics;
    using System.Security.Permissions;

    /// <devdoc>
    ///    <para>Specifies whether a property should be localized.</para>
    /// </devdoc>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class LocalizableAttribute : Attribute {
        private bool isLocalizable = false;

        /// <devdoc>
        ///    <para>
        ///       Initializes a new instance of the <see cref='System.ComponentModel.LocalizableAttribute'/> class.
        ///    </para>
        /// </devdoc>
        public LocalizableAttribute(bool is