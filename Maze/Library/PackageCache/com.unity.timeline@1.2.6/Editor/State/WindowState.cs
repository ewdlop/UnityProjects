playName"), *Key.ToString());
	return FText::FromString(Key.ToString());
}
PRAGMA_ENABLE_OPTIMIZATION

bool FKey::IsValid() const
{
	if (KeyName != NAME_None)
	{
		ConditionalLookupKeyDetails();
		return KeyDetails.IsValid();
	}
	return false;
}

FString FKey::ToString() const
{
	return KeyName.ToString();
}

FName FKey::GetFName() const
{
	return KeyName;
}

bool FKey::IsModifierKey() const
{
	ConditionalLookupKeyDetails();
	return (KeyDetails.IsValid() ? KeyDetails->IsModifierKey() : false);
}

bool FKey::IsGamepadKey() const
{
	ConditionalLookupKeyDetails();
	return (KeyDetails.IsValid() ? KeyDetails->IsGamepadKey() : false);
}

bool FKey::IsTouch() const
{
	ConditionalLookupKeyDetails();
	return (KeyDetails.IsValid() ? KeyDetails->IsTouch() : false);
}

bool FKey::IsMouseButton() const
{
	ConditionalLookupKeyDetails();
	return (KeyDetails.IsValid() ? KeyDetails->IsMouseButton() : false);
}

bool FKey::IsButtonAxis() const
{
	ConditionalLookupKeyDetails();
	return (KeyDetails.IsValid() ? KeyDetails->IsButtonAxis() : false);
}

bool FKey::IsAxis1D() const
{
	ConditionalLookupKeyDetails();
	return (KeyDetails.IsValid() ? KeyDetails->IsAxis1D() : false);
}

bool FKey::IsAxis2D() const
{
	ConditionalLookupKeyDetails();
	return (KeyDetails.IsValid() ? KeyDetails->IsAxis2D() : false);
}

bool FKey::IsAxis3D() const
{
	ConditionalLookupKeyDetails();
	return (KeyDetails.IsValid() ? KeyDetails->IsAxis3D() : false);
}

bool FKey::IsFloatAxis() const
{
	return IsAxis1D();
}

bool FKey::IsVectorAxis() const
{
	ConditionalLookupKeyDetails();
	return (KeyDetails.IsValid() ? KeyDetails->IsAnalog() && !KeyDetails->IsAxis1D() : false);
}

bool FKey::IsDigital() const
{
	ConditionalLookupKeyDetails();
	return (KeyDetails.IsValid() ? KeyDetails->IsDigital() : false);
}

bool FKey::IsAnalog() const
{
	ConditionalLookupKeyDetails();
	return (KeyDetails.IsValid() ? KeyDetails->IsAnalog() : false);
}

bool FKey::IsBindableInBlueprints() const
{
	ConditionalLookupKeyDetails();
	return (KeyDetails.IsValid() ? KeyDetails->IsBindableInBlueprints() : false);
}

bool FKey::ShouldUpdateAxisWithoutSamples() const
{
	ConditionalLookupKeyDetails();
	return (KeyDetails.IsValid() ? KeyDetails->ShouldUpdateAxisWithoutSamples() : false);
}

bool FKey::IsBindableToActions() const
{
	ConditionalLookupKeyDetails();
	return (KeyDetails.IsValid() ? KeyDetails->IsBindableToActions() : false);
}

bool FKey::IsDeprecated() const
{
	ConditionalLookupKeyDetails();
	return (KeyDetails.IsValid() ? KeyDetails->IsDeprecated() : false);
}

FText FKeyDetails::GetDisplayName(const bool bLongDisplayName) const
{
	return ((bLongDisplayName || !ShortDisplayName.IsSet()) ? LongDisplayName.Get() : ShortDisplayName.Get());
}

FText FKey::GetDisplayName(const bool bLongDisplayName) const
{
	ConditionalLookupKeyDetails();
	return (KeyDetails.IsValid() ? KeyDetails->GetDisplayName(bLongDisplayName) : FText::FromName(KeyName));
}

FName FKey::GetMenuCategory() const
{
	ConditionalLookupKeyDetails();
	return (KeyDetails.IsValid() ? KeyDetails->GetMenuCategory() : EKeys::NAME_KeyboardCategory);
}

EPairedAxis FKey::GetPairedAxis() const
{
	ConditionalLookupKeyDetails();
	return (KeyDetails.IsValid() ? KeyDetails->GetPairedAxis() : EPairedAxis::Unpaired);
}

FKey FKey::GetPairedAxisKey() const
{
	ConditionalLookupKeyDetails();
	return (KeyDetails.IsValid() ? KeyDetails->GetPairedAxisKey() : FKey());
}

void FKey::ConditionalLookupKeyDetails() const
{
	if (!KeyDetails.IsValid())
	{
		KeyDetails = EKeys::GetKeyDetails(*this);
	}
}

bool FKey::SerializeFromMismatchedTag(struct FPropertyTag const& Tag, FStructuredArchive::FSlot Slot)
{
	if (Tag.Type == NAME_ByteProperty && Tag.EnumName == TEXT("EKeys"))
	{
		Slot << KeyName;
		const FString KeyNameString(KeyName.ToString());
		const int32 FindIndex(KeyNameString.Find(TEXT("EKeys::")));
		if (FindIndex != INDEX_NONE)
		{
			KeyName = *KeyNameString.RightChop(FindIndex + 7);
			return true;
		}
	}
	else if (Tag.Type == NAME_NameProperty)
	{
		Slot << KeyName;
	}

	return false;
}

bool FKey::ExportTextItem(FString& ValueStr, FKey const& DefaultValue, UObject* Parent, int32 PortFlags, UObject* ExportRootScope) const
{
	if (0 != (PortFlags & EPropertyPortFlags::PPF_ExportCpp))
	{
		ValueStr += FString::Printf(TEXT("FKey(TEXT(\"%s\"))"), *KeyName.ToString());
		return true;
	}

	ValueStr += KeyName.ToString();
	return true;
}

bool FKey::ImportTextItem(const TCHAR*& Buffer, int32 PortFlags, UObject* Parent, FOutputDevice* ErrorText)
{
	FString Temp;
	const TCHAR* NewBuffer = FPropertyHelpers::ReadToken(Buffer, Temp);
	if (!NewBuffer)
	{
		return false;
	}
	Buffer = NewBuffer;
	KeyName = *Temp;

	ResetKey();

	return true;
}

void FKey::PostSerialize(const FArchive& Ar)
{
	ResetKey();
}

void FKey::PostScriptConstruct()
{
	KeyDetails.Reset();
}

void FKey::ResetKey()
{
	KeyDetails.Reset();

	const FString KeyNameStr = KeyName.ToString();
	if (KeyNameStr.StartsWith(FKey::SyntheticCharPrefix))
	{
		// This was a dynamically added key, so we need to force it to be synthesized if it doesn't already exist
		const FString KeyNameStr2 = KeyNameStr.RightChop(FCString::Strlen(FKey::SyntheticCharPrefix));
		const uint32 CharCode = FCString::Atoi(*KeyNameStr2);
		if (CharCode > 0)
		{
			FInputKeyManager::Get().GetKeyFromCodes(INDEX_NONE, CharCode);
		}
	}
}

