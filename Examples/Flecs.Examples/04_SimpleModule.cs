using System;
using static Flecs.Macros;

namespace Flecs.Examples
{
	public unsafe class SimpleModuleExample
	{
		struct SimpleModule
		{
			public EntityId PositionEntityId;
			public TypeId PositionTypeId;
			public EntityId VelocityEntityId;
			public TypeId VelocityTypeId;
			public EntityId MoveEntityId;
			public TypeId MoveTypeId;
		}

		public static void Run(World world)
		{
			ECS_IMPORT(world, "SimpleModule", SimpleModuleImport, 0);

			/* Create an entity with Position and Velocity */
			var e = ECS_ENTITY(world, "MyEntity", "Position, Velocity");

			/* Initialize values for the entity */
			ecs.set(world, e, new Position { X = 0, Y = 0 });
			ecs.set(world, e, new Velocity { X = 1, Y = 1 });

			/* Run systems */
			ecs.progress(world, 0);
			ecs.progress(world, 0);
			ecs.progress(world, 0);
		}

		/* Implement a simple move system */
		static void Move(ref Rows rows)
		{
			ECS_COLUMN<Position>(ref rows, out var p, 1);
			ECS_COLUMN<Velocity>(ref rows, out var v, 2);

			for (var i = 0; i < rows.count; i++)
			{
				p[i].X += v[i].X;
				p[i].Y += v[i].Y;

				Console.WriteLine("{0} moved to (.x = {1}, .y = {2})",
					ecs.get_id(rows.world, rows[i]), p[i].X, p[i].Y);
			}
		}

		static void SimpleModuleImport(World world, int flags)
		{
			/* Define module */
			var handles = ECS_MODULE<SimpleModule>(world);

			/* Register components */
			var posTypeId = ECS_COMPONENT<Position>(world);
			var velTypeId = ECS_COMPONENT<Velocity>(world);

			/* Define a system called Move that is executed every frame, and subscribes
			 * for the 'Position' and 'Velocity' components */
			var systemEntity = ECS_SYSTEM(world, Move, SystemKind.OnUpdate, "Position, Velocity");

			///* Export handles to module contents */
			handles->PositionEntityId = ecs.type_to_entity(world, posTypeId);
			handles->PositionTypeId = posTypeId;
			handles->VelocityEntityId = ecs.type_to_entity(world, velTypeId);
			handles->VelocityTypeId = velTypeId;
			handles->MoveEntityId = systemEntity;
			handles->MoveTypeId = ecs.type_from_entity(world, systemEntity);
		}
	}
}
