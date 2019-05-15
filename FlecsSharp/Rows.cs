using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using SharpC;

namespace FlecsSharp
{
    public unsafe partial struct Rows
    {
        //public static Rows New(in Data val = default) => new Rows(Alloc(in val));
        //public void Dispose()
        //{
            //if(ptr != null) Free(ptr);
        //}

        public World World
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->world;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->world = value; }
        }

        public Entity System
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->system;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->system = value; }
        }

        public int* Columns
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->columns;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->columns = value;
        }

        public ushort ColumnCount
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->columnCount;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->columnCount = value;
        }

        public IntPtr Table
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->table;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->table = value;
        }

        public IntPtr TableColumns
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->tableColumns;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->tableColumns = value;
        }

        public Reference References
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->references;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->references = value; }
        }

        public Entity* Components
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->components;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->components = value; }
        }

        public Entity* Entities
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->entities;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->entities = value; }
        }

        public IntPtr Param
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->param;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->param = value;
        }

        public float DeltaTime
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->deltaTime;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->deltaTime = value;
        }

        public uint FrameOffset
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->frameOffset;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->frameOffset = value;
        }

        public uint Offset
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->offset;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->offset = value;
        }

        public uint Count
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->count;
            [MethodImpl(MethodImplOptions.AggressiveInlining)] set => ptr->count = value;
        }

        public Entity InterruptedBy
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)] get => ptr->interruptedBy;
            //[MethodImpl(MethodImplOptions.AggressiveInlining)]  set => ptr->interruptedBy = value; }
        }

        ///<summary>
        /// Obtain a column from inside a system 
        ///</summary>
        ///<code>
        ///void *_ecs_column(ecs_rows_t *rows, uint32_t index, bool test)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr EcsColumn(uint index, Enable test)
        {
            return _ecs.column( this, index, test);
        }

        ///<summary>
        /// Obtain a reference to a shared component 
        ///</summary>
        ///<code>
        ///void *_ecs_shared(ecs_rows_t *rows, uint32_t index, bool test)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr EcsShared(uint index, Enable test)
        {
            return _ecs.shared( this, index, test);
        }

        ///<summary>
        /// Obtain a single field.  This is an alternative method to ecs_column to access data in a system, which accesses data from individual fields (one column per row). This method is slower than iterating over a column array, but has the added benefit that it automatically abstracts between shared components and owned components. 
        ///</summary>
        ///<remarks>
        /// This is particularly useful if a system is unaware whether a particular  column is from a prefab, as a system does not explicitly state in an argument expression whether prefabs should be matched with, thus it is possible that a system receives both shared and non-shared data for the same column.
        /// When a system uses fields, these differences will be transparent, and it is therefore the method that provides the most flexibility with respect to the kind of data the system can accept.
        ///</remarks>
        ///<code>
        ///void *_ecs_field(ecs_rows_t *rows, uint32_t index, uint32_t column, bool test)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IntPtr EcsField(uint index, uint column, Enable test)
        {
            return _ecs.field( this, index, column, test);
        }

        ///<summary>
        /// Obtain the source of a column from inside a system. This operation lets you obtain the entity from which the column data was resolved. In most cases a component will come from the entities being iterated over, but when using prefabs or containers, the component can be shared between entities. For shared components, this function will return the original entity on which the component is stored.
        ///</summary>
        ///<param name="rows"> [in]  Pointer to the rows object passed into the system callback. </param>
        ///<param name="index"> [in]  An index identifying the column for which to obtain the component. </param>
        ///<returns>
        /// The source entity for the column. 
        ///</returns>
        ///<remarks>
        /// If a column is specified for which the component is stored on the entities being iterated over, the operation will return 0, as the entity id in that case depends on the row, not on the column. To obtain the entity ids for a row, a system should access the entity column (column zero) like this:
        /// ecs_entity_t *entities = ecs_column(rows, ecs_entity_t, 0);
        ///</remarks>
        ///<code>
        ///ecs_entity_t _ecs_column_source(ecs_rows_t *rows, uint32_t column, bool test)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Entity EcsColumnSource(uint column, Enable test)
        {
            return _ecs.column_source( this, column, test);
        }

        ///<summary>
        /// Obtain the component for a column inside a system. This operation obtains the component handle for a column in the system. This function wraps around the 'components' array in the ecs_rows_t type.
        ///</summary>
        ///<param name="rows"> [in]  Pointer to the rows object passed into the system callback. </param>
        ///<param name="index"> [in]  An index identifying the column for which to obtain the component. </param>
        ///<returns>
        /// The component for the specified column, or 0 if failed.
        ///</returns>
        ///<remarks>
        /// Note that since component identifiers are obtained from the same pool as regular entities, the return type of this function is ecs_entity_t.
        /// When a system contains an argument that is prefixed with 'ID', the resolved entity will be accessible through this function as well.
        /// Column indices for system arguments start from 1, where 0 is reserved for a column that contains entity identifiers. Passing 0 to this function for the column index will return 0.
        ///</remarks>
        ///<code>
        ///ecs_entity_t _ecs_column_entity(ecs_rows_t *rows, uint32_t column, bool test)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Entity EcsColumnEntity(uint column, Enable test)
        {
            return _ecs.column_entity( this, column, test);
        }

        ///<summary>
        /// Obtain the type of a column from inside a system.  This operation is equivalent to ecs_column_entity, except that it returns a type, instead of an entity handle. Invoking this function is the same as doing:
        ///</summary>
        ///<param name="rows"> [in]  Pointer to the rows object passed into the system callback. </param>
        ///<param name="index"> [in]  An index identifying the column for which to obtain the component. </param>
        ///<returns>
        /// The type for the specified column, or 0 if failed.
        ///</returns>
        ///<remarks>
        /// ecs_type_from_entity( ecs_column_entity(rows, index));
        /// This function is wrapped in the following convenience macro which ensures that the type variable is named so it can be used with functions like ecs_add and ecs_set:
        /// ECS_COLUMN_COMPONENT(rows, Position, 1);
        /// After this macro you can invoke functions like ecs_set as you normally would:
        /// ecs_set(world, e, Position, {10, 20});
        ///</remarks>
        ///<code>
        ///ecs_type_t _ecs_column_type(ecs_rows_t *rows, uint32_t column, bool test)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Type EcsColumnType(uint column, Enable test)
        {
            return _ecs.column_type( this, column, test);
        }

    }

}

