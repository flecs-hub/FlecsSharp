using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using SharpC;

namespace FlecsSharp
{
    unsafe partial struct MemoryStats
    {
        public MemStat Total
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ptr->total;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            //set => ptr->total = value; }
        }

        public MemStat Components
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ptr->components;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            //set => ptr->components = value; }
        }

        public MemStat Entities
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ptr->entities;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            //set => ptr->entities = value; }
        }

        public MemStat Systems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ptr->systems;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            //set => ptr->systems = value; }
        }

        public MemStat Families
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ptr->families;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            //set => ptr->families = value; }
        }

        public MemStat Tables
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ptr->tables;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            //set => ptr->tables = value; }
        }

        public MemStat Stage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ptr->stage;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            //set => ptr->stage = value; }
        }

        public MemStat World
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ptr->world;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]
            //set => ptr->world = value; }
        }

    }

}

