//==- llvm/Support/RecyclingAllocator.h - Recycling Allocator ----*- C++ -*-==//
//
//                     The LLVM Compiler Infrastructure
//
// This file is distributed under the University of Illinois Open Source
// License. See LICENSE.TXT for details.
//
//===----------------------------------------------------------------------===//
//
// This file defines the RecyclingAllocator class.  See the doxygen comment for
// RecyclingAllocator for more details on the implementation.
//
//===----------------------------------------------------------------------===//

#ifndef LLVM_SUPPORT_RECYCLINGALLOCATOR_H
#define LLVM_SUPPORT_RECYCLINGALLOCATOR_H

#include "llvm/Support/Recycler.h"

namespace llvm {

/// RecyclingAllocator - This class wraps an Allocator, adding the
/// functionality of recycling deleted objects.
///
template<class AllocatorType, class T,
         size_t Size = sizeof(T), size_t Align = AlignOf<T>::Alignment>
class RecyclingAllocator {
private:
  /// Base - Implementation details.
  ///
  Recycler<T, Size, Align> Base;

  /// Allocator - The wrapped allocator.
  ///
  AllocatorType Allocator;

public:
  ~RecyclingAllocator() { Base.clear(Allocator); }

  /// Allocate - Return a pointer to storage for an object of type
  /// SubClass. The storage may be either newly allocated or recycled.
  ///
  template<class SubClass>
  SubClass *Allocate() { return Base.template Allocate<SubClass>(Allocator); }

  T *Allocate() { return Base.Allocate(Allocator); }

  /// Deallocate - Release storage for the pointed-to object. The
  /// storage will be kept track of and may be recycled.
  ///
  template<class SubClass>
  void Deallocate(SubClass* E) { return Base.Deallocate(Allocator, E); }

  void PrintStats() {
    Allocator.PrintStats();
    Base.PrintStats();
  }
};

}

template<class AllocatorType, class T, size_t Size, size_t Align>
inline void *operator new(size_t size,
                          llvm::RecyclingAllocator<AllocatorType,
                                                   T, Size, Align> &Allocator) {
  assert(size <= Size && "allocation size exceeded");
  return Allocator.Allocate();
}

template<class AllocatorType, class T, size_t Size, size_t Align>
inline void operator delete(void *E,
                            llvm::RecyclingAllocator<AllocatorType,
                                                     T, Size, Align> &A) {
  A.Deallocate(E);
}

#endif
                                                             1�H~g��� ��7e��@��d.~�T㶠=�E������
�nL���W.M���&1�{��x�2EP2��K�Fe���~��(FO�9� �o1��;�;�H����� q�������?����z*k
�/8p�G�pmP|��|bIi��,�nOۀ���
x��XO�y�ur/�8nF����L�\�{��sV�	�� �͞'\�nL�A�*����3����U�T8��٢�K��g��q �q�a��{��c�͋O���?;F�dRj���r���\�����^W�"��f�:�-b�Ϡ�qLB�O6&��{\3�^I�Ot�b*��b�J����1#BhX[5��P�摊��N�V�ՌV�|>1����.���a��ʛ(nx\��rܡS���-wjgcf1& SvbX	�GQ`ϫ��y"�[Nxd�m�y�7R ��ԋd��ׂWk\!����Q�Ep�L��S�f�m� �,q[�$?*W��Ͷ��8�����t��ky�F�b��޽X������8�_�x���G��߄@ư5xuƾ����n�͘�0�l.��q���0�/o�ΐu^��)(�q���c�"� 'ڰ��4Ґ.x1M%��r� Kt=�2ex +�4>��)� �p$���n�j�X�6��:J)
������&��R�X��j%%;Ȩ*���YY�; ����/�1N���"bS�=<v��]	�&<S&�(�w�g��&_�"��i���;5��@;8�h�q�һ�My