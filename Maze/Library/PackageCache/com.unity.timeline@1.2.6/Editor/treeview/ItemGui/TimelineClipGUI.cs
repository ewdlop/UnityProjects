
; CHECK-NEXT: ret

  %tmp = bitcast <16 x i8> %b to <2 x i64>
  %shuffle.i.i.i = shufflevector <2 x i64> %tmp, <2 x i64> undef, <1 x i32> <i32 1>
  %tmp1 = bitcast <1 x i64> %shuffle.i.i.i to <8 x i8>
  %tmp2 = bitcast <16 x i8> %c to <2 x i64>
  %shuffle.i3.i.i = shufflevector <2 x i64> %tmp2, <2 x i64> undef, <1 x i32> <i32 1>
  %tmp3 = bitcast <1 x i64> %shuffle.i3.i.i to <8 x i8>
  %vmull.i.i.i = tail call <8 x i16> @llvm.aarch64.neon.smull.v8i16(<8 x i8> %tmp1, <8 x i8> %tmp3) nounwind
  %add.i = add <8 x i16> %vmull.i.i.i, %a
  ret <8 x i16> %add.i
}

define <4 x i32> @bar1(<4 x i32> %a, <8 x i16> %b, <8 x i16> %c) nounwind {
; CHECK-LABEL: bar1:
; CHECK: smlal2.4s v0, v1, v2
; CHECK-NEXT: ret

  %tmp = bitcast <8 x i16> %b to <2 x i64>
  %shuffle.i.i.i = shufflevector <2 x i64> %tmp, <2 x i64> undef, <1 x i32> <i32 1>
  %tmp1 = bitcast <1 x i64> %shuffle.i.i.i to <4 x i16>
  %tmp2 = bitcast <8 x i16> %c to <2 x i64>
  %shuffle.i3.i.i = shufflevector <2 x i64> %tmp2, <2 x i64> undef, <1 x i32> <i32 1>
  %tmp3 = bitcast <1 x i64> %shuffle.i3.i.i to <4 x i16>
  %vmull2.i.i.i = tail call <4 x i32> @llvm.aarch64.neon.smull.v4i32(<4 x i16> %tmp1, <4 x i16> %tmp3) nounwind
  %add.i = add <4 x i32> %vmull2.i.i.i, %a
  ret <4 x i32> %add.i
}

define <2 x i64> @bar2(<2 x i64> %a, <4 x i32> %b, <4 x i32> %c) nounwind {
; CHECK-LABEL: bar2:
; CHECK: smlal2.2d v0, v1, v2
; CHECK-NEXT: ret

  %tmp = bitcast <4 x i32> %b to <2 x i64>
  %shuffle.i.i.i = shufflevector <2 x i64> %tmp, <2 x i64> undef, <1 x i32> <i32 1>
  %tmp1 = bitcast <1 x i64> %shuffle.i.i.i to <2 x i32>
  %tmp2 = bitcast <4 x i32> %c to <2 x i64>
  %shuffle.i3.i.i = shufflevector <2 x i64> %tmp2, <2 x i64> undef, <1 x i32> <i32 1>
  %tmp3 = bitcast <1 x i64> %shuffle.i3.i.i to <2 x i32>
  %vmull2.i.i.i = tail call <2 x i64> @llvm.aarch64.neon.smull.v2i64(<2 x i32> %tmp1, <2 x i32> %tmp3) nounwind
  %add.i = add <2 x i64> %vmull2.i.i.i, %a
  ret <2 x i64> %add.i
}

define <8 x i16> @bar3(<8 x i16> %a, <16 x i8> %b, <16 x i8> %c) nounwind {
; CHECK-LABEL: bar3:
; CHECK: umlal2.8h v0, v1, v2
; CHECK-NEXT: ret

  %tmp = bitcast <16 x i8> %b to <2 x i64>
  %shuffle.i.i.i = shufflevector <2 x i64> %tmp, <2 x i64> undef, <1 x i32> <i32 1>
  %tmp1 = bitcast <1 x i64> %shuffle.i.i.i to <8 x i8>
  %tmp2 = bitcast <16 x i8> %c to <2 x i64>
  %shuffle.i3.i.i = shufflevector <2 x i64> %tmp2, <2 x i64> undef, <1 x i32> <i32 1>
  %tmp3 = bitcast <1 x i64> %shuffle.i3.i.i to <8 x i8>
  %vmull.i.i.i = tail call <8 x i16> @llvm.aarch64.neon.umull.v8i16(<8 x i8> %tmp1, <8 x i8> %tmp3) nounwind
  %add.i = add <8 x i16> %vmull.i.i.i, %a
  ret <8 x i16> %add.i
}

