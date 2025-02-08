<?xml version="1.0" encoding="utf-8"?>
<Type Name="IPGlobalProperties" FullName="System.Net.NetworkInformation.IPGlobalProperties">
  <TypeSignature Language="C#" Value="public abstract class IPGlobalProperties" />
  <TypeSignature Language="ILAsm" Value=".class public auto ansi abstract beforefieldinit IPGlobalProperties extends System.Object" />
  <AssemblyInfo>
    <AssemblyName>System</AssemblyName>
    <AssemblyVersion>2.0.0.0</AssemblyVersion>
    <AssemblyVersion>4.0.0.0</AssemblyVersion>
  </AssemblyInfo>
  <Base>
    <BaseTypeName>System.Object</BaseTypeName>
  </Base>
  <Interfaces />
  <Docs>
    <since version=".NET 2.0" />
    <remarks>
      <attribution license="cc4" from="Microsoft" modified="false" />
      <para>This class provides configuration and statistical information about the local computer's network interfaces and network connections.</para>
      <para>The information provided by this class is similar to that provided by the Internet Protocol Helper API functions. For information about the Internet Protocol Helper, see the documentation in the MSDN Library.</para>
    </remarks>
    <summary>
      <attribution license="cc4" from="Microsoft" modified="false" />
      <para>Provides information about the network connectivity of the local computer.</para>
    </summary>
  </Docs>
  <Members>
    <Member MemberName=".ctor">
      <MemberSignature Language="C#" Value="protected IPGlobalProperties ();" />
      <MemberSignature Language="ILAsm" Value=".method familyhidebysig specialname rtspecialname instance void .ctor() cil managed" />
      <MemberType>Constructor</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <Parameters />
      <Docs>
        <since version=".NET 2.0" />
        <remarks>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>This constructor can be called only by classes derived from <see cref="T:System.Net.NetworkInformation.IPGlobalProperties" />.</para>
        </remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Initializes a new instance of the <see cref="T:System.Net.NetworkInformation.IPGlobalProperties" /> class.</para>
        </summary>
      </Docs>
    </Member>
    <Member MemberName="DhcpScopeName">
      <MemberSignature Language="C#" Value="public abstract string DhcpScopeName { get; }" />
      <MemberSignature Language="ILAsm" Value=".property instance string DhcpScopeName" />
      <MemberType>Property</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>System.String</ReturnType>
      </ReturnValue>
      <Docs>
        <value>To be added.</value>
        <since version=".NET 2.0" />
        <remarks>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>A DHCP scope is an administrative grouping of networked computers that are on the same subnet.</para>
        </remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Gets the Dynamic Host Configuration Protocol (DHCP) scope name.</para>
        </summary>
      </Docs>
    </Member>
    <Member MemberName="DomainName">
      <MemberSignature Language="C#" Value="public abstract string DomainName { get; }" />
      <MemberSignature Language="ILAsm" Value=".property instance string DomainName" />
      <MemberType>Property</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
        <AssemblyVersion>4.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>System.String</ReturnType>
      </ReturnValue>
      <Docs>
        <value>To be added.</value>
        <remarks>To be added.</remarks>
        <since version=".NET 2.0" />
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Gets the domain in which the local computer is registered.</para>
        </summary>
      </Docs>
    </Member>
    <Member MemberName="GetActiveTcpConnections">
      <M