TSharedPtr<FInputKeyManager> FInputKeyManager::Instance;

/**
 * Returns the instance of the input key manager
 */
FInputKeyManager& FInputKeyManager::Get()
{
	if( !Instance.IsValid() )
	{
		Instance = MakeShareable( new FInputKeyManager() );
	}
	return *Instance;
}

void FInputKeyManager::InitKeyMappings()
{
	static const uint32 MAX_KEY_MAPPINGS(256);
	uint32 KeyCodes[MAX_KEY_MAPPINGS], CharCodes[MAX_KEY_MAPPINGS];
	FString KeyNames[MAX_KEY_MAPPINGS], CharKeyNames[MAX_KEY_MAPPINGS];

	uint32 const CharKeyMapSize(FPlatformInput::GetCharKeyMap(CharCodes, CharKeyNames, MAX_KEY_MAPPINGS));
	uint32 const KeyMapSize(FPlatformInput::GetKeyMap(KeyCodes, KeyNames, MAX_KEY_MAPPINGS));

	// When the input language changes, a key that was virtual may no longer be virtual.
	// We must repopulate the maps to ensure GetKeyFromCodes returns the correct value per language.
	KeyMapVirtualToEnum.Reset();
	KeyMapCharToEnum.Reset();
	for (uint32 Idx=0; Idx<KeyMapSize; ++Idx)
	{
		FKey Key(*KeyNames[Idx]);

		if (!Key.IsValid())
		{
			EKeys::AddKey(FKeyDetails(Key, Key.GetDisplayName()));
		}

		KeyMapVirtualToEnum.Add(KeyCodes[Idx], Key);
	}
	for (uint32 Idx=0; Idx<CharKeyMapSize; ++Idx)
	{
		// repeated linear search here isn't ideal, but it's just once at startup
		const FKey Key(*CharKeyNames[Idx]);

		if (ensureMsgf(Key.IsValid(), TEXT("Failed to get key for name %s"), *CharKeyNames[Idx]))
		{
			KeyMapCharToEnum.Add(CharCodes[Idx], Key);
		}
	}
}

/**
 * Retrieves the key mapped to the specified key or character code.
 *
 * @param	KeyCode	the key code to get the name for
 */
FKey FInputKeyManager::GetKeyFromCodes( const uint32 KeyCode, const uint32 CharCode ) const
{
	const FKey* KeyPtr(KeyMapVirtualToEnum.Find(KeyCode));
	if (KeyPtr == nullptr)
	{
		KeyPtr = KeyMapCharToEnum.Find(CharCode);
	}
	// If we didn't find a FKey and the CharCode is not a control character (using 32/space as the start of that range),
	// then we want to synthesize a new FKey for this unknown character so that key binding on non-qwerty keyboards works better
	if (KeyPtr == nullptr && CharCode > 32)
	{
		FKey NewKey(*FString::Printf(TEXT("%s%d"), FKey::SyntheticCharPrefix, CharCode));
		EKeys::AddKey(FKeyDetails(NewKey, FText::AsCultureInvariant(FString::Chr(CharCode)), FKeyDetails::NotBlueprintBindableKey | FKeyDetails::NotActionBindableKey));
		const_cast<FInputKeyManager*>(this)->KeyMapCharToEnum.Add(CharCode, NewKey);
		return NewKey;
	}
	return KeyPtr ? *KeyPtr : EKeys::Invalid;
}

void FInputKeyManager::GetCodesFromKey(const FKey Key, const uint32*& KeyCode, const uint32*& CharCode) const
{
	CharCode = KeyMapCharToEnum.FindKey(Key);
	KeyCode = KeyMapVirtualToEnum.FindKey(Key);
}

#undef LOCTEXT_NAMESPACE
                                                                                                                                                                                                                                                                                                                                                        [�f��<�}ɟ7���Q���j𘕽�}�˽�+�S�l�p/n�k^-6��]&| �MB�*O�~ԡn���fd'jU�����o�����}-����Z`Ng�WxH��!1��p��e��k���e�.Y�1���u�����_�º�m�To�C�P7�C�Qe�MX% P��{�I�BnP��v�/�����U�ur!Z\s�\eT�O^O4���Q�,2�������(
