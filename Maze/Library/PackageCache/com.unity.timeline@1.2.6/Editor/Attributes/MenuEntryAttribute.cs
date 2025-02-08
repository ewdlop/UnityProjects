//------------------------------------------------------------------------------
// <copyright file="ExtenderProvidedPropertyAttribute.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

/*
 */
namespace System.ComponentModel {

    using System;
    using System.Diagnostics;
    using System.Security.Permissions;    

    /// <internalonly/>
    /// <devdoc>
    ///    <para>
    ///       ExtenderProvidedPropertyAttribute is an attribute that marks that a property
    ///       was actually offered up by and extender provider.
    ///    </para>
    /// </devdoc>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class ExtenderProvidedPropertyAttribute : Attribute {

        private PropertyDescriptor extenderProperty;
        private IExtenderProvider  provider;
        private Type               receiverType;

        /// 