define <4 x i32> @bar4(<4 x i32> %a, <8 x i16> %b, <8 x i16> %c) nounwind {
; CHECK-LABEL: bar4:
; CHECK: umlal2.4s v0, v1, v2
; CHECK-NEXT: ret

  %tmp = bitcast <8 x i16> %b to <2 x i64>
  %shuffle.i.i.i = shufflevector <2 x i64> %tmp, <2 x i64> undef, <1 x i32> <i32 1>
  %tmp1 = bitcast <1 x i64> %shuffle.i.i.i to <4 x i16>
  %tmp2 = bitcast <8 x i16> %c to <2 x i64>
  %shuffle.i3.i.i = shufflevector <2 x i64> %tmp2, <2 x i64> undef, <1 x i32> <i32 1>
  %tmp3 = bitcast <1 x i64> %shuffle.i3.i.i to <4 x i16>
  %vmull2.i.i.i = tail call <4 x i32> @llvm.aarch64.neon.umull.v4i32(<4 x i16> %tmp1, <4 x i16> %tmp3) nounwind
  %add.i = add <4 x i32> %vmull2.i.i.i, %a
  ret <4 x i32> %add.i
}

define <2 x i64> @bar5(<2 x i64> %a, <4 x i32> %b, <4 x i32> %c) nounwind {
; CHECK-LABEL: bar5:
; CHECK: umlal2.2d v0, v1, v2
; CHECK-NEXT: ret

  %tmp = bitcast <4 x i32> %b to <2 x i64>
  %shuffle.i.i.i = shufflevector <2 x i64> %tmp, <2 x i64> undef, <1 x i32> <i32 1>
  %tmp1 = bitcast <1 x i64> %shuffle.i.i.i to <2 x i32>
  %tmp2 = bitcast <4 x i32> %c to <2 x i64>
  %shuffle.i3.i.i = shufflevector <2 x i64> %tmp2, <2 x i64> undef, <1 x i32> <i32 1>
  %tmp3 = bitcast <1 x i64> %shuffle.i3.i.i to <2 x i32>
  %vmull2.i.i.i = tail call <2 x i64> @llvm.aarch64.neon.umull.v2i64(<2 x i32> %tmp1, <2 x i32> %tmp3) nounwind
  %add.i = add <2 x i64> %vmull2.i.i.i, %a
  ret <2 x i64> %add.i
}

define <4 x i32> @mlal2_1(<4 x i32> %a, <8 x i16> %b, <4 x i16> %c) nounwind {
; CHECK-LABEL: mlal2_1:
; CHECK: smlal2.4s v0, v1, v2[3]
; CHECK-NEXT: ret
  %shuffle = shufflevector <4 x i16> %c, <4 x i16> undef, <8 x i32> <i32 3, i32 3, i32 3, i32 3, i32 3, i32 3, i32 3, i32 3>
  %tmp = bitcast <8 x i16> %b to <2 x i64>
  %shuffle.i.i = shufflevector <2 x i64> %tmp, <2 x i64> undef, <1 x i32> <i32 1>
  %tmp1 = bitcast <1 x i64> %shuffle.i.i to <4 x i16>
  %tmp2 = bitcast <8 x i16> %shuffle to <2 x i64>
  %shuffle.i3.i = shufflevector <2 x i64> %tmp2, <2 x i64> undef, <1 x i32> <i32 1>
  %tmp3 = bitcast <1 x i64> %shuffle.i3.i to <4 x i16>
  %vmull2.i.i = tail call <4 x i32> @llvm.aarch64.neon.smull.v4i32(<4 x i16> %tmp1, <4 x i16> %tmp3) nounwind
  %add = add <4 x i32> %vmull2.i.i, %a
  ret <4 x i32> %add
}

define <2 x i64> @mlal2_2(<2 x i64> %a, <4 x i32> %b, <2 x i32> %c) nounwind {
; CHECK-LABEL: mlal2_2:
; CHECK: smlal2.2d v0, v1, v2[1]
; CHECK-NEXT: ret
  %shuffle = shufflevector <2 x i32> %c, <2 x i32> undef, <4 x i32> <i32 1, i32 1, i32 1, i32 1>
  %tmp = bitcast <4 x i32> %b to <2 x i64>
  %shuffle.i.i = shufflevector <2 x i64> %tmp, <2 x i64> undef, <1 x i32> <i32 1>
  %tmp1 = bitcast <1 x i64> %shuffle.i.i to <2 x i32>
  %tmp2 = bitcast <4 x i32> %shuffle to <2 x i64>
  %shuffle.i3.i = shufflevector <2 x i64> %tmp2, <2 x i64> undef, <1 x i32> <i32 1>
  %tmp3 = bitcast <1 x i64> %shuffle.i3.i to <2 x i32>
  %vmull2.i.i = tail call <2 x i64> @llvm.aarch64.neon.smull.v2i64(<2 x i32> %tmp1, <2 x i32> %tmp3) nounwind
  %add = add <2 x i64> %vmull2.i.i, %a
  ret <2 x i64> %add
}

