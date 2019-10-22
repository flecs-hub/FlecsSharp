using NUnit.Framework;

namespace Flecs.Tests
{
	public abstract class AbstractTest
	{
		protected World world;

		[SetUp]
		protected virtual void Setup() => world = World.Create();

		[TearDown]
		protected virtual void TearDown() => world.Dispose();
	}
}
