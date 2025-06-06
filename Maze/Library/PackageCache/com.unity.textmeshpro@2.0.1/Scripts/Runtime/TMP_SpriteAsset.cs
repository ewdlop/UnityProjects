<?xml version="1.0" encoding="utf-8"?>
<Type Name="PointConverter" FullName="System.Drawing.PointConverter">
  <TypeSignature Language="C#" Value="public class PointConverter : System.ComponentModel.TypeConverter" Maintainer="auto" />
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
    <remarks>
      <attribution license="cc4" from="Microsoft" modified="false" />
      <para>The <see cref="T:System.Drawing.PointConverter" /> class is typically used for creating a design-time experience for controls that have properties that are of type <see cref="F:System.Drawing.GraphicsUnit.Point" />, and is used to convert points to strings for display in a designer and to convert strings entered in a designer to a <see cref="F:System.Drawing.GraphicsUnit.Point" /> object. Access this class through the <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</para>
    </remarks>
    <summary>
      <attribution license="cc4" from="Microsoft" modified="false" />
      <para>Converts a <see cref="T:System.Drawing.Point" /> object from one data type to another. </para>
    </summary>
  </Docs>
  <Members>
    <Member MemberName=".ctor">
      <MemberSignature Language="C#" Value="public PointConverter ();" />
      <MemberType>Constructor</MemberType>
      <ReturnValue />
      <Parameters />
      <Docs>
        <remarks>To be added</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Initializes a new instance of the <see cref="T:System.Drawing.PointConverter" /> class.</para>
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
          <para>Determines if this converter can convert an object in the given source type to the native type of the converter.</para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>true if this object can perform the conversion; otherwise, false.</para>
        </returns>
        <param name="context">
          <attribution license="cc4" from="Microsoft" modified="false" />A formatter context. This object can be used to get additional information about the environment this converter is being called from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
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
          <para>The <paramref name="context" /> parameter can be used to get additional information about the environment this converter is being called from. This can be null, so you should always check. Also, properties on the context object can also return null.</para>
        </remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>true if this converter can perform the conversion; otherwise, false.</para>
        </returns>
        <param name="context">
          <attribution license="cc4" from="Microsoft" modified="false" />An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that provides a format context. </param>
        <param name="destinationType">
          <attribution license="cc4" from="Microsoft" modified="false" />A <see cref="T:System.Type" /> object that represents the type you want to convert to. </param>
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
          <para>Converts the specified object to a <see cref="T:System.Drawing.Point" /> object.</para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>The converted object. </para>
        </returns>
        <param name="context">
          <attribution license="cc4" from="Microsoft" modified="false" />A formatter context. This object can be used to get additional information about the environment this converter is being called from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
        <param name="culture">
          <attribution license="cc4" from="Microsoft" modified="false" />An object that contains culture specific information, such as the language, calendar, and cultural conventions associated with a specific culture. It is based on the RFC 1766 standard. </param>
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
          <para>The most common types to convert are to and from a string object. The default implementation calls the object's <see cref="M:System.Object.ToString" /> method if the object is valid and if the destination type is string. If this method cannot convert to the destination type, this will throw a <see cref="T:System.NotSupportedException" /> exception.</para>
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
          <attribution license="cc4" from="Microsoft" modified="false" />A formatter context. This object can be used to get additional information about the environment this converter is being called from. This may be null, so you should always check. Also, properties on the context object may also return null. </param>
        <param name="culture">
          <attribution license="cc4" from="Microsoft" modified="false" />An object that contains culture specific information, such as the language, calendar, and cultural conventions associated with a specific culture. It is based on the RFC 1766 standard. </param>
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
        <remarks>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>
            <see cref="M:System.Drawing.PointConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> is useful for objects that are immutable but still want to provide changeable properties.</para>
        </remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Creates an instance of this type given a set of property values for the object.</para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>The newly created object, or null if the object could not be created. The default implementation returns null.</para>
        </returns>
        <param name="context">
          <attribution license="cc4" from="Microsoft" modified="false" />A type descriptor through which additional context can be provided. </param>
        <param name="propertyValues">
          <attribution license="cc4" from="Microsoft" modified="false" />A dictionary of new property values. The dictionary contains a series of name-value pairs, one for each property returned from <see cref="M:System.Drawing.PointConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" />. </param>
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
          <para>Determines if changing a value on this object should require a call to <see cref="M:System.Drawing.PointConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> to create a new value.</para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>true if the <see cref="M:System.Drawing.PointConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> method should be called when a change is made to one or more properties of this object; otherwise, false.</para>
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
          <para>A simple implementation of this method can just call <see cref="Overload:System.ComponentModel.TypeDescriptor.GetProperties" /> for the correct data type.</para>
        </remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Retrieves the set of properties for this type. By default, a type does not return any properties. </para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>The set of properties that are exposed for this data type. If no properties are exposed, this method might return null. The default implementation always returns null.</para>
        </returns>
        <param name="context">
          <attribution license="cc4" from="Microsoft" modified="false" />A type descriptor through which additional context can be provided. </param>
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
      <MemberSignature Language="C#" Value="public override bool GetPropertiesSupported (System.ComponentModel.ITypeDescriptorContext context);" />
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
          <para>Determines if this object supports properties. By default, this is false.</para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>true if <see cref="M:System.Drawing.PointConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> should be called to find the properties of this object; otherwise, false.</para>
        </returns>
        <param name="context">
          <attribution license="cc4" from="Microsoft" modified="false" />A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided. </param>
      </Docs>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
      </AssemblyInfo>
    </Member>
  </Members>
</Type>                                                                                                                                                                                                                                                                                                   FH
�<��<H3-��������[��UW�Ǻ�3�Q���p(��p�)�.����53-��C����!��\\y����J�;u!���#�����ިvt��<�D�݇�rEb��2��3C����C'�03�1|�N���FA�@4}y�-IW\�+j��{2ZV�$ُI�x����^Π.�S`rA���}�]a���#�̈�Xh��/u�����o{Q��ݭ�'鼴Zq(9�'�v���_78+ӄ�Ѥi� kU�y=9�1x7'[9�����Wr/a���Ů�ԝ����G0�Ŷ6����\m������Wd)�\!�]ZX8,z�Ð3��;v�T`�ie:׭�u���g�&`�\]�������T��h�����#�;bh.��vx~!Uo�v`	��)��
W���OֲHt�� ]� ��θ>ع^��]�?��"B����,�����.g�ޞ2��*���9}�^���x	�kRy$���7�E�+����pz�zN��xl�ХX��ʆz̸�ih�U ���5���.��Ah���`��=)3�Ĭ�ʩ�N���涶i�s\�B(Z���������o�Y�X��%��k�W20�r��`���$�ㅐ���ѡk���P�nV�t�����lѾ�ǩz�<���]9/�D����P��K)k��nܬ�Y�N����髎I�|�L���
���|P��< �B�v--�d�E"�-�/���o*0��z]�oq�|���3��c	)��� ��c�ơ��^�r��j���gj�<.7��j���^���v���\dy��S�H�Q�%��E����G�4�x��q��4X'ы�J$[�'�{�g�U�����};�£�nU�J�i�,p5:�<8�<�>