define <4 x i32> @mlal2_4(<4 x i32> %a, <8 x i16> %b, <4 x i16> %c) nounwind {
; CHECK-LABEL: mlal2_4:
; CHECK: umlal2.4s v0, v1, v2[2]
; CHECK-NEXT: ret

  %shuffle = shufflevector <4 x i16> %c, <4 x i16> undef, <8 x i32> <i32 2, i32 2, i32 2, i32 2, i32 2, i32 2, i32 2, i32 2>
  %tmp = bitcast <8 x i16> %b to <2 x i64>
  %shuffle.i.i = shufflevector <2 x i64> %tmp, <2 x i64> undef, <1 x i32> <i32 1>
  %tmp1 = bitcast <1 x i64> %shuffle.i.i to <4 x i16>
  %tmp2 = bitcast <8 x i16> %shuffle to <2 x i64>
  %shuffle.i3.i = shufflevector <2 x i64> %tmp2, <2 x i64> undef, <1 x i32> <i32 1>
  %tmp3 = bitcast <1 x i64> %shuffle.i3.i to <4 x i16>
  %vmull2.i.i = tail call <4 x i32> @llvm.aarch64.neon.umull.v4i32(<4 x i16> %tmp1, <4 x i16> %tmp3) nounwind
  %add = add <4 x i32> %vmull2.i.i, %a
  ret <4 x i32> %add
}

define <2 x i64> @mlal2_5(<2 x i64> %a, <4 x i32> %b, <2 x i32> %c) nounwind {
; CHECK-LABEL: mlal2_5:
; CHECK: umlal2.2d v0, v1, v2[0]
; CHECK-NEXT: ret
  %shuffle = shufflevector <2 x i32> %c, <2 x i32> undef, <4 x i32> zeroinitializer
  %tmp = bitcast <4 x i32> %b to <2 x i64>
  %shuffle.i.i = shufflevector <2 x i64> %tmp, <2 x i64> undef, <1 x i32> <i32 1>
  %tmp1 = bitcast <1 x i64> %shuffle.i.i to <2 x i32>
  %tmp2 = bitcast <4 x i32> %shuffle to <2 x i64>
  %shuffle.i3.i = shufflevector <2 x i64> %tmp2, <2 x i64> undef, <1 x i32> <i32 1>
  %tmp3 = bitcast <1 x i64> %shuffle.i3.i to <2 x i32>
  %vmull2.i.i = tail call <2 x i64> @llvm.aarch64.neon.umull.v2i64(<2 x i32> %tmp1, <2 x i32> %tmp3) nounwind
  %add = add <2 x i64> %vmull2.i.i, %a
  ret <2 x i64> %add
}

; rdar://12328502
define <2 x double> @vmulq_n_f64(<2 x double> %x, double %y) nounwind readnone ssp {
entry:
; CHECK-LABEL: vmulq_n_f64:
; CHECK-NOT: dup.2d
; CHECK: fmul.2d v0, v0, v1[0]
  %vecinit.i = insertelement <2 x double> undef, double %y, i32 0
  %vecinit1.i = insertelement <2 x double> %vecinit.i, double %y, i32 1
  %mul.i = fmul <2 x double> %vecinit1.i, %x
  ret <2 x double> %mul.i
}

define <4 x float> @vmulq_n_f32(<4 x float> %x, float %y) nounwind readnone ssp {
entry:
; CHECK-LABEL: vmulq_n_f32:
; CHECK-NOT: dup.4s
; CHECK: fmul.4s v0, v0, v1[0]
  %vecinit.i = insertelement <4 x float> undef, float %y, i32 0
  %vecinit1.i = insertelement <4 x float> %vecinit.i, float %y, i32 1
  %vecinit2.i = insertelement <4 x float> %vecinit1.i, float %y, i32 2
  %vecinit3.i = insertelement <4 x float> %vecinit2.i, float %y, i32 3
  %mul.i = fmul <4 x float> %vecinit3.i, %x
  ret <4 x float> %mul.i
}

define <2 x float> @vmul_n_f32(<2 x float> %x, float %y) nounwind readnone ssp {
entry:
; CHECK-LABEL: vmul_n_f32:
; CHECK-NOT: dup.2s
; CHECK: fmul.2s v0, v0, v1[0]
  %vecinit.i = insertelement <2 x float> undef, float %y, i32 0
  %vecinit1.i = insertelement <2 x float> %vecinit.i, float %y, i32 1
  %mul.i = fmul <2 x float> %vecinit1.i, %x
  ret <2 x float> %mul.i
}

define <4 x i16> @vmla_laneq_s16_test(<4 x i16> %a, <4 x i16> %b, <8 x i16> %c) nounwind readnone ssp {
entry:
; CHECK: vmla_laneq_s16_test
; CHECK-NOT: ext
; CHECK: mla.4h v0, v1, v2[6]
; CHECK-NEXT: ret
  %shuffle = shufflevector <8 x i16> %c, <8 x i16> undef, <4 x i32> <i32 6, i32 6, i32 6, i32 6>
  %mul = mul <4 x i16> %shuffle, %b
  %add = add <4 x i16> %mul, %a
  ret <4 x i16> %add
}

define <2 x i32> @vmla_laneq_s32_test(<2 x i32> %a, <2 x i32> %b, <4 x i32> %c) nounwind readnone ssp {
entry:
; CHECK: vmla_laneq_s32_test
; CHECK-NOT: ext
; CHECK: mla.2s v0, v1, v2[3]
; CHECK-NEXT: ret
  %shuffle = shufflevector <4 x i32> %c, <4 x i32> undef, <2 x i32> <i32 3, i32 3>
  %mul = mul <2 x i32> %shuffle, %b
  %add = add <2 x i32> %mul, %a
  ret <2 x i32> %add
}

