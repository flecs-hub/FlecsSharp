using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using SharpC;

namespace FlecsSharp
{
    public unsafe partial struct Memory
    {
        //public static Memory New(in Data val = default) => new Memory(Alloc(in val));
        //public void Dispose()
        //{
            //if(ptr != null) Free(ptr);
        //}

        public Total Total
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->total;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->total = value; }
        }

        public Total Components
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->components;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->components = value; }
        }

        public Total Entities
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->entities;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->entities = value; }
        }

        public Total Systems
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->systems;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->systems = value; }
        }

        public Total Families
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->families;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->families = value; }
        }

        public Total Tables
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->tables;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->tables = value; }
        }

        public Total Stage
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->stage;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->stage = value; }
        }

        public Total World
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->world;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->world = value; }
        }

    }

}

