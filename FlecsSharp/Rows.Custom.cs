using System.Runtime.CompilerServices;


namespace FlecsSharp
{
	unsafe partial struct Rows
	{
		public EntityId this[int i]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get => this.entities[i];
		}
	}
}