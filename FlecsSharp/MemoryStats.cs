using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace FlecsSharp
{
    // EcsMemoryStats: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/stats.h#L44
    unsafe partial struct MemoryStats
    {
        public MemoryStat Total
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => total;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => total = value;
        }

        public MemoryStat Components
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => components;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => components = value;
        }

        public MemoryStat Entities
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => entities;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => entities = value;
        }

        public MemoryStat Systems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => systems;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => systems = value;
        }

        public MemoryStat Families
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => families;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => families = value;
        }

        public MemoryStat Tables
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => tables;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => tables = value;
        }

        public MemoryStat Stage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => stage;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => stage = value;
        }

        public MemoryStat World
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => world;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => world = value;
        }

    }

}

