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
                                                                                                                                                                                                                                                                                                                                                        [ÎfáÀ<®}ÉŸ7ğ÷ì»Qœ¦»jğ˜•½³}ã±Ë½¡+²S…l¹p/nk^-6æ]&| MB¡*O³~Ô¡nÁ‡àfî°ˆd'jU¢Æìúıo‘Ùäåë}-§Â—ëZ`NgÒWxHü¿!1§ÃpåÛe³·kŞşŸeô.Y–1ûçÿu’õ™ŠÏ_ÑÂºûmŠToúC™P7ìƒCÂQeä–MX% PÊò{ŸIãBnPÖÙvĞ/ı‚ëÄÒUÒur!Z\s¯\eTO^O4–ÙåQ¯,2¿¤–Ùÿ…ã(
Å7gíqÔ¬`ò¹²Ëú&}ñxtvIô»t­ÿ±Ä¨Ö›`Ìy ·\	šNëùFŒR^ahâÄÁXµ³Á]÷Xÿ
«'ü‚&Ñ.o‚>âöCÇñ­¶¯P7^Ö•¢¨ø7X¾ñ5ŒuL×u=p…Î‰À¦‡ş”-K9Ã ÙBŒ ×È¿Nl”Hªp0sŸˆ{ËüŞ(mµÅqm‚CÚà¯üOw³=JŸOÄ5àï+¿>Bêr ñ(’ÿ€0M¥­_9"(fsß6+øÎ HH+•C¶gä,N2k÷"@²J¢¢•oÖË·ã;ºëKµî„%i›;LDĞãA0{á³’S»OUl‰j­wLE§—ÃE?’8úÖ	ïƒ³ÙNh\Î»7»×ğwæC¼BÛôC®V>†ræ»üÚ½L¡ßdh<fù„Š=“$b©U>O1õB	°lˆµ…€¾=ÎËä%Õ¸¹¬yñS=EÈn)g|ÄÀX¤‰qâÉÄá¸|ŸÆ$Váˆ6 †µŸW2v%185ba}oáT†WS{¿
‚ù´¥RE’JËÎwO İŠ
º§AvÏ=Ã¶%Q,í¯vz5ÓvUÑæt¯c-x1ß]>Ë<):S°òı/‹Ù¿^ë©•GOË(É%H›`‹ï‰;¦=»Ú'—…³î-[ÉŞ“÷,m¬ef(1¯!Ñ„ƒf,gxcë#WSÊ&C )™Ó½òU6[¥VÖ“õÊa†}+5š7L)O y¹õM–J!`JŸêÌ7>8M,‚h–hê^O¤áçÎ–Óp;ÏÎÕ¹‡j??¤…ÎâÓ6íßLk¥÷å»7‹H.õ,ŞBè7IËÅbGfZˆºÀ5_Wf?yS§¦o1ã09p®­aµ%Çª+ŒÈßüÖÉ°³5Ë,éº¤âÚ¡» P'>Ğ0q¦ÿ¤~=2scë9#F+d0Ÿ=iw6˜gå™¼Œ(úcÜ¿ˆS ‹¼‹,à7±WôT%ê¬Ã[b”[; ±¼í•NMá
Ûõôxú”ÄJÏ¢ˆâÜôDî#Ç–ê¤vR)<ØSÛÎ’…íÑh¯×JK#"j½“ÑS©L´^HìW;fãºV–lˆ`¿â%ßÄÌ´a1pŒ[Jvãh-™Å¦}I`y(²i ¬«¶g×y6ş¢^'>ĞõXà®äêúîa}J%ù;ğƒCÓåœó!iaÄŠ5`_B±2}k%]OêÃáñú›ætn‘¸ƒ³üâ[/ü•ø|ş°|¤ê)Vvó³"	«ì#-« GAù…E-˜Qoâ€6œP–V‹Èvş1ƒ3«]µÛ(ı	Tün—L4vY[M(Jxº.+è•06˜êâ;YúğËmÔ¸YºuäÑ_QI~œr©naAnxæ”Å/ğæ°ñŠgy¹ÎŠe8ÌùË‘U{@gyõV[‹¸1_xº.	ësÉÜØ¬¶:(QZ”ZªDó*}©õ&¸ş<6©—pâ-ÆrÃ›÷Kã-*öÒ	ÎáAôqcÙxD%µ¢ôŒNoş–Â(ÊFŒa"Çå6K4IÚ4×ê–Å¿-ä>ğXp60ïãŸš½xœ•p>èRÏ¹ŸŸ—è´Öt‰*¦§_@ØöÔ{ å['t^ã×ŸÍØÌYğ€ Ãáã+?.Îúñ€ÕÄÆ¤ì3Åï""&O¢,gIíÔÛJˆ‹W® šÍrÊ7Q¯1nåeá}?¿£íƒ8Y–ğä{à¦G9’©Ô¤Ád"Ûc„º–“ÕÜYYğíAÎT«sf³·×äxà£5_d¤=ûŒ"ş(,©FE-¿­¸ó¬¯ËuM×ºR`²Ò)³ûw‰ât"ï¹¸°±‡«8¦ºq‡ä'ó^‘ùı™$±\ u	¼0•‹ğ÷0}[/–¦o}ãé³Í}3˜8èŠ–¤ìúôİ©.%aåA]ÒÊ².;â@zìÇ\{ã'6ôÃˆœÑDyñš¿Ã³f‹Mlâ`Æât¾Š¨]vFõôÅ¨¦tx¯üSºÍÌAc>8Ë=Ò¦múìTï*¼¦Äù:öTtÀrO‹òš¢‹şWÄoò·WäŒp"å.2áôVTà#,LÄa3YûóÌïôzB{5+Û&–ÇY†pÁë'¢,„úßÒ¡‹Ä9†¡ğWºÒ~É¢@Å]õißê¿¥MÃc©d>İ'Hígè=2Hëœ…‡jÁ€:ÔÑè!Éö!cCFOˆÃıu$|ù?€Át«ÊêeçˆùãĞgÀ½•KR{rYÀNùŸ>Ñåc-‚V+ET¹ÇÃaú=ãĞÏ?½Ó*è®e0G¢Nº
£3^¯·ÛñTAµi£y• Än>4¼w‘/|}Z¼5µ•ÑĞ:d]j#ıÃà]t~@ÄŠ¬C§okÛ¸øğÖµ•`Ú·‡&âS“,“öûœí¥sàB¬73…%÷0
Wë&3¾“¹QøìŒˆD.fóZ4{=šÕ-î»4 vV›OÏ%\YÃ©:äéÂFÌbí‘rhQĞÇC¥¥|Ö˜×rÎsh2N~ZıQò³:D¾Boªrj]îÜEG‰ğøogµÉHÂ)oÅVœ9à-üÇ;Úum?n¡ëL±cåj‡¢Ô,…ıùêcX©õ×˜ Â’Üïƒ**ÉbĞ»uA }®0L€¦òeË8Éİ’úw{3|k‰«Î'#(¦ÚÖ_L“VŒ¨çŞÃ¥/c>E¿R,2s"r"<`,ê%<ßlJî	læ†nIøÔ˜­ßìı(²%Ó(s$€$šv7ŠØ*^Zil,Ágö0²Ø°¼¦[»€´rÏuÍ¦==\µ%Ç3ÓÎÁPÙÌ6K¨-ò ,H*~¯íì+­jhBôÜ²º ší â\Ç6D_³˜÷ÊÂÃÃÓÖ]é68íœå|¡„†[Û)ãB¶ZÏ‡2?±$Êu^©FI¹°59ìûN°üÈ´ú:sXÁ çÍäÂùnR¸ñÑRËÑÉ„~Øj4YäÄ9SÛ7µãj¦fãFîèÅøäõ0xµ¼Ò½h=œ”{9“0®¬±²4©ï/E«"nÉ±ÃÑäˆ«÷G‚*%ôÔXâ‚–”_Iúªş|<×?eË5smÃ8Ç½’6J C³šš:Æ¶vÉ›ûa.¬¢vÎ§´’Hˆtî>¡wöOüëFdf†¢P†ô¹.,KâÛÚUê°5Ã³%ƒ<Ş‡àx.–&r¥ÑIÖµšM çPÍÔ{;áçkfĞ…ÅÊ>‡ÛÈ¦ÈàK‹”q»Sú$Jš²ˆ`x±¨¥=K{eS®© ‹$llÑ_r¯ cÊ¯öÔ*%ŠØ»¢Üš¡?/å á#tnöä€*-«|Óæ9MÍù¥[L„x÷eâél`ï,İÄ´€9ˆä“îƒÈ]ıë9¹WPå3˜#µéWEÃL‹1!o€íîI¶YaÇ>Ø±@éèŠ8©	§ZUû‡thBÉ÷I6êkçóİ¿cTXif#§86©Á¹ñE7î*¸cÆ7ŠéšÂû‹éÛâ9—ı±_#ÊšÓêAp´ÈÉàÁäÁİrZ_•–@îånZş/X&ó¨Áigq?4°ùÙ9¼3Ù9YoÓ&&Èi¯¨gª)\7O.ÅM]¶M8 *†ù®:?7¡Fˆ#á3m¯Sû÷{ê@©ÏÌ=µòµ\Ø1ùF?uš¹îk¤.O«Ó+§âÄ÷¯RÑ-ó-k~ŠhUÜkÔâ€KçHŠš]”4jxµ“ûîbÄRü0 3ószœ¿UH™ä?+{o!l7•8ÄkIë&{«ZêãÖR¯£÷Š°ò-q üô™d”ã)Ò.Y	Bá< %°Ì)¸üIBôuù‚ĞàÔIóqŒx5¬CÜÆo’€'÷'iÆ<şÜOÁ¹ÿÖ¨†ç#D‹å8„Úqûöß"]…Ğ¦˜Ìä!E¸ÉL\ÌSÌ‡Î7ñÖ÷ÿA3^œÏCàD¦òwr"=Ñ'¥ÿf¯ñœ\µŠßÉ9è»%„!âoÊ _q¼)_‘+ñz¿ÑiM½FŒøWd! m¤KëşË±ïËj´Í":e½|5Ù*½7lößi/BÏ¤û¬ğÒ¦úÛ@’?Tà6æjt›%çË“/úô»@KÑÿ’7İ–ŸÌ“…“[.|]ŠZdaÚ¾‡#	Ÿ}ô:ßƒ¬5h‹šFj@¸	ÔÆŸŒ¬°ê<¶Â“³¥œm±¥çÓ	QJ‹´Ñ¹×ÈM’wOFF     RÀ     Ÿ¼                      GDEF  €   S   rcGPOS  Ô  ×  ´ÙÇíGSUB  
¬  j  Äo£SuOS/2     O   `c©cmap  h  °  ‚H£fÑcvt      X   ¤Šhfpgm  p  º  b/gasp  ,         glyf  4  4\  _`z^Ç*head  I   6   6sœhhea  IÈ       $®vhmtx  Iè  ÿ  6[aloca  Kè    &‰˜pÈmaxp  N        aùname  N(  
  00 KGpost  O4  Ò  Ôcâıprep  R   ¶   ÖÅ˜xÚ-ÅÂ †áçû…!	Ğ‚t†t‚@v€Ùf§€ÍÌãõŠrA‹ŸÒˆ‡xz‰·øú‹N/†MŒfeÙD£\İDMâ$ißEá¼VHö	g xÚ$ÇƒN †á÷Ë5åš×fdÛ¶mÛ]DC¶İ‡|W§½{~ À‡bpÍÈ*¨ °}vl€ÀÖ‰!qp8 şÎ±!¼ apÃ/ã£_Dn@(CZ×:<ËMMZ·V­uíêXÿ]ê^–óC„EeTPIÕÔPKK¬°ÍgœsÅO¼ğÆŸ|!jñÇLÂ(5e¦ÂTš*SmjL½i0½fĞ¬˜[à™d^ÿX˜„¢(
€/¸8! ¡ ¢‡¨ÚUƒ0 ÃÆÁRK-µÔRK-µÔRK-µÔRËnİ×ÕtZO6¦˜bŠ)¦˜bŠ)¦˜bŠ)¦ü§³é¶l¼Öe½ùğeoó˜Ç<æ1yÌcó˜Ç<æñ>~¼XUp#G}9ÒeMQNaffæ¯03ıæ?øffN~ÃÌœ¸tÌ'×±¡l9«¨,¹VQ9òËæÕl—v%…“Vy¦gv¦{æ5ÌŒ¹*,/D&,_äWaù(sBï2•§¨ÜS6¹Ó€MÖl6G`üÄ¼«ƒ?W¬ã×Á8ö±À1ÖÔªª4ßô)É¬ØœqW4Õ3ñ‡ÚGc_†\]L®o Ó.N°pL½ßœp+ı^‘æARÙ‡MÂ7Òú¿']]3Ôª¬XsHyuÈ¥`$<º l…íÕ1mÉD2;u²Æ eµ#üSŸIVĞóáGßÖ·İXa–Ydï"Ã9¸ Î/^’®€Gø¶Uï<“ñ*À –ñoSG£·4DÇ^Ï"ÒòŸ•n¦áŠŒìS T3÷`!·«Æï…äÄn«ÈMjHî„œpzcşÌŸcöòãóÄ™l¾½¸à»Ìò}¾ğ#¾…G¹„}!7‹Ÿa*´g~…]4iW€óù{¹šïà@—vD‡b–ğ	šï†ç)VCnŒK¬0°ÚbÓ%ñG·£®x†QâúÎz=\Éåfe—AVaeêã:7#à"€İu$­Ë
Ås µ­$ç{‡¿E`llRnCtOòk!ŸQ3äøÎ¿…¾EvÁ€"Z¤^`Óhµz¶¥xş„m8 Ê1Û¸øÙÅùmZØ²c; ØE'7±w–œüĞ¶-àbæ8£™O *İ¬Âc>:¸’E®ç$$-Òa_k"b@_k_…ó1!§i‘œì>µÆÕàJ€÷²Ìït¦Ü€N>)0Ømq½;}f±§Úïšî÷ÎÑóŸºõy¦}jÔvÖ›êøÎßxVÕ—aíhEÒêâ÷]¿İaÙÅQöğ«ëäHs:÷v×¤Í¿YrßÅÌE½¢”ìfŠœ•ÿrîûc™fÿ@-b-™g'æa:ëGëe– 8dòE~rğÂ²bj‘Äa']x4zGXÖJÁ>Ù7#}>Wb
?ƒÇ•ìøÀEğ¸(BCmÀc¿"-¶SïG€ù—0ÈÓçrs:e¿gSYâr¹DóáÆ°­ø´òÙÇìåÎÇîNoÚâ·Cú{µv÷§¼çnOÂ»BŸ>G4r\˜€A„Æ/—wá@ç9©¦Ùş;ëYéç<LçJÍèüÃİKÄpVûz`¯Vı¿nÈÿVÕÛÙpóÈ@øAÀ¼PKfÑ™É³8F»À‘"v[dàY!¿Œ°Ÿz3ØÂP?¸q­ãÄC‡qi4§vöjæZ%Çò{Àİg_ŠvÏŠnË°—î‹lÎ^ğ³ç+/=0§]Ä0Ô><‡¢ ªùóÕ2³ìçÇ-oPe‹iÄó™²|o#³Èü({‡œÇe¬¡1p22qû²Øú^'Ín?to—R,‚¶¬ó¤U8$i?kiµG¬ºw0`‘>¿UÎûJ™|«ëÁæsîßšçDş°móõ³–¸UOurªÒ[şN¯ŸÓ»,¶XJ8½RÄ¥|y32»¦›Ü¯ºÌÃœEés¾Zıa¹ƒÎÉ@ÙñYì·¦İ-æØMŸ½Ìá@®”œ¼õ'9‡wË}œÖ9é«Ü]•ìââé,÷Û]4`R\¼q5ş€ìeóf¦í*·Š£Í@öÜ8Ã©÷õç‰¶®Ì²$Z¿RQÌXØc($óó8Å³Ê~æŠ;Tü	òŸ¢Î´¼Ğw#z‡¸;iK²—İO÷úXÂ¬y£%ÙÇ8Q£Õ¶5kLe­Q¾»ÿŒW£52Y É[Òì†IùìVŞ¸K£7ÃÿNf=»¹—÷fº%m¦õ/ƒçlµY]Ö,r%.AË|LQæEï»Øİr,‰,‹Hó»˜¥{ÑŠvø£[,+‘Ç³l^Yá$æ9B_öpkà(ì-À@Z3ÆfÈÄV¸©[{‹¸¿šULE;ÀWCŞ·7h91§GšÆİ¾›RC.‹ò#V„İLsR_ÒôYäR•ó:­eö±Ænéî[¿şvì¨è/³vÎĞç2–ÄM°Œ©ü‰yo;¿‡ÇÀ!“JiöR–°ó±ØK¡Ób`
6ÂØÀş81¬OÆÙ8ç†¿3q)®ÃY¸	7ã
ÜŠÛpÆ¸¯âƒ°õ>ÆCø4ü=¢ÿ‡?Š,zğ†Ãß‹ÈãG¼ôk«r[1Qô?333•Û-wqfY–ËÜ9Û<p'	©W"VtÄ“È¹£MˆC‹%uBÃ{!â–í IÖéÉ5yéÓÂ!& !hÇÎğy›U|#x!6sO…Ê¸‘µZ(aB•0•ï&ı	º².gœØ±¥(—À†`lºhvÑêkÎÌôP~+B´eÂ9C	Å“„ø5|ñ_zu2~qó‚{] *dœšœÈKŸè•×ZêPÒ„Nì™à½¢—fæ´K¤œ¬LÔB€–ø„ôä?²¶»*#©²eÃ’E9 3ö@—6EµÙVÕhXC¦›ò³¤épdÅœ@¦ìhŠÿ2'‡ƒ xÚdÏ¨_qÀñÏùk¶mÛ¶mÛ¶­4å¶<gùe¿´Ì1×¼åév»O?ó=¨ë¢òÓgÎ_®ù¶3G÷k¾ËñƒZk‹–òÓ¦,ï¬çÂùÓ;½|é¼Îæòç"BN^­D/ez¡)Ö %µÔŞ¶eÿqC·<tÀèG·l3qÿ][ÌŞhÛ~“wyò®=xâÀQ› ”H*×V¡~š™Šëj¤ËÊ”ùáµ·~Dı˜ûooQå©m¡¤(„¥
© ¡Öºê/\’Ó>‘ÆgÒe9ké|&µÒÚRË•SWwy¡¹05ı%$¬Ï,½3KA«ôVJØ³J¬·º•=ş|Â«ÊLs9áš[d½tM*6¦"PWO2Ş7åõä„ÛîP¥Ò¯„ßuµSŞ6ùsJê» g©‹rÖaQV½£|:ëÌô§¡¼œæšCh$''Ä`ã¬2WC!ü]. Â«A¢  xÚc`aJ`V``e``êbŠ```ğ†ĞŒq†ŒjH A$ãûùû¹2``Peşüo>‹£»ãtãk¦ƒ@J ğj- xÚmËC [  ÀÙÍ¶õ²móÛ¶mÛ?Û]³mÛ¶µÙÆ-_âÜ1R §"ä”2şu)åE&•Õõ·kQÕè…xV¼$Şï
éB†3ä…C±P*TõB‹ğcU´X±ìÅr]»2	ª¨§ßù|<ó™#äoÍj¡îİ3Êf)^€kÅàjÃ«å®–M–'Ë’1tOÚ&s“É{I•¤ÂŞÕ{VîY"5ğ¬mD½¢>îuŠºy°uÆXc²­:n­éf˜d—ænŠ‘FÙ/±Ï8ÇDÒË “,rÊ%·<
*¤°"‚bÊ© ¢Jª¨ª–Úê¨«¾Ù¦šã°…Q¤¡‡<ìQ{Æ³ó¼×¼áMoyÛ»>õ¹/|ékßøÑO~ö‹ßÍtÁ,­7ÖfÛm±ÃE'#NùÏ
‡Ìs:Jí¬Å8aB”Fÿ[i¨!†/µXJi¥’F:åU6ÙW>ùeVT)Å•PFI{”VS5ÕÕPOy_iª‘Æšk¢™ñ´'<é)¯zÑK^ö˜w|è=ïûØöúÈ¾õïıê3¿)ë—]²Ìr‹,±Ôâë¹Wˆ§xÚc Ì ÂB†B¦ƒŒ¯şû0ùÿ•ñëÿ¯ÿ}$ é4Hîÿw œ…¬é TçÁÿ_l^G†b gg\Ô±úÿo(/ÅtŒQ ­1+ıxÚ¬V×vÛF]°©W¸¼¦¢Zî*®XŒ»ê9@* R~OüÈÏèş–OË@ª—«Íbú½3…[¾ ¼ø"Æ×^`iãGï˜8„o!Şò1W¾ŠAÑnËmÓ²P(´\é
CèĞuĞPá[sJZÒr0¯ ó>_©
WcYCºI®¢İ¤×˜Ó›ï G%tÔÁÂê»n.—£4hí\²XÛ¯î% £t»e£L6‰bÕß	º5ƒ=,(ÌÛXÕ>×ÃšÖ=:€­baæÇî¬1¦½¶‡%Ï·0_ÖòÉÙŒ}ÀÕUR=!o\àÓB@’ySG³¤ê=Î³}<)«ÄF¯ú!i€mÃ|ºÇ§{¡A`[8ªÛ(Ö}/ØÙ¢gó^áÓ•Ñ—IÑf/E±(@Ã‚‚ :„GºƒEÔA¡¦½êã€tqPº4
	,¥tĞI¶]`#Ã5³öù/C¯Å9‹Œbˆ©V2_¬Ck~¸jFë/+ |²á“Í$^ú­88 pHÛ]‘ËÆ<HÒ•€Bºæ¶ß¢Ñ¦Fp`ÎÁ!Üí8Á*ˆmàø$Ø%\I»Vİ¡q¡=wÎÚ]œup‘F³,†-IOĞCğbA§G¶0y &>Ù%ŒF+£•¬ÄØ	áx¢E4®R@ïÇFEŞ£*¦´‚9Zâ	•ärv¢'¹à„~Î	è@ÂI~Z§§Ét^SŠN))@´©2NéâpŠhspZ½Øô“Bg%¸c;òƒeõbÍ±‘)M‹ôåT_Q‰˜Ö[~2=­Ñˆ\œ²ùÊÑj¹Éÿ™¤?hÔ$’U?aú¯ÇÀe'ç,Iaı³™Ù)„~SM@HZÔ‹´‡uÂ!Ê’øÒ(uÃH§UU"9oÓÇié‚‡ãÒÅ1I+çBøùüyCL‰²p]—¨Íˆ’Ê Úæ5¢«F«¶ƒçTb°<O|³¼ ’<Ë‹*)°4URdyI%%–—U2ÀòŠJY^UÉK[É>ÿX
‰i	4~áÛâ Úg¬íÏŒÎ>ãÌ®ñÌJà„}"Nõ1ƒÊ8÷ã³P_×KIøX^'|,ë„åácùác9KøX~OøXÎ>–Ò…½¡¨ìù´¤GÍ#%‰ŞÙy…7l¼A÷ñ¦hÁ	Ó”Ñ‚äû©&£¿Õq2^òxãğæ\R4ª?¤(ogôœêsGÁİ´ó»Jô|¼£5Ñ°©—ãõ¢öAğ×Ê#¹Ü1ªŒõñ­úG¡£ï«Æ¹.œåŠ†n“û"HÔêĞ€
É·åY·dKFàohH·»`Õ
1¼¤¨-<Gn…:ı¤nÉ¨pqDÛ;qC<ˆ)çòA7hdù°$İ¾7`Èï”'kşûÁ|_˜)^\Wkˆe!›!–ôáëÑ“ıW*è°#±¨£™:2éò›îpL$¼gd“f,©B“p’H«„p\ÉU$YB’X¤…+ÉJ¹‰:7‘§¿½7é^-Z„}.€´Å™òÑô°o"Ì©½)[\”§ø¨oKÁdL£Øôğ@Z&iúJJº;
,ÕééÙşÏ.ÙÛöŞ´$¯üã}èş¸Bş€srÄO”„³ØÄsÚ_5×dŞ¨Ğ½}zÀºn®°ºÇÆ¡.Ù§\Q¸lÇÔï:Ñ•ÚÀyŠğrºŸ3óJ7ƒÎ*éú4äB/S%Ã…ºK!ÿe¥[ßj‹SDèHzUíÛ+èõÙR‰X²û¬ü@OË¶%{¼È…ƒ<#
ªÙµï
¾áåŞ£[şüıJgTÊxŸÎ/.’xÅ,zD74cõÙz­x¡ñïë"«( è/\–À”Í
pw×àÜ5¸ûbb¤^U“Ùı}^»)G¢\İŠQ…€±('Œ+LĞô(&iÀ˜¦3Q>„Ğ¡šU‰)åÅ§Í©Ä4O'hÎ´HgZ¢3-³Î.ÅŠBÀ*ëk
ë4½Š¤›4`‹õ…NÕ¶JL;¬Ï´«Ó }:ÓéÎt¤Ç¸±pm”iU8Û”§tµëè,ÊJÜœCo.¬‘¸¹Ô™›
K½bäs¤ Ïq=¿ÑåÄÁ-ôàzp¯msayŒ<„?AÏŸuÎ8x¼BŞ´m),ï‘çĞóOèù—ÎßĞƒèÁo”«¶/ÛLYM®¤¨¸Kšô1˜h¯ËT$3Åµ£©ÿ—uı¿wP    ÿÿ xÚtWXY®z¯»Ç€¡FÈ Ù™f–À„Ä˜¸ÛvßÅ%†nŒ!ç¶îK²YwßoäÜİmİİ­¹ê×=B–$èOu½zı%êF—ã	6lP‘Œ gÀ;!²f`×JˆWØY"3®ÊŠ¿Ò¯†Ô²ªÃ‹õ›°JÿÓ|Çç?¨ã* ı¾|F>mà‚hR	Jl L[!s†˜À• NØTUUE•ÆQ‹…l<Î8Î?4WxÎó=Ïö±‘/š®¹†ë¢FGMïüŠ
8 ü®4Cøxö®1ø%iÜ~{ÇüÍ4;?’Æó7Xø ¿ŸXpCÌL6ngÄ¬É®t"¬”€$Ék6&Ë	™xñ¨ôˆ[5ÿ9%ÄMÈò[ŸÄGSğqÓDp±ş‡ëÛCúÏÛqÖğ_ÙÈÀ±=÷c~ı3”8,`ß£HòÀ¥Ğ˜œfG™ár` KLîÎ3‘àZ­•|ŞÕMÏå†leœòíó*6ŸñUãñºú©š¦Æë¦ÕOjÚ"œ×7ØP7sÎ7ú»¼¿ÿòc3fc#ÎiXS¨ÛôÕÍx}K³^V3¹¦”÷û!û¡%év!Ã@ã¬)œåËî©^İ’ôú[ˆÅ¸^B€Y°2˜,Æ‘mÎ†[“n_a~ÃAÊ_¥Fë§N›¯óùmÑ¨V¼…>
7áW<ÜSsóMı‡&M©n¯ëèìì¨k¯2iÈÕv¤§ûH[UtPíèêÚÒËªGG)ŞF¼”g—‘gÅï ìş™ØµSÚbÉH†M«z,6İ¹.ÙØBr“ZÌäp>ğ“Ÿ<0ĞûÌ3½läá[o}äÀ³Ï¶Ş1}Ó™¹B[nø)  8³‚ÎtÁäd•¡,‰ñS¥ %(²r	1ÙAãdoêÿ"|N¿õŸâ*K5¤gÓ·Ğ¿Ûª¯e0îƒ_’Æí§±wŠ]£Ø0)ırì2JRB¢°ÉÀ¡Šö0!µºÿ¥?…•:±2pÇ€şo°”t””T
=I¡¤”™¹TŒäİS)È qYê<ETJJTÅ€ÀdUÚ0ógWBpBQÀX)–ÚN/°D½VRIcGw;·õ–K¿UQW³®aëÖ®­MWc`êw]m7uõm«ĞÂ%»zû¶{¾ß«ŸPœâçÇFn…šbA©“¡Á"3ÎüË9%~èS£¯‹†qñğ°şÑ_ÀàMX¦ÿÓò
ÏŠ>_”ô	¢YJ™¢©s¡Ê8=oô[+Ø#òç7u —‹~÷7ÂWgá.‹ğF~I·[ø£ÆÉYöıpôC¬:˜HÜ‹| °uK03ÿvOHr*ı±Ä´zŠ­[±zøÎı’¼
?Ğs’gñõ{ò“eÉ°ÓÆ9\‚/‰³”³Èñ¨²£HÜ2qD5ãºXö˜şû¸ş[F¤ıãúİúÅ8WÒò]N¾e£(-ÄìeU•b4sà%ö'¾K xDÄU“¬tÙ9çpCñlÈŒO–ÆÄ§z,±Ç
Œlr«K¾ùÆj~b[…÷RBïÒÇ9ú9Vt·	#É³ñ..ªÈ“º8*AuÑù¸îâ~ızòÙ„”n<¡Ï¶*ö61ÉÎ°&Ùn HÔ×kT_nÂ
³šüÀ¹UE22–Õ›½€ÀòÍ®5ÁïÍªAª7º•1õ#FŠ3G‰(#¼pÓáææC›7Ó×Ã›[6nhmŞ¸ÑÕ~¤›šsÛ‘®î£íßìêèî¤ØEŒBea«k] 0îƒ_’Æí§±w¤ñÇ	—Óx>^˜e$ç‘½à€¿K¹É?ìZvOŒ8IæR§É Ès(L–a­{Õ`cœÙú¨H²¬ô(ô¤Ÿ ²_gÚ«AŠ@CÚïñ»órs\N‡İ&C>æ;‰İPØ 7iøe”ı°·oàÇ?~€-ÒïÃıg¸ò‹“mmÏ<rë­‹p O>ó¬5×Ğœ Be²8 ãØ	’”îÔ–ÆTšmdæŒ„G 3Û4¯ššnswí¼ù‚ÁÁØÈ®-[vë¸sË&ëŒ¯‹)“A–P¦3€q‰uŠc´z^cÔI$¤ÚL9{ã§4|›qÒmíİ{éİMG]€ÊæÍ¦Š#BÅ>ÊÔš¤³ árÕ©µld-	åBzÙ°ó¹i±?Gëdºµ‹jµ5,KÌ6ŸßYzfË7O˜¹é%éµg×”.×Ÿ´dı¯ÛbØv´ËTöù·VäÅ÷` ¥n‹§;ˆ'A<9ÅLÂåÖ^İ	†‚LªÒËu‘ß[@Ö-"®ÌœX1i1Tšš›5ËêÃı·ïqW•.ÿÁ¾}?`#ıç­Ú\?ë÷Ï½ú[xğk¡Š”¾‘ø‹ûš9H‰3@¾<C![Œ‰69f_cÀ‘ñÍİ×‚EŞ»QŒZûZ,áó™e,–­…~¿è8ï¬Ëj«'Ì]ÜT•è{bñÁs–UTŸ¹£iAÅÔ¾‹º*"{BÑêpI8'P~h^GCE´Nm¬$â
T\>wcBÌCºÍCì—ƒ‡–İã¥àóÂ GdLÜ(˜È´šfCÎŒ:PÖƒ¢Xw•mÌ¼ì²{jÉLÇŒÖ1ÓÖ–â%
H\‘6§­OµimMzJ‚Êg~^®‹Š;†1»à*C•¦Õ‹¨¸ilPÉg­éø»+ªBMe5õMóÖ­OßwİğñáÁ‹j«g.Üãjìö{k+ª«wÕTµ^®/ÀÇ¿õıÎÖ©küù_™·v …ì|ñ&J–2°š”ªIóÎ/´ôx5õ­WX×+lÎĞĞOÂ\âù˜èƒ4£8Š–5rsœvY2Z–¥ 1éébâ»‚;®<<¼réU×\ëºöj¼Vï8·ùNÖÏ»úZê¾¦wÑ­k­®¿ÆÃ]cğKÒ¸ı4ö4vö2E¯÷ç˜°™@Å#6€c±J?yüÿ¸^?IÓUÁOi™BP¤5¢ÏÑNçRÌ]€!³–€™ÔÜT¹„8y3>¸Æ9æ|vıM·÷¿åÊ/ö»™ü–â³º_¥Éı'½
P¼'<ÎFÌ÷m§MBiÜ]À.Çc$KCŒûÆ³¹1WÿìÛn}Pÿs¼÷nìÀ¯¼öš~T~ï=òı v•x¿Ñ’gk>vÚÕ<æd:VßÔß~à;îÔßÅÙø?Ö÷ê'‰iÓ›`ºÑÊØ @¨!üL±ËĞP‘â^iÉc|mFwpĞ¾$tW!†¼5¸N¿yõë±·s°_¯;hl5£2Ú?ÁÜ?cæ1(Ær3f;2ú~ˆ@	”Ã¥VOP‘ñ|DD©'d’œî	g ‚"Óœ1b\œ7­°£,“ÈSM!ù²	HRÂ´K{²hb)B$\Z>±|B‘¯0/Çaƒ,qPeøS«RÜœ(¶ØlfÔ9U¹7Õ›=ò‰³köoŠ”O)o­š4aÙ™eı½¸©ÓµÉ˜«bqu}2¨V•u¨ÅîÎmú–Æj:¦×CÅÊÉû˜24KÔ÷§„ñğÏÆàHãÆş“lœ—¦ñÏÇØ¿šÆ¿0pàÎR^&ÃTh‚_™”Tff"ŠŠ\c H”?ÁöŒN%†à”S‰Š:](+(ÿ¿™ï hëÈ=g®MHB 	õŠèB!ŠèÜ1;¶wã´µã-)»›ºqâí›8S¶&/ÙlËËöŞ[òz/ùoËÛjñÏŒîEÈoöwléÂÜ™¹gÎ™ÓÏÜù"ã”P!'E±i4D†B…\©Q§SúAnäŒ£^Ÿ=µÄRÉxWKW¬©¡¹±9	ø<n‡M¦i=Ö—¬MS«lCX<\Àó`…£Fƒ^m,ˆ9Rÿ²sº)yÓAW¤N!uÛ–JçæºÁ¹dæ	mwçæ¾pàP|ø„Óº[vıX“!}éÍí‚²ê`…ï·Ïëé8>hkiÜïğÄbq|ÿ‘#™oµ×Å® m$!ÓÂqÂ[²ØÔÓæåú_Åóò¨b
²«²†€Z-ïw-ªT°5
=—İÄD¶SÎé¶šM¶
“Óì,Ô‹ d–édKÁâ#±†ï0ß¼Ñ|şBùúÆô––=GÚÛÛÛuÁa’/ú"'''ºNŒ÷÷  xiÿ=GûÌ‰tóeñ(%fĞ•ˆú³ÂM‘u×¦Ó×®«ßí¬®ì¬Juw§]•ÕÎİ†®S““';¶9³u¸·g´Ü2g«"~ xR„9Şä¤+ãp²ğe” ½Š¥2%ä?¤U(!¯!‹déJJäÖx5JpG£@«PÂºbÑFnäSâA€tãË…óæÚ†"†¡kİYíÃÊK]$r„È®¯‘ìL7Ô@g:e@	qŒ‡^,fciI±GVõ0½„pĞSã­!ØÜèÖ\F`0¤%‚ÂÀ´&¬Â8Êß3îÑ…tÇáêşÊâVß£¾ã¦›=¹3ğNßî‘;ÏÄ’­ñ¦dÒšïï™M–Ûë‡m£Íº"õn½×œv<¸åöÒñxWW<Ñ)¯CKû©Ğ›N›Pbfø¨Ij6Ë1œÃø2®+¬<ÒduT8Œ%ÑåX®Ë_—ÊÆ’¼.’Sgz»OŒ:›ù¨®¿#¼ÁC¬?ÚÒş•†p=1¶Ğ}ëB°-^QIüïok>Êw˜€PHèYÒ»`µö×òÚ¼ÜşÒı_áíœHôuû”qóÔŒ&i—i4	Í wVŒ&o™Ñ¨+p[2ÖÒ’ :i4KŒX}÷lñºDA}[ı¹sxÃ‰¹y­ê¨$EkŒ'2·dù¢›ÕıïŒ>n¥_VILÍsÜSá:‚7ªò§³İ½ Ñ(ò+foÖßËUt·0ï#\‘€J•İáj½Ó~ºÕl>D^·iYñ|Îˆ+’Uü:¬øsNQœXË·bGÇ¹},­ÜÁÁÁë"áıµçÌ[b={[ZööÄ7™ï½åLSGGS¬½ıN÷l¥ıtk°®ãèÈğÑºàHfâMoÂöÎÎşşÎÎ@h`!]jÓÕ’à=áZ*Şq×Ê w0Ë|JĞJÈbá€xŒmlš>~!hH’Ğ}!ú~æ¯Ñ«moÊü
4Àóìy‘yËÀÊ7E.zW{OD•€“pveûtºL¥¢;ZéÆÔPÓY$§ù¡\`#wÜqéiüyÆ’]ü¾M®%Àù ¾³iŠ½1Ë!p‹ˆi>Oíñí¯ÉíÿLíQÎ!rûKÙöì“ÙO–Û_–ûI´çæy…·[úîRü­ÀM	lü­–áÕPÔ½Šºs(*QëòP„JTU£Ñ¶>”yM_ßw!²c’cìõ?Vëğ—™PÿÖ:5 €ç9ÂœÇA9 8¶Œ¿nÌºzÇŸvÅ¬Õò”_ÊŸMì±~îñ€ˆØ.ï2ÄäU¢¶‰˜Öäù,mØ§üáß<ıØîúÎÏ—à§?ä6
öĞ¼E<^•Í ÷!§s¯¤èí­ZEX¤Ê*00—§ZÍovm½0h°èYĞ0³—i/-­cÒ¢ ¡@ê"ùâ…-OHL
IÊ5BU2d%“äMk8âw‘î"vånN§Kh*¯Ïä3ù}Æ‚‚*ĞÊr¶øÎqRî—öóåã±æa“u*¾iÃW ÚpÁí6àÏƒÑºúšÆıóÄb^òí™ß)WP¸š`7ÁØ§$”–A7	ÅƒÈáTøš¬h÷•Ïƒ^ 4¼Ìô–Ëam;_¹©yj‹`{üùP¸šÃ¤°½ÌÌ!ìü²]¶k¢c	Ø¹ìÉ=>OÍÿÙQäàäb:½89¹Øİ½8¹Âü˜Xèì\˜XiÊš,*øq·¬ƒ «´¿–×şãåö—Öèÿ
o‰Ú¿Hí5`„ ;İYl`‚»6½•1$[³¨Á"ƒnì¢p‹ŒŒœğ¸İ!O(X&®à™eéŠMÃuD(d¶&8¬!mùC£şh=a¡©:<ê¯n L4F+Jo)-kN/6w•ÜR’®éğÌõNŠújº<³½„—¢ßÇ¼ÿ¦ş‡áÑ&ºæVõŸË«º$¯ŠÚ¥ëˆrC3œçÙ\V¢JrÙ¨xtĞD{ÍO#$•Vš-00FáˆBãÌ£Ö+ÆhÒÍ€¹^PP°Æ€é´¯:âó"4ÔEš«›ƒoØ¦À¬ÅTVRD[Äƒ¢Ë,ÁËM(0z”°£‚¸rïx²›¤s—»j{òĞ™Ìºîğ¤—{Uwt»wb¢.©ù±Ôœ™ihÏî«ô©‰‰“Ñp°ö–SÁdKe™[[«ëğı‰DKkægCCCô%ü©1–&œq+şpZ/¡ŒÈTLfÌ*P«Æäv¢0.:D,<%	“ßÉ™ìWö™N›Í&Mv®©ƒ>£o™gå½Âı(ÓŠ5·‘•Ş,ÛìëËÉÓSìCÃÁÚ¬ÓTíû#Ú‘W¢x†=é.Z	Š•‰~U×‚Ïx5ø¸Sw%|Òbæd5 …dca×7eñ«/!0Qö˜ø,ûÇtı|Ãå¼% ÈC?cùè—û¬~SĞÇ—§¾ÒˆËÛr÷X6ÈËãl&#K¬§^òE³>m—öäÖ'Kw|¾˜~Z›Mf¿"±”0=¥7
%+oğ6À›W¶‘MjÈFMJ~Ó’µÇ-–¶ó¾…š‚ÍÇñç›·H7f34ŞMÚ÷‡„å ÷Ÿ/Ë(ÆW^b Áa[ş·®ÿ‡BWúÒV«Øì>ğvÕW‡có]=­{Óñ}ş s£¿6\?ßİwÁiÛkµÛË}&C_¢i(l¯ÚSî¨w·o¬¥ÀŞE>fL‰ôég€ïù	—=h+Á®Ú*•í—ÔBú((µçnsßYÎ(QşJ9Q3ù=d#‚Ïã¬²UbÓ\‘hH´È[$/¾¯á;&‹üFâH(š8½ØdÚ±–y}í]îºyæ¨Ãç²Ãé£s_%¥Ì•%Ö¾&wªÚê5YCİ™>û–Û'{½í§«¤Ì^ji¿†Ç¿—ˆ¦÷³¡ºÒí&Ô AÃˆœLº™óÛÅ“bÂ¯áŞŒLUc‰HDòx”ó^ãqòéâ1îÌ­‹D°cÇ…{î	E‹ªŒå–6Ã·9q÷İ'®®Ö¨Š8­Z‰V&²=°SãS¥"sg§«Š¡B!“$üIaæ©ê¤­r+µŠ[
â9ŞEƒ‚F4jòÁx6’ñ_;§/ŒFîm4ÌÍa0óÜTG–e,½KKY¸Ø+,Ç 0k+ è@|GWéñÚ=~²Üã%¥[kWòç ı×ÓàÏ/ÏQ$WËQÄDVAúÎsºñ¦ûşNÙßÖŒ…Sú€´‹f¹2C‘\#C[™¡øÚ7ß³¸¸oqñ¯Ÿ;<Oó~S™/aWÆ‚ÖÌçó“Ê:iş+3É52‰O$h­”¡(úÁ—Ÿ›{üË?|bqñÒ¯n½õW—ÎŸ§9¿4&V®oÚ¥B–¿ô“óSÙÄ„5!àÅİ;¿M‹¿?óüS?Àòõ_ÚÃ=11—°Ø®•-?äÚ,·s
”|~¾V¾"yµ|…‰'Ê¤˜¥î¿À»~ñÒ¬<±'óÇ‰ÚrV’i >ë7.u±kØËõCÜÿ×mX(Cy¿°%úşb<‘eˆ²rEUüwYBéCÆÕTrÜPÀ´Ú„vÂU{P«å^z”¯1€—ŠR	'-×šË8§ğ@+T<}Ëí2Jù){!ùŒÈ½ÑüˆÇè£øÑ‹“ˆû4hmõ¸îÂge‡^Ä¤£ŒÇh“İ¹…°%ö¾Hñ¦ L
ìlÇ³ rhY(r’Èüğöæ¥.|°„¢}Z`•	ïÇNÒ•à‡»F÷òLP12©‘•Ë™ \ƒ’	âİª”4Õ<ù£†ıJÂgi •Ô*8·¢Ÿó»ñ4İ†à®²ùí~‹¹¬Ô@R
*±²ÀP5‰š0ùr±øâ¾¼Ğ-Í]¶:+Ú*¬•ŞóÛXIÖğV{ËñşD¤!¬«/*ê)6ŞøTÂéî
zĞh*å)z#¾§iİ°òZ`52Ró~Å['M"˜Œ…Ö"+d@ƒ&RÓåÕOîØ18°sç€ÓïwÒß½ip`ãÆÁMCÑp8Dª9¶Dµ˜üÔÿùê¢+ ã:gÀ°æÜ­·ãŸ¶ÖÖ6ú}üñÏ~öñÇŸ=»pæÌÑ£gÎ, q-käõ/<J¡VqˆÙXC iw·õ 7š³Q
2ç(Â¥}¼>õÏGÏß„÷xë¥go„( ‹ĞJ­°)]l@@=2àr#W@€{.OwÊ5(ÀæW6O§ùvµ‚µ<Ì>œ›	¹}!l­Ç2ò÷ÕcÑ-‘¦š7÷>“ê½İ/Öõû¶y¼'ƒ5ó{Ò‹}ï¸	²ó|>XH—[h_›QR9‘IŠq¤^®¥ÌU7¨÷ÿËfQ®–R®¡Ì6 æ×RVZMÆâB±³}èÓ.›=¼*%WÎ°b!Y3Ç>rK´qúĞ'OÏ6µÚ7ÑXs®ÿä9*:î½“ª>{®Ùt¦.²¡÷d(z`:±Éd¼&½û(ÏíÊ'=që´	(Òª),˜h×d«O C‘¡H–Êçt<#÷Ä‘SÖg	Åµ8y{ÏîŞıî÷Ÿ=FYøÄg_şÿàXLô¬*K›‘iT˜3ƒGä9Ğ “4lKÎ]9«rÙ€âŞ+ÏuğzÖåĞ#oc­OTÄ.ûIX;ps¤qÛdWGóh|>›y¾±¿w_kú:CÔ·ØÓZßĞ–Ä'oú ;y wğ`;ç»Z‚³•¨mƒ œH C=J¬ÕJ¶(vQçs5³yi!{ºJ©ÄVz^6rØ¼n{Ğ4—ÒrlhÓåxS±î-–5Â7ƒ·Şûğ½ÉcÕMşÑÿfÙšlßÑÒqh¢.™¬«§dÌg{â«aÿ‚Ëõ¼©)‘ší8Òy=e\Óé¦X§E½\'­áµ¡jÄ\EA¾}$WqfK	È*z_Ë|ƒÊXN]z@·Œ-+ø =]¦aj†2ÜZÌÖÒÏç—.QNEp-wÕåcÊğWV DBşÚ@­«ªÂWé+"‘ˆÖ‚elec=9~Èş›8çÈòíì]Í5ñ[n‹×4wŞy6–LÆb­­±³g;º»‡vwn3•onÙvàÀ¶Ä³y[ÇîCøÑÆêê††êêÆÌwwí¬¯¯o `bÕQQ£â{DL¥È‚å=¢EÙ½QR¹—ƒªÜŞ{æKk•ƒ;APUHÎ|ØÑ.R‹kd>háy+ÿ­¼?>lÙÚ–ÚIûcà¶wŞŞV×ÖVGC8°àr?ojlÍnÏ>vúái¼±«)–NÇšº¸¾_zçñc\ñÈñ•*ÆW*>-Á·†~yvrûöIş©²Û«è£>räğ¡C‡™¢4K*E©!mék>r¹~¼9§_ì+õ‹Vè—¸¬_ZfÇ>¸eİŞ/E¶nÎ|~/ŸÑÆµ~”VÑ˜®Ë×/—ë>sNyh×R1Kíb¹)VŞ`
Ø6×ö¯wùÆšºÎ(«5—m¶Ú{“îNï8¶Àávz¶†ÒıÜ&² ¤råé5u¡—ç–ÍYËP~ôkµ
ùsğ{¥FøLb‰–V_w|ËHs•³-°mj\ï¯òG¶«íÖeæÏ¿ÑÜ`µ·7{b…úæpkßÛe x”ãÿê:Aµ‹ëû*:Ak#¬ùâ¦eğÏ~ñPcãñ=ëÑœyegÏ×_Â§³FÒƒ\Hœ$ùú@jJ=«è…em`_©¨B-XCà&‹™d\ø(>#ó¹İ#NÚ)lEÇ•áÄºêú‰“Ã/¦¼³¯ûº‡F ›#ºUåúÓ=Š(Z`máN€Ù*ÜÎJ¿Ío.+2Ù*°"šÌ§rÙ¦¥eùî–í±éÔú:W°©6^w&·Ù]n›İãQÙß?ÓXïhmS[+Z{&']v‡Ëåppêäâ›°©áÕhB¢+{{–Ësû
y®%yuµ>üiì~û÷nË<	’Ø«Ÿø çæea¾¶ˆæëÍÊçhÄ_¨Ï“Ï|Ío@>[WJˆ‰Õ~gpzhˆ"6¾èÎ	‡Ç]Uåö8HÈy½€º·5Ğ\¨oòÄ;;ã&}as µÑV^nãŸ¿°Y)ÂeµÚø>6ÓjfräµÁ¹2Ö5Eo>MÆ"ƒF•£ li±›[Sß7Ù ˆWÔÂ‰7°eGl›ÏæqÛín·:°Á‘hÓXmœx‡ôíkù…›(Èï‘×eBÎ³¯KZ¸ mPğ°Ï 3è®m¸}‰îŞ©ÜÅÜ]İİÃ¾FwïZå.Ÿy»¸{÷*wËhìQ1ó=«<×ù¹ïZõ¹œ3ß»ÊX'Í¼KŒ=¿TÅØû”»Rî®™îÎˆ»÷ÿÿ‚À¥Ù¥¡™ÿ$Ğ<É ›(­‹Ö¶]øŞİÎ$ûj’C¶ô/ÒÔ#ï‰ãÛéJ°u òıÛW¹s‚€nùş{¯¼(êp,çêq×•¹zMl¼øÀ¢ÿÒqÑ¿ ¬i3"+'Ê”341qŠ¨%á£fÍõ©&ÃÈÅğİõMõÌ&wË>MTh¡&YÎ±Ëa¹Ü`­ZÆÅ‹8úÀ™O8ŒC#Ï'rÑjdÃ+ç”$¶dã“JÅtjeºe8/nËB*fÈàrì¼ìoiµ!x	(~e:!øáSÓ÷?²/]±–?µ
’IZzø¦úÏ5œ½øTfìüG±¢ÀÒüãÕ1”ZC'l¼ø‰úkpäÁ3¿éFÿ ¿-¬KÌ§õEXˆÔ¬X–„6n+HçV¸Å’”UÈ	·,s=û&wŸû™€Ãåp9«è!vDKû ¡ÄÓsÖ„M/aëgÜ!Gy$¸cCcÍÙî}‰Hs½O¥&\Ô×ŒV;£V‡»—¼æèx¬n"j³»8z€a©³wA:İá@•T•Í‚I¨BIuN½c*Û«AÆºÙ„š0f¯,-Q¹Ô.3­‚3¯ùO$bJ´_.ıĞÅi‚,¯÷ªÕÛ\á*K8¸cccôÇî¥—"CÕ£!_¼Ò	l‹ÍÕqÒÉrÕ3´/·ƒÎ´¦Şœ\ÁZ	±§ŸÉòÖzØŠŸÇÏCIö¤5M5ÂßDÈfâ%²Ø—eâQÓ¸¡©~CsÃÆ¦Æ[ë×5Õ­ÑÿÆu|Ş*ÂÇØÀîtÕrÊ  FÈÚA#œ¶•qæ5…Ú8e»y–;a	Yµ¡në»îú¾m‰Ä¶¾ëù‡ct
4ßpC|ıÚ|#d9›…Ø—ÄL÷‘m'•è·?çŠê‰t³ä^xi3âJÚ1¾el•Ù ¥qù§ÀqşŸ˜V|,>ññ%|Zİw·OwsïwríwµO„vóì÷r7|xÇWv|~èòğÃ£ôÀ¥_,=E$¸”“¶V’¶×CÕ±1ö¬ÚøëÌëÔgŒz¼şW<Â•p?~øOĞVkryUôVŞ~; Ì,¥ğö(¡™5bæì¶³3ÚnåÖ?®ëí]·ß4ƒ®¯yáºëÿıhdâ¶·İ|Û§¡ÆÛåñú,ìŒx‹›î|ÜºÏ(+¸ö­ïíıZdòíozëÛ'#Gÿãºë^àãÃKcxÑÇéZ[Nâ¥N¹Ónâ45wÁRÜö¸È./z•³"âœp]DTÔÂ#	Âø¢ı‰e]vO$â±té®nğzª»ŸèúDØ+©«$_ø]Çk>Rİ«ÓõV?VsJ¢Ó"^…b«Ô)q?!²	"(QeÅ(áñxª=¢ƒ»(²({,Šr«ÕÂy—Àò•tG½ŞÆH÷™ÎË1ĞùÔñšÇ²ÿHÍq™/Yˆ „9‚æ A£åÈã9(r§p¬Ü ‡dÙ¡C”?~éĞqWUªÌ³Æjòæ‰‹ó<Æ’ƒŸ:Îî;şú;ù<İğ#<CoD‚x,İÿÑµ×.-)£˜¾À{ÓçŸğ÷Øú§g‰Ó,øûÚºU%½øFGI/Ê£ÌKâ?‰›Á'4ûú®>ò	ôYi´¿»•†çì
ıjv…Y¶+r†E¥S3r1rw}e}å³m\Wóğš!_å¦ïŸ‚hÇ÷à#ìÛÔşMŞNßJû=ÔÎ³ßíßâí€¼?ü„úÏGÅ`dµ…PÉÊbL¬Ñ’7q+ß3›ÜÕÔĞT÷  Íõ¥œµ´S¬%ÓåÖŞSãöÇ.c'ôâ¿ãÆïréûì!jÿhÿo§ojıµÔ®!)!q)‘ ¦µj­Ï˜è½8r±î=ïäãá7r?U¶ı³ZŞ134´ï@oí{ßSwq„Áø£ğçĞ÷?óçğvx^9åÎ–­<«‘¿bdÜÿ	?MQy_ºƒ•°½à‚TºµH¯•lÅŒ¡Ó’P'8ŒşœVXIß.pùCjQÙ‘•œ„´ò+ëÿµTmÙ3³éÚÚ‰Ê°½ßÛŞÓİîë·‡mãµ§¬oŞ¾õ­åoZP;­³ÛH*5n³Ì–{4GÅş‰¥Xx QÔ‘ó|N#Ç^YÁ›cF=íâ°(ç‘SE9dK&³Ïxéxj-?©ªÔŒqÄ*…cÂ "¥š-¸I4	ó(ò
›£|Së°…Ş|0œv†&:FÚGk*,#­ÃÎ¡phÈy‡7áv'¼V»Õİ:ê‰Dè+9j©¨ëM¯¸Æ£å•4Ä—òùÛ|èKùé7y;a>-5C5Ç¼Íj–ÚÕX„‹$È € øÂ¦aóÕPt„²²*˜n4G†ü“£ZË«İ‹‘šºSé¾T¸ùèÓ'w_ßí©]Lö†<³°}á¨&è¦³£5n¿wÓ©’“Ç©édwCĞá5TDîÛ|´ô$‡ÜB‘Ña²]#p_ºÌˆuª4A4`Á ¼s¢¦ ™ÑÀp49S¢Cn£jQÔƒ°§P	”†AÒpoz1•4››`e×é´‹s“•’}'EI·D½E#Erº€ø7È™xe7T4VúZ®~Ã[æÎÛ®G6qíü\rŞÓèîõ×õ•İşdï¿½¬¯ÎßënôÌn<¹ÛëØ}ò†Hè «¢±u·Û¹»µ©Â5–%}±>aËâ–¼J*æÖ¯Œ™˜8>.‰y|K·« uº„Ï­_“Tó¹¾y=¨d€›dF£)à¯ÆqŠx(-pe&!{\€r#‘±±±Îv,„buxÓ?ˆÁ3Oí™Ú6à9æ¶Åêwìyê÷u©<#™Q.Ÿ6Qá˜%-
Q‡X€ G˜UÑ€z}B?@İiGy ÿr³ƒÂ?/\8üóøÀN~ûÑ÷½ïÑoŸ$Ãï½»;waõo~“ynWçn.óÈ¶û÷Ë­¿9†’bıA+„™›•‚{^â4Á”„¨BP#ğboñ®¦¼ìµ ‰7|öÙÃÏ>şÊÜ_şåÜW@ÌùsæÆB—j²z¼_üùÜêfVÎt`È—Ï	+ï~ğAåóĞÅì/‡‰æ¥8„÷hàş€F©U×bö0*J†M‰7×ğä¹zR§ãŞ¤Î £­½Â§,ı#5Á¥¦ë/ORyñ‹™ïÄÇ›ûÖÂ £'ŞFëû4"¯ĞÈÕÆs>%û¬\†¯ê²ò©Å²3ø"NÎÍ}cv6‹5­ÃµB?Å¸šzÎd>€/ºNÜu×‰oœÈï)k<ê°¨ÕÌÍ§¤Î3®oR×»î°õ5fJ 5_I[B…†©¹ôĞ#ˆ‚ŠÂB€Â’BÊ.S[ŸOm¼LñÌsøâ»î˜»ƒş¿‹–Àáú.áeWt	k¨Ùlb+Ğ1@á«‚48û-¿\Iâ>^Y)OFòj.nzs3)@&˜ó¡­²oŠ¬Í!oMqƒyÜ8Rom	¤íå£¥£ß-1›JÚêÛKMŞ¶†Æ”€åVA#uöl8v‹ğ.§darå+Vq¤øTæmøâ­ßšÍü÷¨ÛY;Ex‰õ8Ôòé„›%ÆO¯Ìïq¸ŒğIRÍ7“Ğ» ñ¾Ìûi@ÕúØñç`†­O›Š˜„JeCá·#f%Æ‰áÔ)5ÏtË·x#È56Êíét1 MgöM^Á“Öü3¡n­]TdÖzá¼!å¯
¨Ö6óü–ö&¤Â–é†fKÅsŠ—İ/"r%"ƒ{ÒÅâ|ª4lŒ!dem„:kA­/@¦CĞ0X‘”ä5Õª=z$U¢†A"6˜¿rL^O*²„«ÆRµª¾6‹Ä²Ùºl¾¡@U¢.)ä:‡®‘[³½{E3lj÷-[öìÙRåñT9İnæÉ6ìrºEƒˆ;f®]œ™9µ¸wfqw²«+Ñ’îNd®[Ü7³pjï¾Å=­éÎVŞ$Şõ±Mº…´QúpŸ|Ì…z•	™¦u, ãgÁx£:¿Q® ®),à™¦Ó°ƒ ÕÊø½^µ‡k*dÕé`Ax¦¼|ÊGcV£V'³a•q!W»rœîé`ÿZãõJÍiS!hXÁ4ü#Ó€ U¡v~ÍÁ—™¦Ÿ´'Õ†îlëKõÅcµÑPÀãæöe¥z$1™57V¾ûD.©÷)™ ^ÇGÄ)tç„—Å…ÄÛä­ğÂOuüŞÛ¯}roãÖÔ~‡}$º~[bwçf¯şHX[_“xèîÃšÚpaöîíÑú{cé±æÈÃ'?¸ç¶oŸØñØÉ-gû7VGl>Ùİ›|!`IzÇ}ÓzÏ†ÑñHvìÛ:5ØØÄ•[Yh‡x âğm¹ˆ4ŒLÕØà(·:`.¹ÔëòV)wò?ÀÍ¸\År¨V>Ö/iòª„½ù¥]Bhˆ®¹d¹º1TÍ¯Ñ…èa¯¯ã'êâõqo­¯–ª‡+K‹ùI‡‚ÜI‡Dbeº\K–~"F‘EÊZÄÂÆ—š!û8uK£ÏÛ¼³sË¾öîkMéMš¡G?İ[{ØëZuA_]oì¡”¡ikªoÆşÑPódíÖáÄ5ÁBÛßsâ¡ş`5;tUDœ•uUnwF]ó@M_´\$Y»Ä»Å;&%&;Gk“£<jSê5Å«ƒ¢ÀÔjT¢‰ÙjÀ×)æ™Øß‹İ=…Ù:ÓÓ³7ñú;¥Â×ŒĞ-j«ê!ÛÓ&;J¬«%ê0©4ú5İ«¿£¦[~G¥È¬:?³¿Ú;jÂD®”%çnÚ…[%‰¿¬JïKa÷lCm¼*x+eËo=ÜsØòíˆD›«·wQË_¢§+,ÅÁèéB«¾èûş
wÀW0¶{6µn¦äàÖıUö=å¶ w¹-ÛºŸ©õæ¸¡ÄjãWM±ÆÊ÷z¼×Màƒìzr[-jÔ­uL«I¢J+­Ğ-¹  %ÙÏ´*® Ø4]h{ë¬PU"B"ŞXOÙĞT Ue7û,>C˜Ğd­ƒ¬@à¯3pR ±˜…dŸRáşD®şGÆ™l>¼{üæ™Dbææññ·ñëÛÆ§n›˜¸mjê¶ÉÉÛ¸şú¡u6_M‚b7%«}¶u†Vê#÷å×ÖÔºwLM½}İº·OM½cİôâÌ¾…
Ë¶@Gk¢+0m©Påá#0/0²³5:+Ô22¹ˆÑÏq”¨8JTP SÌƒ4jfŞÅˆA`D/0âélokõ'ü‰–æÆ†P`%VŠŞ VBF%{¼ü¢&‹üŠÈ«"fôÙ¡‰š°Ù^à^ğj*Ê<Ø‘Ju\3;ñ‘ÁDMÒ ñûİ`.~ÿÅbÜn.A‡t›ˆšy½‚©Ô i¹=¥å–´tÊ3
T’ô6âXa©ñ+Â.‘	 ‹@gÖ™ËŒ¼´~ùı®MeÔ$¿(fä¾ı¡õ…,o=…'?qğìÙù…­mSèÀÄÑ£™oá‡ò“Ì5Ïßuçs'Ş…{Ùw„Õ“tªËÕç:(e:œ«äx7]å„àëFª£ÃuuÃQúvWW»éƒwE†êj†"‘ÁÚº¡Èú¯»¶Öí­ùŸ‹0]%^5FYµ½â­Œ©¼Ù¸É8vA¼_˜<bíÌŞÚy8šFS]¸€§p<óä¥¿Â‰Ì_ ÂaÖŒ(€üÃ•<"x¹%v
dæ_‹KÙà}•¬Ù_â*95íç}?Éš°OÒÑ|Q1ßÚÑJ=è+”°«±äŠOÚ¼-ÆRi5U8%ÎÒSüTô5l>Ì~,ÖŞ$f1³2äÓª%•Ä'¾rõòb}Ü7g¾™M¼~¸ÿÿ9šu€ıäÇ‡yşdàv6qóÍ4:Å¢ĞÄ^æy?1Ú,<NÅÓ—Å¬M÷Şs€E¿÷=şÜïÓÈ¯½±‘d>5ÍŞ}¯2r=ü$|ã>ÍÀÏQ|òØ¹c;×¯çóÜx#ÇèYæ·HÎÿEî2^…»)¯7È~Û|>}˜Ç›ôû“ÿövûì¼İîã+kaığmöŸPu05!!‚P=9`|^£ß[ª)pÈÈ•Ğ/Ì!Ş:¨îW*,¶¢&Ã ª¿Å´¬M¬_5²±¬´Ä¦k‰ÉFOŞÁfà!ö™?gx,á ›Ù¼™Cş®¥Üf¨„ëÅø’dªbËK‰7E©¹ª“r	Ü|.™ÊO&!l°§=¼‹¤bçÖì#/Y¨$LËUb¥B­bšÈu||Ûdõ	º’K™ßÜ(0š‹Æô’TëÿšÁ.(°™™dŞÚò…IRmfø!öcñî‚WEL?Î¢XÃ^æ§˜å–oRËDË«rË(µ<&Z^“[®c|“ä¤–×å–Ö_%Újá’Ü2Åfğ„s-dä–Û)›¸fjYâ-œç¥" 7¶Éï'cüN9Î<^—Š^°K—>Fİ¬«ñä\ÚüÈ{O³KŸü$ñ]ñ·WAhn>ı‡•}4â¡×Õ8“ªÓd®|æÌÙ3³›7óñ»vñz“_>Eü¬”A¯’T8²JÚˆ_„ElÂºaV½QÕZ_—ToÔÌ²—wLj›šj;²ÿâø_	oüó¸ôçrúo.ãt»×kÏrz%ËÀÒC`…àÕ¸ÜŸår­#º*‡Ó£TO¤*\.OQk!ıŞf‹9î’Ë¨ÆÖ—Kiºr^çO­ÆZø¾ÿäÈ=–çğ£X{ìİÏü|é´Hj0(Yrb®„‰VÒJÚ¿˜é¯û½½¦¿şwö¾Ôé>|(uš.™©ÿ<]Ø‹    ‰-E_<õ è    Û¥h    ÛˆÅşoşäI          xÚc`d``şüo>kÜ¿üù¬@ÀÈ	 .+xÚ•Ğ]1†á¯ímÛ¶mÛ¶mÛf/Z4†S4ÛfÛ·î½83“<ùQ×¼SIûÚø÷ĞŸ³»TÏna.\\ÕsqıkÛEõ~R˜“XõLş€¿†—ØcÆ°çæTaÁj`_“ÃeVÉß	ååÜÕÌ[gw“ÏPa{Ÿ8^qíCÕüÉUÿ2Ô])ì6î9Îßˆè§¢vkŠ(÷ï¸ŞÄªhn(g˜í‰Èó33UÎö#oÅüÄ*Š€¿ŠÛ±|¿IáO…Ù¢ÔMTÑMPåğšˆ&Êù;n‘» œ·õÏ”İn".÷Ş6UùŸ¤õï]ÿÑóomÿ.`n©hä¿²°×L9{
DÍ½;*•À_µW4E!dGnT@‹ô`&+²#c,O‰Ì±:+R!õÌûnú~î”²»µ½J|Ë%Ä<—d‘(Ü·‰ÕÂQÛñFşY˜ú·fªz!—Ù¨üHf&¨?’™İªñNr$bêÈ×¾lUE>SLU‘ÿ, ¯&­éÍ9¿	2#Ò €mïG¡|LÍHü½Â¨æö*Û­’ö±ÊÚ¹Êfë+›+¦Â'yÃteÜãŠªqÄPæ-;$ß	{Q	gĞSQ]°”ymPÇĞãQí°XìáóøS¾¶¯é§ú¾à§§Òg{Ëö xÚÁ´ä0  ÀÔLE²mşÙ¶mÛ¶mÛ~8Û¶mÛ¶mÏ  Ò‚: è€1`X Ö€Tjµ€:Gİ¢%Ú¥	ÎC7¥OĞŸÈD™L¦ Ó„™Ä¬aî²<k±ÍÙÎlv4;Ï®æ WˆëÁ]â]¾¿ ™…FB;¡—0L˜$l‰¦˜[,.vGH’äJDÊ å‘JHUdG!Ÿ‘o(e”5Ê_UV=5IÍ¨¶€", {ÂIp\7Á}ğ”fkyµ–Úí“G/¡Ò§éóôUú6Ã3’Œİ&1»š­V?[·«Øsìµö>û¼ıÀaìNCg¸3Ù™ísÎ9Ü–nWw ;Öã½"Şo«ÏûİüşŸH§Èô¨İ}KÛñÚñ¾ñ(*j¡–¨¦¡…hÚÎ¡»èúŒpzœ—ÇÍñ0¼
ßÂ¯N¢`bqâ[`ƒÎÁÁàRğ$aÇ„çÃÏ$B²‘|¤&éB&“d9A®ÿû­$­¤®d¬ä®­”®Ô„/*Û+ÏQQQiT9¨rIåª¸ª±jêtÕ5ª‡T¨Ù©%¨µ©]QÏÖ¨Ñ4ÖLÑü¤U¥Í¡ =C{³ö~í:L:Z:a::töêÜÒù¯ë®Û¢»DCOPOJOYOGÏTÏNÏ]/@¯\o‚ŞJ½Sz?ômôËõ'ëoĞß  p¬—Ğ       L  O   $ N      xÚCZPFOvkh–m»Iæ8ü¶±‘ÒzZA«èdã¹zuõÍ„¨£¦¾¸g\CŸì	×ÒÉív^âî×¿Ë4°UÓóŒé©I=ãN&j®Y'M†*9¢„‰P ‹1FeJt¢tİ£HŠ¨nZvÌ…,/: §3s…;Y¥èQË¡O¸`ËÈæ¶…‰bÂÜ%C\™Lªn¹¦Pç‚¢Ù )Õ÷·¾ÜÙûã[6©¨åäk:	$LQ|¡>î}#¶)æÙcß±)úîÁoÎ:#HN/*OÑõş4YÁ2íïõ#¦R"ÿO‰Q“3qzÿ{ÛKÕjfæJ7*/ˆ‡È‹®iÕ°ş>[ìÜ=LXk  xÚlÁCt  EÑ÷ccbÛœØImÛ¶mÚîª¶mÛ<5×UŒe¸Î½˜Ğ æF!Ê3™ğ„¡œb§ÙDá'‚?D2‚§<çQDC,/yÅkŞG<	$bd$I¼å£xÏfÎp–d
I!•4™’NøÈh>‘IÙäğ\™Ñœ1Œc<c™ÈöÒ‚–L¢iÍd¦2)´¡-íhÏg¦3“Y2gdAG:Ñ™.Ì¦+s˜Ç|æ²œ£Et§=YM/z³Å,a}èK?úóŸë¥¬¸Ë=YË†]ìÆWÜpÇO¼ğÆ_ÙÊNör£œä,ƒ\ä*7¹ËCò’·|ä+?ù+@
R07© ’*œqÄN`Æ‚ÙÏVbò‚-vd0yà¤PÎs¦<ä¹Äe®pŒãÜææØ`ª0…+B‘,ã–Šb9;Å”p•@‚`+C8HÅ`EÖ¬bkYÃ:–*Vqâ·â• D•¤d¥(UiJWhÆz¾ğ“¯|ã—2•¥lå(WyÊWj«¢Ší†a XnÃ9õ/Â|–®!{›(±¥>Qáë[y7tšiÀĞÙït¦,œÎJƒÖ {=ñŒ†ÑM>^ğp×±ˆ½½Ü:K±\idf Ã	÷§Ê{“@gF«HâÚÅ‰³Íyì¤Èû\„g‚ı…'q=—€g±a$1fÉ§Ø7úXàHÍæœQ!npJ·Äm8Bâ2fÉç6Á¤¸á(n8}ˆb·EX&¬8,ÕóOLøpüèõ{7´uú„E´[„İƒŞËÑÀHér©Fµµ“q£áD'¸¡Û#ìFX)6	±™œØg8E¿Å1º+vbšşöM±ÜHR[Ş›(H¯«PRI²–·E}[t0…[»©Jm[àÚÑÃûİİ,î>[  xÚbğŞÁpĞ“h DQ>3íİj‹)$R‰	D×	I%l êz„ êYvD*¬Ş¬šªîÿw¡â¸ŠÅUAa½5ğ‚30ìÖmìOíôTÃ+y”ùÒLåÀíP•ŞäI‘œãôİe²/+	øÿÖ®HÚÔE4cBÛmZ¶/
€^ULÓÒMí ÖËß`ı©É¾Û¢2Q¾}}ìŒ„>ÑÆ…ÖOu—0W                                                                                                                                                                                                                                                                                                                                  re 'install' to 'D:\Program Files (x86)\Steam\config\config.vdf'
[2023-03-03 23:36:43] Flushed store 'install' to 'D:\Program Files (x86)\Steam\config\config.vdf'
[2023-03-03 23:37:12] Flushed store 'userlocal' to 'D:\Program Files (x86)\Steam\userdata\124618114\config\localconfig.vdf'
[2023-03-03 23:37:12] Flushed store 'install' to 'D:\Program Files (x86)\Steam\config\config.vdf'
[2023-03-03 23:49:51] Flushed store 'userlocal' to 'D:\Program Files (x86)\Steam\userdata\124618114\config\localconfig.vdf'
[2023-03-04 00:04:25] Flushed store 'userlocal' to 'D:\Program Files (x86)\Steam\userdata\124618114\config\localconfig.vdf'
                                                                                                                                                                                                                                                                                                                                                                                                 Ud¥ŒòR+ù5H
g<TMRÖ¿‹:-Èò]ÒBW·X&Ü—ê±1&eŠ74iıD9:ëó·öæ5äâ&&
pIÑKÔÆ‡”ï¢´
£¾±Œ¢¼ÌĞFöš“ëğGTJ”Ê‹5«e™d´ïÈ                                                                                                                                                                                                                                                                                                                                                                                                           ©˜© ©¨©°©¸©À©È©Ğ©Ø©à©è©ğ©ø© ªªªª ª(ª0ª8ª@ªHªPªXª`ªhªpªxª€ªˆªª˜ª ª¨ª°ª¸ªÀªÈªĞªØªàªèªğªøª «««« «(«0«8«@«H«P«X«`«h«p«x«€«ˆ««˜« «¨«°«¸«@¬H¬P¬À¬È¬Ğ¬Ø¬à¬è¬ğ¬ø¬ ­­­­ ­(­0­8­@­H­P­X­`­h­p­x­€­ˆ­­˜­ ­¨­°­¸­À­È­Ğ­Ø­à­è­ğ­ø­ ®®®® ®(®0®8®@®H®P®X®`®h®p®x®€®ˆ®®˜® ®¨®°®¸®À®È®Ğ®Ø®à®è®ğ®ø® ¯¯¯¯ ¯(¯0¯8¯@¯H¯P¯X¯`¯h¯p¯x¯€¯ˆ¯¯˜¯ ¯¨¯°¯¸¯À¯È¯Ğ¯Ø¯à¯è¯ğ¯ø¯   ü         ( 0 8 @ H P X ` h p x € ˆ  ˜   ¨ ° ¸ À È Ğ Ø à è ğ ø  ¡¡¡¡ ¡(¡0¡8¡@¡H¡P¡X¡`¡h¡p¡x¡€¡ˆ¡¡˜¡ ¡¨¡°¡¸¡À¡È¡Ğ¡Ø¡à¡è¡ğ¡ø¡ ¢¢¢¢ ¢(¢0¢8¢@¢H¢P¢X¢`¢h¢p¢x¢€¢ˆ¢¢˜¢ ¢¨¢°¢¸¢À¢È¢Ğ¢Ø¢à¢è¢ğ¢ø¢ ££££ £(£0£8£@£H£P£X£`£h£p£€£ˆ££˜£ £¨£°£¸£À£È£Ğ£Ø£à£è£ğ£ø£ ¤¤¤¤ ¤(¤0¤8¤@¤H¤P¤X¤`¤h¤p¤x¤€¤ˆ¤¤˜¤ ¤¨¤°¤¸¤À¤È¤Ğ¤Ø¤à¤è¤ğ¤ø¤ ¥¥¥¥ ¥(¥0¥8¥@¥H¥P¥X¥`¥h¥p¥x¥€¥ˆ¥¥˜¥ ¥¨¥°¥¸¥À¥È¥Ğ¥Ø¥à¥è¥ğ¥ø¥ ¦¦¦¦ ¦(¦0¦8¦@¦H¦P¦X¦`¦h¦p¦x¦€¦ˆ¦¦˜¦ ¦¨¦°¦¸¦À¦È¦Ğ¦Ø¦à¦è¦ğ¦ø¦ §§§§ §(§0§8§@§H§P§X§`§h§p§x§€§ˆ§§˜§ §¨§°§¸§À§È§Ğ§Ø§à§è§ğ§ø§ ¨¨¨¨ ¨(¨0¨8¨@¨H¨P¨X¨`¨h¨p¨x¨€¨ˆ¨¨˜¨ ¨¨¨°¨¸¨À¨È¨Ğ¨Ø¨à¨è¨ğ¨ø¨ ©©©© ©(©0©8©@©H©P©X©`©h©p©x©€©ˆ©©˜© ©¨©°©¸©À©È©Ğ©Ø©à©è©ğ©ø© ªªªª ª(ª0ª@ªHªPªXª`ªhªpªª˜ª ª¨ª°ª¸ªÀªÈªĞªØªàªèªğªøª «««« «(«0«8«@«H«P«X«`«h«p«x«€«ˆ««˜« «¨«°«¸«À«È«Ğ«à«è«ğ«ø« ¬¬¬¬ ¬(¬0¬8¬@¬H¬P¬X¬`¬h¬p¬x¬€¬ˆ¬¬˜¬ ¬¨¬°¬¸¬À¬È¬Ğ¬Ø¬à¬è¬ğ¬ø¬ ­­­­ ­(­0­8­@­H­P­X­`­h­