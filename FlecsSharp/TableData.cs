using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace FlecsSharp
{
    // ecs_table_data_t: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L717
    unsafe partial struct TableData
    {
        public uint RowCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => rowCount;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => rowCount = value;
        }

        public uint ColumnCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => columnCount;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => columnCount = value;
        }

        public EntityId* Entities
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => entities;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => entities = value;
        }

        public EntityId* Components
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => components;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => components = value;
        }

        public TableColumns* Columns
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => columns;
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => columns = value;
        }

    }

}

