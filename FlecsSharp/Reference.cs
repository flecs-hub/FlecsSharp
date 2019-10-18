using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace FlecsSharp
{
    // ecs_reference_t: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L97
    unsafe partial struct Reference
    {
        public EntityId Entity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => entity;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => entity = value;
        }

        public EntityId Component
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => component;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => component = value;
        }

        public IntPtr CachedPtr
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => cachedPtr;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => cachedPtr = value;
        }

    }

}

