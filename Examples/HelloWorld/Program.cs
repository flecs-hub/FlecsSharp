using FlecsSharp;

namespace HelloWorld
{
    class Program
    {

        public struct Position
        {
            public float X { get; set; }
            public float Y { get; set; }
        }

        public struct Speed
        {
            public int Value { get; set; }
        }

        static void MoveSystem(EntitySet ids, Set<Position> position, Set<Speed> speed)
        {
            for(uint i = 0; i < ids.Count; i++)
            {
                EntityId id = ids[i];
                position[i].X += speed[i].Value * ids.DeltaTime;
                position[i].Y += speed[i].Value * ids.DeltaTime;
            }
        }

        static void Main(string[] args)
        {
            using (var world = World.Create())
            {
                world.AddSystem<Position, Speed>(MoveSystem, SystemKind.OnUpdate);

                var myEntity = world.NewEntity("MyEntity", new Position { X = 1, Y = 2 }, new Speed { Value = 5 });

                while (world.Progress(1))
                {

                }
            }
        }

    }
}