define <8 x i16> @not_really_vmlaq_laneq_s16_test(<8 x i16> %a, <8 x i16> %b, <8 x i16> %c) nounwind readnone ssp {
entry:
; CHECK: not_really_vmlaq_laneq_s16_test
; CHECK-NOT: ext
; CHECK: mla.8h v0, v1, v2[5]
; CHECK-NEXT: ret
  %shuffle1 = shufflevector <8 x i16> %c, <8 x i16> undef, <4 x i32> <i32 4, i32 5, i32 6, i32 7>
  %shuffle2 = shufflevector <4 x i16> %shuffle1, <4 x i16> undef, <8 x i32> <i32 1, i32 1, i32 1, i32 1, i32 1, i32 1, i32 1, i32 1>
  %mul = mul <8 x i16> %shuffle2, %b
  %add = add <8 x i16> %mul, %a
  ret <8 x i16> %add
}

define <4 x i32> @not_really_vmlaq_laneq_s32_test(<4 x i32> %a, <4 x i32> %b, <4 x i32> %c) nounwind readnone ssp {
entry:
; CHECK: not_really_vmlaq_laneq_s32_test
; CHECK-NOT: ext
; CHECK: mla.4s v0, v1, v2[3]
; CHECK-NEXT: ret
  %shuffle1 = shufflevector <4 x i32> %c, <4 x i32> undef, <2 x i32> <i32 2, i32 3>
  %shuffle2 = shufflevector <2 x i32> %shuffle1, <2 x i32> undef, <4 x i32> <i32 1, i32 1, i32 1, i32 1>
  %mul = mul <4 x i32> %shuffle2, %b
  %add = add <4 x i32> %mul, %a
  ret <4 x i32> %add
}

define <4 x i32> @vmull_laneq_s16_test(<4 x i16> %a, <8 x i16> %b) nounwind readnone ssp {
entry:
; CHECK: vmull_laneq_s16_test
; CHECK-NOT: ext
; CHECK: smull.4s v0, v0, v1[6]
; CHECK-NEXT: ret
  %shuffle = shufflevector <8 x i16> %b, <8 x i16> undef, <4 x i32> <i32 6, i32 6, i32 6, i32 6>
  %vmull2.i = tail call <4 x i32> @llvm.aarch64.neon.smull.v4i32(<4 x i16> %a, <4 x i16> %shuffle) #2
  ret <4 x i32> %vmull2.i
}

define <2 x i64> @vmull_laneq_s32_test(<2 x i32> %a, <4 x i32> %b) nounwind readnone ssp {
entry:
; CHECK: vmull_laneq_s32_test
; CHECK-NOT: ext
; CHECK: smull.2d v0, v0, v1[2]
; CHECK-NEXT: ret
  %shuffle = shufflevector <4 x i32> %b, <4 x i32> undef, <2 x i32> <i32 2, i32 2>
  %vmull2.i = tail call <2 x i64> @llvm.aarch64.neon.smull.v2i64(<2 x i32> %a, <2 x i32> %shuffle) #2
  ret <2 x i64> %vmull2.i
}
define <4 x i32> @vmull_laneq_u16_test(<4 x i16> %a, <8 x i16> %b) nounwind readnone ssp {
entry:
; CHECK: vmull_laneq_u16_test
; CHECK-NOT: ext
; CHECK: umull.4s v0, v0, v1[6]
; CHECK-NEXT: ret
  %shuffle = shufflevector <8 x i16> %b, <8 x i16> undef, <4 x i32> <i32 6, i32 6, i32 6, i32 6>
  %vmull2.i = tail call <4 x i32> @llvm.aarch64.neon.umull.v4i32(<4 x i16> %a, <4 x i16> %shuffle) #2
  ret <4 x i32> %vmull2.i
}

define <2 x i64> @vmull_laneq_u32_test(<2 x i32> %a, <4 x i32> %b) nounwind readnone ssp {
entry:
; CHECK: vmull_laneq_u32_test
; CHECK-NOT: ext
; CHECK: umull.2d v0, v0, v1[2]
; CHECK-NEXT: ret
  %shuffle = shufflevector <4 x i32> %b, <4 x i32> undef, <2 x i32> <i32 2, i32 2>
  %vmull2.i = tail call <2 x i64> @llvm.aarch64.neon.umull.v2i64(<2 x i32> %a, <2 x i32> %shuffle) #2
  ret <2 x i64> %vmull2.i
}

define <4 x i32> @vmull_high_n_s16_test(<4 x i32> %a, <8 x i16> %b, <4 x i16> %c, i32 %d) nounwind readnone optsize ssp {
entry:
; CHECK: vmull_high_n_s16_test
; CHECK-NOT: ext
; CHECK: smull2.4s
; CHECK-NEXT: ret
  %conv = trunc i32 %d to i16
  %0 = bitcast <8 x i16> %b to <2 x i64>
  %shuffle.i.i = shufflevector <2 x i64> %0, <2 x i64> undef, <1 x i32> <i32 1>
  %1 = bitcast <1 x i64> %shuffle.i.i to <4 x i16>
  %vecinit.i = insertelement <4 x i16> undef, i16 %conv, i32 0
  %vecinit1.i = insertelement <4 x i16> %vecinit.i, i16 %conv, i32 1
  %vecinit2.i = insertelement <4 x i16> %vecinit1.i, i16 %conv, i32 2
  %vecinit3.i = insertelement <4 x i16> %vecinit2.i, i16 %conv, i32 3
  %vmull2.i.i = tail call <4 x i32> @llvm.aarch64.neon.smull.v4i32(<4 x i16> %1, <4 x i16> %vecinit3.i) nounwind
  ret <4 x i32> %vmull2.i.i
}

