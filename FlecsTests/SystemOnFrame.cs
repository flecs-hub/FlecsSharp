using System;
using NUnit.Framework;

namespace Flecs.Tests
{
	[TestFixture]
	public unsafe class SystemOnFrame : AbstractTest
	{
		static void Iter(ref Rows rows)
		{
			ecs.ECS_COLUMN<Position>(ref rows, out var p, 1);

			Span<Velocity> v = null;
			Span<Mass> m = null;

			var wtf = ecs.ecs_column<Velocity>(ref rows, 2);
			if (rows.columnCount >= 2)
			{
				var column = ecs.ecs_column<Velocity>(ref rows, 2);
				if (column != null)
					v = new Span<Velocity>(column, (int)rows.count);
			}

			if (rows.columnCount >= 3)
			{
				var column = ecs.ecs_column<Mass>(ref rows, 3);
				if (column != null)
					m = new Span<Mass>(column, (int)rows.count);
			}

			TestData.ProbeSystem(ref rows);

			for (var i = 0; i < rows.count; i ++)
			{
				p[i].x = 10;
				p[i].y = 20;

				if (v != null)
				{
					v[i].x = 30;
					v[i].y = 40;
				}

				if (m != null)
					m[i].mass = 50;
			}
		}

		[Test]
		public void SystemOnFrame_1_type_1_component()
		{
			var positionTypeId = ecs.ECS_COMPONENT<Position>(world);

			var e_1 = ecs.ECS_ENTITY(world, "e_1", "Position");
			var e_2 = ecs.ECS_ENTITY(world, "e_2", "Position");
			var e_3 = ecs.ECS_ENTITY(world, "e_3", "Position");

			var systemEntityId = ecs.ECS_SYSTEM(world, Iter, SystemKind.OnUpdate, "Position");

			var ctx = Heap.Alloc<SysTestData>();
			ecs.set_context(world, (IntPtr)ctx);

			ecs.progress(world, 1);

			Assert.IsTrue(ctx->count == 3);
			Assert.IsTrue(ctx->invoked == 1);
			Assert.IsTrue(ctx->system.Value == systemEntityId.Value);
			Assert.IsTrue(ctx->column_count == 1);

			Assert.IsTrue(ctx->e[0] == e_1.Value);
			Assert.IsTrue(ctx->e[1] == e_2.Value);
			Assert.IsTrue(ctx->e[2] == e_3.Value);

			var positionEntityId = ecs.type_to_entity(world, positionTypeId);
			Assert.IsTrue(positionEntityId.Value == ctx->c[0]);
		}

		[Test]
		public void SystemOnFrame_1_type_3_component()
		{
			var positionTypeId = ecs.ECS_COMPONENT<Position>(world);
			var velTypeId = ecs.ECS_COMPONENT<Velocity>(world);
			var massTypeId = ecs.ECS_COMPONENT<Mass>(world);

			var systemEntityId = ecs.ECS_SYSTEM(world, Iter, SystemKind.OnUpdate, "Position, Velocity, Mass");

			var ctx = Heap.Alloc<SysTestData>();
			ecs.set_context(world, (IntPtr)ctx);

			var e_1 = ecs.ECS_ENTITY(world, "e_1", "Position, Velocity, Mass");
			var e_2 = ecs.ECS_ENTITY(world, "e_2", "Position, Velocity, Mass");
			var e_3 = ecs.ECS_ENTITY(world, "e_3", "Position, Velocity, Mass");

			ecs.progress(world, 1);

			Assert.IsTrue(ctx->count == 3);
			Assert.IsTrue(ctx->invoked == 1);
			Assert.IsTrue(ctx->system.Value == systemEntityId.Value);
			Assert.IsTrue(ctx->column_count == 3);

			Assert.IsTrue(ctx->e[0] == e_1.Value);
			Assert.IsTrue(ctx->e[1] == e_2.Value);
			Assert.IsTrue(ctx->e[2] == e_3.Value);

			Assert.IsTrue(ctx->GetC(0, 0) == ecs.type_to_entity(world, positionTypeId).Value);
			Assert.IsTrue(ctx->GetS(0, 0) == 0);
			Assert.IsTrue(ctx->GetC(0, 1) == ecs.type_to_entity(world, velTypeId).Value);
			Assert.IsTrue(ctx->GetS(0, 1) == 0);
			Assert.IsTrue(ctx->GetC(0, 2) == ecs.type_to_entity(world, massTypeId).Value);
			Assert.IsTrue(ctx->GetS(0, 2) == 0);

			var p = (Position*)ecs.ecs_get_ptr(world, e_1, positionTypeId);
			Assert.IsTrue(p->x == 10);
			Assert.IsTrue(p->y == 20);

			p = (Position*)ecs.ecs_get_ptr(world, e_2, positionTypeId);
			Assert.IsTrue(p->x == 10);
			Assert.IsTrue(p->y == 20);

			p = (Position*)ecs.ecs_get_ptr(world, e_3, positionTypeId);
			Assert.IsTrue(p->x == 10);
			Assert.IsTrue(p->y == 20);

			var v = (Velocity*)ecs.ecs_get_ptr(world, e_1, velTypeId);
			Assert.IsTrue(v->x == 30);
			Assert.IsTrue(v->y == 40);

			v = (Velocity*)ecs.ecs_get_ptr(world, e_2, velTypeId);
			Assert.IsTrue(v->x == 30);
			Assert.IsTrue(v->y == 40);

			v = (Velocity*)ecs.ecs_get_ptr(world, e_3, velTypeId);
			Assert.IsTrue(v->x == 30);
			Assert.IsTrue(v->y == 40);
		}

		[Test]
		public void SystemOnFrame_2_type_1_and_1_not()
		{
			var positionTypeId = ecs.ECS_COMPONENT<Position>(world);
			var velTypeId = ecs.ECS_COMPONENT<Velocity>(world);

			var systemEntityId = ecs.ECS_SYSTEM(world, Iter, SystemKind.OnUpdate, "Position, !Velocity");

			var ctx = Heap.Alloc<SysTestData>();
			ecs.set_context(world, (IntPtr)ctx);

			var e_1 = ecs.ECS_ENTITY(world, "e_1", "Position");
			var e_2 = ecs.ECS_ENTITY(world, "e_2", "Position");
			var e_3 = ecs.ECS_ENTITY(world, "e_3", "Position, Velocity");

			ecs.progress(world, 1);

			Assert.IsTrue(ctx->count == 2);
			Assert.IsTrue(ctx->invoked == 1);
			Assert.IsTrue(ctx->system.Value == systemEntityId.Value);
			Assert.IsTrue(ctx->column_count == 2);

			Assert.IsTrue(ctx->e[0] == e_1.Value);
			Assert.IsTrue(ctx->e[1] == e_2.Value);

			Assert.IsTrue(ctx->GetC(0, 0) == ecs.type_to_entity(world, positionTypeId).Value);
			Assert.IsTrue(ctx->GetS(0, 0) == 0);
			Assert.IsTrue(ctx->GetC(0, 1) == ecs.type_to_entity(world, velTypeId).Value);
			Assert.IsTrue(ctx->GetS(0, 1) == 0);

			var p = (Position*)ecs.ecs_get_ptr(world, e_1, positionTypeId);
			Assert.IsTrue(p->x == 10);
			Assert.IsTrue(p->y == 20);

			p = (Position*)ecs.ecs_get_ptr(world, e_2, positionTypeId);
			Assert.IsTrue(p->x == 10);
			Assert.IsTrue(p->y == 20);
		}
	}
}