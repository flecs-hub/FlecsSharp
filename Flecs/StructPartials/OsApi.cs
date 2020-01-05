using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Flecs
{
	// ecs_os_api_t: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/os_api.h#L138
	unsafe partial struct OsApi
	{
		public OsApiMallocDelegate MallocCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _malloc != default ? Marshal.GetDelegateForFunctionPointer<OsApiMallocDelegate>(_malloc) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _malloc = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiReallocDelegate ReallocCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _realloc != default ? Marshal.GetDelegateForFunctionPointer<OsApiReallocDelegate>(_realloc) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _realloc = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiCallocDelegate CallocCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _calloc != default ? Marshal.GetDelegateForFunctionPointer<OsApiCallocDelegate>(_calloc) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _calloc = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiFreeDelegate FreeCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _free != default ? Marshal.GetDelegateForFunctionPointer<OsApiFreeDelegate>(_free) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _free = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiThreadNewDelegate ThreadNewCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _threadNew != default ? Marshal.GetDelegateForFunctionPointer<OsApiThreadNewDelegate>(_threadNew) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _threadNew = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiThreadJoinDelegate ThreadJoinCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _threadJoin != default ? Marshal.GetDelegateForFunctionPointer<OsApiThreadJoinDelegate>(_threadJoin) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _threadJoin = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiMutexNewDelegate MutexNewCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _mutexNew != default ? Marshal.GetDelegateForFunctionPointer<OsApiMutexNewDelegate>(_mutexNew) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _mutexNew = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiMutexFreeDelegate MutexFreeCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _mutexFree != default ? Marshal.GetDelegateForFunctionPointer<OsApiMutexFreeDelegate>(_mutexFree) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _mutexFree = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiMutexLockDelegate MutexLockCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _mutexLock != default ? Marshal.GetDelegateForFunctionPointer<OsApiMutexLockDelegate>(_mutexLock) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _mutexLock = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiMutexLockDelegate MutexUnlockCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _mutexUnlock != default ? Marshal.GetDelegateForFunctionPointer<OsApiMutexLockDelegate>(_mutexUnlock) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _mutexUnlock = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiCondNewDelegate CondNewCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _condNew != default ? Marshal.GetDelegateForFunctionPointer<OsApiCondNewDelegate>(_condNew) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _condNew = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiCondFreeDelegate CondFreeCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _condFree != default ? Marshal.GetDelegateForFunctionPointer<OsApiCondFreeDelegate>(_condFree) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _condFree = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiCondSignalDelegate CondSignalCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _condSignal != default ? Marshal.GetDelegateForFunctionPointer<OsApiCondSignalDelegate>(_condSignal) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _condSignal = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiCondBroadcastDelegate CondBroadcastCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _condBroadcast != default ? Marshal.GetDelegateForFunctionPointer<OsApiCondBroadcastDelegate>(_condBroadcast) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _condBroadcast = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiCondWaitDelegate CondWaitCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _condWait != default ? Marshal.GetDelegateForFunctionPointer<OsApiCondWaitDelegate>(_condWait) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _condWait = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiSleepDelegate SleepCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _sleep != default ? Marshal.GetDelegateForFunctionPointer<OsApiSleepDelegate>(_sleep) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _sleep = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiGetTimeDelegate GetTimeCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _getTime != default ? Marshal.GetDelegateForFunctionPointer<OsApiGetTimeDelegate>(_getTime) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _getTime = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiLogDelegate LogCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _log != default ? Marshal.GetDelegateForFunctionPointer<OsApiLogDelegate>(_log) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _log = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiLogDelegate LogErrorCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _logError != default ? Marshal.GetDelegateForFunctionPointer<OsApiLogDelegate>(_logError) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _logError = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiLogDelegate LogDebugCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _logDebug != default ? Marshal.GetDelegateForFunctionPointer<OsApiLogDelegate>(_logDebug) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _logDebug = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiLogDelegate LogWarningCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _logWarning != default ? Marshal.GetDelegateForFunctionPointer<OsApiLogDelegate>(_logWarning) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _logWarning = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiAbortDelegate AbortCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _abort != default ? Marshal.GetDelegateForFunctionPointer<OsApiAbortDelegate>(_abort) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _abort = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiDlopenDelegate DlopenCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _dlopen != default ? Marshal.GetDelegateForFunctionPointer<OsApiDlopenDelegate>(_dlopen) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _dlopen = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiDlprocDelegate DlprocCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _dlproc != default ? Marshal.GetDelegateForFunctionPointer<OsApiDlprocDelegate>(_dlproc) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _dlproc = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiDlcloseDelegate DlcloseCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _dlclose != default ? Marshal.GetDelegateForFunctionPointer<OsApiDlcloseDelegate>(_dlclose) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _dlclose = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}

		public OsApiModuleToDlDelegate ModuleToDlCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _moduleToDl != default ? Marshal.GetDelegateForFunctionPointer<OsApiModuleToDlDelegate>(_moduleToDl) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _moduleToDl = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}
	}
}