define <2 x i64> @vmull_high_n_s32_test(<2 x i64> %a, <4 x i32> %b, <2 x i32> %c, i32 %d) nounwind readnone optsize ssp {
entry:
; CHECK: vmull_high_n_s32_test
; CHECK-NOT: ext
; CHECK: smull2.2d
; CHECK-NEXT: ret
  %0 = bitcast <4 x i32> %b to <2 x i64>
  %shuffle.i.i = shufflevector <2 x i64> %0, <2 x i64> undef, <1 x i32> <i32 1>
  %1 = bitcast <1 x i64> %shuffle.i.i to <2 x i32>
  %vecinit.i = insertelement <2 x i32> undef, i32 %d, i32 0
  %vecinit1.i = insertelement <2 x i32> %vecinit.i, i32 %d, i32 1
  %vmull2.i.i = tail call <2 x i64> @llvm.aarch64.neon.smull.v2i64(<2 x i32> %1, <2 x i32> %vecinit1.i) nounwind
  ret <2 x i64> %vmull2.i.i
}

define <4 x i32> @vmull_high_n_u16_test(<4 x i32> %a, <8 x i16> %b, <4 x i16> %c, i32 %d) nounwind readnone optsize ssp {
entry:
; CHECK: vmull_high_n_u16_test
; CHECK-NOT: ext
; CHECK: umull2.4s
; CHECK-NEXT: ret
  %conv = trunc i32 %d to i16
  %0 = bitcast <8 x i16> %b to <2 x i64>
  %shuffle.i.i = shufflevector <2 x i64> %0, <2 x i64> undef, <1 x i32> <i32 1>
  %1 = bitcast <1 x i64> %shuffle.i.i to <4 x i16>
  %vecinit.i = insertelement <4 x i16> undef, i16 %conv, i32 0
  %vecinit1.i = insertelement <4 x i16> %vecinit.i, i16 %conv, i32 1
  %vecinit2.i = insertelement <4 x i16> %vecinit1.i, i16 %conv, i32 2
  %vecinit3.i = insertelement <4 x i16> %vecinit2.i, i16 %conv, i32 3
  %vmull2.i.i = tail call <4 x i32> @llvm.aarch64.neon.umull.v4i32(<4 x i16> %1, <4 x i16> %vecinit3.i) nounwind
  ret <4 x i32> %vmull2.i.i
}

define <2 x i64> @vmull_high_n_u32_test(<2 x i64> %a, <4 x i32> %b, <2 x i32> %c, i32 %d) nounwind readnone optsize ssp {
entry:
; CHECK: vmull_high_n_u32_test
; CHECK-NOT: ext
; CHECK: umull2.2d
; CHECK-NEXT: ret
  %0 = bitcast <4 x i32> %b to <2 x i64>
  %shuffle.i.i = shufflevector <2 x i64> %0, <2 x i64> undef, <1 x i32> <i32 1>
  %1 = bitcast <1 x i64> %shuffle.i.i to <2 x i32>
  %vecinit.i = insertelement <2 x i32> undef, i32 %d, i32 0
  %vecinit1.i = insertelement <2 x i32> %vecinit.i, i32 %d, i32 1
  %vmull2.i.i = tail call <2 x i64> @llvm.aarch64.neon.umull.v2i64(<2 x i32> %1, <2 x i32> %vecinit1.i) nounwind
  ret <2 x i64> %vmull2.i.i
}

define <4 x i32> @vmul_built_dup_test(<4 x i32> %a, <4 x i32> %b) {
; CHECK-LABEL: vmul_built_dup_test:
; CHECK-NOT: ins
; CHECK-NOT: dup
; CHECK: mul.4s {{v[0-9]+}}, {{v[0-9]+}}, {{v[0-9]+}}[1]
  %vget_lane = extractelement <4 x i32> %b, i32 1
  %vecinit.i = insertelement <4 x i32> undef, i32 %vget_lane, i32 0
  %vecinit1.i = insertelement <4 x i32> %vecinit.i, i32 %vget_lane, i32 1
  %vecinit2.i = insertelement <4 x i32> %vecinit1.i, i32 %vget_lane, i32 2
  %vecinit3.i = insertelement <4 x i32> %vecinit2.i, i32 %vget_lane, i32 3
  %prod = mul <4 x i32> %a, %vecinit3.i
  ret <4 x i32> %prod
}

