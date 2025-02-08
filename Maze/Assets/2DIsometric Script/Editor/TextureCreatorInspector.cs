ctor<llvm::SmallString<1024>, 4> CppIncludeBases;
    CppIncludeBases.emplace_back(Base);
    llvm::sys::path::append(CppIncludeBases[0], Arch, "include", "c++");
    CppIncludeBases.emplace_back(Base);
    llvm::sys::path::append(CppIncludeBases[1], Arch, "include", "c++", Ver);
    CppIncludeBases.emplace_back(Base);
    llvm::sys::path::append(CppIncludeBases[2], "include", "c++", Ver);
    CppIncludeBases.emplace_back(GccLibDir);
    llvm::sys::path::append(CppIncludeBases[3], "include", "c++");
    for (auto &CppIncludeBase : CppIncludeBases) {
      addSystemInclude(DriverArgs, CC1Args, CppIncludeBase);
      CppIncludeBase += llvm::sys::path::get_separator();
      addSystemInclude(DriverArgs, CC1Args, CppIncludeBase + Arch);
      addSystemInclude(Driv