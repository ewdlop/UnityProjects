 0.22f), FFrameTime(-7,  0.539999962f), FFrameTime(-7,  0.899999917f),
			FFrameTime(-6,  0.9f),  FFrameTime(-5, 0.f), FFrameTime(-5,  0.22f), FFrameTime(-5,  0.539999962f), FFrameTime(-5,  0.899999917f),
			FFrameTime(-5,  0.9f),  FFrameTime(-4, 0.f), FFrameTime(-4,  0.22f), FFrameTime(-4,  0.539999962f), FFrameTime(-4,  0.899999917f),
			FFrameTime(-1,  0.9f),  FFrameTime(0,  0.f), FFrameTime(0,   0.22f), FFrameTime(0,   0.539999962f), FFrameTime(0,   0.899999917f),
		};

		for (int32 Index = 0; Index < NumFrames; ++Index)
		{
			FFrameTime Time     = TestTimes[Index];
			FFrameTime Actual   = Time - TimeToSubtract;
			FFrameTime Expected = ExpectedTimes[Index];

			ensureAlwaysMsgf(IsNearlyEqual(Actual, Expected), TEXT("%d+%.3f - 10.1: %d+%.3f (actual) == %d+%0.3f (expected)"),
				Time.GetFrame().Value,     Time.GetSubFrame(),
				Actual.GetFrame().Value,   Actual.GetSubFrame(),
				Expected.GetFrame().Value, Expected.GetSubFrame()
			);
		}
	}

	// Test adding a positive FrameTime with a large sub frame
	{
		FFrameTime TimeToSubtract(10, 0.8f);
		FFrameTime ExpectedTimes[] = {
			FFrameTime(-21, 0.199999988f), FFrameTime(-21, 0.3f), FFrameTime(-21, 0.52f), FFrameTime(-21, 0.84f), FFrameTime(-20, 0.19999993f),
			FFrameTime(-17, 0.199999988f), FFrameTime(-17, 0.3f), FFrameTime(-17, 0.52f), FFrameTime(-17, 0.84f), FFrameTime(-16, 0.19999993f),
			FFrameTime(-16, 0.199999988f), FFrameTime(-16, 0.3f), FFrameTime(-16, 0.52f), FFrameTime(-16, 0.84f), FFrameTime(-15, 0.19999993f),
			FFrameTime(-14, 0.199999988f), FFrameTime(-14, 0.3f), FFrameTime(-14, 0.52f), FFrameTime(-14, 0.84f), FFrameTime(-13, 0.19999993f),
			FFrameTime(-11, 0.199999988f), FFrameTime(-11, 0.3f), FFrameTime(-11, 0.52f), FFrameTime(-11, 0.84f), FFrameTime(-10, 0.19999993f), 
			FFrameTime(-8,  0.199999988f), FFrameTime(-8,  0.3f), FFrameTime(-8,  0.52f), FFrameTime(-8,  0.84f), FFrameTime(-7,  0.19999993f),
			FFrameTime(-6,  0.199999988f), FFrameTime(-6,  0.3f), FFrameTime(-6,  0.52f), FFrameTime(-6,  0.84f), FFrameTime(-5,  0.19999993f),
			FFrameTime(-5,  0.199999988f), FFrameTime(-5,  0.3f), FFrameTime(-5,  0.52f), FFrameTime(-5,  0.84f), FFrameTime(-4,  0.19999993f),
			FFrameTime(-1,  0.199999988f), FFrameTime(-1,  0.3f), FFrameTime(-1,  0.52f), FFrameTime(-1,  0.84f), FFrameTime(0,   0.19999993f),
		};

		for (int32 Index = 0; Index < NumFrames; ++Index)
		{
			FFrameTime Time     = TestTimes[Index];
			FFrameTime Actual   = Time - TimeToSubtract;
			FFrameTime Expected = ExpectedTimes[Index];

			ensureAlwaysMsgf(IsNearlyEqual(Actual, Expected), TEXT("%d+%.3f - 10.8: %d+%.3f (actual) == %d+%0.3f (expected)"),
				Time.GetFrame().Value,     Time.GetSubFrame(),
				Actual.GetFrame().Value,   Actual.GetSubFrame(),
				Expected.GetFrame().Value, Expected.GetSubFrame()
			);
		}
	}

	// Test adding a negative FrameTime with a tiny sub frame
	{
		FFrameTime TimeToSubtract(-13, 0.01f);
		FFrameTime ExpectedTimes[] = {
			FFrameTime(2,  0.99f), FFrameTime(3,  0.09f), FFrameTime(3,  0.31f), FFrameTime(3,  0.63f), FFrameTime(3,  0.98999997f),
			FFrameTime(6,  0.99f), FFrameTime(7,  0.09f), FFrameTime(7,  0.31f), FFrameTime(7,  0.63f), FFrameTime(7,  0.98999997f),
			FFrameTime(7,  0.99f), FFrameTime(8,  0.09f), FFrameTime(8,  0.31f), FFrameTime(8,  0.63f), FFrameTime(8,  0.98999997f),
			FFrameTime(9,  0.99f), FFrameTime(10, 0.09f), FFrameTime(10, 0.31f), FFrameTime(10, 0.63f), FFrameTime(10, 0.98999997f),
			FFrameTime(12, 0.99f), FFrameTime(13, 0.09f), FFrameTime(13, 0.31f), FFrameTime(13, 0.63f), FFrameTime(13, 0.98999997f),
			FFrameTime(15, 0.99f), FFrameTime(16, 0.09f), FFrameTime(16, 0.31f), FFrameTime(16, 0.63f), FFrameTime(16, 0.98999997f),
			FFrameTime(17, 0.99f), FFrameTime(18, 0.09f), FFrameTime(18, 0.31f), FFrameTime(18, 0.63f), FFrameTime(18, 0.98999997f),
			FFrameTime(18, 0.99f), FFrameTime(19, 0.09f), FFrameTime(19, 0.31f), FFrameTime(19, 0.63f), FFrameTime(19, 0.98999997f),
			FFrameTime(22, 0.99f), FFrameTime(23, 0.09f), FFrameTime(23, 0.31f), FFrameTime(23, 0.63f), FFrameTime(23, 0.98999997f),
		};

		for (int32 Index = 0; Index < NumFrames; ++Index)
		{
			FFrameTime Time     = TestTimes[Index];
			FFrameTime Actual   = Time - TimeToSubtract;
			FFrameTime Expected = ExpectedTimes[Index];

			ensureAlwaysMsgf(IsNearlyEqual(Actual, Expected), TEXT("%d+%.3f - -13.01: %d+%.3f (actual) == %d+%0.3f (expected)"),
				Time.GetFrame().Value,     Time.GetSubFrame(),
				Actual.GetFrame().Value,   Actual.GetSubFrame(),
				Expected.GetFrame().Value, Expected.GetSubFrame()
			);
		}
	}

	// Test adding a negative FrameTime with a large sub frame
	{
		FFrameTime TimeToSubtract(-13, 0.9f);
		FFrameTime ExpectedTimes[] = {
			FFrameTime(2,  0.100000024f), FFrameTime(2,  0.200000048f), FFrameTime(2,  0.420000017f), FFrameTime(2,  0.74f), FFrameTime(3,  0.0999999642f),
			FFrameTime(6,  0.100000024f), FFrameTime(6,  0.200000048f), FFrameTime(6,  0.420000017f), FFrameTime(6,  0.74f), FFrameTime(7,  0.0999999642f),
			FFrameTime(7,  0.100000024f), FFrameTime(7,  0.200000048f), FFrameTime(7,  0.420000017f), FFrameTime(7,  0.74f), FFrameTime(8,  0.0999999642f),
			FFrameTime(9,  0.100000024f), FFrameTime(9,  0.200000048f), FFrameTime(9,  0.420000017f), FFrameTime(9,  0.74f), FFrameTime(10, 0.0999999642f),
			FFrameTime(12, 0.100000024f), FFrameTime(12, 0.200000048f), FFrameTime(12, 0.420000017f), FFrameTime(12, 0.74f), FFrameTime(13, 0.0999999642f),
			FFrameTime(15, 0.100000024f), FFrameTime(15, 0.200000048f), FFrameTime(15, 0.420000017f), FFrameTime(15, 0.74f), FFrameTime(16, 0.0999999642f),
			FFrameTime(17, 0.100000024f), FFrameTime(17, 0.200000048f), FFrameTime(17, 0.420000017f), FFrameTime(17, 0.74f), FFrameTime(18, 0.0999999642f),
			FFrameTime(18, 0.100000024f), FFrameTime(18, 0.200000048f), FFrameTime(18, 0.420000017f), FFrameTime(18, 0.74f), FFrameTime(19, 0.0999999642f),
			FFrameTime(22, 0.100000024f), FFrameTime(22, 0.200000048f), FFrameTime(22, 0.420000017f), FFrameTime(22, 0.74f), FFrameTime(23, 0.0999999642f),
		};

		for (int32 Index = 0; Index < NumFrames; ++Index)
		{
			FFrameTime Time     = TestTimes[Index];
			FFrameTime Actual   = Time - TimeToSubtract;
			FFrameTime Expected = ExpectedTimes[Index];

			ensureAlwaysMsgf(IsNearlyEqual(Actual, Expected), TEXT("%d+%.3f - -13.9: %d+%.3f (actual) == %d+%0.3f (expected)"),
				Time.GetFrame().Value,     Time.GetSubFrame(),
				Actual.GetFrame().Value,   Actual.GetSubFrame(),
				Expected.GetFrame().Value, Expected.GetSubFrame()
			);
		}
	}
	return true;
}


