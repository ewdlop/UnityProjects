<?xml version="1.0" encoding="utf-8"?>
<Type Name="SizeConverter" FullName="System.Drawing.SizeConverter">
  <TypeSignature Language="C#" Value="public class SizeConverter : System.ComponentModel.TypeConverter" Maintainer="auto" />
  <AssemblyInfo>
    <AssemblyName>System.Drawing</AssemblyName>
    <AssemblyPublicKey>[00 24 00 00 04 80 00 00 94 00 00 00 06 02 00 00 00 24 00 00 52 53 41 31 00 04 00 00 01 00 01 00 07 D1 FA 57 C4 AE D9 F0 A3 2E 84 AA 0F AE FD 0D E9 E8 FD 6A EC 8F 87 FB 03 76 6C 83 4C 99 92 1E B2 3B E7 9A D9 D5 DC C1 DD 9A D2 36 13 21 02 90 0B 72 3C F9 80 95 7F C4 E1 77 10 8F C6 07 77 4F 29 E8 32 0E 92 EA 05 EC E4 E8 21 C0 A5 EF E8 F1 64 5C 4C 0C 93 C1 AB 99 28 5D 62 2C AA 65 2C 1D FA D6 3D 74 5D 6F 2D E5 F1 7E 5E AF 0F C4 96 3D 26 1C 8A 12 43 65 18 20 6D C0 93 34 4D 5A D2 93]</AssemblyPublicKey>
    <AssemblyVersion>1.0.3300.0</AssemblyVersion>
    <AssemblyVersion>1.0.5000.0</AssemblyVersion>
    <AssemblyVersion>2.0.0.0</AssemblyVersion>
  </AssemblyInfo>
  <ThreadSafetyStatement>Gtk# is thread aware, but not thread safe; See the &lt;link location="node:gtk-sharp/programming/threads"&gt;Gtk# Thread Programming&lt;/link&gt; for details.</ThreadSafetyStatement>
  <Base>
    <BaseTypeName>System.ComponentModel.TypeConverter</BaseTypeName>
  </Base>
  <Interfaces />
  <Docs>
    <remarks>To be added</remarks>
    <summary>
      <attribution license="cc4" from="Microsoft" modified="false" />
      <para>The <see cref="T:System.Drawing.SizeConverter" /> class is used to convert from one data type to another. Access this class through the <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</para>
    </summary>
  </Docs>
  <Members>
    <Member MemberName=".ctor">
      <MemberSignature Language="C#" Value="public SizeConverter ();" />
      <MemberType>Constructor</MemberType>
      <ReturnValue />
      <Parameters />
      <Docs>
        <remarks>To be added</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Initializes a new <see cref="T:System.Drawing.SizeConverter" /> object.</para>
        </summary>
      </Docs>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
      </AssemblyInfo>
    </Member>
    <Member MemberName="CanConvertFrom">
      <MemberSignature Language="C#" Value="public override bool CanConvertFrom (System.ComponentModel.ITypeDescriptorContext context, Type sourceType);" />
      <MemberType>Method</MemberType>
      <ReturnValue>
        <ReturnType>System.Boolean</ReturnType>
      </ReturnValue>
      <Parameters>
        <Parameter Name="context" Type="System.ComponentModel.ITypeDescriptorContext" />
        <Parameter Name="sourceType" Type="System.Type" />
      </Parameters>
      <Docs>
        <remarks>To be added</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Determines whether this converter can convert an object in the specified source type to the native type of the converter.</para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>This method returns true if this object can perform the conversion.</para>
        </returns>
        <param name="context">
          <attribution license="cc4" from="Microsoft" modified="false" />A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
        <param name="sourceType">
          <attribution license="cc4" from="Microsoft" modified="false" />The type you want to convert from. </param>
      </Docs>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
      </AssemblyInfo>
    </Member>
    <Member MemberName="CanConvertTo">
      <MemberSignature Language="C#" Value="public override bool CanConvertTo (System.ComponentModel.ITypeDescriptorContext context, Type destinationType);" />
      <MemberType>Method</MemberType>
      <ReturnValue>
        <ReturnType>System.Boolean</ReturnType>
      </ReturnValue>
      <Parameters>
        <Parameter Name="context" Type="System.ComponentModel.ITypeDescriptorContext" />
        <Parameter Name="destinationType" Type="System.Type" />
      </Parameters>
      <Docs>
        <remarks>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>The <paramref name="context" /> parameter can be used to extract additional information about the environment this converter is being invoked from. </para>
        </remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>This method returns true if this converter can perform the conversion; otherwise, false.</para>
        </returns>
        <param name="context">
          <attribution license="cc4" from="Microsoft" modified="false" />A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This can be null, so always check. Also, properties on the context object can return null.</param>
        <param name="destinationType">
          <attribution license="cc4" from="Microsoft" modified="false" />A <see cref="T:System.Type" /> that represents the type you want to convert to. </param>
      </Docs>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
      </AssemblyInfo>
    </Member>
    <Member MemberName="ConvertFrom">
      <MemberSignature Language="C#" Value="public override object ConvertFrom (System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value);" />
      <MemberType>Method</MemberType>
      <ReturnValue>
        <ReturnType>System.Object</ReturnType>
      </ReturnValue>
      <Parameters>
        <Parameter Name="context" Type="System.ComponentModel.ITypeDescriptorContext" />
        <Parameter Name="culture" Type="System.Globalization.CultureInfo" />
        <Parameter Name="value" Type="System.Object" />
      </Parameters>
      <Docs>
        <remarks>To be added</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Converts the specified object to the converter's native type.</para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>The converted object. </para>
        </returns>
        <param name="context">
          <attribution license="cc4" from="Microsoft" modified="false" />A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
        <param name="culture">
          <attribution license="cc4" from="Microsoft" modified="false" />An <see cref="T:System.Globalization.CultureInfo" /> object that contains culture specific information, such as the language, calendar, and cultural conventions associated with a specific culture. It is based on the RFC 1766 standard. </param>
        <param name="value">
          <attribution license="cc4" from="Microsoft" modified="false" />The object to convert. </param>
      </Docs>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
      </AssemblyInfo>
    </Member>
    <Member MemberName="ConvertTo">
      <MemberSignature Language="C#" Value="public override object ConvertTo (System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType);" />
      <MemberType>Method</MemberType>
      <ReturnValue>
        <ReturnType>System.Object</ReturnType>
      </ReturnValue>
      <Parameters>
        <Parameter Name="context" Type="System.ComponentModel.ITypeDescriptorContext" />
        <Parameter Name="culture" Type="System.Globalization.CultureInfo" />
        <Parameter Name="value" Type="System.Object" />
        <Parameter Name="destinationType" Type="System.Type" />
      </Parameters>
      <Docs>
        <remarks>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>The most common type conversion is to and from a string type. The default implementation calls the <see cref="M:System.Object.ToString" /> method of the object if the object is valid and if the destination type is string. If this method cannot convert the specified object to the destination type, it passes a <see cref="T:System.NotSupportedException" /> exception.</para>
        </remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Converts the specified object to the specified type.</para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>The converted object.</para>
        </returns>
        <param name="context">
          <attribution license="cc4" from="Microsoft" modified="false" />A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
        <param name="culture">
          <attribution license="cc4" from="Microsoft" modified="false" />An <see cref="T:System.Globalization.CultureInfo" /> object that contains culture specific information, such as the language, calendar, and cultural conventions associated with a specific culture. It is based on the RFC 1766 standard. </param>
        <param name="value">
          <attribution license="cc4" from="Microsoft" modified="false" />The object to convert. </param>
        <param name="destinationType">
          <attribution license="cc4" from="Microsoft" modified="false" />The type to convert the object to. </param>
      </Docs>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
      </AssemblyInfo>
    </Member>
    <Member MemberName="CreateInstance">
      <MemberSignature Language="C#" Value="public override object CreateInstance (System.ComponentModel.ITypeDescriptorContext context, System.Collections.IDictionary propertyValues);" />
      <MemberType>Method</MemberType>
      <ReturnValue>
        <ReturnType>System.Object</ReturnType>
      </ReturnValue>
      <Parameters>
        <Parameter Name="context" Type="System.ComponentModel.ITypeDescriptorContext" />
        <Parameter Name="propertyValues" Type="System.Collections.IDictionary" />
      </Parameters>
      <Docs>
        <remarks>To be added</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Creates an object of this type by using a specified set of property values for the object. This is useful for creating non-changeable objects that have changeable properties.</para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>The newly created object, or null if the object could not be created. The default implementation returns null.</para>
        </returns>
        <param name="context">
          <attribution license="cc4" from="Microsoft" modified="false" />A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided. </param>
        <param name="propertyValues">
          <attribution license="cc4" from="Microsoft" modified="false" />A dictionary of new property values. The dictionary contains a series of name-value pairs, one for each property returned from the <see cref="M:System.Drawing.SizeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> method. </param>
      </Docs>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
      </AssemblyInfo>
    </Member>
    <Member MemberName="GetCreateInstanceSupported">
      <MemberSignature Language="C#" Value="public override bool GetCreateInstanceSupported (System.ComponentModel.ITypeDescriptorContext context);" />
      <MemberType>Method</MemberType>
      <ReturnValue>
        <ReturnType>System.Boolean</ReturnType>
      </ReturnValue>
      <Parameters>
        <Parameter Name="context" Type="System.ComponentModel.ITypeDescriptorContext" />
      </Parameters>
      <Docs>
        <remarks>To be added</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Determines whether changing a value on this object should require a call to the <see cref="M:System.Drawing.SizeConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> method to create a new value.</para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>true if the <see cref="M:System.Drawing.SizeConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> object should be called when a change is made to one or more properties of this object.</para>
        </returns>
        <param name="context">
          <attribution license="cc4" from="Microsoft" modified="false" />A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided. </param>
      </Docs>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
      </AssemblyInfo>
    </Member>
    <Member MemberName="GetProperties">
      <MemberSignature Language="C#" Value="public override System.ComponentModel.PropertyDescriptorCollection GetProperties (System.ComponentModel.ITypeDescriptorContext context, object value, Attribute[] attributes);" />
      <MemberType>Method</MemberType>
      <ReturnValue>
        <ReturnType>System.ComponentModel.PropertyDescriptorCollection</ReturnType>
      </ReturnValue>
      <Parameters>
        <Parameter Name="context" Type="System.ComponentModel.ITypeDescriptorContext" />
        <Parameter Name="value" Type="System.Object" />
        <Parameter Name="attributes" Type="System.Attribute[]" />
      </Parameters>
      <Docs>
        <remarks>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>An easy implementation of this method can call the <see cref="Overload:System.ComponentModel.TypeConverter.GetProperties" /> method for the correct data type.</para>
        </remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Retrieves the set of properties for this type. By default, a type does not have any properties to return. </para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>The set of properties that should be exposed for this data type. If no properties should be exposed, this may return null. The default implementation always returns null.</para>
        </returns>
        <param name="context">
          <attribution license="cc4" from="Microsoft" modified="false" />A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided. </param>
        <param name="value">
          <attribution license="cc4" from="Microsoft" modified="false" />The value of the object to get the properties for. </param>
        <param name="attributes">
          <attribution license="cc4" from="Microsoft" modified="false" />An array of <see cref="T:System.Attribute" /> objects that describe the properties. </param>
      </Docs>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
      </AssemblyInfo>
    </Member>
    <Member MemberName="GetPropertiesSupported">
      <MemberSignature Language="C#" Value="public override bool Get