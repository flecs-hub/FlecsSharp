using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Flecs
{
	// ecs_vector_params_t: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L22
	partial struct VectorParams
	{
		public MoveDelegate MoveActionCallback
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => _moveAction != default ? Marshal.GetDelegateForFunctionPointer<MoveDelegate>(_moveAction) : default;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set => _moveAction = value != default ? Marshal.GetFunctionPointerForDelegate(value) : default;
		}
	}
}