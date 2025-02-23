﻿<?xml version="1.0" encoding="utf-8"?>
<!-- WARNING: this file is autogenerated, don't modify it. Edit the .sources file of the corresponding assembly instead if you want to add/remove C# source files. -->
<Project ToolsVersion="14.0" DefaultTargets="Build" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">net_4_x</Platform>
    <ProjectGuid>{C5666569-A77E-4725-A60B-117278A90399}</ProjectGuid>
    <OutputType>Library</OutputType>
    <NoWarn>1699</NoWarn>
    <LangVersion>latest</LangVersion>
    <HostPlatform Condition=" '$(HostPlatform)' == '' and '$(OS)' == 'Windows_NT'">win32</HostPlatform>
    <HostPlatform Condition=" '$(HostPlatform)' == '' and '$(OS)' == 'Unix' and $([System.IO.File]::Exists('/usr/lib/libc.dylib'))">macos</HostPlatform>
    <HostPlatform Condition=" '$(HostPlatform)' == '' and '$(OS)' == 'Unix'">linux</HostPlatform>
    <GenerateTargetFrameworkAttribute>false</GenerateTargetFrameworkAttribute>
    <NoStdLib>True</NoStdLib>
    <NoConfig>True</NoConfig>
    <AssemblyName>System.Reactive.Providers</AssemblyName>
    <TargetFrameworkVersion>v4.6.2</TargetFrameworkVersion>
    <SignAssembly>true</SignAssembly>
    <DelaySign>true</DelaySign>
    <AssemblyOriginatorKeyFile>../reactive.pub</AssemblyOriginatorKeyFile>
  </PropertyGroup>
  <PropertyGroup>
    <!-- Set AddAdditionalExplicitAssemblyReferences to false, otherwise if targetting .NET4.0,
    Microsoft.NETFramework.props will force a dependency on the assembly System.Core. This
    is a problem to compile the Mono mscorlib.dll -->
    <AddAdditionalExplicitAssemblyReferences>false</AddAdditionalExplicitAssemblyReferences>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Platform)' == 'net_4_x' ">
    <OutputPath>./../../class/lib/net_4_x-$(HostPlatform)</OutputPath>
    <IntermediateOutputPath>./../../class/obj/$(AssemblyName)-net_4_x-$(HostPlatform)</IntermediateOutputPath>
    <DefineConstants>NET_4_0;NET_4_5;NET_4_6;MONO;WIN_PLATFORM;SIGNED;HAS_EDI;PREFERASYNC;PREFER_ASYNC;HAS_AWAIT</DefineConstants>
  </PropertyGroup>
  <!-- @ALL_PROFILE_PROPERTIES@ -->
  <PropertyGroup Condition=" '$(Configuration)' == 'Debug' ">
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <!-- TRACE is set only for Debug configuration, so inherit from platform-specific value -->
    <DefineConstants>TRACE;$(DefineConstants)</DefineConstants>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)' == 'Release' ">
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
  </PropertyGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
  <!-- @BUILT_SOURCES@ -->
  <!--Common files-->
  <ItemGroup>
    <Compile Include="..\..\..\external\rx\Rx\NET\Source\System.Reactive.Providers\GlobalSuppressions.cs" />
    <Compile Include="..\..\..\external\rx\Rx\NET\Source\System.Reactive.Providers\Properties\AssemblyInfo.cs" />
    <Compile Include="..\..\..\external\rx\Rx\NET\Source\System.Reactive.Providers\Reactive\Internal\Constants.cs" />
    <Compile Include="..\..\..\external\rx\Rx\NET\Source\System.Reactive.Providers\Reactive\Joins\QueryablePattern.cs" />
    <Compile Include="..\..\..\external\rx\Rx\NET\Source\System.Reactive.Providers\Reactive\Joins\QueryablePlan.cs" />
    <Compile Include="..\..\..\external\rx\Rx\NET\Source\System.Reactive.Providers\Reactive\Linq\ExpressionVisitor.cs" />
    <Compile Include="..\..\..\external\rx\Rx\NET\Source\System.Reactive.Providers\Reactive\Linq\Observable.Queryable.cs" />
    <Compile Include="..\..\..\external\rx\Rx\NET\Source\System.Reactive.Providers\Reactive\Linq\Qbservable.Generated.cs" />
    <Compile Include="..\..\..\external\rx\Rx\NET\Source\System.Reactive.Providers\Reactive\Linq\Qbservable.Joins.cs" />
    <Compile Include="..\..\..\external\rx\Rx\NET\Source\System.Reactive.Providers\Reactive\Linq\Qbservable.cs" />
    <Compile Include="..\..\..\external\rx\Rx\NET\Source\System.Reactive.Providers\Reactive\ObservableQuery.cs" />
    <Compile Include="..\..\..\external\rx\Rx\NET\Source\System.Reactive.Providers\Strings_Providers.Generated.cs" />
    <Compile Include="Assembly\AssemblyInfo.cs" />
  </ItemGroup>
  <!--End of common files-->
  <!-- @ALL_SOURCES@ -->
  <!-- @COMMON_PROJECT_REFERENCES@ -->
  <ItemGroup Condition=" '$(Platform)' == 'net_4_x' ">
    <ProjectReference Include="../System/System.csproj" />
    <ProjectReference Include="../System.Core/System.Core.csproj" />
    <ProjectReference Include="../System.Reactive.Interfaces/System.Reactive.Interfaces.csproj" />
    <ProjectReference Include="../System.Reactive.Core/System.Reactive.Core.csproj" 