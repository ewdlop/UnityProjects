//------------------------------------------------------------------------------
// <copyright file="FilterElement.cs" company="Microsoft Corporation">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
#if CONFIGURATION_DEP
using System.Configuration;
using System;

namespace System.Diagnostics {
    internal class FilterElement : TypedElement {
        public FilterElement() : base(typeof(TraceFilter)) {}

        public TraceFilter GetRuntimeObject() {
            TraceFilter newFilter = (TraceFilter) BaseGetRuntimeObject();
            newFilter.initializeData = InitData;
            return newFilter;
        }

        internal TraceFilter RefreshRuntimeObject(TraceFilter filter) {
            if (Type.GetType(TypeName) != filter.GetType() || InitData !