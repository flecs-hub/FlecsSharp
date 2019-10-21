using System;

namespace Flecs
{
	public unsafe static partial class ecs
	{
		#region Imperitive Macros

		public static EntityId ecs_new(World world, TypeId typeId)
		{
			return _ecs.@new(world, typeId);
		}

		public static EntityId ecs_new(World world, Type componentType)
		{
			return _ecs.@new(world, world.GetComponentTypeId(componentType));
		}

		public static bool ecs_has(World world, EntityId entity, TypeId typeId)
		{
			return _ecs.has(world, entity, typeId);
		}

		public static bool ecs_has(World world, EntityId entity, Type componentType)
		{
			return _ecs.has(world, entity, world.GetComponentTypeId(componentType));
		}

		public static bool ecs_has<T>(World world, EntityId entity) where T : unmanaged
		{
			return _ecs.has(world, entity, world.GetComponentTypeId(typeof(T)));
		}

		public static EntityId ecs_new_w_count(World world, TypeId typeId, uint count)
		{
			return _ecs.new_w_count(world, typeId, count);
		}

		public static EntityId ecs_new_child(World world, EntityId parent, TypeId type)
		{
			return _ecs.new_child(world, parent, type);
		}

		public static EntityId ecs_new_child_w_count(World world, EntityId parent, TypeId type, uint count)
		{
			return _ecs.new_child_w_count(world, parent, type, count);
		}

		public static EntityId ecs_new_instance(World world, EntityId baseEntityId, TypeId type)
		{
			return _ecs.new_instance(world, baseEntityId, type);
			//#define ecs_new_instance(world, base, type)\
			//		_ecs_new_instance(world, base, T##type)
		}

		public static EntityId ecs_new_instance_w_count(World world, EntityId baseEntityId, TypeId type, uint count)
		{
			return _ecs.new_instance_w_count(world, baseEntityId, type, count);
			//#define ecs_new_instance_w_count(world, base, type, count)\
			//		_ecs_new_instance_w_count(world, base, T##type, count)
		}

		public static void ecs_set(World world, EntityId entity)
		{
			//#define ecs_set(world, entity, component, ...)\
			//		_ecs_set_ptr(world, entity, ecs_entity(component), sizeof(component), &(component) __VA_ARGS__)
		}

		public static void ecs_set_ptr(World world)
		{
			//#define ecs_set_ptr(world, entity, component, ptr)\
			//		_ecs_set_ptr(world, entity, ecs_entity(component), sizeof(component), ptr)
		}

		public static void ecs_set_singleton(World world)
		{
			//#define ecs_set_singleton(world, component, ...)\
			//		_ecs_set_singleton_ptr(world, ecs_entity(component), sizeof(component), &(component) __VA_ARGS__)
		}

		public static void ecs_set_singleton_ptr(World world)
		{
			//#define ecs_set_singleton_ptr(world, component, ptr)\
			//    _ecs_set_singleton_ptr(world, ecs_entity(component), sizeof(component), ptr)
		}

		public static void ecs_add(World world)
		{
			//#define ecs_add(world, entity, type)\
			//		_ecs_add(world, entity, T##type)
		}

		public static void ecs_remove(World world)
		{
			//#define ecs_remove(world, entity, type)\
			//		_ecs_remove(world, entity, T##type)
		}

		public static void ecs_add_remove(World world)
		{
			//#define ecs_add_remove(world, entity, to_add, to_remove)\
			//		_ecs_add_remove(world, entity, T##to_add, T##to_remove)
		}

		#endregion

		#region Declarative Macros

		public static void ECS_COMPONENT(World world, Type componentType)
		{
			world.GetComponentTypeId(componentType);

			//#define ECS_COMPONENT(world, id) \
			//	  ECS_ENTITY_VAR(id) = ecs_new_component(world, #id, sizeof(id));\
			//    ECS_TYPE_VAR(id) = ecs_type_from_entity(world, ecs_entity(id));\
			//    (void)ecs_entity(id);\
			//    (void)ecs_type(id);\
		}

		public static TypeId ECS_COMPONENT<T>(World world) where T : unmanaged => world.GetComponentTypeId(typeof(T));

		public static void ECS_SYSTEM(World world)
		{
//#define ECS_SYSTEM(world, id, kind, ...) \
//			ecs_entity_t F##id = ecs_new_system(world, #id, kind, #__VA_ARGS__, id);\
//    ecs_entity_t id = F##id;\
//    ECS_TYPE_VAR(id) = ecs_type_from_entity(world, id);\
//    (void)id;\
//    (void)ecs_type(id);
		}

		public static EntityId ECS_ENTITY(World world, string id, string expr)
		{
			var idPtr = world.StringBuffer.AddUTF8String(id);
			var entityId = ecs.new_entity(world, idPtr, expr);
			//ecs.type_from_entity(world, entityId);
			return entityId;

//#define ECS_ENTITY(world, id, ...)\
//			ecs_entity_t id = ecs_new_entity(world, #id, #__VA_ARGS__);\
//    ECS_TYPE_VAR(id) = ecs_type_from_entity(world, id);\
//    (void)id;\
//    (void)ecs_type(id);
		}

		public static TypeId ECS_TAG(World world, string tag)
		{
			var idPtr = world.StringBuffer.AddUTF8String(tag);
			var entityId = ecs.new_component(world, idPtr, (UIntPtr)0);
			return ecs.type_from_entity(world, entityId);
		}

		public static TypeId ECS_TYPE(World world, string id, string expr)
		{
			var idPtr = world.StringBuffer.AddUTF8String(id);
			var entityId = ecs.new_type(world, idPtr, expr);
			return ecs.type_from_entity(world, entityId);
		}

		public static void ECS_PREFAB(World world)
		{
//#define ECS_PREFAB(world, id, ...) \
//			ecs_entity_t id = ecs_new_prefab(world, #id, #__VA_ARGS__);\
//    ECS_TYPE_VAR(id) = ecs_type_from_entity(world, id);\
//    (void)id;\
//    (void)ecs_type(id);\
		}

		#endregion
	}
}
