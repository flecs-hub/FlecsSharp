using System;
using System.Runtime.CompilerServices;

namespace Flecs
{
	// ecs_table_data_t: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L717
	unsafe partial struct TableData
	{
		public Set<EntityId> Entities
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new Set<EntityId>(entities, rowCount);
		}

		public Set<EntityId> Components
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new Set<EntityId>(components, columnCount);
		}
	}
}