define <4 x i16> @vmul_built_dup_fromsmall_test(<4 x i16> %a, <4 x i16> %b) {
; CHECK-LABEL: vmul_built_dup_fromsmall_test:
; CHECK-NOT: ins
; CHECK-NOT: dup
; CHECK: mul.4h {{v[0-9]+}}, {{v[0-9]+}}, {{v[0-9]+}}[3]
  %vget_lane = extractelement <4 x i16> %b, i32 3
  %vecinit.i = insertelement <4 x i16> undef, i16 %vget_lane, i32 0
  %vecinit1.i = insertelement <4 x i16> %vecinit.i, i16 %vget_lane, i32 1
  %vecinit2.i = insertelement <4 x i16> %vecinit1.i, i16 %vget_lane, i32 2
  %vecinit3.i = insertelement <4 x i16> %vecinit2.i, i16 %vget_lane, i32 3
  %prod = mul <4 x i16> %a, %vecinit3.i
  ret <4 x i16> %prod
}

define <8 x i16> @vmulq_built_dup_fromsmall_test(<8 x i16> %a, <4 x i16> %b) {
; CHECK-LABEL: vmulq_built_dup_fromsmall_test:
; CHECK-NOT: ins
; CHECK-NOT: dup
; CHECK: mul.8h {{v[0-9]+}}, {{v[0-9]+}}, {{v[0-9]+}}[0]
  %vget_lane = extractelement <4 x i16> %b, i32 0
  %vecinit.i = insertelement <8 x i16> undef, i16 %vget_lane, i32 0
  %vecinit1.i = insertelement <8 x i16> %vecinit.i, i16 %vget_lane, i32 1
  %vecinit2.i = insertelement <8 x i16> %vecinit1.i, i16 %vget_lane, i32 2
  %vecinit3.i = insertelement <8 x i16> %vecinit2.i, i16 %vget_lane, i32 3
  %vecinit4.i = insertelement <8 x i16> %vecinit3.i, i16 %vget_lane, i32 4
  %vecinit5.i = insertelement <8 x i16> %vecinit4.i, i16 %vget_lane, i32 5
  %vecinit6.i = insertelement <8 x i16> %vecinit5.i, i16 %vget_lane, i32 6
  %vecinit7.i = insertelement <8 x i16> %vecinit6.i, i16 %vget_lane, i32 7
  %prod = mul <8 x i16> %a, %vecinit7.i
  ret <8 x i16> %prod
}

define <2 x i64> @mull_from_two_extracts(<4 x i32> %lhs, <4 x i32> %rhs) {
; CHECK-LABEL: mull_from_two_extracts:
; CHECK-NOT: ext
; CHECK: sqdmull2.2d

  %lhs.high = shufflevector <4 x i32> %lhs, <4 x i32> undef, <2 x i32> <i32 2, i32 3>
  %rhs.high = shufflevector <4 x i32> %rhs, <4 x i32> undef, <2 x i32> <i32 2, i32 3>

  %res = tail call <2 x i64> @llvm.aarch64.neon.sqdmull.v2i64(<2 x i32> %lhs.high, <2 x i32> %rhs.high) nounwind
  ret <2 x i64> %res
}

define <2 x i64> @mlal_from_two_extracts(<2 x i64> %accum, <4 x i32> %lhs, <4 x i32> %rhs) {
; CHECK-LABEL: mlal_from_two_extracts:
; CHECK-NOT: ext
; CHECK: sqdmlal2.2d

  %lhs.high = shufflevector <4 x i32> %lhs, <4 x i32> undef, <2 x i32> <i32 2, i32 3>
  %rhs.high = shufflevector <4 x i32> %rhs, <4 x i32> undef, <2 x i32> <i32 2, i32 3>

  %res = tail call <2 x i64> @llvm.aarch64.neon.sqdmull.v2i64(<2 x i32> %lhs.high, <2 x i32> %rhs.high) nounwind
  %sum = call <2 x i64> @llvm.aarch64.neon.sqadd.v2i64(<2 x i64> %accum, <2 x i64> %res)
  ret <2 x i64> %sum
}

define <2 x i64> @mull_from_extract_dup(<4 x i32> %lhs, i32 %rhs) {
; CHECK-LABEL: mull_from_extract_dup:
; CHECK-NOT: ext
; CHECK: sqdmull2.2d
  %rhsvec.tmp = insertelement <2 x i32> undef, i32 %rhs, i32 0
  %rhsvec = insertelement <2 x i32> %rhsvec.tmp, i32 %rhs, i32 1

  %lhs.high = shufflevector <4 x i32> %lhs, <4 x i32> undef, <2 x i32> <i32 2, i32 3>

  %res = tail call <2 x i64> @llvm.aarch64.neon.sqdmull.v2i64(<2 x i32> %lhs.high, <2 x i32> %rhsvec) nounwind
  ret <2 x i64> %res
}

define <8 x i16> @pmull_from_extract_dup(<16 x i8> %lhs, i8 %rhs) {
; CHECK-LABEL: pmull_from_extract_dup:
; CHECK-NOT: ext
; CHECK: pmull2.8h
  %rhsvec.0 = insertelement <8 x i8> undef, i8 %rhs, i32 0
  %rhsvec = shufflevector <8 x i8> %rhsvec.0, <8 x i8> undef, <8 x i32> <i32 0, i32 0, i32 0, i32 0, i32 0, i32 0, i32 0, i32 0>

  %lhs.high = shufflevector <16 x i8> %lhs, <16 x i8> undef, <8 x i32> <i32 8, i32 9, i32 10, i32 11, i32 12, i32 13, i32 14, i32 15>

  %res = tail call <8 x i16> @llvm.aarch64.neon.pmull.v8i16(<8 x i8> %lhs.high, <8 x i8> %rhsvec) nounwind
  ret <8 x i16> %res
}

