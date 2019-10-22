using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace Flecs
{
	unsafe partial struct World : IDisposable
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static World Create()
		{
			var world = ecs.init();
			Caches.RegisterWorld(world);

			return world;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Dispose()
		{
			Caches.DeregisterWorld(this);
			ecs.fini(this);
		}

		public EntityId NewEntity<T1, T2>() where T1 : unmanaged where T2 : unmanaged
		{
			var typeId = ecs.expr_to_type(this, $"{typeof(T1).Name}, {typeof(T2).Name}");
			return _ecs.@new(this, typeId);
		}

		public EntityId NewEntity<T1, T2>(T1 comp1 = default, T2 comp2 = default) where T1 : unmanaged where T2 : unmanaged
		{
			var entityId = NewEntity<T1, T2>();
			Set(entityId, comp1);
			Set(entityId, comp2);
			return entityId;
		}

		public unsafe EntityId NewEntity<T1>(string entityName, T1 comp1 = default) where T1 : unmanaged
		{
			var entt = _ecs.@new(this, (TypeId)0);
			Set(entt, comp1);
			return entt;
		}

		public unsafe EntityId NewEntity<T1, T2>(string entityName, T1 comp1 = default, T2 comp2 = default) where T1 : unmanaged where T2 : unmanaged
		{
			var entt = NewEntity<T1, T2>();
			Set(entt, comp1);
			Set(entt, comp2);
			return entt;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public EntityId Set<T>(EntityId entity, T value) where T : unmanaged
		{
			var type = Caches.GetComponentTypeId<T>(this);
			T* val = &value;
			return _ecs.set_ptr(this, entity, ecs.type_to_entity(this, type), (UIntPtr)Marshal.SizeOf<T>(), (IntPtr)val);
		}

		public void Add<T>(EntityId entity) where T : unmanaged
		{
			_ecs.add(this, entity, Caches.GetComponentTypeId<T>(this));
		}
	}
}