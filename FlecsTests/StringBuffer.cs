using NUnit.Framework;

namespace Flecs.Tests
{
	public class StringBuffer
	{
		protected UnmanagedStringBuffer buffer;

		[SetUp]
		protected virtual void Setup() => buffer = new UnmanagedStringBuffer();

		[TearDown]
		protected virtual void TearDown() => buffer.Dispose();

		[Test]
		public void Test_resize()
		{
			buffer.Dispose();
			buffer = UnmanagedStringBuffer.Create(10);
			CharPtr firstStringPtr = default;

			for (var i = 0; i < 10; i++)
			{
				var ptr = buffer.AddString("some string to add");
				if (i == 0)
					firstStringPtr = ptr;
			}

			Assert.AreEqual(firstStringPtr.ToString(), "some string to add");
		}
	}
}
