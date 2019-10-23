using System;
using NUnit.Framework;
using static Flecs.Macros;

namespace Flecs.Tests
{
	[TestFixture]
	public unsafe class Modules : AbstractTest
	{
		struct SimpleModule
		{
			public EntityId PositionEntityId;
			public TypeId PositionTypeId;
			public EntityId VelocityEntityId;
			public TypeId VelocityTypeId;
		}

		void SimpleModuleImport(World world, int flags)
		{
			var handles = ECS_MODULE<SimpleModule>(world);

			var posTypeId = ECS_COMPONENT<Position>(world);
			var velTypeId = ECS_COMPONENT<Velocity>(world);

			handles->PositionEntityId = ecs.type_to_entity(world, posTypeId);
			handles->PositionTypeId = posTypeId;
			handles->VelocityEntityId = ecs.type_to_entity(world, velTypeId);
			handles->VelocityTypeId = velTypeId;
		}

		[Test]
		public void Modules_simple_module()
		{
			ECS_IMPORT(world, "SimpleModule", SimpleModuleImport, 0);

			var e = ecs_new<Position>(world);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs_has<Position>(world, e));

			ecs_add<Velocity>(world, e);
			Assert.IsTrue(ecs_has<Velocity>(world, e));
		}

		void AddVtoP(ref Rows rows)
		{
			var modulePtr = ecs_column<SimpleModule>(ref rows, 2);

			for (var i = 0; i < rows.count; i++)
				ecs_add(world, rows[i], modulePtr->VelocityTypeId);
		}

		[Test]
		public void Modules_import_module_from_system()
		{
			var moduleTypeId = ECS_IMPORT(world, "SimpleModule", SimpleModuleImport, 0);
			ECS_SYSTEM(world, AddVtoP, SystemKind.OnUpdate, "Position, $.SimpleModule");

			var module_ptr = ecs_get_singleton_ptr(world, moduleTypeId);
			Assert.IsTrue(module_ptr != IntPtr.Zero);

			var e = ecs_new<Position>(world);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs_has<Position>(world, e));

			ecs.progress(world, 1);

			Assert.IsTrue(ecs_has<Velocity>(world, e));
		}

		[Test]
		public void Modules_import_from_on_add_system()
		{
			var moduleTypeId = ECS_IMPORT(world, "SimpleModule", SimpleModuleImport, 0);
			ECS_SYSTEM(world, AddVtoP, SystemKind.OnAdd, "Position, $.SimpleModule");

			var module_ptr = ecs_get_singleton_ptr(world, moduleTypeId);
			Assert.IsTrue(module_ptr != IntPtr.Zero);

			var e = ecs_new<Position>(world);
			Assert.NotZero((UInt64)e);
			Assert.IsTrue(ecs_has<Position>(world, e));
			Assert.IsTrue(ecs_has<Velocity>(world, e));
		}

		[Test]
		public void Modules_import_again() {

			var moduleTypeId = ECS_IMPORT(world, "SimpleModule", SimpleModuleImport, 0);
			var moduleTypeId2 = ECS_IMPORT(world, "SimpleModule", SimpleModuleImport, 0);

			Assert.AreEqual(moduleTypeId.ptr, moduleTypeId2.ptr);
		}
	}
}
