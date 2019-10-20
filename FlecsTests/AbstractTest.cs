using NUnit.Framework;

namespace Flecs.Tests
{
	public abstract class AbstractTest
	{
		protected World _world;

		[SetUp]
		protected virtual void Setup()
		{
			_world = World.Create();
		}

		[TearDown]
		protected virtual void TearDown()
		{
			_world.Dispose();
		}
	}
}