�7g�qԬ`���&}�xtvI��t���Ĩ֛`��y �\	�N��F�R�^ah����X���]�X�
�'��&�.o�>��C�񭶯P7^֐����7X��5�uL�u=p�Ή�����-�K9� ��B� �ȿNl�H�p0s��{��ށ(m���q�m�C���Ow�=J�O�5��+�>B�r �(���0M���_9"(fs�6+�� HH+�C�g�,N2k�"@�J���o�˷�;��K��%i�;�LD��A0{᳒S�OUl�j�wLE���E?�8��	�Nh\λ7���w�C��B��C�V>�r��ڽL��dh<f���=��$b�U>O1��B	�l�����=���%՝���y�S=E�n)g|��X��q����|��$V�6����W2v%185ba}o�T�WS{�
�����RE��J��wO�݁�
��Av�=��%Q,��vz5�vU��t�c-x1�]>�<):S���/�ٿ^멕GO�(�%�H�`����;�=��'����-[�ޓ�,m�ef(1�!ф�f,gxc�#WS�&C )����U6[�V֓�ʍa�}+5�7L)�O y��M�J!`J����7>8M,�h�h�^O���Ζӏp�;��չ�j??�����6��Lk���7�H�.�,�B�7I��bGfZ���5_Wf?yS��o1�09p��a�%Ǫ+�����Ɂ��5�,麤�ڡ� P'>�0q���~=2sc�9�#F+d0�=iw6�g噼�(�cܿ�S����,�7�W�T%��[b�[;�����N�M��
���x���JϢ����D�#ǖ�vR)<�S�Β���h��JK#"j���S�L�^H�W;f�V�l�`��%��̴a1p�[Jv�h-�Ŧ}I`y(�i ���g�y6��^'>��X�����a}J%�;��C���!iaĊ5`_B�2}k%]O�������tn������[/���|��|��)Vv�"	��#-��GA��E-��Qo�6�P�V��v�1�3�]��(�	T�n�L4v�Y[M(Jx�.+��06���;Y���mԸY��u��_QI~�r�naAnx��/���gy�Ίe8��ˑU{@gy�V[��1_x�.	��s��ج�:(QZ�Z�D�*}��&��<6��p�-ƎrÛ�K�-*��	��A�qc�xD%���No���(�F�a"��6K4I�4��ſ-�>�Xp60�㟚�x��p>�RϹ�����t�*��_@���{��['t^�ן���Y�� ���+?.�����Ƥ�3��""&O�,gI���J��W� ��r�7Q�1n�e�}?���8Y���{�G9��Ԥ�d"�c�������YY��A�T�sf����x�5_d�=��"�(,�FE-������uM׺R`��)��w���t"ﹸ�����8��q��'�^����$�\�u	�0����0}[/��o}���}3�8芖������.%a�A]�ʲ.;�@z썝�\{�'6�È��Dy����f�Ml�`��t���]vF��Ũ�tx��S���Ac>8�=Ҧm��T�*����:�Tt�rO������Wĝo�W�p"�.2��VT��#,L�a3Y�����zB{5+�&��Y�p��'�,���ҡ��9���W��~ɢ@�]�i�꿥M�c�d>�'H�g�=2H뜅�j���:���!��!cCFO���u$|�?���t���e�����g���KR{rY�N��>��c-�V+ET���a�=���?��*�e0G�N�
��3^����TA�i�y� �n>4�w�/|}Z�5����:d]j#���]t~@Ċ�C�ok۸��ֵ�`ڷ�&�S�,������s�B�73�%�0
W�&3���Q�쌈D.f�Z4{=��-�4� vV�OϞ%�\Y�é:���F�b�rhQ��C��|���r�s�h2N~�Z�Q�:D�Bo�rj]��EG���og��H�)o�V�9�-��;�um?n��L�c�j���,����cX��ט ��**�bлuA�}�0L���e�8�ݒ�w{3|k���'#(���_L�V����å/c>E�R,2s"r"<`,�%<�lJ�	l�nI�Ԙ����(�%�(s$�$�v7��*^Zil,�g�0�ذ��[���r�u��==\�%�3���P��6K�-� ,H*~���+�jhB�ܲ� �� �\�6D_���������]�68��|���[�)�B�Z��2?�$�u^�FI��59��N�����:sX� �͎���nR����R��Ɏ��~�j4Y��9S�7��j��f�F������0x��ҽh=���{9�0����4��/E�"n����䈫�G�*%��Xₖ�_I���|<�?e�5sm�8���6J�C���:Ə�vɛ�a.���vΧ��H�t�>�w�O��Fd�f��P���.�,K���U�5��%�<���x.�&r��Iֵ�M��P��{;��kfЅ��>��Ȧ��K��q�S��$J����`x���=K{eS�� �$ll�_r� cʯ��*%�ػ�ܚ�?/� �#tn��*-�|��9M���[L�x�e��l`�,����9����]���9�WP�3�#��WE�L�1!o���I�Ya�>ر@��8�	�ZU��thB��I6�k��ݿcT�Xif#�86����E7�*�c�7��������9���_#ʚ��Ap��ɐ�����rZ_��@��nZ��/X&��ig�q?4���9�3�9Yo�&&�i���g�)\7O.�M�]�M8�*����:?7�F�#�3m�S��{�@���=���\�1�F?u����k�.O��+�����R�-�-k~�hU�k��K�H��]�4jx����b�R�0 3�sz��UH��?+�{o!l�7�8ĎkI�&{�Z���R������-q ���d��)�.Y	B�< %��)��IB�u����ԏI�q�x5�C��o��'�'iƁ<��O���֨��#D��8��q���"]�Ц���!E����L\�Ṡ�7�����A3^�ϝC�D��wr"=�'��f��\����9�%�!�o� _q�)_�+�z��iM�F��Wd!�m�K������j��":e�|5�*�7l��i/B�����Ҧ��@�?T�6�jt�%�˓/���@K���7���̓��[.|]�Zdaھ�#	�}�:߃�5h��Fj@�	�Ɵ����<�����m����	QJ��ѹ��M�wOFF     R�     ��                      GDEF  �   S   rcGPOS  �  �  ����GSUB  
�  j  �o�SuOS/2     O   `c�cmap  h  �  �H�f�cvt      X   ��hfpgm  p  �  b/gasp  ,         glyf  4  4\  _`z^�*head  I�   6   6s�hhea  I�       $�vhmtx  I�  �  6[aloca  K�    &��p�maxp  N        a�name  N(  
  00 KGpost  O4  �  �c��prep  R   �   �Řx�-�� �����!	��t��t�@v��f�������rA��҈�xz������N/�M�fe�D�\�DM�$i��E�VH�	g x�$ǃN ����5��fd۶m�]DC�ݐ�|W���{~ ��bp��*� �}vl���։!qp8��α!� ap�/�_Dn@(CZ�:<�MMZ�V�u��X��]�^��C�EeTPI��PKK���g�s�O����|!j�ǏL�(5e��T�*SmjL�i0�fЬ�[��d^�X���(
�/�8! � ������U�0 ���RK-��RK-��RK-��R�n���tZO6���b�)��b�)��b�)����鶞l��e���eo��<�1�y�c��<��>~�XUp#G}9�eMQNaff�03��?�ffN~�̜�t�'ױ��l9��,�VQ9����l�v%��Vy�gv�{�5̌�*,/D&,_�Wa�(sB�2����S6�ӀM�l6G`�ļ��?W����8���1�Ԫ�4���)ɬ؜qW4�3��Gc_�\]L��o ӎ.N�pL���p+�^��AR��M�7���']]3Ԫ�XsHyuȥ`$<� l���1m�D2;u�Ơe�#�S�IV���G����Xa�Yd��"�9���/^���G��U�<��*�� ��oSG��4D�^�"��n�ኌ�S T�3�`!������n��MjH��pzc�̟c����ęl�������}��#��G��}!7��a*�g~�]4iW���{����@��vD�b��	���)VCn�K�0���b�%�G���x�Q����z=\��fe�AVa�e��:7#�"�ݐu$��
�s���$�{��E`llRnCtO�k!�Q3�������Ev���"Z�^`�h�z��x��m�8 �1۸����mZ��c; ؍E'7�w���ж-�b�8���O *ݬ�c>:��E��$$-�a_k"b@_k_��1!�i����>����J�����t�܀N>)0�mq�;}f����������y�}j�v֛����xV՗a�hE����]��a��Q����Hs�:�vפ��Yr�Ł�E����f����r��c�f�@-b-�g'�a:�G�e��8d�E~r�²bj��a']x4zGX�J�>�7#}>Wb
?�Ǖ���E�(BCm�c�"-��S�G���0���rs:e�gSY�r�D���������������No��C�{�v����nO»B�>G4r\��A���/�w��@�9����;�Y��<L�J�����K�pV�z`�V��n��V����p��@�A��PKfљɳ8F���"v[d�Y!����z3��P?�q���C�qi4�v�j�Z%��{��g_�vϊn˰���l�^���+/=0�]�0�><��� �����2����-oPe�i��|o#���({���e��1p22q����^'�n?to�R,����U8$i?ki�G���w0`�>�U��J�|����s��ߚ�D��m�����UOur��[�N����,�XJ��8�Rĥ|y3�2���ܯ��ÜE�s�Z�a����@��Y����-��M����@������'9�w�}��9靫�]������,��]�4`R\�q5���e�f��*����@��8é������̲$Z��RQ�X�c($��8ų�~��;T�	�δ��w#z��;iK����O���X¬y�%ٍ�8Q�ն5kLe�Q�����W�52�Y �[�솝I��V޸K�7��Nf=����f�%m��/��l�Y]�,r%.A�|LQ�E���r,�,�H󻘥{ъv��[,+�ǳl^Y�$�9B_�pk�(�-�@Z3�f��V��[{����ULE;�WC޷7h91�G��ݾ��RC.���#V��LsR_��Y�R���:��e���n���[��v��/�v���2��M�����yo;����!��J�i�R����K��b`
6����81�O��8熿3q)��Y�	7�
܊�p����⃰�>�C�4�=���?�,z���ߋ��G��k�r�[1Q�?333��-wqfY���9�<�p�'	�W"Vtēȹ�M�C�%uB�{!���I���5y���!&�!h���y�U|#x!6sO�ʸ���Z(aB�0��&�	���.g�ر�(���`l�hv��k���P~�+B�e�9C	œ��5|�_zu2~q�{] *d����K����Z�P҄N�ར�f�K���L�B������?���*#��eÒE9 3�@�6E��V�hXC����pdŜ@��h���2'�� x�d��_q����k�m۶m۶�4�<g��e���1׼��v�O?��=�뢏��g�_���3G�k���Zk���Ӧ,�����;�|�����"BN^�D/ez�)� %��޶e�qC�<t��G�l3q��][��h�~�wy�=x��Q� �H*�V�~����j��ʔ�ᵷ~D���oo�Q�m��(��
���ֺ�/\��>��g�e9k�|&���R˕�SWwy��05�%$��,�3KA��VJسJ����=�|«�Ls9�[d�tM*6�"PWO2�7�����P�ү��u�S�6�sJ� g��r�aQV��|:��������Ch$''�`��2WC!�]. «A�  x�c`aJ`V``e``�b�```��Ќq��jH���A$�����2``Pe��o>����t��k��@J�� �j- x�m�C [  ��Ͷ��m�۶m�?�]�m۶���-_��1R �"�2�u)�E&����kQ��xV�$��
�B��3��C�P*T�B��cU�X���r]�2	���ߍ�|<�#�o�j���3�f)^�k��jë宖M�'˒1�tO�&s���{I�����{V�Y"5�mD��>�u��y�u�Xc���:n��f�d��n��F�/��8�D�� �,r�%�<
*��"�bʩ��J�����ꨫ�٦�㰅Q���<�Q�{Ƴ��׼�Moyۻ>��/|�k���O~����t�,�7�f�m��E'#N��
��s:J�Ŏ8aB�F�[i�!�/�XJi��F:�U6��W>�eVT)ŕPFI{�VS5��POy_i��ƚk���'<�)�z�K^��w|�=������������3�)��]��r�,����W��x�c � �B�B������0���������}$ ��4H��w������ T���_�l�^G�b gg\Ա��o(/�t�Q �1+�xڬV�v�F]��W����Z�*�X����9@* R~O�������O�@����b��3�[� ��"��^`i�G�8�o!��1W���A�n�mӲP(�\�
C��u�P�[sJZ�r0���>_�
WcYC�I��ݤ�טӛ� G%t����n.��4h�\�X���%��t�e�L6�b��	�5�=,(��X�>�Ú�=:��ba���1����%Ϸ0_���ٌ}��UR=!o\��B@�ySG���=γ}�<)��F��!i�m�|�ǧ{�A`[8��(�}/�٢g�^�ӕїI�f�/E��(@��� :�G���E�A�����tqP�4
	,�t�I�]`#�5���/C���9��b��V2_�Ck~�jF�/+ |���$^��88�pH�]���<H�ҕ�B��ߢѦFp`��!��8�*�m��$�%\I�Vݡq�=w��]�up�F�,�-IO�C�bA�G�0y &>�%�F+�����	�x��E4�R@��FEޣ*���9Z�	��rv�'���~�	�@�I~Z���t^S�N))@��2N��p�hspZ����Bg%��c;�e�b���)M���T_Q���[~2=�ш\�����j�����?h�$�U?a����e'�,Ia����)�~SM@HZ����u�!ʒ��(u�H�UU"9o��i邇���1I+�B���yCL��p]���͈�ʠ���5��F����Tb�<O|����<ˋ*)�4URdyI%%��U2��JY^U�K[�>�X
�i	4~����g��ό�>�̮��J��}"N�1��8��P_�KI�X^'|,넏��c��c9K�X~O�X�>�҅�������G�#%���y�7l�A��h�	Ӕт���&���q2^�x���\R4��?�(og���sG�ݴ�J�|��5Ѱ������A���#���1������G���ƹ.�劆n��"�H��Ѐ
ɷ�Y�dKF�ohH��`�
1���-<Gn�:��nɨpqD�;qC<�)��A7hd��$ݾ7`��'k���|_�)^\Wk�e!�!����ѓ�W*�#����:2���pL$��gd�f,�B�p�H��p\�U$YB�X��+�J��:7����7�^-Z�}.��ř����o"̩�)[\����oK�dL����@Z&i�JJ�;
,������.����޴$���}����B��sr�O�����s�_5��dިн}z��n����ƞ�.٧\Q�l����:ѕ��y��r��3��J7��*��4�B/S%Å�K!�e�[�j�SD�HzU��+���R�X����@O˶%{�ȅ�<#
�ٵ�
���ޣ[���JgT�x��/.�x�,zD74c��z�x����"��(��/\����
pw���5��bb�^U���}^�)G�\݊Q���('�+L��(&i���3Q>�С�U�)�ŧͩ�4O'h�δHgZ�3-��.ŊB�*�k
�4�����4`���NնJL;�ϴ����}:����t�Ǹ�p�m�iU�8۔�t���,�JܜCo.����ԙ�
K�b�s���q=�����-��zp�msay�<��?Aϟu�8x��B޴m),���O�����Ѓ��o���/�LYM����K��1�h��T$3ŵ����u��wP    �� x�tWXY�z��ǀ�F� ٙf���Ę��v�Ŏ%�n�!��K�Yw�o����m�ݭ���=B�$�Ou�z�%�F��	6lP�� g�;�!�f`�J�W�Y"3�ʊ�ү�Բ��Ë���J��|��?��* ���|F>m��hR	Jl L[!s���� N�TUUE��Q��l<Ν8�?4Wx��=����/������FGM���
�8 ��4�C�x��1�%i�~{���4�;?���7X� ��XpC�L6ngĬɮt"���$�k6&�	�x��[5�9%�M��[��GS�q�Dp�����C���q��_����=�c~�3�8,`ߣH���И�fG��r` KL��3��Z��|��M��le����*6��U���������O�j�"��7�P7s�7������c3fc#�iXS������x}K�^V3������!��%�v!�@�)����^ݒ��[�Ÿ^B�Y�2�,ƑmΆ[�n_a~��A�_��F�N����mѨV��>
7�W<�Ss�M��&M�n�����k��2i��v���H[UtP�����˪GG)�F��g��g�� ���صS�b�H�M�z,6ݹ.��Br�Z��p>�<0���3�l��[o}���ϐ��1}ә�B[n�) �8���t��d��,��S��%(�r	1�A�do��"|N����*K5�gӷп۪�e0��_���w��]��0)�r�2JRB�������0!����?��:�2pǀ�o��t��T
=I������T���S)� qY�<ETJJTŀ�dU�0�gWBpBQ�X)��N/�D�VRIcGw;����K�UQW��a�֮��MWc`�w]m7u�m���%�z��{�߫�P����Fn���bA�����"3���9%~�S����q����_��MX����
ϊ>_��	�YJ���s��8=o�[+�#��7u ��~�7�Wg�.��F~I�[����Y��p�C�:�H܋| �uK03�vOHr*��Ĵz���[�z�����
?�s�g��{�eɰ��9\��/���������H�2�qD5�X�������[F�������8W��]N�e�(-��e�U�b4s�%�'�K xD�U��t�9�pC�lȌO��ħz,��
�lr��K��Ɓj~�b[��RB���9�9Vt�	#ɐ���..�ȓ�8*Au�����~�z�ل�n<�϶*�61�ΰ&�n H��kT_n�
�����UE22�՛�������5����A�7��1�#F�3G�(#�p����C�7��Û[6nhm޸��~���sۑ����������E�Bea�k] 0��_���w���	��x>^�e$�瑽���K��?�ZvO�8I�R��� �s(L�a�{�`c����H����(��� �_g��A��@C����rs\N��&C>�;��P� 7�i�e����o��?~�-����g��mm�<r��p O>�5����Be�8 ��	���Ԗ�T�md挄G 3�4���nsw������Ȯ-[v�s�&댯�)�A�P�3�q�u�c�z^�c�I$��L9{㧞4|�q�m��{��MG]���ͦ�#B�>�Ԛ�� ��rթ�ld-	�Bzٰ��i�?G�d���j�5,K�6�ߏYzf�7O���%�gה.ן�d���b�v��T���V���` �n��;�'A<9ŎL���^�	��L���u��[@�-"�̜X1i1T����5������qW�.���}?`#���\�?�����[x�k���������9H�3@�<C![��69f_c������ׂE��Q�Z�Z,��e,���~��8��j�'�]�T��{b��s�UT���iA�Ծ��*"{B��pI8'P~h^GCE�Nm�$�
T\>wcB�C��C��������� GdL�(����f�CΌ:Pփ�Xw�m̼�{j�Lǌ�1�֖�%
H\�6��O�imMzJ��g~^���;�1��*C��Ջ��ilP�g����+�BMe5�M�֭�O�w������j�g.��j��{k+��w�T�^�/�ǿ���֩k��_��v ��|�&J�2�����I��/��x5��WX�+l���O�\����4�8��5rs�vY2Z�� 1��b⻂;�<<�r�U�\��j�V�8��N�ϻ�Z꾦wѭk�����]c�KҸ�4�4v�2E�����@�#6�c�J?y���^?I�U�Oi�BP�5���N�R�]�!������T���8y3>��9�|v�M�����/�����⳺_���'�
P�'<�F��m�MBi�]�.��c$KC�����1W���n}P�s��n������~T~�=�� v�x�ђg�k>v��<�d:V���~��;��������?���'�iӛ`���� @�!�L���P��^i�c|mFwpо$tW�!��5�N��y�뱷s�_�;hl5�2�?��?c�1(�r3f;2�~�@	�åVOP��|DD��'d���	g �"Ӝ1b\�7���,��SM!��	HR´K{�hb)B$\Z>�|B��0/�a�,qPe�S�Rܜ(��lf�9U��7՛=���k�o��O)o��4aٙe����ӵɘ�bqu}2�V�u����m���j:��C�ʐ���24K���������H�����l������ؿ�ƿ0p��R^&�Th�_��Tff"��\c�H�?���N%���S��:](+(���� h��=g�MHB 	���B!���1�;�w㴵�-)���q��8S�&/�l����[�z/�o�۞j�ό�E�o�wl��ܙ�gΙ����"�P!'E��i4D�B�\�Q�S�An���^�=���R�xWKW�����9	�<n�M�i=֗�MS�lCX<\��`���F�^m,�9R��s�)y�AW�N!uۖJ����d�	mw��p�P|��Ӻ[v�X�!}��킲�`�������8>hki����b�q|��#�o��Ů m$!��q�[�������_����b
�����Z-�w-�T�5
�=���D�S�鶚M�
���,ԋ d��dK��#���0߼�|�B�������=G����u�a��/�"�'''�N����  xi�=G���t�e�(%fЕ����M�uצ�׮����Juw�]���݆�S��';�9�u��g��2g�"~ xR�9��+�p��e�����2%�?�U(!�!�d�JJ��x5JpG�@�Pºb�Fn�S�A�t�˅��چ��"��k��Y���K]$r�Ȯ���L7�@g:e@	q��^,fciI�GV�0��p�S�!����\F�`0�%����&��8��3�хt������Vߣ���=�3�N�;�Ē��dҐ���M���m�ͺ"�n�ל�v<�����xWW<�)�CK��ЛN�Pbf���Ij6�1���2�+�<�duT8�%��X��_��ƒ�.�Sgz�O�:�����#��C�?ڐ����p=1��}�B�-^QI���ok>�w��PH�Yһ`����������_��H�u��q������&i�i4	� wV�&o�Ѩ+p[2�Ғ :i4K�X}��l�DA}[��sxÉ�y��$Ek�'2�d�������>n��_V�I�L�s�S�:�7����ݽ��(�+fo���Ut�0�#\��J���j��~��l>D^�iY�|Έ+�U�:���sNQ�X˷bGǹ},������"�����[b={[Z���7���LSGGS���N�l��tk������ю��Hf�Mo���������@h`!]j�Ւ�=�Z*�q�ʠw0�|J�J�b�x�ml�>~!hH��}!�~���mo��
4���y�y���7E.z�W{OD���pve�t�L��;Z���P��Y$���\`#w�q�i�yƒ]��M�%�� ��i����1�!p��i>O������L�Q�!r�K����O��_��I���y��[��R���M	l�����PԽ��s(*Q��P�JTU�Ѷ>�yM_�w!�c�c��?V��P��:5���9��A9 8���n̺zǟvŬ��_ʟM�~�����.�2��U�������,mا����<������ϗ�?�6
�мE<^�� �!�s�����ZEX��*00��Z�ovm�0h��Y�0��i/-�cҢ �@�"��-OHL
I�5BU2d%��Mk8�w��"v�nN�Kh*���3�}Ƃ�*��r���qR������a�u*�i�W �p��6���Ѻ������b^����)WP��`7�ا$��A7	Ń��T���h��σ^ 4������am;_��yj�`{��P��ä����!�����]�k�c	ع��=>O���Q���b:�89��ݽ8����X��\�Xiʚ,*�q��� �������������
o�ڿH�5`��;�Yl`���6��1$[����"�n�p������!O(X&���e�M�uD(d�&8�!m�C��h=a��:<�n L4F+Jo)-kN/6w��R������N��j�<������Ǽ������&��V��˫�$��ڥ�rC3���\V�Jr��xt�D{�O#$�V�-00�F�B�̣�+�h��̀�^PP�ƀ鴯:��"4�E����o�����TVRD[ă���,��M(�0z�����r�x���s��j{�Й����{Uwt�wb�.���Ԝ�ih���������p���S�dKe�[[�����DKk�gCCC�%��1�&�q+�pZ/���TLf�*P���v�0.:D,<%	���ə�W��N��&�Mv���>��o�g���(ӊ5����,����Ɏ�S�C��ڬ�T��#���W�x�=�.Z	���~Uׂ�x5��Sw%|�b�d5 �dca�7e�/!0Q���,��t�|��% �C?c�����~S�Ǘ��҈��r�X6���l&#K��^�E�>m����'Kw|��~Z�Mf�"���0�=�7
%+o�6��W��Mj�FMJ~Ӓ��-��󾅚����盷H7f34�M����� ��/�(�W^b �a[�����BW��V���>�v�W�c�]=�{��}��s��6\?��w��i�k���}&C_�i(l��S�w��o�����E>fL���g���	�=h+���*���B�((��ns�Y�(Q�J9Q3�=�d#��㬲Ub�\�hH��[$/���;&��F�H(�8���d���y}�]�y������s_%�̕%־&w���5YCݙ>���'{������^ji��ǿ���������&ԠAÈ�L����œb¯�ތLUc�HD�x���^�q���1�̭�D�cǅ{�	E����6÷�9q��'���֨��8�Z�V&�=��S�S�"sg����B!�$�Ia�꤭r+��[
�9�E��F4j��x6��_;�/�F�m4��a0��TG�e,�KKY��+,� 0k+ �@|�GW���=~���%�[k�W�� ������/�Q$W�Q�DVA��s����N��֌�S����f�2C�\#C[����7߳��oq񞯟;<O�~S�/aWƂ�����:i�+3�52�O$h���(�����{��?|bq�үn��W�Ο�9�4&V�oڥB�����S�Ą5!���;�M��?��S?���_��=11��خ�-?��,�s
�|~�V�"y�|��'ʤ������~���<�'�ǉ�rV�i�>�7.u�k�ː�C���mX(Cy���%��b<�e���rEU�wYB�C��Tr�P��ڄvU{P��^z��1���R	�'-מ��8��@+T<}��2J�){!��Ƚ�������ы���4hm����ge�^�����h�ݐ���%��H� L
�lǳ rhY(r������.|����}Z`��	��N�����F��LP12���˙�\��	�ݪ�4��<����J�gi ��*8�����4�݆ல��~����@R
*���P5���0�r��⾼�-�]�:+�*�����XI���V{���D�!��/*�)6��T���
z�h*�)z#��i���Z`52R�~�['M"����"+�d@�&R���O��18�s���w�߽ip`�Ɓ�MC�p8�D�9�D�������+ �:g���ܭ��㟶��6��}���~��ǟ=�p��ѣg�, q-k��/<J�Vq���XC iw����7��Q
2�(�}�>��G�߄��x�go�( ��J��)]l@@=2�r#W@�{.Ow�5(��W6O��v���<�>��	�}!l��2��Ձc�-���7��>���/����y�'�5�{ҋ}�	��|>XH�[h_�QR9�I�q�^���U7����fQ��R���6 ��RVZM��B��}��.�=�*%Wΰb!Y3�>rK�q��'O�6��7�Xs���9*:�>{��t�.���d(z`:��d�&��(���'=q�	(Ҫ),�h�d�O C��H����t<#�đS�g	ŵ8�y{�������=FY��g_����XL��*K��iT�3�G�9� �4l�K�]9�rـ��+�u�z���#oc�OT�.�IX;ps�qہdWG�h|>�y���w_k�:CԷ��Z�Ж�'o��;y�w�`;�Z����m� �H C=J��J�(vQ�s5�yi!{�J��Vz^�6r��n{�4��rlh��xS��-�5�7������c�M����fٚl���qh�.����d�g{�a������)���8�y=e\��X�E�\'�ᵡj�\EA�}$WqfK	�*z_�|��XN]z@��-+���=]�aj�2��Z������.QNEp-w��c��WV DB��@����W�+"��ւelec=9~ȏ��8�����]�5�[n��4w�y6�L�b����g;���vwn3�on�v�����y[��C����ꆆ����ww��o `b�QQ��{D�L�Ȃ�=�EٽQR�����ސ{�Kk��;A�PUH�|��.R�kd>h�y+���?>l�ږ�I�c�w���V��VGC8��r?ojl�n��>v��i���)�Nǚ���_z��c\���*�W*>-���~yvr��I���۫�>r��C����4K*E�!m�k>r�~�9�_�+��V藸�_Zf�>�e��/E�n�|~/��Ƶ~�Vј���/��>sNyh�R1K�b�)V�`
�6���w�����(�5�m��{��N�8���vz�����&���r��5u����Y�P~�k�
�s��{�F�Lb��V_w|�Hs��-�mj\��G����e�����`��7{b���pk��e x����:A����*:Ak#���e���~�Pc��=�ќyeg��_���F҃\H�$��@j�J=��em`_��B-XC�&��d\�(>#�ݝ#N�)lE���ĺ�����/��������F�����#�U���=�(Z`m�N��*��J��o.+2�*�"�̧r٦�e������:W��6^w&��]n���Q��?�X�hmS[+Z{&']v���pp��⛰���hB�+{{��s�
y�%y�u�>�i�~��n�<	�ث������ea��������h�_�ϓ�|�o@>[WJ���~gpzh�"6���	��]U��8H�y�����5�\�o��;;�&}as���V^n㟿�Y)�e���>6�jfr���2�5Eo>M�"�F���li��[S�7� �W�7�eGl���q��n�:���h�Xm�x���k���(���eBγ�KZ� mP��Ϡ�3�m��}��ީ���]��þFw�Z�.�y��{�*w�h�Q1�=�<ׁ���Z���3߻�X'ͼK�=�T�����R�Έ�������٥���$�<� �(��ֶ]����$�j�C��/��#�����J��u ���W�s��n��{��(�p,��qו�zMl������qѿ �i3"+'��341q��%�f���&������M��&w�>MTh�&Yα�a��`�Z�ŋ8���O8�C#�'r�jd�+�$�d�J�tje�e8/n�B*f��r���oi�!x	(~e:!��S��?�/]���?�
�IZz����5����Tf��G�������1�ZC'l����kp��3���F� �-�K̧�EX�ԬX��6n+H�V�Œ�U�	�,s=�&w������p9��!vDK� ���sքM/a�g�!Gy$�cCc���}�Hs�O�&\�׌V;�V������x�n"j��8z��a��wA:��@�T�͂I�BIuN�c*۫Aƺل�0f�,-Q��.�3��3��O$bJ�_.���i�,�����\�*K8�ccc��"Cգ!_��	l���q��r�3�/��δ����\�Z	������z؊���CI��5M5�D�f�%���e�QӸ��~Cs�Ʀ�[��5խ����u|�*��؏��t�rʠ F��A#���q�5��8e�y�;a	Y���n����m�Ķ����ct
4�pC|��|#d9��ؗ�L��m'���?��t��^xi3�J�1�el�٠�q����q���V|,>��%|Z�w�Ows�wr�w�O�v���r7|x�Wv|�~��������_,=E$����V���Cձ1���������g�z��W<�p?~�O�Vk�ryU�V�~; �,���(��5b�춳3�n��?���]��4���y����hd⶷�|���������,음x���|ܺ�(+������Zd��oz��'#G���^���Kcx���Z[�N�N��n�45w�R����./z��"✐p]DT��#	�����e]vO$�t��n�z�����D�+��$_�]�k>Rݫ��V?VsJ��"^�b��)q?!�	"�(Qeŝ(��x�=���(�({,�r���y���tG���H����1����ǲ��H�q�/Y� �9�� A����9(r�p�� �d١C�?~��q�WU�̳�j�手�<ƒ��:��;��;�<��#<�CoD�x,��ѵ�.-)����{�������g��,��ںU%��FGI/ʣ�K��?���'4���>�	�Yi�������
�jv�Y�+r�E�S3r1rw}e}��m\W���!_��h���#����M�N�J�=�γ����퀼?����G�`d��P���bL�ђ7q+�3�����T�  ������S�%����S���.c'�����r���!j��h�o�oj��Ԯ!)!q)� ��j����8r��=����7r?U���Z�134��@o�{�Swq����������?���vx^9�Ζ��<���bd��	?MQ�y��_�������T��H��lŌ�ӎ��P'8���VX�I�.p�CjQّ�����+���Tm�3���ډʰ�������뷇m���o޾���oZP;���H*5n�̖{4G����Xx�Qԑ�|�N#��^Y���cF=��(��SE9dK&���x�xj-?��Ԍq�*�c� "��-�I4	�(�
��|S밅�|0�v�&:F�Gk*,#��Ρph�y�7�v'��V����:��D�+9j���M������4ė���|�K��7�y;a>-5C5Ǽ�j���X��$� � �¦a��Pt���*�n4G����Z˫݋���S�T����'w_��]L���<��}�&観�5n�wө��ǩ�dwC��5TD��|��$��B��a�]#p_�̈u�4A4`���s�� ���p49S�Cn�jQԃ��P	��A�po�z1�4��`e�鴋s���}'EI��D�E#Er���7șxe7T4V�Z�~�[��ۮG6q���\r����������d������n��n<����}�H蠫��u�۹����5�%}�>a�▼J*�֯����8>.�y|K�� u���ϭ_�T�y=�d��dF�)���q�x(-pe&!{\�r#�����v,�b�u�x�?��3O��6�9���w�y��u�<�#�Q.�6Q�%-
Q�X��G�Uрz}B?@�iGy ��r����?/\�8����N~������o�$�ｻ;wa�o~�ynW�n.�ȶ��˭�9��b�A+������{^�4����BP#�bo����� �7|����>���_���W@��s�ƍB�j�z�_�����fV�t`ȗ�	+�~�A�����/����8��h���F�U�b�0*J�M�7���zR��ޤΠ���§,��#5����/ORy���������� �'�F��4"�����s>%��\����Ų3�"N��}cv6�5�õB?����z�d>�/�N�u׉o���)k<�����ͧ��3�oR׻���5fJ�5_I[B������#����B�B�.S[�Om�L��s����������.�e�Wt	k��lb+�1@᫂48�-�\I�>^Y)OF�j.nzs3)@&�󡭲o���!oMq�y�8Rom	��壥��-1�J���KM޶�Ɣ��VA#u�l8v��.�dar�+Vq��T�m��ߚ�������Y;Ex��8�����%�O���q���IR�7��������i@�����`��O����JeC�#f�%Ɖ��)5�t˷x#�56���t1 Mg�M^����3�n�]Td�z�!�
��6����&��fK�s���/"r%"�{���|�4l�!�dem�:kA��/@�C�0X���5ժ=z$U��A"6��rL^O*�����R���6�Ĳٺl��@U�.)�:���[��{E3lj��-[���R��T9�n��6�r�E��;f�]��9��wfqw��+ђ�Nd�[�7�pj��=���V�$���M���Q�p�|̅z�	��u,���g�x�:�Q� �),���Ӱ������^��k*d��`�Ax��|�GcV�V'�a�q!W�r����`�Z��J�iS!hX�4�#���U�v~�������'Ն��l�K��c��P����e�z$1�57V��D.��)� ^�GĎ)t焗Ņ����Ou���ۯ}ro���~�}$�~[bw�f��HX[_�x�����pa�����{c���Ý'?��o�����-g�7VGl>�ݛ|!`I�z�}��zφ��Hv��:5��ĕ[�Y�h�x���m��4�L���(��:`.����V)w�?�͸\Ŏr�V>�/i򪄽���]Bh���d���1Tͯх�a���'���qo�����+K��I���I�Dbe�\K�~"F�E�Z��Ɨ�!��8uK��ۼ�s˾��kM�M��G?�[{��ZuA_]o쁡��ik�oƏ��P�d����5�B��s��`5;tUD��uUnwF]�@M_�\$Y�Ļ�;&%&;Gk��<jS�5ū����jT���j��)��ߋ��=��:�ӳ7��;������-j��!��&;J��%�0�4�5ݫ���[~G�Ȭ:?���;j�D��%�nڅ[%���J�Ka�lCm�*x+e�o=�s���D���wQ�_��+,����B�����
w�W0�{6�n�����U�=嶠w�-ۺ���渡�j�WM����z���M���zr[-jԭuL�I�J+���-� �%�ϴ*� �4]h{��PU"B"�XO��T Ue7�,>C��d����@�3pR ���d�R��D��Gƙl>�{��Db�������Ƨn���mj��������u6_M��b7%�}�u�V�#����ԺwLM�}ݺ�OM�c���̾�
˶@Gk�+0m�P��#�0/0��5:+�22����q��8JTP�S̃4j�fސňA`D/0��lok�'����ƆP`%V�� VBF%{���&���ȫ"f�١����^�^�j*�<ؑJu\3;��DMҠ���`.~��b�n.A�t���y���� i�=�喴t��3
T��6�Xa��+�.�	 �@g֙ˌ��~���Me�$�(f�����,o=�'?q������mS���ѣ�o���5��u�s'ޅ{�w�Փt����:(e:����x7]���F���uu�Q�vWW��wE��j�"��ں�����������0]%^5FY��⭌��ٸ�8vA�_�<b����y8�FS]���p<�䥿�_ �a֌(��Õ<"x�%�v
d��_�K��}���_�*95��}?ɚ�O��|Q1���J=�+������Oڼ-�Ri�5U8%��S�T�5l>�~,��$f1�2�Ӫ%��'�r��b}�7g���M��~���9�u���Ǉy�d�v6q��4:Ţ��^�y?1�,<N�ӗŬM��s�E��=����ȯ���d>5��}�2r=��$�|�>���Q|�عc;ׯ���x#��Y恷H��E�2^��)�7�~�|>}�Ǜ�����v�����+ka��m��Pu05!!�P=9`|^��[�)p�ȕ��/�!�:��W*,��&à�����M�_5����Đ�k��FO��f�!��?gx,� �ټ�C����f������d�b�K�7E����r	�|.��O&!l��=���b���#/Y�$L�Ub�B�b��u||�d�	��K���(0�����T�����.(���d���IRmf�!�c��WEL?΢X�^様�oR�D˫r�(�<&Z^�[�c|�䤖��֏_%�j��2�f��s-d��)��fjY�-��"�7���'c�N9�<^��^�K�>Fݬ���\���{O�K��$�]�WAhn>����}4����8���d�|���3��7��v�z�_�>E���A��T8�Jڝ�_�El��aV�Q�Z_�To�̲�wLj��j;����_	�o����r�o.�t��k�rz%���C`��ոܟ�r�#�*�ӣTO�*\.OQk!��f�9�˨�֗Ki�r^�O��Z������=���X{����|��Hj0(Yrb���V�Jڿ��������w����>|(u�.����<]؋    �-E_<� �    ��h    ����o��I          x�c`d``��o>kܿ����@��	 �.+xڕ�]1���m۶m۶m�f/Z4�S4�f���83�<�Q׼SI����П��T�na.\\�sq�k�E�~R��X�L������cư��Ta��j`_��eV��	�����[�gw��Pa{�8^q�C���U�2�])�6�9�߈觢vk�(���Ğ�hn(g����33U��#o���*�����۱|�I�O�٢�MT�MP��&��;n�� ����ϔ�n".��6U�����]���om�.`n�h俲��L9{
Dͽ;*��_�W4E!dGnT@��`&+�#c,O�̱:+R!���n���~����J|�%��<�d�(ܷ���Q۞�F�Y���f�z!�٨�Hf&�?��ݪ�Nr�$b��׾lUE>SLU���, �&���9�	2#Ҡ�m�G�|L�H��¨��*�ۭ����ڹ�f�+�+��'y�te�����q�P�-;$�	{Q	g�SQ]��ymP���Q�X����S������১�g{�� x����0  ��L�E�m�ٶm۶m�~8۶m۶m�  ҂:���1`X ր�Tj��:Gݢ%ڥ	���C7�OП�D�L� ӄ�Ĭa�<k����lv4;��Ϯ� W���]�]�����FB;��0L�$l���[,.vGH��JD� �JHUdG!��o(e�5�_UV=5Iͨ��", {�Ip\7�}�fky���퓞G/��ҧ���U�6�3���&1���V?[���s��>����a��NCg�3ٙ�s�9�ܖnWw�;��"�o�������H�����}K�����(*�j�������hڏΡ�����pz�����0�
�¯N�`bq�[`�����R�$aǄ���$B��|�&�B&�d9A�����$���d�����Ԅ/*�+�QQQiT9�rI只���j��t�5��T�٩%���]Q�֨�4�L���U�͡�=C{��~�:L:Z:a:�:t�������ۢ�D�COPOJOYOG�T�N�]/@�\o��J�Sz?�m���'�o��  p���       L  O   $ N �   �  xڍ�CZPFOvkh�m�I�8�����zZA��d�zu�̈́������g\C��	����v^��׿�4�U���I=�N&j�Y'M�*9���P��1FeJt�tݣH��nZv̅,/: �3s�;Y��QˡO��`������b��%C\�L�n��P炢� )��������[6����k:	$LQ|�>�}#�)��c߱)���o�:#HN/*O���4Y�2���#�R"�O�Q�3�qz�{�K՞jf�J7*/��ȋ�iհ�>[��=LXk  x�l�Ct  E��ccbۜ�Im۶m��m�<5�U�e�ν�Р�F!�3�����b��D�'�?D2��<�QDC,/y�k�G<	$bd$I���x�f�p�d
I!�4��N��h>�I����\�ќ1�c<c���҂�L�i�d�2�)��-�h�g�3�Y2gdAG:љ.̦+s��|沏��Et�=YM/z���,a}�K?��������=Yˆ]��W�p�O���_��N�r����,�\�*7��C��|�+?�+@�
R07���*�q�N`����Vb���-vd0y�P�s��<���e�p������`�0�+B�,���b9;�Ŕp�@�`+C8H�`E֬bkY�:�*Vq�╠D��d�(UiJWh�z��|�2��l�(Wy�W�j����a Xn�9�/�|��!{�(��>Q��[y7t�i����t�,��J�� {=��M>^�p׏�����:K�\idf �	����{�@gF�H��ŉ��y����\�g���'q=��g�a$1fɧ�7�X�H�����Q!npJ���m8B��2f��6����(n8}�b�EX&�8,��O�L�p���{�7�u��E�[�݃����H�r�F����q��D�'����#�FX)6	����g8�E��1�+vb���M��HR[ޛ(H��PRI���E}[t0�[��Jm[�������,�>[  x�b���pГh DQ>3�ݝj�)$R�	D�	I%l �z� �YvD*�ެ����w�⸊�UAa�5��30��m�O��T�+y���L����P���I�����e�/+	��֮H��E4c�B�mZ�/
�^UL��M� ����`���ɾۢ2Q�}�}���>�ƅ�Ou�0W                                                                                                                                                                                                                                                                                                                                  re 'install' to 'D:\Program Files (x86)\Steam\config\config.vdf'
[2023-03-03 23:36:43] Flushed store 'install' to 'D:\Program Files (x86)\Steam\config\config.vdf'
[2023-03-03 23:37:12] Flushed store 'userlocal' to 'D:\Program Files (x86)\Steam\userdata\124618114\config\localconfig.vdf'
[2023-03-03 23:37:12] Flushed store 'install' to 'D:\Program Files (x86)\Steam\config\config.vdf'
[2023-03-03 23:49:51] Flushed store 'userlocal' to 'D:\Program Files (x86)\Steam\userdata\124618114\config\localconfig.vdf'
[2023-03-04 00:04:25] Flushed store 'userlocal' to 'D:\Program Files (x86)\Steam\userdata\124618114\config\localconfig.vdf'
                                                                                                                                                                                                                                                                                                                                                                                                 U�d���R+�5H
g<TMRֿ��:-��]�BW�X&ܗ�1&e�74i�D9:�������5��&&
pI�K�Ƈ�����
��������F�����GTJ���5�e�d���                                                                                                                                                                                                                                                                                                                                                                                                           ��������������ȩЩة����� ���� �(�0�8�@�H�P�X�`�h�p�x�������������������ȪЪت����� ���� �(�0�8�@�H�P�X�`�h�p�x�����������������@�H�P���ȬЬج����� ���� �(�0�8�@�H�P�X�`�h�p�x�������������������ȭЭح����� ���� �(�0�8�@�H�P�X�`�h�p�x�������������������ȮЮخ����� ���� �(�0�8�@�H�P�X�`�h�p�x�������������������ȯЯد�����   ��   ���� �(�0�8�@�H�P�X�`�h�p�x�������������������ȠРؠ����� ���� �(�0�8�@�H�P�X�`�h�p�x�������������������ȡСء����� ���� �(�0�8�@�H�P�X�`�h�p�x�������������������ȢТآ����� ���� �(�0�8�@�H�P�X�`�h�p�������������������ȣУأ����� ���� �(�0�8�@�H�P�X�`�h�p�x�������������������ȤФؤ����� ���� �(�0�8�@�H�P�X�`�h�p�x�������������������ȥХإ����� ���� �(�0�8�@�H�P�X�`�h�p�x�������������������ȦЦئ����� ���� �(�0�8�@�H�P�X�`�h�p�x�������������������ȧЧا����� ���� �(�0�8�@�H�P�X�`�h�p�x�������������������ȨШب����� ���� �(�0�8�@�H�P�X�`�h�p�x�������������������ȩЩة����� ���� �(�0�@�H�P�X�`�h�p���������������ȪЪت����� ���� �(�0�8�@�H�P�X�`�h�p�x�������������������ȫЫ����� ���� �(�0�8�@�H�P�X�`�h�p�x�������������������ȬЬج����� ���� �(�0�8�@�H�P�X�`�h