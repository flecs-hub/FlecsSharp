using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using SharpC;

namespace FlecsSharp
{
    public unsafe partial struct WorldStats
    {
        //public static WorldStats New(in Data val = default) => new WorldStats(Alloc(in val));
        //public void Dispose()
        //{
            //if(ptr != null) Free(ptr);
        //}

        public uint SystemCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->systemCount;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->systemCount = value;
        }

        public uint TableCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->tableCount;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->tableCount = value;
        }

        public uint ComponentCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->componentCount;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->componentCount = value;
        }

        public uint EntityCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->entityCount;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->entityCount = value;
        }

        public uint ThreadCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->threadCount;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->threadCount = value;
        }

        public uint TickCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->tickCount;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->tickCount = value;
        }

        public float SystemTime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->systemTime;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->systemTime = value;
        }

        public float FrameTime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->frameTime;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->frameTime = value;
        }

        public float MergeTime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->mergeTime;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->mergeTime = value;
        }

        public MemoryStats Memory
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->memory;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->memory = value; }
        }

        public Vector Features
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->features;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->features = value; }
        }

        public Vector OnLoadSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->onLoadSystems;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->onLoadSystems = value; }
        }

        public Vector PostLoadSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->postLoadSystems;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->postLoadSystems = value; }
        }

        public Vector PreUpdateSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->preUpdateSystems;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->preUpdateSystems = value; }
        }

        public Vector OnUpdateSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->onUpdateSystems;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->onUpdateSystems = value; }
        }

        public Vector OnValidateSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->onValidateSystems;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->onValidateSystems = value; }
        }

        public Vector PostUpdateSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->postUpdateSystems;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->postUpdateSystems = value; }
        }

        public Vector PreStoreSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->preStoreSystems;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->preStoreSystems = value; }
        }

        public Vector OnStoreSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->onStoreSystems;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->onStoreSystems = value; }
        }

        public Vector TaskSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->taskSystems;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->taskSystems = value; }
        }

        public Vector InactiveSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->inactiveSystems;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->inactiveSystems = value; }
        }

        public Vector OnDemandSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->onDemandSystems;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->onDemandSystems = value; }
        }

        public Vector OnAddSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->onAddSystems;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->onAddSystems = value; }
        }

        public Vector OnRemoveSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->onRemoveSystems;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->onRemoveSystems = value; }
        }

        public Vector OnSetSystems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->onSetSystems;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->onSetSystems = value; }
        }

        public Vector Components
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->components;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->components = value; }
        }

        public Bool FrameProfiling
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->frameProfiling;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->frameProfiling = value; }
        }

        public Bool SystemProfiling
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->systemProfiling;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->systemProfiling = value; }
        }

    }

}

