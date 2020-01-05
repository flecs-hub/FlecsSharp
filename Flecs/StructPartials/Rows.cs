using System;
using System.Runtime.CompilerServices;

namespace Flecs
{
	// ecs_rows_t: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L104
	unsafe partial struct Rows
	{
		public EntityId this[int i]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => entities[i];
		}

		public EntityId this[uint i]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => entities[i];
		}

		public Span<int> Columns
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new Span<int>(columns, columnCount);
		}

		public Span<EntityId> Components
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new Span<EntityId>(components, columnCount);
		}

		public Span<EntityId> Entities
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new Span<EntityId>(entities, (int)count);
		}

		///<summary>
		/// Obtain a pointer to column data.  This function is to be used inside a system to obtain data from a column in the system signature. The provided index corresponds with the index of the element in the system signature, starting from one. For example, for the following system signature:
		///</summary>
		///<param name="rows"> [in]  The rows parameter passed into the system. </param>
		///<param name="index"> [in]  The index identifying the column in a system signature. </param>
		///<returns>
		/// A pointer to the column data if index is valid, otherwise NULL.
		///</returns>
		///<remarks>
		/// Position, Velocity
		/// Position is at index 1, and Velocity is at index 2.
		/// This function is typically invoked through the `ECS_COLUMN` macro which automates declaring a variable of the correct type in the scope of the system function.
		/// When a valid pointer is obtained, it can be used as an array with rows->count elements if the column is owned by the entity being iterated over, or as a pointer if the column is shared (see ecs_is_shared).
		///</remarks>
		///<code>
		///void *_ecs_column(ecs_rows_t *rows, size_t size, uint32_t column)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IntPtr Column(UIntPtr size, uint column) => _ecs.column(ref this, size, column);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T* Column<T>(uint column) where T : unmanaged => (T*)_ecs.column(ref this, Heap.SizeOf<T>(), column);

		///<summary>
		/// Test if column is shared or not.  The following signature shows an example of owned components and shared components:
		///</summary>
		///<returns>
		/// true if the column is shared, false if it is owned.
		///</returns>
		///<remarks>
		/// Position, CONTAINER.Velocity, MyEntity.Mass
		/// Position is an owned component, while Velocity and Mass are shared  components. While these kinds of relationships are expressed explicity in a system signature, inheritance relationships are implicit. The above signature matches both entities for which Position is owned as well as entities for which Position appears in an entity that they inherit from.
		/// If a system needs to support both cases, it needs to test whether the component is shared or not. This test only needs to happen once per system callback invocation, as all the entities being iterated over will either own or not own the component.
		///</remarks>
		///<code>
		///bool ecs_is_shared(ecs_rows_t *rows, uint32_t column)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsShared(uint column) => ecs.is_shared(ref this, column);

		///<summary>
		/// Obtain a single field.  This is an alternative method to column to access data in a system, which accesses data from individual fields (one column per row). This method is slower than iterating over a column array, but has the added benefit that it automatically abstracts between shared components and owned components.
		///</summary>
		///<remarks>
		/// This is particularly useful if a system is unaware whether a particular  column is from a prefab, as a system does not explicitly state in an argument expression whether prefabs should be matched with, thus it is possible that a system receives both shared and non-shared data for the same column.
		/// When a system uses fields, these differences will be transparent, and it is therefore the method that provides the most flexibility with respect to the kind of data the system can accept.
		///</remarks>
		///<code>
		///void *_ecs_field(ecs_rows_t *rows, size_t size, uint32_t column, uint32_t row)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T* Field<T>(uint column, uint row) where T : unmanaged => (T*)_ecs.field(ref this, Heap.SizeOf<T>(), column, row);

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
		/// ecs_entity_t *entities = column(rows, ecs_entity_t, 0);
		///</remarks>
		///<code>
		///ecs_entity_t ecs_column_source(ecs_rows_t *rows, uint32_t column)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId ColumnSource(uint column) => ecs.column_source(ref this, column);

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
		///ecs_entity_t ecs_column_entity(ecs_rows_t *rows, uint32_t column)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId ColumnEntity(uint column) => ecs.column_entity(ref this, column);

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
		/// This function is wrapped in the following convenience macro which ensures that the type variable is named so it can be used with functions like add and set:
		/// ECS_COLUMN_COMPONENT(rows, Position, 1);
		/// After this macro you can invoke functions like set as you normally would:
		/// set(world, e, Position, {10, 20});
		///</remarks>
		///<code>
		///ecs_type_t ecs_column_type(ecs_rows_t *rows, uint32_t column)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TypeId ColumnType(uint column) => ecs.column_type(ref this, column);

		///<summary>
		/// Get type of table that system is currently iterating over.
		///</summary>
		///<code>
		///ecs_type_t ecs_table_type(ecs_rows_t *rows)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public TypeId TableType() => ecs.table_type(ref this);

		///<summary>
		/// Get type of table that system is currently iterating over.
		///</summary>
		///<code>
		///void *ecs_table_column(ecs_rows_t *rows, uint32_t column)
		///</code>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public IntPtr TableColumn(uint column) => ecs.table_column(ref this, column);
	}
}