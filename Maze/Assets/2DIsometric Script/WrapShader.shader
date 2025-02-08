iple::x86_64;
}

bool MinGW::isPICDefault() const { return getArch() == llvm::Triple::x86_64; }

bool MinGW::isPIEDefault() const { return false; }

bool MinGW::isPICDefaultForced() const {
  return getArch() == llvm::Triple::x86_64;
}

bool MinGW::UseSEHExceptions() const {
  return getArch() == llvm::Triple::x86_64;
}

// Include directories for various hosts:

// Windows, mingw.org
// c:\mingw\lib\gcc\mingw32\4.8.1\include\c++
// c:\mingw\lib\gcc\mingw32\4.8.1\include\c++\mingw32
// c:\mingw\lib\gcc\mingw32\4.8.1\include\c++\backward
// c:\mingw\lib\gcc\mingw32\4.8.1\include
// c:\mingw\include
// c:\mingw\lib\gcc\mingw32\4.8.1\include-fixed
// c:\mingw\mingw32\include

// Windows, mingw-w64 mingw-builds
// c:\mingw32\lib\gcc\i686-w64-mingw32\4.9.1\include
// c:\mingw32\lib\gcc\i686-w64-mingw32\4.9.1\include-fixed
// c:\mingw32\i686-w64-mingw32\include
// c:\mingw32\i686-w64-mingw32\include\c++
// c:\mingw32\i686-w64-mingw32\include\c++\i686-w64-mingw32
// c:\mingw32\i686-w64-mingw32\include\c++\backward

// Windows, mingw-w64 msys2
// c:\msys64\mingw32\lib\gcc\i686-w64-mingw32\4.9.2\include
// c:\msys64\mingw32\include
// c:\msys64\mingw32\lib\gcc\i686-w64-mingw32\4.9.2\include-fixed
// c:\msys64\mingw32\i686-w64-mingw32\include
// c:\msys64\mingw32\include\c++\4.9.2
// c:\msys64\mingw32\include\c++\4.9.2\i686-w64-mingw32
// c:\msys64\mingw32\include\c++\4.9.2\backward

// openSUSE
// /usr/lib64/gcc/x86_64-w64-mingw32/5.1.0/include/c++
// /usr/lib64/gcc/x86_64-w64-mingw32/5.1.0/include/c++/x86_64-w64-mingw32
// /usr/lib64/gcc/x86_64-w64-mingw32/5.1.0/include/c++/backward
// /usr/lib64/gcc/x86_64-w64-mingw32/5.1.0/include
// /usr/lib64/gcc/x86_64-w64-mingw32/5.1.0/include-fixed
// /usr/x86_64-w64-mingw32/sys-root/mingw/include

// Arch Linux
// /usr/i686-w64-mingw32/include/c++/5.1.0
// /usr/i686-w64-mingw32/include/c++/5.1.0/i686-w64-mingw32
// /usr/i686-w64-mingw32/include/c++/5.1.0/backward
// /usr/lib/gcc/i686-w64-mingw32/5.1.0/include
// /usr/lib/gcc/i686-w64-mingw32/5.1.0/include-fixed
// /usr/i686-w64-mingw32