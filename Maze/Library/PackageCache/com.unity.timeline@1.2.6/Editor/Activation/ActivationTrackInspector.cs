AN2008,MIPS64R3 %s
# RUN: llvm-mc -filetype=obj -triple mipsel-unknown-linux   -mcpu=mips64r5 -target-abi=n32 -mattr=+nan2008 %s -o - | llvm-readobj -h | FileCheck --check-prefixes=ALL,ELF32,LE,N32,NAN2008,MIPS64R5 %s
# RUN: llvm-mc -filetype=obj -triple mipsel-unknown-linux   -mcpu=mips64r6 -target-abi=n32                 %s -o - | llvm-readobj -h | FileCheck --check-prefixes=ALL,ELF32,LE,N32,NAN2008,MIPS64R6 %s
# RUN: llvm-mc -filetype=obj -triple mips64-unknown-linux   -mcpu=mips3    -target-abi=n32                 %s -o - | llvm-readobj -h | FileCheck --check-prefixes=ALL,ELF32,BE,N32,NAN1985,MIPS3 %s
# RUN: llvm-mc -filetype=obj -triple mips64-unknown-linux   -mcpu=mips4    -target-abi=n32                 %s -o - | llvm-readobj -h | FileCheck --check-prefixes=ALL,ELF32,BE,N32,NAN1985,MIPS4 %s
# RUN: llvm-mc -filetype=obj -triple mips64-unknown-linux   -mcpu=mips5    -target-abi=n32                 %s -o - | llvm-readobj -h | FileCheck --check-prefixes=ALL,ELF32,BE,N32,NAN1985,MIPS5 %s
# RUN: llvm-mc -filetype=obj -triple mips64-unknown-linux                  -target-abi=n32                 %s -o - | llvm-readobj -h | FileCheck --check-prefixes=ALL,ELF32,BE,N32,NAN1985,MIPS64R1 %s
# RUN: llvm-mc -filetype=obj -triple mips64el-unknown-linux                -target-abi=n32                 %s -o - | llvm-rea