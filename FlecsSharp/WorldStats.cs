using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace FlecsSharp
{
    // ecs_world_stats_t: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs/util/stats.h#L55
    unsafe partial struct WorldStats
    {
        public uint SystemCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => systemCount;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => systemCount = value;
        }

        public uint TableCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => tableCount;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => tableCount = value;
        }

        public uint ComponentCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => componentCount;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => componentCount = value;
        }

        public uint EntityCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => entityCount;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => entityCount = value;
        }

        public uint ThreadCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => threadCount;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => threadCount = value;
        }

        public uint TickCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => tickCount;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => tickCount = value;
        }

        public float SystemTime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => systemTime;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => systemTime = value;
        }

        public float FrameTime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => frameTime;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => frameTime = value;
        }

        public float MergeTime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => mergeTime;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => mergeTime = value;
        }

        public MemoryStats Memory
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => memory;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => memory = value;
        }

        public Vector Features
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => features;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => features = value;
        }

        public Vector OnLoadSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => onLoadSystems;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => onLoadSystems = value;
        }

        public Vector PostLoadSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => postLoadSystems;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => postLoadSystems = value;
        }

        public Vector PreUpdateSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => preUpdateSystems;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => preUpdateSystems = value;
        }

        public Vector OnUpdateSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => onUpdateSystems;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => onUpdateSystems = value;
        }

        public Vector OnValidateSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => onValidateSystems;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => onValidateSystems = value;
        }

        public Vector PostUpdateSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => postUpdateSystems;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => postUpdateSystems = value;
        }

        public Vector PreStoreSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => preStoreSystems;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => preStoreSystems = value;
        }

        public Vector OnStoreSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => onStoreSystems;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => onStoreSystems = value;
        }

        public Vector TaskSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => taskSystems;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => taskSystems = value;
        }

        public Vector InactiveSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => inactiveSystems;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => inactiveSystems = value;
        }

        public Vector OnDemandSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => onDemandSystems;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => onDemandSystems = value;
        }

        public Vector OnAddSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => onAddSystems;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => onAddSystems = value;
        }

        public Vector OnRemoveSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => onRemoveSystems;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => onRemoveSystems = value;
        }

        public Vector OnSetSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => onSetSystems;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => onSetSystems = value;
        }

        public Vector Components
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => components;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => components = value;
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