IMPLEMENT_SIMPLE_AUTOMATION_TEST(FFrameTimeConversionTest, "System.Core.Time.Conversion", EAutomationTestFlags::ApplicationContextMask | EAutomationTestFlags::SmokeFilter | EAutomationTestFlags::HighPriority)
bool FFrameTimeConversionTest::RunTest(const FString& Parameters)
{
	const int32 NumFrames = UE_ARRAY_COUNT(TestTimes);

	{
		FFrameRate SrcRate = FCommonFrameRates::FPS_60();
		FFrameRate DstRate = FCommonFrameRates::FPS_30();

		FFrameTime ExpectedTimes[] = {
			FFrameTime(-5, 0.f),  FFrameTime(-5, 0.05f), FFrameTime(-5, 0.16f), FFrameTime(-5, 0.32f), FFrameTime(-5, 0.499999985f),
			FFrameTime(-3, 0.f),  FFrameTime(-3, 0.05f), FFrameTime(-3, 0.16f), FFrameTime(-3, 0.32f), FFrameTime(-3, 0.499999985f),
			FFrameTime(-3, 0.5f), FFrameTime(-3, 0.55f), FFrameTime(-3, 0.66f), FFrameTime(-3, 0.82f), FFrameTime(-3, FFrameTime::MaxSubframe),
			FFrameTime(-2, 0.5f), FFrameTime(-2, 0.55f), FFrameTime(-2, 0.66f), FFrameTime(-2, 0.82f), FFrameTime(-2, FFrameTime::MaxSubframe),
			FFrameTime(0,  0.f),  FFrameTime(0,  0.05f), FFrameTime(0,  0.16f), FFrameTime(0,  0.32f), FFrameTime(0,  0.499999985f),
			FFrameTime(1,  0.5f), FFrameTime(1,  0.55f), FFrameTime(1,  0.66f), FFrameTime(1,  0.82f), FFrameTime(1,  FFrameTime::MaxSubframe),
			FFrameTime(2,  0.5f), FFrameTime(2,  0.55f), FFrameTime(2,  0.66f), FFrameTime(2,  0.82f), FFrameTime(2,  FFrameTime::MaxSubframe),
			FFrameTime(3,  0.f),  FFrameTime(3,  0.05f), FFrameTime(3,  0.16f), FFrameTime(3,  0.32f), FFrameTime(3,  0.499999985f),
			FFrameTime(5,  0.f),  FFrameTime(5,  0.05f), FFrameTime(5,  0.16f), FFrameTime(5,  0.32f), FFrameTime(5,  0.499999985f),
		};

		for (int32 Index = 0; Index < NumFrames; ++Index)
		{
			FFrameTime Time     = TestTimes[Index];
			FFrameTime Actual   = FFrameRate::TransformTime(Time, SrcRate, DstRate);
			FFrameTime Expected = ExpectedTimes[Index];

			ensureAlwaysMsgf(Actual.GetFrame() == Expected.GetFrame() && FMath::IsNearlyEqual(Actual.GetSubFrame(), Expected.GetSubFrame(), KINDA_SMALL_NUMBER),
				TEXT("%d+%.3f 60fps -> 30fps: %d+%.3f (actual) == %d+%0.3f (expected)"),
				Time.GetFrame().Value,     Time.GetSubFrame(),
				Actual.GetFrame().Value,   Actual.GetSubFrame(),
				Expected.GetFrame().Value, Expected.GetSubFrame()
			);
		}
	}

	{
		FFrameRate SrcRate = FCommonFrameRates::FPS_60();
		FFrameRate DstRate = FCommonFrameRates::NTSC_30();

		FFrameTime ExpectedTimes[] = {
			FFrameTime(-5, 0.004995004995f), FFrameTime(-5, 0.05494505495f),  FFrameTime(-5, 0.1648351648f),   FFrameTime(-5, 0.3246753247f),   FFrameTime(-5, 0.5044954895f),
			FFrameTime(-3, 0.002997002997f), FFrameTime(-3, 0.05294705295f),  FFrameTime(-3, 0.1628371628f),   FFrameTime(-3, 0.3226773227f),   FFrameTime(-3, 0.5024974875f),
			FFrameTime(-3, 0.5024975025f),   FFrameTime(-3, 0.5524475524f),   FFrameTime(-3, 0.6623376623f),   FFrameTime(-3, 0.8221778222f),   FFrameTime(-2, 0.001997987013f),
			FFrameTime(-2, 0.5014985015f),   FFrameTime(-2, 0.5514485514f),   FFrameTime(-2, 0.6613386613f),   FFrameTime(-2, 0.8211788212f),   FFrameTime(-1, 0.000998986014f),
			FFrameTime(0,  0.f),             FFrameTime(0,  0.04995004995f),  FFrameTime(0,  0.1598401598f),   FFrameTime(0,  0.3196803197f),   FFrameTime(0,  0.4995004845f),
			FFrameTime(1,  0.4985014985f),   FFrameTime(1,  0.5484515485f),   FFrameTime(1,  0.6583416583f),   FFrameTime(1,  0.8181818182f),   FFrameTime(1,  0.998001983f),
			FFrameTime(2,  0.4975024975f),   FFrameTime(2,  0.5474525475f),   FFrameTime(2,  0.6573426573f),   FFrameTime(2,  0.8171828172f),   FFrameTime(2,  0.997002982f),
			FFrameTime(2,  0.997002997f),    FFrameTime(3,  0.04695304695f),  FFrameTime(3,  0.1568431568f),   FFrameTime(3,  0.3166833167f),   FFrameTime(3,  0.4965034815f),
			FFrameTime(4,  0.995004995f),    FFrameTime(5,  0.04495504496f),  FFrameTime(5,  0.1548451548f),   FFrameTime(5,  0.3146853147f),   FFrameTime(5,  0.4945054795f),
		};

		for (int32 Index = 0; Index < NumFrames; ++Index)
		{
			FFrameTime Time     = TestTimes[Index];
			FFrameTime Actual   = FFrameRate::TransformTime(Time, SrcRate, DstRate);
			FFrameTime Expected = ExpectedTimes[Index];

			ensureAlwaysMsgf(Actual.GetFrame() == Expected.GetFrame() && FMath::IsNearlyEqual(Actual.GetSubFrame(), Expected.GetSubFrame(), KINDA_SMALL_NUMBER),
				TEXT("%d+%.3f 60fps -> 29.97fps: %d+%.3f (actual) == %d+%0.3f (expected)"),
				Time.GetFrame().Value,     Time.GetSubFrame(),
				Actual.GetFrame().Value,   Actual.GetSubFrame(),
				Expected.GetFrame().Value, Expected.GetSubFrame()
			);
		}
	}

	{
		FFrameRate SrcRate = FCommonFrameRates::FPS_60();
		FFrameRate DstRate = FCommonFrameRates::FPS_60();

		for (int32 Index = 0; Index < NumFrames; ++Index)
		{
			FFrameTime Time     = TestTimes[Index];
			FFrameTime Actual   = FFrameRate::TransformTime(Time, SrcRate, DstRate);
			FFrameTime Expected = TestTimes[Index];

			ensureAlwaysMsgf(IsNearlyEqual(Actual, Expected), TEXT("%d+%.3f 60fps -> 60fps: %d+%.3f (actual) == %d+%0.3f (expected)"),
				Time.GetFrame().Value,     Time.GetSubFrame(),
				Actual.GetFrame().Value,   Actual.GetSubFrame(),
				Expected.GetFrame().Value, Expected.GetSubFrame()
			);
		}
	}

	return true;
}

