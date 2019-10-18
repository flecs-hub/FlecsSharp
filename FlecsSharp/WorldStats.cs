using System;
using System.Runtime.CompilerServices;

namespace FlecsSharp
{
    // ecs_world_stats_t: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/stats.h#L55
    unsafe partial struct WorldStats
    {
        public uint SystemCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => systemCount;
        }

        public uint TableCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => tableCount;
        }

        public uint ComponentCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => componentCount;
        }

        public uint EntityCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => entityCount;
        }

        public uint ThreadCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => threadCount;
        }

        public uint TickCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => tickCount;
        }

        public float SystemTime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => systemTime;
        }

        public float FrameTime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => frameTime;
        }

        public float MergeTime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => mergeTime;
        }

        public MemoryStats Memory
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => memory;
        }

        public Vector Features
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => features;
        }

        public Vector OnLoadSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => onLoadSystems;
        }

        public Vector PostLoadSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => postLoadSystems;
        }

        public Vector PreUpdateSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => preUpdateSystems;
        }

        public Vector OnUpdateSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => onUpdateSystems;
        }

        public Vector OnValidateSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => onValidateSystems;
        }

        public Vector PostUpdateSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => postUpdateSystems;
        }

        public Vector PreStoreSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => preStoreSystems;
        }

        public Vector OnStoreSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => onStoreSystems;
        }

        public Vector TaskSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => taskSystems;
        }

        public Vector InactiveSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => inactiveSystems;
        }

        public Vector OnDemandSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => onDemandSystems;
        }

        public Vector OnAddSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => onAddSystems;
        }

        public Vector OnRemoveSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => onRemoveSystems;
        }

        public Vector OnSetSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => onSetSystems;
        }

        public Vector Components
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => components;
        }

        public bool FrameProfiling
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => frameProfiling!= 0;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => frameProfiling = (byte) (value ? 1 : 0);
        }

        public bool SystemProfiling
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => systemProfiling!= 0;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => systemProfiling = (byte) (value ? 1 : 0);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void FreeStats()
        {
            ecs.free_stats(out this);
        }

    }

}