define <8 x i16> @pmull_from_extract_duplane(<16 x i8> %lhs, <8 x i8> %rhs) {
; CHECK-LABEL: pmull_from_extract_duplane:
; CHECK-NOT: ext
; CHECK: pmull2.8h

  %lhs.high = shufflevector <16 x i8> %lhs, <16 x i8> undef, <8 x i32> <i32 8, i32 9, i32 10, i32 11, i32 12, i32 13, i32 14, i32 15>
  %rhs.high = shufflevector <8 x i8> %rhs, <8 x i8> undef, <8 x i32> <i32 0, i32 0, i32 0, i32 0, i32 0, i32 0, i32 0, i32 0>

  %res = tail call <8 x i16> @llvm.aarch64.neon.pmull.v8i16(<8 x i8> %lhs.high, <8 x i8> %rhs.high) nounwind
  ret <8 x i16> %res
}

define <2 x i64> @sqdmull_from_extract_duplane(<4 x i32> %lhs, <4 x i32> %rhs) {
; CHECK-LABEL: sqdmull_from_extract_duplane:
; CHECK-NOT: ext
; CHECK: sqdmull2.2d

  %lhs.high = shufflevector <4 x i32> %lhs, <4 x i32> undef, <2 x i32> <i32 2, i32 3>
  %rhs.high = shufflevector <4 x i32> %rhs, <4 x i32> undef, <2 x i32> <i32 0, i32 0>

  %res = tail call <2 x i64> @llvm.aarch64.neon.sqdmull.v2i64(<2 x i32> %lhs.high, <2 x i32> %rhs.high) nounwind
  ret <2 x i64> %res
}

define <2 x i64> @sqdmlal_from_extract_duplane(<2 x i64> %accum, <4 x i32> %lhs, <4 x i32> %rhs) {
; CHECK-LABEL: sqdmlal_from_extract_duplane:
; CHECK-NOT: ext
; CHECK: sqdmlal2.2d

  %lhs.high = shufflevector <4 x i32> %lhs, <4 x i32> undef, <2 x i32> <i32 2, i32 3>
  %rhs.high = shufflevector <4 x i32> %rhs, <4 x i32> undef, <2 x i32> <i32 0, i32 0>

  %res = tail call <2 x i64> @llvm.aarch64.neon.sqdmull.v2i64(<2 x i32> %lhs.high, <2 x i32> %rhs.high) nounwind
  %sum = call <2 x i64> @llvm.aarch64.neon.sqadd.v2i64(<2 x i64> %accum, <2 x i64> %res)
  ret <2 x i64> %sum
}

define <2 x i64> @umlal_from_extract_duplane(<2 x i64> %accum, <4 x i32> %lhs, <4 x i32> %rhs) {
; CHECK-LABEL: umlal_from_extract_duplane:
; CHECK-NOT: ext
; CHECK: umlal2.2d

  %lhs.high = shufflevector <4 x i32> %lhs, <4 x i32> undef, <2 x i32> <i32 2, i32 3>
  %rhs.high = shufflevector <4 x i32> %rhs, <4 x i32> undef, <2 x i32> <i32 0, i32 0>

  %res = tail call <2 x i64> @llvm.aarch64.neon.umull.v2i64(<2 x i32> %lhs.high, <2 x i32> %rhs.high) nounwind
  %sum = add <2 x i64> %accum, %res
  ret <2 x i64> %sum
}

define float @scalar_fmla_from_extract_v4f32(float %accum, float %lhs, <4 x float> %rvec) {
; CHECK-LABEL: scalar_fmla_from_extract_v4f32:
; CHECK: fmla.s s0, s1, v2[3]
  %rhs = extractelement <4 x float> %rvec, i32 3
  %res = call float @llvm.fma.f32(float %lhs, float %rhs, float %accum)
  ret float %res
}

define float @scalar_fmla_from_extract_v2f32(float %accum, float %lhs, <2 x float> %rvec) {
; CHECK-LABEL: scalar_fmla_from_extract_v2f32:
; CHECK: fmla.s s0, s1, v2[1]
  %rhs = extractelement <2 x float> %rvec, i32 1
  %res = call float @llvm.fma.f32(float %lhs, float %rhs, float %accum)
  ret float %res
}

define float @scalar_fmls_from_extract_v4f32(float %accum, float %lhs, <4 x float> %rvec) {
; CHECK-LABEL: scalar_fmls_from_extract_v4f32:
; CHECK: fmls.s s0, s1, v2[3]
  %rhs.scal = extractelement <4 x float> %rvec, i32 3
  %rhs = fsub float -0.0, %rhs.scal
  %res = call float @llvm.fma.f32(float %lhs, float %rhs, float %accum)
  ret float %res
}

