<?xml version="1.0" encoding="utf-8"?>
<root>
  <xsd:schema id="root" xmlns="" xmlns:xsd="http://www.w3.org/2001/XMLSchema" xmlns:msdata="urn:schemas-microsoft-com:xml-msdata">
    <xsd:import namespace="http://www.w3.org/XML/1998/namespace" />
    <xsd:element name="root" msdata:IsDataSet="true">
      <xsd:complexType>
        <xsd:choice maxOccurs="unbounded">
          <xsd:element name="metadata">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name="value" type="xsd:string" minOccurs="0" />
              </xsd:sequence>
              <xsd:attribute name="name" use="required" type="xsd:string" />
              <xsd:attribute name="type" type="xsd:string" />
              <xsd:attribute name="mimetype" type="xsd:string" />
              <xsd:attribute ref="xml:space" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name="assembly">
            <xsd:complexType>
              <xsd:attribute name="alias" type="xsd:string" />
              <xsd:attribute name="name" type="xsd:string" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name="data">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name="value" type="xsd:string" minOccurs="0" msdata:Ordinal="1" />
                <xsd:element name="comment" type="xsd:string" minOccurs="0" msdata:Ordinal="2" />
              </xsd:sequence>
              <xsd:attribute name="name" type="xsd:string" use="required" msdata:Ordinal="1" />
              <xsd:attribute name="type" type="xsd:string" msdata:Ordinal="3" />
              <xsd:attribute name="mimetype" type="xsd:string" msdata:Ordinal="4" />
              <xsd:attribute ref="xml:space" />
            </xsd:complexType>
          </xsd:element>
          <xsd:element name="resheader">
            <xsd:complexType>
              <xsd:sequence>
                <xsd:element name="value" type="xsd:string" minOccurs="0" msdata:Ordinal="1" />
              </xsd:sequence>
              <xsd:attribute name="name" type="xsd:string" use="required" />
            </xsd:complexType>
          </xsd:element>
        </xsd:choice>
      </xsd:complexType>
    </xsd:element>
  </xsd:schema>
  <resheader name="resmimetype">
    <value>text/microsoft-resx</value>
  </resheader>
  <resheader name="version">
    <value>2.0</value>
  </resheader>
  <resheader name="reader">
    <value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
  </resheader>
  <resheader name="writer">
    <value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value>
  </resheader>
  <data name="Arg_EmptyOrNullString" xml:space="preserve">
    <value>String cannot be empty or null.</value>
  </data>
  <data name="Arg_RankMultiDimNotSupported" xml:space="preserve">
    <value>Only single dimensional arrays are supported for the requested action.</value>
  </data>
  <data name="Argument_InvalidOffLen" xml:space="preserve">
    <value>Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.</value>
  </data>
  <data name="Argument_InvalidOidValue" xml:space="preserve">
    <value>The OID value was invalid.</value>
  </data>
  <data name="ArgumentOutOfRange_Index" xml:space="preserve">
    <value>Index was out of range.  Must be non-negative and less than the size of the collection.</value>
  </data>
  <data name="Cryptography_Asn_EnumeratedValueRequiresNonFlagsEnum" xml:space="preserve">
    <value>ASN.1 Enumerated values only apply to enum types without the [Flags] attribute.</value>
  </data>
  <data name="Cryptography_Asn_NamedBitListRequiresFlagsEnum" xml:space="preserve">
    <value>Named bit list operations require an enum with the [Flags] attribute.</value>
  </data>
  <data name="Cryptography_Asn_NamedBitListValueTooBig" xml:space="preserve">
    <value>The encoded named bit list value is larger than the value size of the '{0}' enum.</value>
  </data>
  <data name="Cryptography_Asn_UniversalValueIsFixed" xml:space="preserve">
    <value>Tags with TagClass Universal must have the appropriate TagValue value for the data type being read or written.</value>
  </data>
  <data name="Cryptography_Asn_UnusedBitCountRange" xml:space="preserve">
    <value>Unused bit count must be between 0 and 7, inclusive.</value>
  </data>
  <data name="Cryptography_AsnSerializer_AmbiguousFieldType" xml:space="preserve">
    <value>Field '{0}' of type '{1}' has ambiguous type '{2}', an attribute derived from AsnTypeAttribute is required.</value>
  </data>
  <data name="Cryptography_AsnSerializer_Choice_AllowNullNonNullable" xml:space="preserve">
    <value>[Choice].AllowNull=true is not valid because type '{0}' cannot have a null value.</value>
  </data>
  <data name="Cryptography_AsnSerializer_Choice_ConflictingTagMapping" xml:space="preserve">
    <value>The tag ({0} {1}) for field '{2}' on type '{3}' already is associated in this context with field '{4}' on type '{5}'.</value>
  </data>
  <data name="Cryptography_AsnSerializer_Choice_DefaultValueDisallowed" xml:space="preserve">
    <value>Field '{0}' on [Choice] type '{1}' has a default value, which is not permitted.</value>
  </data>
  <data name="Cryptography_AsnSerializer_Choice_NoChoiceWasMade" xml:space="preserve">
    <value>An instance of [Choice] type '{0}' has no non-null fields.</value>
  </data>
  <data name="Cryptography_AsnSerializer_Choice_NonNullableField" xml:space="preserve">
    <value>Field '{0}' on [Choice] type '{1}' can not be assigned a null value.</value>
  </data>
  <data name="Cryptography_AsnSerializer_Choice_TooManyValues" xml:space="preserve">
    <value>Fields '{0}' and '{1}' on type '{2}' are both non-null when only one value is permitted.</value>
  </data>
  <data name="Cryptography_AsnSerializer_Choice_TypeCycle" xml:space="preserve">
    <value>Field '{0}' on [Choice] type '{1}' has introduced a type chain cycle.</value>
  </data>
  <data name="Cryptography_AsnSerializer_MultipleAsnTypeAttributes" xml:space="preserve">
    <value>Field '{0}' on type '{1}' has multiple attributes deriving from '{2}' when at most one is permitted.</value>
  </data>
  <data name="Cryptography_AsnSerializer_NoJaggedArrays" xml:space="preserve">
    <value>Type '{0}' cannot be serialized or deserialized because it is an array of arrays.</value>
  </data>
  <data name="Cryptography_AsnSerializer_NoMultiDimensionalArrays" xml:space="preserve">
    <value>Type '{0}' cannot be serialized or deserialized because it is a multi-dimensional array.</value>
  </data>
  <data name="Cryptography_AsnSerializer_NoOpenTypes" xml:space="preserve">
    <value>Type '{0}' cannot be serialized or deserialized because it is not sealed or has unbound generic parameters.</value>
  </data>
  <data name="Cryptography_AsnSerializer_Optional_NonNullableField" xml:space="preserve">
    <value>Field '{0}' on type '{1}' is declared [OptionalValue], but it can not be assigned a null value.</value>
  </data>
  <data name="Cryptography_AsnSerializer_PopulateFriendlyNameOnString" xml:space="preserve">
    <value>Field '{0}' on type '{1}' has [ObjectIdentifier].PopulateFriendlyName set to true, which is not applicable to a string.  Change the field to '{2}' or set PopulateFriendlyName to false.</value>
  </data>
  <data name="Cryptography_AsnSerializer_SetValueException" xml:space="preserve">
    <value>Unable to set field {0} on type {1}.</value>
  </data>
  <data name="Cryptography_AsnSerializer_SpecificTagChoice" xml:space="preserve">
    <value>Field '{0}' on type '{1}' has specified an implicit tag value via [ExpectedTag] for [Choice] type '{2}'. ExplicitTag must be true, or the [ExpectedTag] attribute removed.</value>
  </data>
  <data name="Cryptography_AsnSerializer_UnexpectedTypeForAttribute" xml:space="preserve">
    <value>Field '{0}' of type '{1}' has an effective type of '{2}' when one of ({3}) was expected.</value>
  </data>
  <data name="Cryptography_AsnSerializer_UtcTimeTwoDigitYearMaxTooSmall" xml:space="preserve">
    <value>Field '{0}' on type '{1}' has a [UtcTime] TwoDigitYearMax value ({2}) smaller than the minimum (99).</value>
  </data>
  <data name="Cryptography_AsnSerializer_UnhandledType" xml:space="preserve">
    <value>Could not determine how to serialize or deserialize type '{0}'.</value>
  </data>
  <data name="Cryptography_AsnWriter_EncodeUnbalancedStack" xml:space="preserve">
    <value>Encode cannot be called while a Sequence or SetOf is still open.</value>
  </data>
  <data name="Cryptography_AsnWriter_PopWrongTag" xml:space="preserve">
    <value>Cannot pop the requested tag as it is not currently in progress.</value>
  </data>
  <data name="Cryptography_BadHashValue" xml:space="preserve">
    <value>The hash value is not correct.</value>
  </data>
  <data name="Cryptography_BadSignature" xml:space="preserve">
    <value>Invalid signature.</value>
  </data>
  <data name="Cryptography_Cms_CannotDetermineSignatureAlgorithm" xml:space="preserve">
    <value>Could not determine signature algorithm for the signer certificate.</value>
  </data>
  <data name="Cryptography_Cms_IncompleteCertChain" xml:space="preserve">
    <value>The certificate chain is incomplete, the self-signed root authority could not be determined.</value>
  </data>
  <data name="Cryptography_Cms_Invalid_Originator_Identifier_Choice" xml:space="preserve">
    <value>Invalid originator identifier choice {0} found in decoded CMS.</value>
  </data>
  <data name="Cryptography_Cms_Invalid_Subject_Identifier_Type" xml:space="preserve">
    <value>The subject identifier type {0} is not valid.</value>
  </data>
  <data name="Cryptography_Cms_InvalidMessageType" xml:space="preserve">
    <value>Invalid cryptographic message type.</value>
  </data>
  <data name="Cryptography_Cms_InvalidSignerHashForSignatureAlg" xml:space="preserve">
    <value>SignerInfo digest algorithm '{0}' is not valid for signature algorithm '{1}'.</value>
  </data>
  <data name="Cryptography_Cms_Key_Agree_Date_Not_Available" xml:space="preserve">
    <value>The Date property is not available for none KID key agree recipient.</value>
  </data>
  <data name="Cryptography_Cms_MessageNotEncrypted" xml:space="preserve">
    <value>The CMS message is not encrypted.</value>
  </data>
  <data name="Cryptography_Cms_MessageNotSigned" xml:space="preserve">
    <value>The CMS message is not signed.</value>
  </data>
  <data name="Cryptography_Cms_MissingAuthenticatedAttribute" xml:space="preserve">
    <value>The cryptographic message does not contain an expected authenticated attribute.</value>
  </data>
  <data name="Cryptography_Cms_NoCounterCounterSigner" xml:space="preserve">
    <value>Only one level of counter-signatures are supported on this platform.</value>
  </data>
  <data name="Cryptography_Cms_NoRecipients" xml:space="preserve">
    <value>The recipients collection is empty. You must specify at least one recipient. This platform does not implement the certificate picker UI.</value>
  </data>
  <data name="Cryptography_Cms_NoSignerCert" xml:space="preserve">
    <value>No signer certificate was provided. This platform does not implement the certificate picker UI.</value>
  </data>
  <data name="Cryptography_Cms_NoSignerAtIndex" xml:space="preserve">
    <value>The signed cryptographic message does not have a signer for the specified signer index.</value>
  </data>
  <data name="Cryptography_Cms_RecipientNotFound" xml:space="preserve">
    <value>The enveloped-data message does not contain the specified recipient.</value>
  </data>
  <data name="Cryptography_Cms_RecipientType_NotSupported" xml:space="preserve">
    <value>The recipient type '{0}' is not supported for encryption or decryption on this platform.</value>
  </data>
  <data name="Cryptography_Cms_Sign_Empty_Content" xml:space="preserve">
    <value>Cannot create CMS signature for empty content.</value>
  </data>
  <data name="Cryptography_Cms_SignerNotFound" xml:space="preserve">
    <value>Cannot find the original signer.</value>
  </data>
  <data name="Cryptography_Cms_Signing_RequiresPrivateKey" xml:space="preserve">
    <value>A certificate with a private key is required.</value>
  </data>
  <data name="Cryptography_Cms_TrustFailure" xml:space="preserve">
    <value>Certificate trust could not be established. The first reported error is: {0}</value>
  </data>
  <data name="Cryptography_Cms_UnknownAlgorithm" xml:space="preserve">
    <value>Unknown algorithm '{0}'.</value>
  </data>
  <data name="Cryptography_Cms_UnknownKeySpec" xml:space="preserve">
    <value>Unable to determine the type of key handle from this keyspec {0}.</value>
  </data>
  <data name="Cryptography_Cms_WrongKeyUsage" xml:space="preserve">
    <value>The certificate is not valid for the requested usage.</value>
  </data>
  <data name="Cryptography_Pkcs_InvalidSignatureParameters" xml:space="preserve">
    <value>Invalid signature paramters.</value>
  </data>
  <data name="Cryptography_Pkcs9_AttributeMismatch" xml:space="preserve">
    <value>The parameter should be a PKCS 9 attribute.</value>
  </data>
  <data name="Cryptography_Pkcs9_MultipleSigningTimeNotAllowed" xml:space="preserve">
    <value>Cannot add multiple PKCS 9 signing time attributes.</value>
  </data>
  <data name="Cryptography_Pkcs_PssParametersMissing" xml:space="preserve">
    <value>PSS parameters were not present.</value>
  </data>
  <data name="Cryptography_Pkcs_PssParametersHashMismatch" xml:space="preserve">
    <value>This platform requires that the PSS hash algorithm ({0}) match the data digest algorithm ({1}).</value>
  </data>
  <data name="Cryptography_Pkcs_PssParametersMgfHashMismatch" xml:space="preserve">
    <value>This platform does not support the MGF hash algorithm ({0}) being different from the signature hash algorithm ({1}).</value>
  </data>
  <data name="Cryptography_Pkcs_PssParametersMgfNotSupported" xml:space="preserve">
    <value>Mask generation function '{0}' is not supported by this platform.</value>
  </data>
  <data name="Cryptography_Pkcs_PssParametersSaltMismatch" xml:space="preserve">
    <value>PSS salt size {0} is not supported by this platform with hash algorithm {1}.</value>
  </data>
  <data name="Cryptography_TimestampReq_BadNonce" xml:space="preserve">
    <value>The response from the timestamping server did not match the request nonce.</value>
  </data>
  <data name="Cryptography_TimestampReq_BadResponse" xml:space="preserve">
    <value>The response from the timestamping server was not understood.</value>
  </data>
  <data name="Cryptography_TimestampReq_Failure" xml:space="preserve">
    <value>The timestamping server did not grant the request. The request status is '{0}' with failure info '{1}'.</value>
  </data>
  <data name="Cryptography_TimestampReq_NoCertFound" xml:space="preserve">
    <value>The timestamping request required the TSA certificate in the response, but it was not found.</value>
  </data>
  <data name="Cryptography_TimestampReq_UnexpectedCertFound" xml:space="preserve">
    <value>The timestamping request required the TSA certificate not be included in the response, but certificates were present.</value>
  </data>
  <data name="InvalidOperation_DuplicateItemNotAllowed" xml:space="preserve">
    <value>Duplicate items are not allowed in the collection.</value>
  </data>
  <data name="InvalidOperation_WrongOidInAsnCollection" xml:space="preserve">
    <value>AsnEncodedData element in the collection has wrong Oid value: expected = '{0}', actual = '{1}'.</value>
  </data>
  <data name="PlatformNotSupported_CryptographyPkcs" xml:space="preserve">
    <value>System.Security.Cryptography.Pkcs is only supported on Windows platforms.</value>
  </data>
  <data name="Cryptography_Der_Invalid_Encoding" xml:space="preserve">
    <value>ASN1 corrupted data.</value>
  </data>
  <data name="Cryptography_Invalid_IA5String" xml:space="preserve">
    <value>The string contains a character not in the 7 bit ASCII character set.</value>
  </data>
  <data name="Cryptography_UnknownHashAlgorithm" xml:space="preserve">
    <value>'{0}' is not a known hash algorithm.</value>
  </data>
  <data name="Cryptography_WriteEncodedValue_OneValueAtATime" xml:space="preserve">
    <value>The input to WriteEncodedValue must represent a single encoded value with no trailing data.</value>
  </data>
</root>
                                   �H�̜��           �   : < C o n f i g u r a t i o n V a l i d a t o r B a s e . c s   x      �k&    �k&    X2�   �H�̜��  �            : < C o n f i g u r a t i o n V a l i d a t o r B a s e . c s   h      �k&    �k&    �2�   �H�̜��           �   , < C o n f i g X m l T e x t R e a d e r . c s h      �k&    �k&    83�   `q�̜��  �            , < C o n f i g X m l T e x t R e a d e r . c s x      �k&    �k&  