using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace FlecsSharp
{

    public delegate void SystemAction<T>(ref Rows ids, Span<T> comp) where T : unmanaged;
    public delegate void SystemAction<T1, T2>(ref Rows ids, Span<T1> comp1, Span<T2> comp2) where T1 : unmanaged where T2 : unmanaged;

    unsafe partial struct World : IDisposable
    {

        static Dictionary<(World, Type), TypeId> typeMap = new Dictionary<(World, Type), TypeId>();
        static Dictionary<(World, EntityId), SystemActionDelegate> systemActions = new Dictionary<(World, EntityId), SystemActionDelegate>();
        public struct ContextData
        {
            internal DynamicBuffer stringBuffer;
        }

        ContextData* ctx => (ContextData*)ecs.get_context(this);
        public DynamicBuffer StringBuffer => ctx->stringBuffer;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static World Create()
        {
            var w = ecs.init();
            var context = Heap.Alloc<ContextData>();
            context->stringBuffer = DynamicBuffer.Create();

            ecs.set_context(w, (IntPtr)context);
            return w;
        }


        ///<summary>
        /// Delete a world. This operation deletes the world, and all entities, components and systems within the world.
        ///</summary>
        ///<param name="world"> [in]  The world to delete.</param>
        ///<code>
        ///int ecs_fini(ecs_world_t *world)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Dispose()
        {
            StringBuffer.Dispose();
            Heap.Free(ctx);
            ecs.fini(this);
        }


        //public EntityId NewComponent<T>() where T : unmanaged
        //{
        //    var name = typeof(T).Name;
        //    var charPtr = StringBuffer.AddUTF8String(name);
        //    var componentId = ecs.new_component(this, charPtr, (UIntPtr)Marshal.SizeOf<T>());
        //    typeMap.Add((this, typeof(T)), TypeFromEntity(componentId));
        //    return componentId;
        //}

        TypeId getTypeId(Type compType)
        {
            if (!typeMap.TryGetValue((this, compType), out var val))
            {
                var name = compType.Name;
                var charPtr = StringBuffer.AddUTF8String(name);
                var componentId = ecs.new_component(this, charPtr, (UIntPtr)Marshal.SizeOf(compType));
                var typeId = TypeFromEntity(componentId);
                typeMap.Add((this, compType), typeId);
                return typeId;
            }

            return val;

        }

        public EntityId AddSystem(SystemKind kind, ReadOnlySpan<char> name, SystemActionDelegate systemImpl, params Type[] componentTypes)
        {
            var systemNamePtr = StringBuffer.AddUTF8String(name);
            var components = BuildComponentQuery(componentTypes);
            var signaturePtr = StringBuffer.AddUTF8String(components);
            var componentId = ecs.new_system(this, systemNamePtr, kind, signaturePtr, systemImpl);
            systemActions[(this, componentId)] = systemImpl;
            return componentId;
        }

        public EntityId AddSystem(SystemKind kind, SystemActionDelegate systemImpl, params Type[] componentTypes)
            => AddSystem(kind, systemImpl.Method.Name, systemImpl, componentTypes);


        public void AddSystem<T1>(SystemAction<T1> systemImpl, SystemKind kind) where T1 : unmanaged
        {
            SystemActionDelegate del = delegate (ref Rows e)
            {
                var set1 = (T1*)_ecs.column(out e, new UIntPtr((uint)sizeof(T1)), 1);
                systemImpl(ref e, new Span<T1>(set1, (int)e.count));
            };

            AddSystem(kind, systemImpl.Method.Name, del, typeof(T1));
        }

        public void AddSystem<T1, T2>(SystemAction<T1, T2> systemImpl, SystemKind kind)
            where T1 : unmanaged
            where T2 : unmanaged
        {
            SystemActionDelegate del = delegate (ref Rows e)
            {
                var set1 = (T1*)_ecs.column(out e, new UIntPtr((uint)sizeof(T1)), 1);
                var set2 = (T2*)_ecs.column(out e, new UIntPtr((uint)sizeof(T2)), 2);
                systemImpl(ref e, new Span<T1>(set1, (int)e.Count), new Span<T2>(set2, (int)e.Count));
            };

            AddSystem(kind, systemImpl.Method.Name, del, typeof(T1), typeof(T2));
        }


        private string BuildComponentQuery(params Type[] componentTypes)
        {
            var sb = new StringBuilder(64);
            for (int i = 0; i < componentTypes.Length; i++)
            {
                getTypeId(componentTypes[i]);
                sb.Append(componentTypes[i].Name);

                if (i != componentTypes.Length - 1)
                    sb.Append(", ");
            }
            var components = sb.ToString();
            return components;
        }


        EntityId NewEntity(string entityName, params Type[] componentTypes)
        {
            var entityNamePtr = StringBuffer.AddUTF8String(entityName);
            var components = BuildComponentQuery(componentTypes);
            var componentsQueryPtr = StringBuffer.AddUTF8String(components);
            var componentId = ecs.new_entity(this, entityNamePtr, componentsQueryPtr);
            return componentId;
        }

        public unsafe EntityId NewEntity<T1>(string entityName, T1 comp1 = default) where T1 : unmanaged
        {
            var entt = NewEntity(entityName, typeof(T1));
            Set(entt, comp1);
            return entt;
        }

        public unsafe EntityId NewEntity<T1, T2>(string entityName, T1 comp1 = default, T2 comp2 = default) where T1 : unmanaged where T2 : unmanaged
        {
            var entt = NewEntity(entityName, typeof(T1), typeof(T2));
            Set(entt, comp1);
            Set(entt, comp2);
            return entt;
        }

        ///<summary>
        /// Set value of component. This function sets the value of a component on the specified entity. If the component does not yet exist, it will be added to the entity.
        ///</summary>
        ///<param name="world"> [in]  The world. </param>
        ///<param name="entity"> [in]  The entity on which to set the component. </param>
        ///<param name="component"> [in]  The component to set.</param>
        ///<remarks>
        /// This function can be used like this: Foo value = {.x = 10, .y = 20}; ecs_set_ptr(world, e, tFoo, &value);
        /// This function is wrapped by the ecs_set convenience macro, which can be used like this:
        /// ecs_set(world, e, Foo, {.x = 10, .y = 20});
        ///</remarks>
        ///<code>
        ///ecs_entity_t _ecs_set_ptr(ecs_world_t *world, ecs_entity_t entity,
        ///                          ecs_type_t type, size_t size, void *ptr)
        ///</code>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public EntityId Set<T>(EntityId entity, T value) where T : unmanaged
        {
            var type = getTypeId(typeof(T));
            T* val = &value;
            return _ecs.set_ptr(this, entity, new EntityId((ulong)type.Ptr), (UIntPtr)Marshal.SizeOf<T>(), (IntPtr)val);
        }

    }
}