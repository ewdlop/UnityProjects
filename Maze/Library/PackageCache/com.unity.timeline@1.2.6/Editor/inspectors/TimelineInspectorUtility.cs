                     �      d               2020.2.1f1 ����      ��f��!�5�9�4Q��B   �          7  �����     �            � �� �                      E �                   �  �#          �           . �,          �           5   a �                   �  �#          �           . �,          �           � �r �                   �  �#      	    �           . �,      
    �           H �� �����   �          1  �1  �����   @           �  � �                  Q  �j  �                  �  �J   ����    �           1  �1  �����    �            �  � �                     �j  �����    �            \   �  �����    �            H �r   ����    �           1  �1  �����   @            �  � �                   Q  �j  �                   H �w   ����    �           1  �1  �����   @            �  � �                   Q  �j  �                   H ��   ����    �           1  �1  �����   @            �  � �                   Q  �j  �                   y �
 �                     �  �#      !    �           . �,      "    �            ��   ����#   @          1  �1  �����$               �  � �   %               . �j  �   &               � ��   ����'    �           1  �1  �����(    �            �  � �   )                  �j  �����*    �            H ��  �����+    �           1  �1  �����,   @            �  � �   -                Q  �j  �   .                y �
 �   /                 �  �#      0    �           . �,      1    �             ��      2    @            � ��      3    @            �  �#      4    �           . �,      5    �           H ��   ����6   �           1  �1  �����7   @            �  � �   8                Q  �j  �   9                H ��   ����:   �           1  �1  �����;   @            �  � �   <                Q  �j  �   =                H ��   ����>   �           1  �1  �����?   @            �  � �   @                Q  �j  �   A              MonoImporter PPtr<EditorExtension> m_FileID m_PathID PPtr<PrefabInstance> m_ExternalObjects SourceAssetIdentifier type assembly name m_UsedFileIDs m_DefaultReferences executionOrder icon m_UserData m_AssetBundleName m_AssetBundleVariant     s    ���G��܏Z56�:!@i�J*   �       �7  �����     �            � �� �                       E �                   �  �          �           . �          �           (   a �                   �  �          �           . �          �           � �r �                   �  �      	    �           . �      
    �           H �� �����   �          1  �1  �����   @           �  � �                  Q  �j  �                  H �� �����   �           1  �1  �����   @            �  � �                   Q  �j  �                   �  �=   ����   �           1  �1  �����   �            �  � �                    �j  �����   �            H ��  �����   �           1  �1  �����   @            �  � �                   Q  �j  �                   y �
 �                   �  �          �           . �          �           y �Q                       �  �          �           . �           �           �  �X      !                H �i   ����"   �           1  �1  �����#   @            �  � �   $                Q  �j  �   %                H �u   ����&   �           1  �1  �����'   @            �  � �   (                Q  �j  �   )              PPtr<EditorExtension> m_FileID m_PathID PPtr<PrefabInstance> m_DefaultReferences m_Icon m_ExecutionOrder m_ClassName m_Namespace                       \       �y�     `       	                                                                                                                                            �y�                                                                                    PlayerTestAssemblyProvider  c  using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework.Interfaces;
using UnityEngine.TestTools.NUnitExtensions;

namespace UnityEngine.TestTools.Utils
{
    internal class PlayerTestAssemblyProvider
    {
        private IAssemblyLoadProxy m_AssemblyLoadProxy;
        private readonly List<string> m_AssembliesToLoad;

        //Cached until domain reload
        private static List<IAssemblyWrapper> m_LoadedAssemblies;

        internal PlayerTestAssemblyProvider(IAssemblyLoadProxy assemblyLoadProxy, List<string> assembliesToLoad)
        {
            m_AssemblyLoadProxy = assemblyLoadProxy;
            m_AssembliesToLoad = assembliesToLoad;
            LoadAssemblies();
        }

        public ITest GetTestsWithNUnit()
        {
            return BuildTests(TestPlatform.PlayMode, m_LoadedAssemblies.ToArray());
        }

        public List<IAssemblyWrapper> GetUserAssemblies()
        {
            return m_LoadedAssemblies;
        }

