<?xml version="1.0" encoding="Windows-1252"?>
<VisualStudioProject
	ProjectType="Visual C++"
	Version="7.10"
	Name="IlmImfExamples"
	ProjectGUID="{50755A60-BCC4-4C6F-B77F-0420493F343B}"
	Keyword="Win32Proj">
	<Platforms>
		<Platform
			Name="Win32"/>
	</Platforms>
	<Configurations>
		<Configuration
			Name="Debug|Win32"
			OutputDirectory="..\Debug"
			IntermediateDirectory="Debug"
			ConfigurationType="1"
			CharacterSet="2">
			<Tool
				Name="VCCLCompilerTool"
				Optimization="0"
				AdditionalIncludeDirectories="..\..\..\..\IlmImfExamples;..\..\..\..\config.windows;..\..\..\..\..\..\Deploy\include"
				PreprocessorDefinitions="OPENEXR_DLL;WIN32;_DEBUG;_CONSOLE"
				MinimalRebuild="TRUE"
				BasicRuntimeChecks="3"
				RuntimeLibrary="3"
				EnableEnhancedInstructionSet="2"
				ForceConformanceInForLoopScope="TRUE"
				RuntimeTypeInfo="TRUE"
				UsePrecompiledHeader="0"
				WarningLevel="3"
				Detect64BitPortabilityProblems="TRUE"
				DebugInformationFormat="4"/>
			<Tool
				Name="VCCustomBuildTool"/>
			<Tool
				Name="VCLinkerTool"
				AdditionalDependencies="..\..\..\..\..\..\Deploy\lib\$(IntDir)\IlmThread.lib ..\..\..\..\..\..\Deploy\lib\$(IntDir)\Imath.lib ..\..\..\..\..\..\Deploy\lib\$(IntDir)\Iex.lib ..\..\..\..\..\..\Deploy\lib\$(IntDir)\Half.lib"
				OutputFile="$(OutDir)/IlmImfExamples.exe"
				LinkIncremental="2"
				GenerateDebugInformation="TRUE"
				ProgramDatabaseFile="$(OutDir)/IlmImfExamples.pdb"
				SubSystem="1"
				TargetMachine="1"/>
			<Tool
				Name="VCMIDLTool"/>
			<Tool
				Name="VCPostBuildEventTool"/>
			<Tool
				Name="VCPreBuildEventTool"/>
			<Tool
				Name="VCPreLinkEventTool"/>
			<Tool
				Name="VCResourceCompilerTool"/>
			<Tool
				Name="VCWebServiceProxyGeneratorTool"/>
			<Tool
				Name="VCXMLDataGeneratorTool"/>
			<Tool
				Name="VCWebDeploymentTool"/>
			<Tool
				Name="VCManagedWrapperGeneratorTool"/>
			<Tool
				Name="VCAuxiliaryManagedWrapperGeneratorTool"/>
		</Configuration>
		<Configuration
			Name="Release|Win32"
			OutputDirectory="..\Release"
			IntermediateDirectory="Release"
			ConfigurationType="1"
			CharacterSet="2">
			<Tool
				Name="VCCLCompilerTool"
				AdditionalIncludeDirectories="..\..\..\..\IlmImfExamples;..\..\..\..\config.windows;..\..\..\..\..\..\Deploy\include"
				PreprocessorDefinitions="OPENEXR_DLL;WIN32;NDEBUG;_CONSOLE"
				RuntimeLibrary="2"
				EnableEnhancedInstructionSet="2"
				ForceConformanceInForLoopScope="TRUE"
				RuntimeTypeInfo="TRUE"
				UsePrecompiledHeader="0"
				WarningLevel="3"
				Detect64BitPortabilityProblems="TRUE"
				DebugInformationFormat="3"/>
			<Tool
				Name="VCCustomBuildTool"/>
			<Tool
				Name="VCLinkerTool"
				AdditionalDependencies="..\..\..\..\..\..\Deploy\lib\$(IntDir)\IlmThread.lib ..\..\..\..\..\..\Deploy\lib\$(IntDir)\Imath.lib ..\..\..\..\..\..\Deploy\lib\$(IntDir)\Iex.lib ..\..\..\..\..\..\Deploy\lib\$(IntDir)\Half.lib"
				OutputFile="$(OutDir)/IlmImfExamples.exe"
				LinkIncremental="1"
				GenerateDebugInformation="TRUE"
				SubSystem="1"
				OptimizeReferences="2"
				EnableCOMDATFolding="2"
				TargetMachine="1"/>
			<Tool
				Name="VCMIDLTool"/>
			<Tool
				Name="VCPostBuildEventTool"/>
			<Tool
				Name="VCPreBuildEventTool"/>
			<Tool
				Name="VCPreLinkEventTool"/>
			<Tool
				Name="VCResourceCompilerTool"/>
			<Tool
				Name="VCWebServiceProxyGeneratorTool"/>
			<Tool
				Name="VCXMLDataGeneratorTool"/>
			<Tool
				Name="VCWebDeploymentTool"/>
			<Tool
				Name="VCManagedWrapperGeneratorTool"/>
			<Tool
				Name="VCAuxiliaryManagedWrapperGeneratorTool"/>
		</Configuration>
	</Configurations>
	<References>
	</References>
	<Files>
		<Filter
			Name="Source Files"
			Filter="cpp;c;cxx;def;odl;idl;hpj;bat;asm;asmx"
			UniqueIdentifier="{4FC737F1-C7A5-4376-A066-2A32D752A2FF}">
			<File
				RelativePath="..\..\..\..\IlmImfExamples\drawImage.cpp">
			</File>
			<File
				RelativePath="..\..\..\..\IlmImfExamples\generalInterfaceExamples.cpp">
			</File>
			<File
				RelativePath="..\..\..\..\IlmImfExamples\generalInterfaceTiledExamples.cpp">
			</File>
			<File
				RelativePath="..\..\..\..\IlmImfExamples\lowLevelIoExamples.cpp">
			</File>
			<File
				RelativePath="..\..\..\..\IlmImfExamples\main.cpp">
			</File>