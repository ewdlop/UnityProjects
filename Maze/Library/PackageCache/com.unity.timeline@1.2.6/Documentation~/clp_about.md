<?xml version="1.0" encoding="utf-8"?>
<Type Name="UrlBuilder" FullName="System.Web.UI.Design.UrlBuilder">
  <TypeSignature Language="C#" Value="public sealed class UrlBuilder" />
  <AssemblyInfo>
    <AssemblyName>System.Design</AssemblyName>
    <AssemblyVersion>1.0.5000.0</AssemblyVersion>
    <AssemblyVersion>2.0.0.0</AssemblyVersion>
  </AssemblyInfo>
  <Base>
    <BaseTypeName>System.Object</BaseTypeName>
  </Base>
  <Interfaces />
  <Docs>
    <remarks>
      <attribution license="cc4" from="Microsoft" modified="false" />
      <para>The <see cref="Overload:System.Web.UI.Design.UrlBuilder.BuildUrl" /> method starts a user interface for selecting a URL.</para>
    </remarks>
    <summary>
      <attribution license="cc4" from="Microsoft" modified="false" />
      <para>Starts a URL editor that allows a user to select or create a URL. This class cannot be inherited.</para>
    </summary>
  </Docs>
  <Members>
    <Member MemberName="BuildUrl">
      <MemberSignature Language="C#" Value="public static string BuildUrl (System.ComponentModel.IComponent component, System.Windows.Forms.Control owner, string initialUrl, string caption, string filter);" />
      <MemberType>Method</MemberType>
      <ReturnValue>
        <ReturnType>System.String</ReturnType>
      </ReturnValue>
      <Parameters>
        <Parameter Name="component" Type="System.ComponentModel.IComponent" />
        <Parameter Name="owner" Type="System.Windows.Forms.Control" />
        <Parameter Name="initialUrl" Type="System.String" />
        <Parameter Name="caption" Type="System.String" />
        <Parameter Name="filter" Type="System.String" />
      </Parameters>
      <Docs>
        <remarks>To be added.</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Creates a UI to create or pick a URL.</para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>The URL returned from the UI.</para>
        </returns>
        <param name="component">
          <attribution license="cc4" from="Microsoft" modified="false" />The <see cref="T:System.ComponentModel.IComponent" /> whose site is to be used to access design-time services. </param>
        <param name="owner">
          <attribution license="cc4" from="Microsoft" modified="false" />The <see cref="T:System.Windows.Forms.Control" /> used as the parent for the picker window. </param>
        <param name="initialUrl">
          <attribution license="cc4" from="Microsoft" modified="false" />The initial URL to be shown in the picker window. </param>
        <param name="caption">
          <attribution license="cc4" from="Microsoft" modified="false" />The caption of the picker window. </param>
        <param name="filter">
          <attribution license="cc4" from="Microsoft" modified="false" />The filter string to use to optionally filter the files displayed in the picker window. </param>
      </Docs>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
      </AssemblyInfo>
    </Member>
    <Member MemberName="BuildUrl">
      <MemberSignature Language="C#" Value="public static string BuildUrl (System.ComponentModel.IComponent component, System.Windows.Forms.Control owner, string initialUrl, string caption, string filter, System.Web.UI.Design.UrlBuilderOptions options);" />
      <MemberType>Method</MemberType>
      <ReturnValue>
        <ReturnType>System.String</ReturnType>
      </ReturnValue>
      <Parameters>
        <Parameter Name="component" Type="System.ComponentModel.IComponent" />
        <Parameter Name="owner" Type="System.Windows.Forms.Control" />
        <Parameter Name="initialUrl" Type="System.String" />
        <Parameter Name="caption" Type="System.String" />
        <Parameter Name="filter" Type="System.String" />
        <Parameter Name="options" Type="System.Web.UI.Design.UrlBuilderOptions" />
      </Parameters>
      <Docs>
        <remarks>To be added.</remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Creates a UI to create or pick a URL, using the specified <see cref="T:System.Web.UI.Design.UrlBuilderOptions" /> object.</para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>The URL returned from the UI.</para>
        </returns>
        <param name="component">
          <attribution license="cc4" from="Microsoft" modified="false" />The <see cref="T:System.ComponentModel.IComponent" /> whose site is to be used to access design-time services. </param>
        <param name="owner">
          <attribution license="cc4" from="Microsoft" modified="false" />The <see cref="T:System.Windows.Forms.Control" /> used as the parent for the picker window. </param>
        <param name="initialUrl">
          <attribution license="cc4" from="Microsoft" modified="false" />The initial URL to be shown in the picker window. </param>
        <param name="caption">
          <attribution license="cc4" from="Microsoft" modified="false" />The caption of the picker window. </param>
        <param name="filter">
          <attribution license="cc4" from="Microsoft" modified="false" />The filter string to use to optionally filter the files displayed in the picker window. </param>
        <param name="options">
          <attribution license="cc4" from="Microsoft" modified="false" />A <see cref="T:System.Web.UI.Design.UrlBuilderOptions" /> indicating the options for URL selection. </param>
      </Docs>
      <AssemblyInfo>
        <AssemblyVersion>1.0.5000.0</AssemblyVersion>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
      </AssemblyInfo>
    </Member>
    <Member MemberName="BuildUrl">
      <MemberSignature Language="C#" Value="public static string BuildUrl (IServiceProvider serviceProvider, System.Windows.Forms.Control owner, string initialUrl, string caption, string filter, System.Web.UI.Design.UrlBuilderOptions options);" />
      <MemberType>Method</MemberType>
      <AssemblyInfo>
        <AssemblyVersion>2.0.0.0</AssemblyVersion>
      </AssemblyInfo>
      <ReturnValue>
        <ReturnType>System.String</ReturnType>
      </ReturnValue>
      <Parameters>
        <Parameter Name="serviceProvider" Type="System.IServiceProvider" />
        <Parameter Name="owner" Type="System.Windows.Forms.Control" />
        <Parameter Name="initialUrl" Type="System.String" />
        <Parameter Name="caption" Type="System.String" />
        <Parameter Name="filter" Type="System.String" />
        <Parameter Name="options" Type="System.Web.UI.Design.UrlBuilderOptions" />
      </Parameters>
      <Docs>
        <remarks>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>The <see cref="T:System.IServiceProvider" /> interface is used to obtain the <see cref="T:System.ComponentModel.Design.IDesignerHost" /> implementation for the designer host. </para>
        </remarks>
        <summary>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>Creates a UI to create or pick a URL, using the specified <see cref="T:System.Web.UI.Design.UrlBuilderOptions" /> object.</para>
        </summary>
        <returns>
          <attribution license="cc4" from="Microsoft" modified="false" />
          <para>The URL returned from the UI.</para>
        </returns>
        <param name="serviceProvider">
          <attribution license="cc4" from="Microsoft" modified="false" />The <see cref="T:System.IServiceProvider" /> to be used to access design-time services.</param>
        <param name="owner">
          <attribution license="cc4" from="Microsoft" modified="false" />The <see cref="T:System.Windows.Forms.Control" /> used as the parent for the picker window.</param>
        <param name="initialUrl">
          <attribution license="cc4" from="Microsoft" modified="false" />The initial URL to be shown in the picker window.</param>
        <param name="caption">
          <attribution license="cc4" from="Microsoft" modified="false" />The caption of the picker window.</param>
        <param name="filter">
          <attribution license="cc4" from="Microsoft" modified="false" />The filter string to use to optionally filter the files displayed in the picker window.</param>
        <param name="options">
   