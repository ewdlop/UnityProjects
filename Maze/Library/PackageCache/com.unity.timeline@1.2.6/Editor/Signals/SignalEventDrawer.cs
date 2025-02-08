//------------------------------------------------------------------------------
// <copyright file="MultilineStringConverter.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

namespace System.ComponentModel {

    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Diagnostics;
    using System.Globalization;
    using System.Security.Permissions;

    /// <devdoc>
    /// Provides a type converter to convert multiline strings to a simple string.
    /// </devdoc>
    [HostProtection(SharedState = true)]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
    public class MultilineStringConverter : TypeConverter {

        /// <devdoc>
        /// Converts the given value object to the specified destination type.
        /// </devdoc>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
            if (destinationType == null) {
                throw new ArgumentNullException("destinationType");
            }

 