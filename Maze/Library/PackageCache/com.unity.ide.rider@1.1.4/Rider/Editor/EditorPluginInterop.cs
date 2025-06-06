<?xml version="1.0" encoding="utf-8"?>
<Type Name="SYSKIND" FullName="System.Runtime.InteropServices.ComTypes.SYSKIND">
  <TypeSignature Language="C#" Value="public enum SYSKIND" />
  <TypeSignature Language="ILAsm" Value=".class public auto ansi serializable sealed SYSKIND extends System.Enum" />
  <AssemblyInfo>
    <AssemblyName>mscorlib</AssemblyName>
    <AssemblyVersion>2.0.0.0</AssemblyVersion>
    <AssemblyVersion>4.0.0.0</AssemblyVersion>
  </AssemblyInfo>
  <Base>
    <BaseTypeName>System.Enum</BaseTypeName>
  </Base>
  <Docs>
    <since version=".NET 2.0" />
    <remarks>
      <attribution license="cc4" from="Microsoft" modified="false" />
      <para>For additional information about the SYSKIND enumeration, see the MSDN Library.</para>
      <para>The common language runtime throws an exception when a COM method in native code returns an HRESULT. For more information, see <format type="text/html"><a href="610b364b-2761-429d-9c4a-afbc3e66f1b9">How to: Map HRESULTs and Exceptions</a></format>.</para>
    </remarks>
    <summary>
      <attribution license="cc4" from="Microsoft" modified="false" />
      <para>Identifies the target operating system platform.</para>
    </summary>
  </Docs>
  <Members>
    <Member MemberName="SYS_MAC">
      <MemberSignature Language="C#" Value="SYS_MAC" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype System.Runtime.InteropServices.ComTypes.SYSKIND SYS_MAC = int32(2)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>System.Runtime.InteropServices.ComTypes.SYSKIND</ReturnType>
      </ReturnValue>
      <Docs>
        <since version=".NET 2.0" />
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>The target operating system for the type library is Apple Macintosh. By default, all data fields are aligned on even-byte boundaries.</para>
        </summary>
      </Docs>
    </Member>
    <Member MemberName="SYS_WIN16">
      <MemberSignature Language="C#" Value="SYS_WIN16" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype System.Runtime.InteropServices.ComTypes.SYSKIND SYS_WIN16 = int32(0)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>System.Runtime.InteropServices.ComTypes.SYSKIND</ReturnType>
      </ReturnValue>
      <Docs>
        <since version=".NET 2.0" />
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>The target operating system for the type library is 16-bit Windows systems. By default, data fields are packed.</para>
        </summary>
      </Docs>
    </Member>
    <Member MemberName="SYS_WIN32">
      <MemberSignature Language="C#" Value="SYS_WIN32" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype System.Runtime.InteropServices.ComTypes.SYSKIND SYS_WIN32 = int32(1)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>System.Runtime.InteropServices.ComTypes.SYSKIND</ReturnType>
      </ReturnValue>
      <Docs>
        <since version=".NET 2.0" />
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>The target operating system for the type library is 32-bit Windows systems. By default, data fields are naturally aligned (for example, 2-byte integers are aligned on even-byte boundaries; 4-byte integers are aligned on quad-word boundaries, and so on).</para>
        </summary>
      </Docs>
    </Member>
    <Member MemberName="SYS_WIN64">
      <MemberSignature Language="C#" Value="SYS_WIN64" />
      <MemberSignature Language="ILAsm" Value=".field public static literal valuetype System.Runtime.InteropServices.ComTypes.SYSKIND SYS_WIN64 = int32(3)" />
      <MemberType>Field</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>System.Runtime.InteropServices.ComTypes.SYSKIND</ReturnType>
      </ReturnValue>
      <Docs>
        <since version=".NET 2.0" />
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>The target operating system for the type library is 64-bit Windows systems.</para>
        </summary>
    