        protected static ITest BuildTests(TestPlatform testPlatform, IAssemblyWrapper[] assemblies)
        {
            var settings = UnityTestAssemblyBuilder.GetNUnitTestBuilderSettings(testPlatform);
            var builder = new UnityTestAssemblyBuilder();
            return builder.Build(assemblies.Select(a => a.Assembly).ToArray(), Enumerable.Repeat(testPlatform, assemblies.Length).ToArray(), settings);
        }

        private void LoadAssemblies()
        {
            if (m_LoadedAssemblies != null)
            {
                return;
            }

            m_LoadedAssemblies = new List<IAssemblyWrapper>();

            foreach (var userAssembly in m_AssembliesToLoad)
            {
                IAssemblyWrapper a;
                try
                {
                    a = m_AssemblyLoadProxy.Load(userAssembly);
                }
                catch (FileNotFoundException)
                {
                    continue;
                }
                if (a != null)
                    m_LoadedAssemblies.Add(a);
            }
        }
    }
}
                        PlayerTestAssemblyProvider     UnityEngine.TestTools.Utils                                                                                                                                                             � @R� `R� �R� �R� �R� �R�  S�  S� @S� `S� �S� �S� �S� �S�  T�  T� @T� `T� �T� �T� �T� �T�  U�  U� @U� `U� �U� �U� �U� �U�  V�  V� @V� `V� �V� �V� �V� �V�  W�  W� @W� `W� �W� �W� �W� �W�  X�  X� @X� `X� �X� �X� �X� �X�  Y�  Y� @Y� `Y� �Y� �Y� �Y� �Y�  Z�  Z� @Z� `Z� �Z� �Z� �Z� �Z�  [�  [� @[� `[� �[� �[� �[� �[�  \�  \� @\� `\� �\� �\� �\� �\�  ]�  ]� @]� `]� �]� �]� �]� �]�  ^�  ^� @^� `^� �^� �^� �^� �^�  _�  _� @_� `_� �_� �_� �_� �_�  `�  `� @`� ``� �`� �`� �`� �`�  a�  a� @a� `a� �a� �a� �a� �a�  b�  b� @b� `b� �b� �b� �b� �b�  c�  c� @c� `c� �c� �c� �c� �c�  d�  d� @d� `d� �d� �d� �d� �d�  e�  e� @e� `e� �e� �e� �e� �e�  f�  f� @f� `f� �f� �f� �f� �f�  g�  g� @g� `g� �g� �g� �g� �g�  h�  h� @h� `h� �h� �h� �h� �h�  i�  i� @i� `i� �i� �i� �i� �i�  j�  j� @j� `j� �j� �j� �j� �j�  k�  k� @k� `k� �k� �k� �k� �k�  l�  l� @l� `l� �l� �l� �l� �l�  m�  m� @m� `m� �m� �m� �m� �m�  n�  n� @n� `n� �n� �n� �n� �n�  o�  o� @o� `o� �o� �o� �o� �o�  p�  p� @p� `p� �p� �p� �p� �p�  q�  q� @q� `q� �q� �q� �q� �q�  r�  r� @r� `r� �r� �r� �r� �r�  s�  s� @s� `s� �s� �s� �s� �s�  t�  t� @t� `t� �t� �t� �t� �t�  u�  u� @u� `u� �u� �u� �u� �u�  v�  v� @v� `v� �v� �v� �v� �v�  w�  w� @w� `w� �w� �w� �w� �w�  x�  x� @x� `x� �x� �x� �x� �x�  y�  y� @y� `y� �y� �y� �y� �y�  z�  z� @z� `z� �z� �z� �z� �z�  {�  {� @{� `{� �{� �{� �{� �{�  |�  |� @|� `|� �|� �|� �|� �|�  }�  }� @}� `}� �}� �}� �}� �}�  ~�  ~� @~� `~� �~� �~� �~� �~�  �  � @� `� �� �� �� ��  ��  �� @�� `�� ��� ��� ��� ���  ��  �� @�� `�� ��� ��� ��� ���  ��  �//===- unittest/Tooling/RecursiveASTVisitorTestDeclVisitor.cpp ------------===//
//
//                     The LLVM Compiler Infrastructure
//
// This file is distributed under the University of Illinois Open Source
// License. See LICENSE.TXT for details.
//
//===----------------------------------------------------------------------===//