define float @scalar_fmls_from_extract_v2f32(float %accum, float %lhs, <2 x float> %rvec) {
; CHECK-LABEL: scalar_fmls_from_extract_v2f32:
; CHECK: fmls.s s0, s1, v2[1]
  %rhs.scal = extractelement <2 x float> %rvec, i32 1
  %rhs = fsub float -0.0, %rhs.scal
  %res = call float @llvm.fma.f32(float %lhs, float %rhs, float %accum)
  ret float %res
}

declare float @llvm.fma.f32(float, float, float)

define double @scalar_fmla_from_extract_v2f64(double %accum, double %lhs, <2 x double> %rvec) {
; CHECK-LABEL: scalar_fmla_from_extract_v2f64:
; CHECK: fmla.d d0, d1, v2[1]
  %rhs = extractelement <2 x double> %rvec, i32 1
  %res = call double @llvm.fma.f64(double %lhs, double %rhs, double %accum)
  ret double %res
}

define double @scalar_fmls_from_extract_v2f64(double %accum, double %lhs, <2 x double> %rvec) {
; CHECK-LABEL: scalar_fmls_from_extract_v2f64:
; CHECK: fmls.d d0, d1, v2[1]
  %rhs.scal = extractelement <2 x double> %rvec, i32 1
  %rhs = fsub double -0.0, %rhs.scal
  %res = call double @llvm.fma.f64(double %lhs, double %rhs, double %accum)
  ret double %res
}

declare double @llvm.fma.f64(double, double, double)

define <2 x float> @fmls_with_fneg_before_extract_v2f32(<2 x float> %accum, <2 x float> %lhs, <4 x float> %rhs) {
; CHECK-LABEL: fmls_with_fneg_before_extract_v2f32:
; CHECK: fmls.2s v0, v1, v2[3]
  %rhs_neg = fsub <4 x float> <float -0.0, float -0.0, float -0.0, float -0.0>, %rhs
  %splat = shufflevector <4 x float> %rhs_neg, <4 x float> undef, <2 x i32> <i32 3, i32 3>
  %res = call <2 x float> @llvm.fma.v2f32(<2 x float> %lhs, <2 x float> %splat, <2 x float> %accum)
  ret <2 x float> %res
}

define <2 x float> @fmls_with_fneg_before_extract_v2f32_1(<2 x float> %accum, <2 x float> %lhs, <2 x float> %rhs) {
; CHECK-LABEL: fmls_with_fneg_before_extract_v2f32_1:
; CHECK: fmls.2s v0, v1, v2[1]
  %rhs_neg = fsub <2 x float> <float -0.0, float -0.0>, %rhs
  %splat = shufflevector <2 x float> %rhs_neg, <2 x float> undef, <2 x i32> <i32 1, i32 1>
  %res = call <2 x float> @llvm.fma.v2f32(<2 x float> %lhs, <2 x float> %splat, <2 x float> %accum)
  ret <2 x float> %res
}

define <4 x float> @fmls_with_fneg_before_extract_v4f32(<4 x float> %accum, <4 x float> %lhs, <4 x float> %rhs) {
; CHECK-LABEL: fmls_with_fneg_before_extract_v4f32:
; CHECK: fmls.4s v0, v1, v2[3]
  %rhs_neg = fsub <4 x float> <float -0.0, float -0.0, float -0.0, float -0.0>, %rhs
  %splat = shufflevector <4 x float> %rhs_neg, <4 x float> undef, <4 x i32> <i32 3, i32 3, i32 3, i32 3>
  %res = call <4 x float> @llvm.fma.v4f32(<4 x float> %lhs, <4 x float> %splat, <4 x float> %accum)
  ret <4 x float> %res
}

define <4 x float> @fmls_with_fneg_before_extract_v4f32_1(<4 x float> %accum, <4 x float> %lhs, <2 x float> %rhs) {
; CHECK-LABEL: fmls_with_fneg_before_extract_v4f32_1:
; CHECK: fmls.4s v0, v1, v2[1]
  %rhs_neg = fsub <2 x float> <float -0.0, float -0.0>, %rhs
  %splat = shufflevector <2 x float> %rhs_neg, <2 x float> undef, <4 x i32> <i32 1, i32 1, i32 1, i32 1>
  %res = call <4 x float> @llvm.fma.v4f32(<4 x float> %lhs, <4 x float> %splat, <4 x float> %accum)
  ret <4 x float> %res
}

define <2 x double> @fmls_with_fneg_before_extract_v2f64(<2 x double> %accum, <2 x double> %lhs, <2 x double> %rhs) {
; CHECK-LABEL: fmls_with_fneg_before_extract_v2f64:
; CHECK: fmls.2d v0, v1, v2[1]
  %rhs_neg = fsub <2 x double> <double -0.0, double -0.0>, %rhs
  %splat = shufflevector <2 x double> %rhs_neg, <2 x double> undef, <2 x i32> <i32 1, i32 1>
  %res = call <2 x double> @llvm.fma.v2f64(<2 x double> %lhs, <2 x double> %splat, <2 x double> %accum)
  ret <2 x double> %res
}

define <1 x double> @test_fmul_v1f64(<1 x double> %L, <1 x double> %R) nounwind {
; CHECK-LABEL: test_fmul_v1f64:
; CHECK: fmul
  %prod = fmul <1 x double> %L, %R
  ret <1 x double> %prod
}

define <1 x double>