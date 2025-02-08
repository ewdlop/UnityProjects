ision) {
  CheckVerifiesHLSL(L"scalar-operators-exact-precision.hlsl");
}

TEST_F(VerifierTest, RunSizeof) {
  CheckVerifiesHLSL(L"sizeof.hlsl");
}

TEST_F(VerifierTest, RunString) {
  CheckVerifiesHLSL(L"string.hlsl");
}

TEST_F(VerifierTest, RunStructAssignments) {
  CheckVerifiesHLSL(L"struct-assignments.hlsl");
}

TEST_F(VerifierTest, RunSubobjects) {
  CheckVerifiesHLSL(L"subobjects-syntax.hlsl");
}

TEST_F(VerifierTest, RunIncompleteArray) {
  CheckVerifiesHLSL(L"incomp_array_err.hlsl");
}

TEST_F(VerifierTest, RunTemplateChecks) {
  CheckVerifiesHLSL(L"template-checks.hlsl");
}

TEST_F(VerifierTest, RunVarmodsSyntax) {
  CheckVerifiesHLSL(L"varmods-syntax.hlsl");
}

TEST_F(VerifierTest, RunVectorAssignments) {
  CheckVerifiesHLSL(L"vector-assignments.hlsl");
}

TEST_F(VerifierTest, RunVectorSyntaxMix) {
  CheckVerifiesHLSL(L"vector-syntax-mix.hlsl");
}

TEST_F(VerifierTest, RunVectorSyntax) {
  CheckVerifiesHLSL(L"vector-syntax.hlsl");
}

TEST_F(VerifierTest, RunVectorSyntaxExactPrecision) {
  CheckVerifiesHLSL(L"vector-syntax-exact-precision.hlsl");
}

TEST_F(VerifierTest, RunTypemodsSyntax) {
  CheckVerifiesHLSL(L"typemods-syntax.hlsl");
}

TEST_F(VerifierTest, RunSemantics) {
  CheckVerifiesHLSL(L"semantics.hlsl");
}

TEST_F(VerifierTest, RunImplicitCasts) {
  CheckVerifiesHLSL(L"implicit-casts.hlsl");
}

TEST_F(VerifierTest, RunDerivedToBaseCasts) {
  CheckVerifiesHLSL(L"deriv