using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using SharpC;

namespace FlecsSharp
{
    unsafe partial struct OsApi
    {
        internal OsApiMallocDelegate MallocCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<OsApiMallocDelegate>(ptr->_malloc);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  ptr->_malloc = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal OsApiReallocDelegate ReallocCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<OsApiReallocDelegate>(ptr->_realloc);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  ptr->_realloc = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal OsApiCallocDelegate CallocCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<OsApiCallocDelegate>(ptr->_calloc);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  ptr->_calloc = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal OsApiFreeDelegate FreeCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<OsApiFreeDelegate>(ptr->_free);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  ptr->_free = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal OsApiThreadNewDelegate ThreadNewCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<OsApiThreadNewDelegate>(ptr->_threadNew);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  ptr->_threadNew = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal OsApiThreadJoinDelegate ThreadJoinCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<OsApiThreadJoinDelegate>(ptr->_threadJoin);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  ptr->_threadJoin = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal OsApiMutexNewDelegate MutexNewCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<OsApiMutexNewDelegate>(ptr->_mutexNew);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  ptr->_mutexNew = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal OsApiMutexFreeDelegate MutexFreeCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<OsApiMutexFreeDelegate>(ptr->_mutexFree);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  ptr->_mutexFree = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal OsApiMutexLockDelegate MutexLockCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<OsApiMutexLockDelegate>(ptr->_mutexLock);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  ptr->_mutexLock = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal OsApiMutexLockDelegate MutexUnlockCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<OsApiMutexLockDelegate>(ptr->_mutexUnlock);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  ptr->_mutexUnlock = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal OsApiCondNewDelegate CondNewCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<OsApiCondNewDelegate>(ptr->_condNew);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  ptr->_condNew = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal OsApiCondFreeDelegate CondFreeCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<OsApiCondFreeDelegate>(ptr->_condFree);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  ptr->_condFree = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal OsApiCondSignalDelegate CondSignalCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<OsApiCondSignalDelegate>(ptr->_condSignal);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  ptr->_condSignal = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal OsApiCondBroadcastDelegate CondBroadcastCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<OsApiCondBroadcastDelegate>(ptr->_condBroadcast);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  ptr->_condBroadcast = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal OsApiCondWaitDelegate CondWaitCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<OsApiCondWaitDelegate>(ptr->_condWait);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  ptr->_condWait = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal OsApiSleepDelegate SleepCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<OsApiSleepDelegate>(ptr->_sleep);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  ptr->_sleep = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal OsApiGetTimeDelegate GetTimeCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<OsApiGetTimeDelegate>(ptr->_getTime);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  ptr->_getTime = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal OsApiLogDelegate LogCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<OsApiLogDelegate>(ptr->_log);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  ptr->_log = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal OsApiLogDelegate LogErrorCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<OsApiLogDelegate>(ptr->_logError);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  ptr->_logError = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal OsApiLogDelegate LogDebugCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<OsApiLogDelegate>(ptr->_logDebug);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  ptr->_logDebug = Marshal.GetFunctionPointerForDelegate(value);
        }

        internal OsApiAbortDelegate AbortCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Marshal.GetDelegateForFunctionPointer<OsApiAbortDelegate>(ptr->_abort);
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set =>  ptr->_abort = Marshal.GetFunctionPointerForDelegate(value);
        }

    }

}

