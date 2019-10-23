using System;
using System.Runtime.CompilerServices;

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
	}
}