IMPLEMENT_SIMPLE_AUTOMATION_TEST(FFrameRateMultiplesTest, "System.Core.Time.FrameRateMultiples", EAutomationTestFlags::ApplicationContextMask | EAutomationTestFlags::SmokeFilter | EAutomationTestFlags::HighPriority)
bool FFrameRateMultiplesTest::RunTest(const FString& Parameters)
{
	FFrameRate TestRates[] = {
		FCommonFrameRates::FPS_12(),
		FCommonFrameRates::FPS_15(),
		FCommonFrameRates::FPS_24(),
		FCommonFrameRates::FPS_25(),
		FCommonFrameRates::FPS_30(),
		FCommonFrameRates::FPS_48(),
		FCommonFrameRates::FPS_50(),
		FCommonFrameRates::FPS_60(),
		FCommonFrameRates::FPS_100(),
		FCommonFrameRates::FPS_120(),
		FCommonFrameRates::FPS_240(),
		FCommonFrameRates::NTSC_24(),
		FCommonFrameRates::NTSC_30(),
		FCommonFrameRates::NTSC_60(),
		FFrameRate(24000,1)
	};

	const int32 NumRates = UE_ARRAY_COUNT(TestRates);

	{
		bool IsMultipleOf[] = {
			true,	false,	true,	false,	false,	true,	false,	true,	false,	true,	true,	false,	false,	false,	true,
			false,	true,	false,	false,	true,	false,	false,	true,	false,	true,	true,	false,	false,	false,	true,
			false,	false,	true,	false,	false,	true,	false,	false,	false,	true,	true,	false,	false,	false,	true,
			false,	false,	false,	true,	false,	false,	true,	false,	true,	false,	false,	false,	false,	false,	true,
			false,	false,	false,	false,	true,	false,	false,	true,	false,	true,	true,	false,	false,	false,	true,
			false,	false,	false,	false,	false,	true,	false,	false,	false,	false,	true,	false,	false,	false,	true,
			false,	false,	false,	false,	false,	false,	true,	false,	true,	false,	false,	false,	false,	false,	true,
			false,	false,	false,	false,	false,	false,	false,	true,	false,	true,	true,	false,	false,	false,	true,
			false,	false,	false,	false,	false,	false,	false,	false,	true,	false,	false,	false,	false,	false,	true,
			false,	false,	false,	false,	false,	false,	false,	false,	false,	true,	true,	false,	false,	false,	true,
			false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	true,	false,	false,	false,	true,
			false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	true,	false,	false,	true,
			false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	true,	true,	false,
			false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	true,	false,
			false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	true,
		};


		bool IsFactorOf[] = {
			true,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,
			false,	true,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,
			true,	false,	true,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,
			false,	false,	false,	true,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,
			false,	true,	false,	false,	true,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,
			true,	false,	true,	false,	false,	true,	false,	false,	false,	false,	false,	false,	false,	false,	false,
			false,	false,	false,	true,	false,	false,	true,	false,	false,	false,	false,	false,	false,	false,	false,
			true,	true,	false,	false,	true,	false,	false,	true,	false,	false,	false,	false,	false,	false,	false,
			false,	false,	false,	true,	false,	false,	true,	false,	true,	false,	false,	false,	false,	false,	false,
			true,	true,	true,	false,	true,	false,	false,	true,	false,	true,	false,	false,	false,	false,	false,
			true,	true,	true,	false,	true,	true,	false,	true,	false,	true,	true,	false,	false,	false,	false,
			false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	true,	false,	false,	false,
			false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	true,	false,	false,
			false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	false,	true,	true,	false,
			true,	true,	true,	true,	true,	true,	true,	true,	true,	true,	true,	true,	false,	false,	true,
		};

		for (int32 Index = 0; Index < NumRates; ++Index)
		{
			FFrameRate SrcRate = TestRates[Index];

			for (int32 OtherIndex = 0; OtherIndex < NumRates; ++OtherIndex)
			{
				FFrameRate OtherRate = TestRates[OtherIndex];

				int32 TestResult = Index*NumRates + OtherIndex;

				if (IsMultipleOf[TestResult])
				{
					ensureAlwaysMsgf(SrcRate.IsMultipleOf(OtherRate),
						TEXT("Expected %d/%d to be a multiple of %d/%d."),
						SrcRate.Numerator, SrcRate.Denominator,
						OtherRate.Numerator, OtherRate.Denominator
					);
				}
				else
				{
					ensureAlwaysMsgf(!SrcRate.IsMultipleOf(OtherRate),
						TEXT("Did not expect %d/%d be a multiple of %d/%d."),
						SrcRate.Numerator, SrcRate.Denominator,
						OtherRate.Numerator, OtherRate.Denominator
					);
				}

				if (IsFactorOf[TestResult])
				{
					ensureAlwaysMsgf(SrcRate.IsFactorOf(OtherRate),
						TEXT("Expected %d/%d to be a factor of %d/%d."),
						SrcRate.Numerator, SrcRate.Denominator,
						OtherRate.Numerator, OtherRate.Denominator
					);
				}
				else
				{
					ensureAlwaysMsgf(!SrcRate.IsFactorOf(OtherRate),
						TEXT("Did not expect %d/%d to be a factor of %d/%d."),
						SrcRate.Numerator, SrcRate.Denominator,
						OtherRate.Numerator, OtherRate.Denominator
					);
				}
			}
		}
	}

	return true;
}

