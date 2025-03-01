using System;

namespace System.Diagnostics.CodeAnalysis
{
    /// <summary>
    /// Specifies that the attributed code should be excluded from code coverage
    /// collection.  Placing this attribute on a class/struct excludes all
    /// enclosed methods and properties from code coverage collection.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Event | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Struct,
        Inherited = false,
        AllowMultiple = false
    )]
    public sealed class ExcludeFromCodeCoverageAttribute : Attribute
    {
        public ExcludeFromCodeCoverageAttribute()
        {
        }
    }
}
                                                                                                                                                                                                                                                                             ,	                     E	                     u	                     �	                     �	                     �	                     5
                     <
                     B
                     I
                      .L.str .L.str.1 .L.str.10 .L.str.11 .L.str.12 .L.str.13 .L.str.14 .L.str.15 .L.str.16 .L.str.17 .L.str.18 .L.str.19 .L.str.2 .L.str.3 .L.str.4 .L.str.5 .L.str.6 .L.str.8 .L.str.9 _ZN12_GLOBAL__N_111JSONEmitter13translateInitERKN4llvm4InitE _ZStL19piecewise_construct _ZN4llvm11raw_ostream5writeEPKcm _ZN4llvm12DenseMapBaseINS_8DenseMapINS_4json9ObjectKeyENS2_5ValueENS_12DenseMapInfoINS_9StringRefEEENS_6detail12DenseMapPairIS3_S4_EEEES3_S4_S7_SA_E10destroyAllEv _ZN4llvm24DisableABIBreakingChecksE _ZN4llvm30VerifyDisableABIBreakingChecksE _ZN4llvm4json5Value7destroyEv _ZN4llvm4json5Value8copyFromERKS1_ _ZN4llvm4json5Value8moveFromEOKS1_ _ZN4llvm4json5ValueC2ENS_9StringRefE _ZN4llvm4json5ValueC2ENSt7__cxx1112basic_stringIcSt11char_traitsIcESaIcEEE _ZN4llvm4json6ObjectixEONS0_9ObjectKeyE _ZN4llvm4json6isUTF8ENS_9StringRefEPm _ZN4llvm4json7fixUTF8B5cxx11ENS_9StringRefE _Z