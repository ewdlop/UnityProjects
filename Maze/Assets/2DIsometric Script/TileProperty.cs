//===--- MinGWToolChain.cpp - MinGWToolChain Implementation ---------------===//
//
//                     The LLVM Compiler Infrastructure
//
// This file is distributed under the University of Illinois Open Source
// License. See LICENSE.TXT for details.
//
//===----------------------------------------------------------------------===//

#include "ToolChains.h"
#include "clang/Driver/Driver.h"
#include "clang/Driver/Options.h"
#include "llvm/Option/ArgList.h"
#include "llvm/Support/FileSystem.h"
#include "llvm/Support/Path.h"

using namespace clang::diag;
using namespace clang::driver;
using namespace clang::driver::toolchains;
using namespace clang;
using namespace llvm::opt;

namespace {
// Simplified from Generic_GCC::GCCInstallationDetector::ScanLibDirForGCCTriple.
bool findGccVersion(StringRef LibDir, std::string &GccLibDir,
                    std::string &Ver) {
  Generic_GCC::GCCVersion Version = Generic_GCC::GCCVersion::Parse("0.0.0");
  std::error_code EC;
  for (llvm::sys::fs::directory_iterator LI(LibDir, EC), LE; !EC && LI != LE;
       LI = LI.increment(EC)) {
    StringRef VersionText = llvm::sys::path::filename(LI->path());
    Generic_GCC::GCCVersion CandidateVersion =
        Generic_GCC::GCCVersion::Parse(VersionText);
    if (CandidateVersion.Major == -1)
      continue;
    if (CandidateVersion <= Version)
      continue;
    Ver = VersionText;
    GccLibDir = LI->path();
  }
  return Ver.size();
}
}

void MinGW::findGccLibDir() {
  llvm::SmallVector<llvm::SmallString<32>, 2> Archs;
  Archs.emplace_back(getTriple().getArchName());
  Archs[0] += "-w64-mingw32";
  Archs.emplace_back("mingw32");
  Arch = Archs[0].str();
  // lib: Arch Linux, Ubuntu, Windows
  // lib64: openSUSE Linux
  for (StringRef CandidateLib : {"lib", "lib64"}) {
    for (StringRef CandidateArch : Archs) {
      llvm::SmallString<1024> LibDir(Base);
      llvm::sys::path::append(LibDir, CandidateLib, "gcc", CandidateArch);
      if (findGccVersion(LibDir, GccLibDir, Ver)) {
        Arch = CandidateArch;
        return;
      }
    }
  }
}

MinGW::MinGW(c