#include "TestVisitor.h"
#include <stack>

using namespace clang;

namespace {

class VarDeclVisitor : public ExpectedLocationVisitor<VarDeclVisitor> {
public:
 bool VisitVarDecl(VarDecl *Variable) {
   Match(Variable->getNameAsString(), Variable->getLocStart());
   return true;
 }
};

TEST(RecursiveASTVisitor, VisitsCXXForRangeStmtLoopVariable) {
  VarDeclVisitor Visitor;
  Visitor.ExpectMatch("i", 2, 17);
  EXPECT_TRUE(Visitor.runOver(
    "int x[5];\n"
    "void f() { for (int i : x) {} }",
    VarDeclVisitor::Lang_CXX11));
}

class ParmVarDeclVisitorForImplicitCode :
  public ExpectedLocationVisitor<ParmVarDeclVisitorForImplicitCode> {
public:
  bool shouldVisitImplicitCode() const { return true; }

  bool VisitParmVarDecl(ParmVarDecl *ParamVar) {
    Match(ParamVar->getNameAsString(), ParamVar->getLocStart());
    return true;
  }
};

// Test RAV visits parameter variable declaration of the implicit
// copy assignment operator and implicit copy constructor.
TEST(RecursiveASTVisitor, VisitsParmVarDeclForImplicitCode) {
  ParmVarDeclVisitorForImplicitCode Visitor;
  // Match parameter variable name of implicit copy assignment operator and
  // implicit copy constructor.
  // This parameter name does not have a valid IdentifierInfo, and shares
  // same SourceLocation with its class declaration, so we match an empty name
  // with the class' source location.
  Visitor.ExpectMatch("", 1, 7);
  Visitor.ExpectMatch("", 3, 7);
  EXPECT_TRUE(Visitor.runOver(
    "class X {};\n"
    "void foo(X a, X b) {a = b;}\n"
    "class Y {};\n"
    "void bar(Y a) {Y b = a;}"));
}

class NamedDeclVisitor
  : public ExpectedLocationVisitor<NamedDeclVisitor> {
public:
  bool VisitNamedDecl(NamedDecl *Decl) {
    std::string NameWithTemplateArgs;
    llvm::raw_string_ostream OS(NameWithTemplateArgs);
    Decl->getNameForDiagnostic(OS,
                               Decl->getASTContext().getPrintingPolicy(),
                               true);
    Match(OS.str(), Decl->getLocation());
    return true;
  }
};

TEST(RecursiveASTVisitor, VisitsPartialTemplateSpecialization) {
  // From cfe-commits/Week-of-Mon-20100830/033998.html
  // Contrary to the approach suggested in that email, we visit all
  // specializations when we visit the primary template.  Visiting them when we
  // visit the associated specialization is problematic for specializations of
  // template members of class templates.
  NamedDeclVisitor Visitor;
  Visitor.ExpectMatch("A<bool>", 1, 26);
  Visitor.ExpectMatch("A<char *>", 2, 26);
  EXPECT_TRUE(Visitor.runOver(
    "template <class T> class A {};\n"
    "template <class T> class A<T*> {};\n"
    "A<bool> ab;\n"
    "A<char*> acp;\n"));
}

TEST(RecursiveASTVisitor, VisitsUndefinedClassTemplateSpecialization) {
  NamedDeclVisitor Visitor;
  Visitor.ExpectMatch("A<int>", 1, 29);
  EXPECT_TRUE(Visitor.runOver(
    "template<typename T> struct A;\n"
    "A<int> *p;\n"));
}

TEST(RecursiveASTVisitor, VisitsNestedUndefinedClassTemplateSpecialization) {
  NamedDeclVisitor Visitor;
  Visitor.ExpectMatch("A<int>::B<char>", 2, 31);
  EXPECT_TRUE(Visitor.runOver(
    "template<typename T> struct A {\n"
    "  template<typename U> struct B;\n"
    "};\n"
    "A<int>::B<char> *p;\n"));
}

TEST(RecursiveASTVisitor, VisitsUndefinedFunctionTemplateSpecialization) {
  NamedDeclVisitor Visitor;
  Visitor.ExpectMatch("A<int>", 1, 26);
  EXPECT_TRUE(Visitor.runOver(
    "template<typename T> int A();\n"
    "int k = A<int>();\n"));
}

TEST(RecursiveASTVisitor, VisitsNestedUndefinedFunctionTemplateSpecialization) {
  NamedDeclVisitor Visitor;
  Visitor.ExpectMatch("A<int>::B<char>", 2, 35);
  EXPECT_TRUE(Visitor.runOver(
    "template<typename T> struct A {\n"
    "  template<typename U> static int B();\n"
    "};\n"
    "int k = A<int>::B<char>();\n"));
}

TEST(RecursiveASTVisitor, NoRecursionInSelfFriend) {
  // From cfe-commits/Week-of-Mon-20100830/033977.html
  NamedDeclVisitor Visitor;
  Visitor.ExpectMatch("vector_iterator<int>", 2, 7);
  EXPECT_TRUE(Visitor.runOver(
    "template<typename Container>\n"
    "class vector_iterator {\n"
    "    template <typename C> friend class vector_iterator;\n"
    "};\n"
    "vector_iterator<int> it_int;\n"));
}

} // end anonymous namespace
                                                                                                                                                                                                                                                                                                                                                                                                         M^�o|H�KB\*�h�KR�L�%��' �s���d	վy�*���Ϫ�u������Gbx �N $�J�zG�� T7�N8�
