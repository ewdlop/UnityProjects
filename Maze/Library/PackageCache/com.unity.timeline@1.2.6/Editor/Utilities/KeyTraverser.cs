Microsoft Visual Studio Solution File, Format Version 8.00
Project("{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942}") = "IlmImf", "IlmImf\IlmImf.vcproj", "{7DF42126-D237-41D3-9ABE-A3ACC8BAC56E}"
	ProjectSection(ProjectDependencies) = postProject
	EndProjectSection
EndProject
Project("{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942}") = "IlmImfTest", "IlmImfTest\IlmImfTest.vcproj", "{5FF57359-FC61-472E-ABDD-30D3BC4A5E9E}"
	ProjectSection(ProjectDependencies) = postProject
		{7DF42126-D237-41D3-9ABE-A3ACC8BAC56E} = {7DF42126-D237-41D3-9ABE-A3ACC8BAC56E}
	EndProjectSection
EndProject
Project("{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942}") = "IlmImfExamples", "IlmImfExamples\IlmImfExamples.vcproj", "{50755A60-BCC4-4C6F-B77F-0420493F343B}"
	ProjectSection(ProjectDependencies) = postProject
		{7DF42126-D237-41D3-9ABE-A3ACC8BAC56E} = {7DF42126-D237-41D3-9ABE-A3ACC8BAC56E}
	EndProjectSection
EndProject
Project("{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942}") = "exrenvmap", "exrenvmap\exrenvmap.vcproj", "{90B61C5D-E217-46EC-B9E6-B67FC86AF42F}"
	ProjectSection(ProjectDependencies) = postProject
		{7DF42126-D237-41D3-9ABE-A3ACC8BAC56E} = {7DF42126-D237-41D3-9ABE-A3ACC8BAC56E}
	EndProjectSection
EndProject
Project("{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942}") = "exrheader", "exrheader\exrheader.vcproj", "{EFD7DCE5-55D9-4486-8C0C-F9884213624F}"
	ProjectSection(ProjectDependencies) = postProject
		{7DF42126-D237-41D3-9ABE-A3ACC8BAC56E} = {7DF42126-D237-41D3-9ABE-A3ACC8BAC56E}
	EndProjectSection
EndProject
Project("{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942}") = "exrmakepreview", "exrmakepreview\exrmakepreview.vcproj", "{00A40B08-A3AD-4E20-BA36-FD528B647CBE}"
	ProjectSection(ProjectDependencies) = postProject
		{7DF42126-D237-41D3-9ABE-A3ACC8BAC56E} = {7DF42126-D237-41D3-9ABE-A3ACC8BAC56E}
	EndProjectSection
EndProject
Project("{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942}") = "exrmaketiled", "exrmaketiled\exrmaketiled.vcproj", "{27A4D882-0F4D-44DE-8912-D1A13CF18184}"
	ProjectSection(ProjectDependencies) = postProject
		{7DF42126-D237-41D3-9ABE-A3ACC8BAC56E} = {7DF42126-D237-41D3-9ABE-A3ACC8BAC56E}
	EndProjectSection
EndProject
Project("{8BC9CEB8-8B4A-11D0-8D11-00A0C91BC942}") = "exrstdattr", "exrstdattr\exrstdattr.vcproj", "{7D3BC4DB-C194-43A7-B9A8-DD8C9F9E7945}"
	ProjectSection(ProjectDependencies) = postProject
		{7DF42126-D237-41D3-9ABE-A3ACC8BAC56E} = {7DF42126-D237-41D3-9ABE-A3ACC8BAC56E}
	EndProjectSection
