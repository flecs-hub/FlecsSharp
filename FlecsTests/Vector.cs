using System;
using NUnit.Framework;

namespace Flecs.Tests
{
	[TestFixture]
	public unsafe class VectorTests : AbstractTest
	{
		int CompreInt(void* p1, void* p2)
		{
			var v1 = *(int*)p1;
			var v2 = *(int*)p2;

			if (v1 == v2)
				return 0;
			if (v1 < v2)
				return -1;

			return 1;
		}

		VectorParams ArrParams = new VectorParams { elementSize = (uint)Heap.SizeOf<int>() };

		Vector* FillArray(Vector* array)
		{
			int* elem;

			for (var i = 0; i < 4; i++)
			{
				elem = (int*)ecs.vector_add(ref array, ref ArrParams);
				*elem = i;
			}
			return array;
		}

		void Vector_setup() => ecs.os_set_api_defaults();

		[Test]
		public void Vector_free_empty()
		{
			var array = ecs.vector_new(ref ArrParams, 0);
			Assert.IsTrue(&array != null);
			ecs.vector_free(array);
		}

		[Test]
		public void Vector_count()
		{
			var array = ecs.vector_new(ref ArrParams, 4);
			array = FillArray(array);
			Assert.IsTrue(ecs.vector_size(array) == 4);
			Assert.IsTrue(ecs.vector_count(array) == 4);
			ecs.vector_free(array);
		}

		[Test]
		public void Vector_count_empty()
		{
			var array = ecs.vector_new(ref ArrParams, 0);
			Assert.IsTrue(&array != null);
			Assert.IsTrue(ecs.vector_count(array) == 0);
			ecs.vector_free(array);
		}

		[Test]
		public void Vector_get()
		{
			var array = ecs.vector_new(ref ArrParams, 4);
			array = FillArray(array);
			int* elem = (int*)ecs.vector_get(array, ref ArrParams, 1);
			Assert.IsTrue(elem != null);
			Assert.IsTrue(*elem == 1);
			ecs.vector_free(array);
		}

		[Test]
		public void Vector_get_first()
		{
			var array = ecs.vector_new(ref ArrParams, 4);
			array = FillArray(array);
			int* elem = (int*)ecs.vector_get(array, ref ArrParams, 0);
			Assert.IsTrue(elem != null);
			Assert.IsTrue(*elem == 0);
			ecs.vector_free(array);
		}

		[Test]
		public void Vector_get_last()
		{
			var array = ecs.vector_new(ref ArrParams, 4);
			array = FillArray(array);
			int* elem = (int*)ecs.vector_get(array, ref ArrParams, 3);
			Assert.IsTrue(elem != null);
			Assert.IsTrue(*elem == 3);
			ecs.vector_free(array);
		}

		[Test]
		public void Vector_get_empty()
		{
			var array = ecs.vector_new(ref ArrParams, 4);
			int* elem = (int*)ecs.vector_get(array, ref ArrParams, 1);
			Assert.IsTrue(elem == null);
			ecs.vector_free(array);
		}

		[Test]
		public void Vector_get_out_of_bound()
		{
			var array = ecs.vector_new(ref ArrParams, 4);
			array = FillArray(array);
			int* elem = (int*)ecs.vector_get(array, ref ArrParams, 4);
			Assert.IsTrue(elem == null);
			ecs.vector_free(array);
		}

		[Test]
		public void Vector_add_empty()
		{
			var array = ecs.vector_new(ref ArrParams, 0);
			Assert.IsTrue(ecs.vector_count(array) == 0);
			Assert.IsTrue(ecs.vector_size(array) == 0);

			ecs.vector_add(ref array, ref ArrParams);
			Assert.IsTrue(ecs.vector_count(array) == 1);
			Assert.IsTrue(ecs.vector_size(array) == 1);
			ecs.vector_free(array);
		}
	}
}
