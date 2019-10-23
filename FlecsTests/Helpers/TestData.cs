using System;
using NUnit.Framework;
using static Flecs.Macros;

namespace Flecs.Tests
{
	public unsafe static class TestData
	{
		public static void ProbeSystem(ref Rows rows)
		{
			var ctx = (SysTestData*)ecs.get_context(rows.world);
			if ((IntPtr)ctx == IntPtr.Zero)
				return;

			ctx->system = rows.system;
			ctx->offset = 0;
			ctx->column_count = rows.columnCount;
			ctx->parameters = rows.param;

			for (var i = 0; i < ctx->column_count; i++)
			{
				ctx->SetC(ctx->invoked, i, rows.Components[i].Value);
				ctx->SetS(ctx->invoked, i, ecs.column_source(ref rows, (uint)i + 1).Value);

				// Make sure ecs_column functions work
				var t = ecs.column_type(ref rows, (uint)i + 1);
				Assert.IsTrue(t.ptr != IntPtr.Zero);

				var e = ecs.column_entity(ref rows, (uint)i + 1);
				Assert.IsTrue(e.Value != 0);
			}

			if (rows.tableColumns != IntPtr.Zero)
			{
				var e = new Span<EntityId>(ecs_column<EntityId>(ref rows, 0), (int)rows.count);
				if (e != null)
				{
					for (var i = 0; i < rows.count; i++)
					{
						ctx->e[i + ctx->count] = e[i].Value;

						// Make sure ecs_field works for all columns
						for (var c = 0; c < ctx->column_count; c++)
							_ecs.field(ref rows, (UIntPtr)0, (uint)c, (uint)i);
					}
					ctx->count += rows.count;
				}
			}

			ctx->invoked++;
		}
	}

	public struct Position
	{
		public float x, y;
	}

	public struct Velocity
	{
		public float x, y;
	}

	public struct Mass
	{
		public float mass;
	}

	public struct Rotation
	{
		public float rotation;
	}

	public struct Speed
	{
		public int SpeedValue;
	}

	public unsafe struct SysTestData
	{
		public EntityId system;
		public uint offset;
		public uint count;
		public int invoked;
		public uint column_count;
		public fixed UInt64 e[64];
		public fixed UInt64 c[64 * 20];
		public fixed UInt64 s[64 * 20];
		public IntPtr parameters;

		public UInt64 GetC(int row, int col) => c[row * 64 + col];
		public void SetC(int row, int col, UInt64 val) => c[row * 64 + col] = val;
		public UInt64 GetS(int row, int col) => s[row * 64 + col];
		public void SetS(int row, int col, UInt64 val) => s[row * 64 + col] = val;
	}
}
