// ==++==
//
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//
// ==--==

// WARNING:
//
// This is just an IObjectReference proxy for the former V1.1 Surrogate Encoder
// All this does is make an encoder of the correct type, it DOES NOT maintain state.
namespace System.Text
{
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Diagnostics.Contracts;

    /*=================================SurrogateEncoder==================================
    ** This class is here only to deserialize the SurrogateEncoder class from Everett (V1.1) into
    ** Appropriate Whidbey (V2.0) objects.
    ==============================================================================*/

    [Serializable]
    internal sealed class SurrogateEncoder : ISerializable, IObjectReference
    {
        // Might need this when GetRealObjecting
        [NonSerialized]
        private Encoding realEncoding = null;

        // Constructor calle