"N��S����-�qc-��c� ��w�ٮ@��Ԗ������S�{�Mx4�hZB���4r7�Q��f �d�䆂���Y�l[����}p�jⱔ�`'���("��UK1
h��/��@"I@��T]���ٷ���և���X���H+p�ҵ�n����n�|H���0O[$\}�س��$���
���;��J�-)�t{��b�UP����4GP$�����Kw��;�p�>��|��]��P�vh8:��Ǳ�Y4b��J�s<�;�[�jj��;������"�ᅨp.��6�/�b�&���p5��[�.<�����Ӗ�\+�;��^�1�Ϸ�=�f�Qn��Ͳ�ӥ��݂�}n�QEq.�7+�C�ߒI�@]xt�XP��@IA���.��}�U�v�[�:4�:�!\qT�LX^�z�ڏ�1�L�n֥����n[�C���		/��({�E�����߈����h��!�}@���ix��,#~ ��d�������j���
M�;(�u�#NC&�L��=�̞��~�������A��8��2��%8Í8�X��J�	98I�`��2Ŋ��~�wQhY3R��!�j����G����s,3?[n�7� �'K���@��ddꋵs��@s���{�c��R�����n̢w<�a����T���Bȍ��y�@5��2=	ҋS;�ؓ��I�Ɋ+C�J�����c6��y /�Ŀ�l�J��ߞZ��)+ے����M����ʸ&N ��^�\�����D,j�����s���7�.m�K,�&�Z�|�o0t �̌b��=�>Z�+�H�a�"�p��d|�gKd�MC�0�ᎯgY6y�E�]05��dʩ��	�!xy:x��p�� �e}�e$&�m&��ֵt�;uh�ϏS#7u�ZUO�.���l�����p��:}M�T�7��-���`���-���.�5R*�Z^�Z�<�4���F�Uqm"�����n꘤G��M�����J[o��q�����'0��z ��l3��P-�1J�F�'ٟ� �(�F�9��Ço�欄]�����h�f�"eL��ӷ�*��m�B+���(��2�5��4�+,5{�ɪ�T,��&�L�(��VY��gl����ހ�����N�4 '����_ϒ�����A%����}G�v�Ӧ�橴�,�{'����v��_��/P�*�D׍��2Z�+�d�Z*l*���$�Bl}�&�_�*z�j��:y#8�e�����y[���>�a5p�Xd&�]3?����-L«�H� �ݿl^f*�{Q�ch�U
�Ä�zh��Oީ
!_�?YϠ$�/�ۻ�|^Kl��]��@xS%o�����BY�^&Η�z�z^��j��3�1	�o�n�rcD(8{��]�h�� ��+��[�)iq�N0O3�-_�{�z�^�.�ھ�n�����
,�G8�{G��o�~�T<�tp���t�ސTbY�[0�3���ֈP��F�mk�]�ob8S�����[Eq^�Ѻ�H�6���[o���x�"�?L\j�7�'���"�-�n��� �����K�g�R�I��o