<?xml version="1.0" encoding="utf-8"?>
<Type Name="AesManaged" FullName="System.Security.Cryptography.AesManaged">
  <TypeSignature Language="C#" Value="public sealed class AesManaged : System.Security.Cryptography.Aes" />
  <TypeSignature Language="ILAsm" Value=".class public auto ansi sealed beforefieldinit AesManaged extends System.Security.Cryptography.Aes" />
  <AssemblyInfo>
    <AssemblyName>System.Core</AssemblyName>
    <AssemblyVersion>4.0.0.0</AssemblyVersion>
  </AssemblyInfo>
  <Base>
    <BaseTypeName>System.Security.Cryptography.Aes</BaseTypeName>
  </Base>
  <Interfaces />
  <Docs>
    <remarks>
      <attribution license="cc4" from="Microsoft" modified="false" />
      <para>The AES algorithm is essentially the Rijndael symmetric algorithm with a fixed block size and iteration count. This class functions the same way as the <see cref="T:System.Security.Cryptography.RijndaelManaged" /> class but limits blocks to 128 bits and does not allow feedback modes.</para>
      <block subset="none" type="note">
        <para>If the Windows security policy setting for Federal Information Processing Standards (FIPS)-compliant algorithms is enabled, using this algorithm throws a <see cref="T:System.Security.Cryptography.CryptographicException" />.</para>
      </block>
    </remarks>
    <summary>
      <attribution license="cc4" from="Microsoft" modified="false" />
      <para>Provides a managed implementation of the Advanced Encryption Standard (AES) symmetric algorithm. </para>
    </summary>
  </Docs>
  <Members>
    <Member MemberName=".ctor">
      <MemberSignature Language="C#" Value="public AesManaged ();" />
      <MemberSignature Language="ILAsm" Value=".method public hidebysig specialname rtspecialname instance void .ctor() cil managed" />
      <MemberType>Constructor</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <Parameters />
      <Docs>
        <remarks>To be added.</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Initializes a new instance of the <see cref="T:System.Security.Cryptography.AesManaged" /> class. </para>
        </summary>
      </Docs>
    </Member>
    <Member MemberName="CreateDecryptor">
      <MemberSignature Language="C#" Value="public override System.Security.Cryptography.ICryptoTransform CreateDecryptor ();" />
      <MemberSignature Language="ILAsm" Value=".method public hidebysig virtual instance class System.Security.Cryptography.ICryptoTransform CreateDecryptor() cil managed" />
      <MemberType>Method</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>System.Security.Cryptography.ICryptoTransform</ReturnType>
      </ReturnValue>
      <Parameters />
      <Docs>
        <remarks>To be added.</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Creates a symmetric decryptor object using the current key and initialization vector (IV).</para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>A symmetric decryptor object.</para>
        </returns>
      </Docs>
    </Member>
    <Member MemberName="CreateDecryptor">
      <MemberSignature Language="C#" Value="public override System.Security.Cryptography.ICryptoTransform CreateDecryptor (byte[] key, byte[] iv);" />
      <MemberSignature Language="ILAsm" Value=".method public hidebysig virtual instance class System.Security.Cryptography.ICryptoTransform CreateDecryptor(unsigned int8[] key, unsigned int8[] iv) cil managed" />
      <MemberType>Method</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>System.Security.Cryptography.ICryptoTransform</ReturnType>
      </ReturnValue>
      <Parameters>
        <Parameter Name="key" Type="System.Byte[]" />
        <Parameter Name="iv" Type="System.Byte[]" />
      </Parameters>
      <Docs>
        <remarks>To be added.</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Creates a symmetric decryptor object using the specified key and initialization vector (IV).</para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>A symmetric decryptor object.</para>
        </returns>
        <param name="key">
          <attribution license="cc4" from="Microsoft" modified="false" />The secret key to use for the symmetric algorithm.</param>
        <param name="iv">
          <attribution license="cc4" from="Microsoft" modified="false" />The initialization vector to use for the symmetric algorithm.</param>
      </Docs>
    </Member>
    <Member MemberName="CreateEncryptor">
      <MemberSignature Language="C#" Value="public override System.Security.Cryptography.ICryptoTransform CreateEncryptor ();" />
      <MemberSignature Language="ILAsm" Value=".method public hidebysig virtual instance class System.Security.Cryptography.ICryptoTransform CreateEncryptor() cil managed" />
      <MemberType>Method</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>System.Security.Cryptography.ICryptoTransform</ReturnType>
      </ReturnValue>
      <Parameters />
      <Docs>
        <remarks>To be added.</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Creates a symmetric encryptor object using the current key and initialization vector (IV).</para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>A symmetric encryptor object.</para>
        </returns>
      </Docs>
    </Member>
    <Member MemberName="CreateEncryptor">
      <MemberSignature Language="C#" Value="public override System.Security.Cryptography.ICryptoTransform CreateEncryptor (byte[] key, byte[] iv);" />
      <MemberSignature Language="ILAsm" Value=".method public hidebysig virtual instance class System.Security.Cryptography.ICryptoTransform CreateEncryptor(unsigned int8[] key, unsigned int8[] iv) cil managed" />
      <MemberType>Method</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>System.Security.Cryptography.ICryptoTransform</ReturnType>
      </ReturnValue>
      <Parameters>
        <Parameter Name="key" Type="System.Byte[]" />
        <Parameter Name="iv" Type="System.Byte[]" />
      </Parameters>
      <Docs>
        <remarks>To be added.</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Creates a symmetric encryptor object using the specified key and initialization vector (IV).</para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>A symmetric encryptor object.</para>
        </returns>
        <param name="key">
          <attribution license="cc4" from="Microsoft" modified="false" />The secret key to use for the symmetric algorithm.</param>
        <param name="iv">
          <attribution license="cc4" from="Microsoft" modified="false" />The initialization vector to use for the symmetric algorithm.</param>
      </Docs>
    </Member>
    <Member MemberName="Dispose">
      <MemberSignature Language="C#" Value="protected override void Dispose (bool disposing);" />
      <MemberSignature Language="ILAsm" Value=".method familyhidebysig virtual instance void Dispose(bool disposing) cil managed" />
      <MemberType>Method</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>System.Void</ReturnType>
      </ReturnValue>
      <Parameters>
        <Parameter Name="disposing" Type="System.Boolean" />
      </Parameters>
      <Docs>
        <param name="disposing">To be added.</param>
        <summary>To be added.</summary>
        <remarks>To be added.</remarks>
      </Docs>
    </Member>
    <Member MemberName="FeedbackSize">
      <MemberSignature Language="C#" Value="public override int FeedbackSize { get; set; }" />
      <MemberSignature Language="ILAsm" Value=".property instance int32 FeedbackSize" />
      <MemberType>Property</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>System.Int32</ReturnType>
      </ReturnValue>
      <Docs>
        <value>To be added.</value>
        <remarks>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>The maximum feedback size is 128 bits.</para>
          <block subset="none" type="note">
            <para>Because this algorithm does not support feedback modes, using this property is discouraged.</para>
          </block>
          <para> </para>
        </remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Gets or sets the number of bits to use as feedback. </para>
        </summary>
      </Docs>
    </Member>
    <Member MemberName="GenerateIV">
      <MemberSignature Language="C#" Value="public override void GenerateIV ();" />
      <MemberSignature Language="ILAsm" Value=".method public hidebysig virtual instance void GenerateIV() cil managed" />
      <MemberType>Method</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>System.Void</ReturnType>
      </ReturnValue>
      <Parameters />
      <Docs>
        <remarks>To be added.</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Generates a random initialization vector (IV) to use for the symmetric algorithm.</para>
        </summary>
      </Docs>
    </Member>
    <Member MemberName="GenerateKey">
      <MemberSignature Language="C#" Value="public override void GenerateKey ();" />
      <MemberSignature Language="ILAsm" Value=".method public hidebysig virtual instance void GenerateKey() cil managed" />
      <MemberType>Method</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>System.Void</ReturnType>
      </ReturnValue>
      <Parameters />
      <Docs>
        <remarks>To be added.</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Generates a random key to use for the symmetric algorithm. </para>
        </summary>
      </Docs>
    </Member>
    <Member MemberName="IV">
      <MemberSignature Language="C#" Value="public override byte[] IV { get; set; }" />
      <MemberSignature Language="ILAsm" Value=".property instance unsigned int8[] IV" />
      <MemberType>Property</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>System.Byte[]</ReturnType>
      </ReturnValue>
      <Docs>
        <value>To be added.</value>
        <remarks>To be added.</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Gets or sets the initialization vector (IV) to use for the symmetric algorithm. </para>
        </summary>
      </Docs>
    </Member>
    <Member MemberName="Key">
      <MemberSignature Language="C#" Value="public override byte[] Key { get; set; }" />
      <MemberSignature Language="ILAsm" Value=".property instance unsigned int8[] Key" />
      <MemberType>Property</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>System.Byte[]</ReturnType>
      </ReturnValue>
      <Docs>
        <value>To be added.</value>
        <remarks>To be added.</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Gets or sets the secret key used for the symmetric algorithm.</para>
        </summary>
      </Docs>
    </Member>
    <Member MemberName="KeySize">
      <MemberSignature Language="C#" Value="public override int KeySize { get; set; }" />
      <MemberSignature Language="ILAsm" Value=".property instance int32 KeySize" />
      <MemberType>Property</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>System.Int32</ReturnType>
      </ReturnValue>
      <Docs>
        <value>To be added.</value>
        <remarks>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>The maximum size of the key is 256 bits.</para>
        </remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Gets or sets the size, in bits, of the secret key used for the symmetric algorithm. </para>
        </summary>
      </Docs>
    </Member>
    <Member MemberName="Mode">
      <MemberSignature Language="C#" Value="public override System.Security.Cryptography.CipherMode Mode { get; set; }" />
      <MemberSignature Language="ILAsm" Value=".property instance valuetype System.Security.Cryptography.CipherMode Mode" />
      <MemberType>Property</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>System.Security.Cryptography.CipherMode</ReturnType>
      </ReturnValue>
      <Docs>
        <value>To be added.</value>
        <remarks>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>The <see cref="F:System.Security.Cryptography.CipherMode.CFB" /> and <see cref="F:System.Security.Cryptography.CipherMode.OFB" /> modes are not supported.</para>
        </remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Gets or sets the mode for operation of the symmetric algorithm.</para>
        </summary>
      </Docs>
    </Member>
    <Member MemberName="Padding">
      <MemberSignature Language="C#" Value="public override System.Security.Cryptography.PaddingMode Padding { get; set; }" />
      <MemberSignature Language="ILAsm" Value=".property instance valuetype System.Security.Cryptography.PaddingMode Padding" />
      <MemberType>Property</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>System.Security.Cryptography.PaddingMode</ReturnType>
      </ReturnValue>
      <Docs>
        <value>To be added.</value>
        <remarks>To be added.</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Gets or sets the padding mode used in the symmetric algorithm. </para>
        </summary>
      </Docs>
    </Member>
  </Members>
</Type>                                                                                                                                                                                                                                                                                                                                                                                                                         