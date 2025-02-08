//
// Copyright 2017 The Abseil Authors.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
// -----------------------------------------------------------------------------
// any.h
// -----------------------------------------------------------------------------
//
// This header file define the `absl::any` type for holding a type-safe value
// of any type. The 'absl::any` type is useful for providing a way to hold
// something that is, as yet, unspecified. Such unspecified types
// traditionally are passed between API boundaries until they are later cast to
// their "destination" types. To cast to such a destination type, use
// `absl::any_cast()`. Note that when casting an `absl::any`, you must cast it
// to an explicit type; implicit conversions will throw.
//
// Example:
//
//   auto a = absl::any(65);
//   absl::any_cast<int>(a);         // 65
//   absl::any_cast<char>(a);        // throws absl::bad_any_cast
//   absl::any_cast<std::string>(a); // throws absl::bad_any_cast
//
// `absl::any` is a C++11 compatible version of the C++17 `std::any` abstraction
// and is designed to be a drop-in replacement for code compliant with C++17.
//
// Traditionally, the behavior of casting to a temporary unspecified type has
// been accomplished with the `void *` paradigm, where the pointer was to some
// other unspecified type. `absl::any` provides an "owning" version of `void *`
// that avoids issues of pointer management.
//
// Note: just as in the case of `void *`, use of `absl::any` (and its C++17
// version `std::any`) is a code smell indicating that your API might not be
// constructed correctly. We have seen that most uses of `any` are unwarranted,
// and `absl::any`, like `std::any`, is difficult to use properly. Before using
// this abstraction, make sure that you should not instead be rewriting your
// code to be more specific.
//
// Abseil expects to release an `absl::variant` type shortly (a C++11 compatible
// version of the C++17 `std::variant), which is generally preferred for use
// over `absl::any`.
#ifndef ABSL_TYPES_ANY_H_
#define ABSL_TYPES_ANY_H_

#include "absl/base/config.h"
#include "absl/utility/utility.h"

#ifdef ABSL_HAVE_STD_ANY

#include <any>

namespace absl {
using std::any;
using std::any_cast;
using std::bad_any_cast;
using std::make_any;
}  // namespace absl

#else  // ABSL_HAVE_STD_ANY

#include <algorithm>
#include <cstddef>
#include <initializer_list>
#include <memory>
#include <stdexcept>
#include <type_traits>
#include <typeinfo>
#include <utility>

#include "absl/base/macros.h"
#include "absl/meta/type_traits.h"
#include "absl/types/bad_any_cast.h"

// NOTE: This macro is an implementation detail that is undefined at the bottom
// of the file. It is not intended for expansion directly from user code.
#ifdef ABSL_ANY_DETAIL_HAS_RTTI
#error ABSL_ANY_DETAIL_HAS_RTTI cannot be directly set
#elif !defined(__GNUC__) || defined(__GXX_RTTI)
#define ABSL_ANY_DETAIL_HAS_RTTI 1
#endif  // !defined(__GNUC__) || defined(__GXX_RTTI)

namespace absl {

namespace any_internal {

template <typename Type>
struct TypeTag {
  constexpr static char dummy_var = 0;
};

template <typename Type>
constexpr char TypeTag<Type>::dummy_var;

// FastTypeId<Type>() evaluates at compile/link-time to a unique pointer for the
// passed in type. These are meant to be good match for keys into maps or
// straight up comparisons.
template<typename Type>
constexpr inline const void* FastTypeId() {
  return &TypeTag<Type>::dummy_var;
}

}  // namespace any_internal

class any;

// swap()
//
// Swaps two `absl::any` values. Equivalent to `x.swap(y) where `x` and `y` are
// `absl::any` types.
void swap(any& x, any& y) noexcept;

// make_any()
//
// Constructs an `absl::any` of type `T` with the given arguments.
template <typename T, typename... Args>
any make_any(Args&&... args);

// Overload of `absl::make_any()` for constructing an `absl::any` type from an
// initializer list.
template <typename T, typename U, typename... Args>
any make_any(std::initializer_list<U> il, Args&&... args);

// any_cast()
//
// Statically casts the value of a `const absl::any` type to the given type.
// This function will throw `absl::bad_any_cast` if the stored value type of the
// `absl::any` does not match the cast.
//
// `any_cast()` can also be used to get a reference to the internal storage iff
// a reference type is passed as its `ValueType`:
//
// Example:
//
//   absl::any my_any = std::vector<int>();
//   absl::any_cast<std::vector<int>&>(my_any).push_back(42);
template <typename ValueType>
ValueType any_cast(const any& operand);

// Overload of `any_cast()` to statically cast the value of a non-const
// `absl::any` type to the given type. This function will throw
// `absl::bad_any_cast` if the stored value type of the `absl::any` does not
// match the cast.
template <typename ValueType>
ValueType any_cast(any& operand);  // NOLINT(runtime/references)

// Overload of `any_cast()` to statically cast the rvalue of an `absl::any`
// type. This function will throw `absl::bad_any_cast` if the stored value type
// of the `absl::any` does not match the cast.
template <typename ValueType>
ValueType any_cast(any&& operand);

// Overload of `any_cast()` to statically cast the value of a const pointer
// `absl::any` type to the given pointer type, or `nullptr` if the stored value
// type of the `absl::any` does not match the cast.
template <typename ValueType>
const ValueType* any_cast(const any* operand) noexcept;

// Overload of `any_cast()` to statically cast the value of a pointer
// `absl::any` type to the given pointer type, or `nullptr` if the stored value
// type of the `absl::any` does not match the cast.
template <typename ValueType>
ValueType* any_cast(any* operand) noexcept;

// -----------------------------------------------------------------------------
// absl::any
// -----------------------------------------------------------------------------
/