EndProject
Global
	GlobalSection(SolutionConfiguration) = preSolution
		Debug = Debug
		Release = Release
	EndGlobalSection
	GlobalSection(ProjectConfiguration) = postSolution
		{7DF42126-D237-41D3-9ABE-A3ACC8BAC56E}.Debug.ActiveCfg = Debug|Win32
		{7DF42126-D237-41D3-9ABE-A3ACC8BAC56E}.Debug.Build.0 = Debug|Win32
		{7DF42126-D237-41D3-9ABE-A3ACC8BAC56E}.Release.ActiveCfg = Release|Win32
		{7DF42126-D237-41D3-9ABE-A3ACC8BAC56E}.Release.Build.0 = Release|Win32
		{5FF57359-FC61-472E-ABDD-30D3BC4A5E9E}.Debug.ActiveCfg = Debug|Win32
		{5FF57359-FC61-472E-ABDD-30D3BC4A5E9E}.Debug.Build.0 = Debug|Win32
		{5FF57359-FC61-472E-ABDD-30D3BC4A5E9E}.Release.ActiveCfg = Release|Win32
		{5FF57359-FC61-472E-ABDD-30D3BC4A5E9E}.Release.Build.0 = Release|Win32
		{50755A60-BCC4-4C6F-B77F-0420493F343B}.Debug.ActiveCfg = Debug|Win32
		{50755A60-BCC4-4C6F-B77F-0420493F343B}.Debug.Build.0 = Debug|Win32
		{50755A60-BCC4-4C6F-B77F-0420493F343B}.Release.ActiveCfg = Release|Win32
		{50755A60-BCC4-4C6F-B77F-0420493F343B}.Release.Build.0 = Release|Win32
		{90B61C5D-E217-46EC-B9E6-B67FC86AF42F}.Debug.ActiveCfg = Debug|Win32
		{90B61C5D-E217-46EC-B9E6-B67FC86AF42F}.Debug.Build.0 = Debug|Win32
		{90B61C5D-E217-46EC-B9E6-B67FC86AF42F}.Release.ActiveCfg = Release|Win32
		{90B61C5D-E217-46EC-B9E6-B67FC86AF42F}.Release.Build.0 = Release|Win32
		{EFD7DCE5-55D9-4486-8C0C-F9884213624F}.Debug.ActiveCfg = Debug|Win32
		{EFD7DCE5-55D9-4486-8C0C-F9884213624F}.Debug.Build.0 = Debug|Win32
		{EFD7DCE5-55D9-4486-8C0C-F9884213624F}.Release.ActiveCfg = Release|Win32
		{EFD7DCE5-55D9-4486-8C0C-F9884213624F}.Release.Build.0 = Release|Win32
		{00A40B08-A3AD-4E20-BA36-FD528B647CBE}.Debug.ActiveCfg = Debug|Win32
		{00A40B08-A3AD-4E20-BA36-FD528B647CBE}.Debug.Build.0 = Debug|Win32
		{00A40B08-A3AD-4E20-BA36-FD528B647CBE}.Release.ActiveCfg = Release|Win32
		{00A40B08-A3AD-4E20-BA36-FD528B647CBE}.Release.Build.0 = Release|Win32
		{27A4D882-0F4D-44DE-8912-D1A13CF18184}.Debug.ActiveCfg = Debug|Win32
		{27A4D882-0F4D-44DE-8912-D1A13CF18184}.Debug.Build.0 = Debug|Win32
		{27A4D882-0F4D-44DE-8912-D1A13CF18184}.Release.ActiveCfg = Release|Win32
		{27A4D882-0F4D-44DE-8912-D1A13CF18184}.Release.Build.0 = Release|Win32
		{7D3BC4DB-C194-43A7-B9A8-DD8C9F9E7945}.Debug.ActiveCfg = Debug|Win32
		{7D3BC4DB-C194-43A7-B9A8-DD8C9F9E7945}.Debug.Build.0 = Debug|Win32
		{7D3BC4DB-C194-43A7-B9A8-DD8C9F9E7945}.Release.ActiveCfg = Release|Win32
		{7D3BC4DB-C194-43A7-B9A8-DD8C9F9E7945}.Release.Build.0 = Release|Win32
	EndGlobalSection
	GlobalSection(ExtensibilityGlobals) = postSolution
	EndGlobalSection
	GlobalSection(ExtensibilityAddIns) = postSolution
	EndGlobalSection
EndGlobal
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                           ��L�7
��M'��%�@E�<o	����U)��?L��%��kv�k��fQ�O�'N?��� n�! N�W�x��������n��4t�p�y%MTP
������DD	p �:���9�<��w*���E����9V2��l����6n��g�A��o)��=Ȣ��B�פxNx�ֶ���Ė]�F뚣�#�3	�7��<A���������i �-��ŀ��[��ĸlt���m�桊�%��A�"CX�{�-ѽ��JQ����a�*.d\Q�?���I�$�����	B8��X�_�)��m�}�(B�s�y.	����6��}5�/>m�l�w�E��e���4���x��҅\ �������/�ye���k�����E�Ĵ�s�Ro�a���G�_��G��z�n��W<GO.y%R�Y���+�D�!���7Q����'��O�͜"S�q�V�#��+�[���ֻ��3�nz�(��`�X�5��1]�G $'_�����)���^p\v}dЕ�b��+A�Ȼy�>ƞ��k���ǘ�C�p��ӫ�/��E����_��6/��x-�>/{��f��\�优��7�8M��pw+q΍����s��@�����	1!��H��oO�Ұ�b�%���-c��֎�<e�X���"V$<����)ꮥq �CY��ό6����q�\�3f��}�e���$"���d�1�����'�%YW 7Nc����Nƌ�t���T8���N�t�֠�t�C �M)�=XԌ뺢�6ݬ(���j��	�Ąɀ�J>w�%��Cx� Q	����﯃)����ŕ��]a���=���)U!q#���d�[��@��F$?f����ׯ���(~��a��o?�<T���B�z�٤��{`c�)����Y�G��p�xm�`̕����@�����s1�!�l��V�#�;N����g&7����\3���1ì���>��Lڭ�/,5��������f)حS6S�A_l�����m��zh�E�+h�Ι?�z�"�p�h��*���X