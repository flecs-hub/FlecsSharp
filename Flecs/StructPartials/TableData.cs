using System;
using System.Runtime.CompilerServices;

namespace Flecs
{
	// ecs_table_data_t: https://github.com/SanderMertens/flecs/blob/612c28635497c1749f8f3e84fa24eabfea58e05a/include/flecs.h#L717
	unsafe partial struct TableData
	{
		public Span<EntityId> Entities
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new Span<EntityId>(entities, (int)rowCount);
		}

		public Span<EntityId> Components
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => new Span<EntityId>(components, (int)columnCount);
		}
	}
}