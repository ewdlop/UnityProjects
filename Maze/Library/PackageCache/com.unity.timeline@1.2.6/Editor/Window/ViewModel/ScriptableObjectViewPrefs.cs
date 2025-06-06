// ==++==
// 
//   Copyright (c) Microsoft Corporation.  All rights reserved.
// 
// ==--==
/*============================================================
**
** Class: SerializableAttribute
**
**
** Purpose: Used to mark a class as being serializable
**
**
============================================================*/
namespace System {

    using System;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Delegate, Inherited = false)]
[System.Runtime.InteropServices.ComVisible(true)]
    public sealed class SerializableAttribute : Attribute 
    {
        internal static Attribute GetCustomAttribute(RuntimeType type) 
        { 
            return (type.Attributes & TypeAttributes.Serializable) == TypeAttributes.Serializable ? new SerializableAttribute() : null; 
        }
        internal static bool IsDefined(RuntimeType type) 
        { 
            return type.IsSerializable; 
        }

        public SerializableAttribute() {
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 %p team=%p
 __kmp_hyper_barrier_gather: T#%d(%d:%d) enter for barrier type %d
 this_thr == other_threads[this_thr->th.th_info.ds.ds_tid] __kmp_hyper_barrier_gather: T#%d(%d:%d) releasing T#%d(%d:%d) arrived(%p): %llu => %llu
 (parent_tid) >= 0 && (team) != __null __kmp_hyper_barrier_gather: T#%d(%d:%d) wait T#%d(%d:%u) arrived(%p) == %llu
 (child_tid) >= 0 && (team) != __null __kmp_hyper_barrier_gather: T#%d(%d:%d) += T#%d(%d:%u)
 __kmp_hyper_barrier_gather: T#%d(%d:%d) set team %d arrived(%p) = %llu
 __kmp_hyper_barrier_gather: T#%d(%d:%d) exit for barrier type %d
 __kmp_release: T#%d releasing flag(%x)
 flag->get() __kmp_release: T#%d set new spin=%d
 __kmp_release: T#%d waking up thread T#%d since sleep flag(%p) set
 __kmp_hierarchical_barrier_gather: T#%d(%d:%d) enter for barrier type %d
 __kmp_hierarchical_barrier_gather: T#%d(%d:%d) waiting for leaf kids
 __kmp_hierarchical_barrier_gather: T#%d(%d:%d) += T#%d(%d:%d)
 __kmp_hierarchical_barrier_gather: T#%d(%d:%d) wait T#%d(%d:%d) arrived(%p) == %llu
 __kmp_hierarchical_barrier_gather: T#%d(%d:%d) releasing T#%d(%d:%d) arrived(%p): %llu => %llu
 (thr_bar->parent_tid) >= 0 && (team) != __null __kmp_hierarchical_barrier_gather: T#%d(%d:%d) set team %d arrived(%p) = %llu
 __kmp_hierarchical_barrier_gather: T#%d(%d:%d) exit for barrier type %d
 __kmp_tree_barrier_gather: T#%d(%d:%d) enter for barrier type %d
 __kmp_tree_barrier_gather: T#%d(%d:%d) wait T#%d(%d:%u) arrived(%p) == %llu
 __kmp_tree_barrier_gather: T#%d(%d:%d) += T#%d(%d:%u)
 __kmp_tree_barrier_gather: T#%d(%d:%d) releasing T#%d(%d:%d) arrived(%p): %llu => %llu
 __kmp_tree_barrier_gather: T#%d(%d:%d) set team %d arrived(%p) = %llu
 __kmp_tree_barrier_gather: T#%d(%d:%d) exit for barrier type %d
 __kmp_linear_barrier_gather: T#%d(%d:%d) enter for barrier type %d
 __kmp_linear_barrier_gather: T#%d(%d:%d) releasing T#%d(%d:%d)arrived(%p): %llu => %llu
 (0) >= 0 && (team) != __null __kmp_linear_barrier_gather: T#%d(%d:%d) wait T#%d(%d:%d) arrived(%p) == %l