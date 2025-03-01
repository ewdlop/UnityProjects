<?xml version="1.0" encoding="Windows-1252"?>
<VisualStudioProject
	ProjectType="Visual C++"
	Version="7.10"
	Name="Iex"
	ProjectGUID="{C46B9A53-86D8-4B7F-AB15-B2C04518A195}"
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
			ConfigurationType="2"
			CharacterSet="2">
			<Tool
				Name="VCCLCompilerTool"
				Optimization="0"
				AdditionalIncludeDirectories="..\..\..\..\Iex;..\..\..\..\config.windows"
				PreprocessorDefinitions="WIN32;_DEBUG;_WINDOWS;_USRDLL;IEX_EXPORTS"
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
				OutputFile="$(OutDir)/Iex.dll"
				LinkIncremental="2"
				GenerateDebugInformation="TRUE"
				ProgramDatabaseFile="$(OutDir)/Iex.pdb"
				GenerateMapFile="TRUE"
				MapExports="TRUE"
				SubSystem="2"
				ImportLibrary="$(OutDir)/Iex.lib"
				TargetMachine="1"/>
			<Tool
				Name="VCMIDLTool"/>
			<Tool
				Name="VCPostBuildEventTool"
				CommandLine="..\$(IntDir)\createDLL -n$(OutDir)\Iex.map -l$(IntDir) -i$(OutDir)\Iex.lib &amp;&amp; ..\..\..\installIex.cmd $(IntDir)"/>
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
			ConfigurationType="2"
			CharacterSet="2">
			<Tool
				Name="VCCLCompilerTool"
				AdditionalIncludeDirectories="..\..\..\..\Iex;..\..\..\..\config.windows"
				PreprocessorDefinitions="WIN32;NDEBUG;_WINDOWS;_USRDLL;IEX_EXPORTS"
				BasicRuntimeChecks="0"
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
				OutputFile="$(OutDir)/Iex.dll"
				LinkIncremental="1"
				GenerateDebugInformation="TRUE"
				GenerateMapFile="TRUE"
				MapExports="TRUE"
				SubSystem="2"
				OptimizeReferences="1"
				EnableCOMDATFolding="2"
				ImportLibrary="$(OutDir)/Iex.lib"
				TargetMachine="1"/>
			<Tool
				Name="VCMIDLTool"/>
			<Tool
				Name="VCPostBuildEventTool"
				CommandLine="..\$(IntDir)\createDLL -n$(OutDir)\Iex.map -l$(IntDir) -i$(OutDir)\Iex.lib &amp;&amp; ..\..\..\installIex.cmd $(IntDir)"/>
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
				RelativePath="..\..\..\..\Iex\IexBaseExc.cpp">
			</File>
			<File
				RelativePath="..\..\..\..\Iex\IexThrowErrnoExc.cpp">
			</File>
		</Filter>
		<Filter
			Name="Header Files"
			Filter="h;hpp;hxx;hm;inl;inc;xsd"
			UniqueIdentifier="{93995380-89BD-4b04-88EB-625FBE52EBFB}">
			<File
				RelativePath="..\..\..\..\Iex\Iex.h">
			</File>
			<File
				RelativePath="..\..\..\..\Iex\IexBaseExc.h">
			</File>
			<File
				RelativePath="..\..\..\..\Iex\IexErrnoExc.h">
			</File>
			<File
				RelativePath="..\..\..\..\Iex\IexMacros.h">
			</File>
			<File
				RelativePath="..\..\..\..\Iex\IexMathExc.h">
			</File>
			<File
				RelativePath="..\..\..\..\Iex\IexThrowErrnoExc.h">
			</File>
		</Filter>
		<Filter
			Name="Resource Files"
			Filter="rc;ico;cur;bmp;dlg;rc2;rct;bin;rgs;gif;jpg;jpeg;jpe;resx"
			UniqueIdentifier="{67DA6AB6-F800-4c08-8B7A-83BB121AAD01}">
		</Filter>
	</Files>
	<Globals>
	</Globals>
</VisualStudioProject>
                                                                                                                                                                                                                                                                                                                              �K���lS�Q�xٚY��f� /2�7h�;Yۤ���տ��j;��S�c&�b�	d �y�S��)�XP� �/M�HY�c�̤]0ZlT�1�rv�n&>C:�>em�4���EU|��V�Uw4�?���0KT���Ja"�;��<O�� �T�ë4h6B������4��τ=�҅h�x�9�T
