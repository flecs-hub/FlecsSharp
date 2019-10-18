using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace FlecsSharp
{
    // ecs_vector_params_t: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/vector.h#L22
    unsafe partial struct VectorParams
    {
        public MoveDelegate MoveActionCallback
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _moveAction != default ? Marshal.GetDelegateForFunctionPointer<MoveDelegate>(_moveAction) : default;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => _moveAction = value != default ?  Marshal.GetFunctionPointerForDelegate(value) : default;
        }

        public IntPtr MoveCtx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => moveCtx;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => moveCtx = value;
        }

        public IntPtr Ctx
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ctx;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ctx = value;
        }

        public uint ElementSize
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => elementSize;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => elementSize = value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector VectorNew(uint size)
        {
            return ecs.vector_new(out this, size);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector VectorNewFromBuffer(uint size, IntPtr buffer)
        {
            return ecs.vector_new_from_buffer(out this, size, buffer);
        }

    }

}