#endif // WITH_DEV_AUTOMATION_TESTS                                   2®£ô“™z”Rﬁ‰fò=Ñ/x¡06≈ræñµ^Ö“≥4ˇ¯äb›äâ9M»]”ÖçÔ]y«‚_Œl9ú!§¬x'†!"ª-“ƒˇßù∑Ø±ù3¨<ÜÛx‡f‡° ~∫U+ÅÕíÛ«Ô"øh;Ö˙ﬂEÕÿèíGDLgïcÛ»©ÜR´ÇkÑç›vÎO—´=€ŸoïÊ:Ï˚¥Ñd_⁄nPùwó¡$»≠œÀ-)∫⁄ÛgYñ#ôızf±±ªË©ªL6˘íj,-@cØ§ı˜tñœ– ˘iäûá˙ÛÄÿQI@|éÃy¯∂ÿ*≤^+‹¢ñî¬óqRñ4—¬B—»î’zÒ4“gçÀ,õâöhY‰5Ò%Q›dÔ÷°wQçrØÕ5’GÏìVt™Æwﬁ;#Ë¨Ï{©©/‰å®KéÍÈu`ä_Æp¸#ç<)ºj›ìïº.P˘ÅS…Tπq>XçÍÁ4âˆ›ﬂ˜ZõºµOÃH´qÈÂ˝7∏·Û|µYG"˝cÈ3‚√TB™‘’UrÓL<âG≠C4Ú—ã¸Æe∑%—˛’%øˇ?ÄØïA~˘˚D∏WΩ∞ :Uè¶50˘PôÌ„‰˚ˇR5◊˚i#~|≤U≈„TÍ?µâ¯Æ˛∆R⁄sW˜Ãˆ.√NÊ±t›Ä¿ãx!ç5Tç0€Y∂àLAf@D≤YzáÃÏCG®B¥\FqÅúí‹{&πÎ(◊Óñoùh`ıà*8Û,◊ÓÁÏe’èzöîT®Hµ/ñƒ&ñ‹töb·6®LRˇ5ˆ≠⁄‡©–Êk5π˝ﬁ«¥TdrxÍÊ":ˇﬂj∂ªp-Gœ∏qøÎß·"ﬁÓtùŒj‚{9h_ø˙ú~·†ãd˘Zg÷˘:uå`í÷¥£S0GKıŒπÀ)£‘∞LK∂ñ03êÛΩR¢TÓGq¨«˛ÿr]\g4óª òÆÇôøgÎGKXN∏6t®41Û˘Ä–=˝Lÿ{˙úZ%ßΩˇcóZªû‹Ú'˘óÿÕáœ‡ÓÚfy]6Dvn6»¨=‚áè‹–Cêƒë9dã[Dæí&ª·“µx∏Xd˝/Ωœ'b˙pœWXãµYôãAô≈Òµﬁ]'»UÇâÆ<uœø’üæiîBä%øG5È≠“ ô◊Ôß≠êdS•¯ìÀB⁄8º±›{í>V ëZU,≠X≥π/Vƒ+¥"NÕEÜî˛è“G^‘≠°íπphk–∫É!Ô‚¸àI  …0whC$:¶≠_ÄÅÅà◊‘·	m>◊í-ˆ$∑a‚Q{D8ı‰ ï
,~∑^;Í¯]Tud£∞ãÕ£~÷Ò1‹n∑∏Fˇí˝á`∏üâ †lïãÛ<ì#ò0ó0≥<˘¡P¥'H«íSz”{æ◊„ÂÛu@Á>·Ú¸˛TAw)ﬂÄdûì,¨˛'E‰#`UA€¸hfUÆíÌ+A >Æ‹,1$¿◊Îo°—ewSsT@ÒuíÊ‹‹cgY¡Õ„„=ŸÅTEIÂ6Öˇ˚!ﬁÀud°ìëõ8R,n$◊„√Œ€Z2Ónm(ﬂˇÿÜ£u1¥Ê_æW[Ò¿X◊ú#çé“ù$≤<µŸr·VÉ‹	2©
‡&ù¥&3xñ°m£/Z"’u«ÅÍ⁄cE]õˆ#IböÚéäœÈYˇÎ€§2ÇÔRÖ"$oÏˇbŒíêc∞∂ó “§S©Qœ´Í\˛Tım'ÏG√≤ú9ß˘«a´àóπæ;Knt