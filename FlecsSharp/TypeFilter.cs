using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace FlecsSharp
{
    // ecs_type_filter_t: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L168
    unsafe partial struct TypeFilter
    {
        public TypeId Include
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => include;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => include = value;
        }

        public TypeId Exclude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => exclude;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => exclude = value;
        }

        public TypeFilterKind IncludeKind
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => includeKind;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => includeKind = value;
        }

        public TypeFilterKind ExcludeKind
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => excludeKind;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => excludeKind = value;
        }

    }

}