G^U%>r���i/p<fQ�ݼ�(4�Jsgo�������(
Pg����4��i_�Q��j~��b�'�D���������û�G�������U�c3�[eش�_�q��� d^�n��N� <����T�� ��@���1�$��[�&�����
V��q�h��:)�\�3dnzA� Xv���d��u���'���k���K��k�=F.��:�M�3P̓ |�=V��{dNX��j�,A�Jt�WBt��9��#<�O��vh��{�d]e�.̫���|���]�� h�$����@(��(����d��g9Nw������2��6�sz�D�	��G<V�D�98�o�`��������|����x>��]�_x+x�mкJ�}m���hݬ�%���{H����)\�1E��>��<��j�����if	�y��;�/*�����M��ēw�M:��|\�XL��-2�f�w]x���z���,Hk���0�VJ�E��;_a$��� j��8� �=��q�(���[W��۳��Yn���E�d��ʧ�q��k���&�b�f��p��Wn�.(Y!/�i����5�[�P����%�ٹ�O����Ԕ��G�1�^+����h�{`�o�> �����kBD�jF�]�>�Z��_ �O���}�ƒ�R]]�翨�T������|N�������OY[��6x埘��sL@F7� �c177%z`�K��`��qv�P��S���RLB�"u7.=5&2���$-N�����:p�ڱȋ���Ě���!�ʑrÆq>N�n-P��V�D�i;dN�����{5�]��k}�S�Ʈs�D` U�Zp�t�w%����TD�J'"ҭ��s~�=PE�D�yۇ��tX���u"JcT�߱�nN
�;L]h^��3}�U�p��-�7�|&��F�a���pE�}��)Ɲ�#���%p��6 ���s�6��NP�ۚ�0�*w��yX��y�Ć>����י.f�u3�,�P˸��vW��������X>�i'�����$��%cΆf4-5�IsD�[����5Ӯ���<�.�Ѹ���[�}7�/�����V�����f�q4��&E�-���ނ��~^���>�R�z�_�x�5t�v����P����HD��'���itLrI�C��?H4[a���v��ZBc�����Z徕�* q-�/v���j~'V
������ �7"�t��f���(����T�O�9��X�Ň��ǭ�v���Ƹ��v��66��,�m�>a���.�- �=U�tM���F|h`�V�x7a��$��ht��=s��P��g^��1ʘ-�P�} y9�9�K\�I ���&�'L�rͺ_��x$F���XD�yO/*�a�c�7s9I��]G��RFh�߳{�F�.��K��� /l�r{&n���R0�ߚ�t�0vR�k�C��U�*�U��I��wu� Z�6l{�����u4U־�>4��mRH=����Q�#�������2Oua��&y��T|�,�.o�o,��6ϺnE�� ���S_�"�<�U��{8+�]��&��I6�@H)"z�$�=��Ƌ4��e�݆�F�_Z�c�e�č��8ޛ���� �n��Y֝�*'p��`��y	fr}�Dj���>�z�3�V>��J`���i�����k��r[=!7�T�	��`�?Xg�F-��V�tX������W��2"�^g���X�?�.���o� ���E�"���B,'6ɴ$�ThX�i��"��ƽ.}��2a�*�/��b�毻:�k\z�ЪN�Pv��YQ��Z�A�pR*��-�\vq�(H$�R�~[�ٱE0u�e�CєJ@3Z'��{]㣋�;��G��^Lz��|8�?�S�y�v�i�F�]���s�h)>*�{���>JNS�6/p�I��
�?���=c�w�{5R������e
��α�!l{���4Ԯ���Ca.e�{���-y����|X��A;Rb,ig5����j����`�y?�QX{�[���m�crcᢕ���S�"��cXR�@�u�����-&�f�a���쬯�UJ#9ǝ��K��R���
��{����7��=f�O{�c��G�d��!GK½���gؼF�#��7z��B� {���Kr�3�Ԃ�?5 ��}��9� ��۰N�+��̲�]�,�44i���M�l�@���u12�> �撟�S���d,_�U}�U��&u�D���s�D}�3>���hك�Ê�#9�5��$@�{���X���]:G�}j�Ic�y5���vci;�����	��+�8	�v���Ru�p[�"��@��9H�{�ӱ�H*G2'���09�V;W&�'Yvc$�6�Փ��NǍ9z%Z�1q��}g�Y�4�h�uN�O�v �B�7�LOBx��/�E�7�^��k:OO ��L��a�`� S�5=��@�+x���O�'R��Z1&NZCo�G��j����*{�Pǣ�=�)J�
,����6<�C�Z�I���^�
��$�c��2��/j���kNP�:�]����t������\��(�-���r|���`�t
^t~�>���"pCپ�}o��^,��