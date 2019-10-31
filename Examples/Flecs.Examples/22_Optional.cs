using System;
using static Flecs.Macros;


namespace Flecs.Examples
{
	public class Optional
	{
		static void Regenerate(ref Rows rows)
		{
			ECS_COLUMN<float>(ref rows, out var health, 1);
			ECS_COLUMN<float>(ref rows, out var stamina, 2);
			ECS_COLUMN<float>(ref rows, out var mana, 3);

			for (var i = 0; i < rows.count; i++)
			{
				if (health != null)
				{
					health[i]++;
					Console.WriteLine("{0}: process health", rows.Entities[i]);
				}

				if (stamina != null)
				{
					stamina[i]++;
					Console.WriteLine("{0}: process stamina", rows.Entities[i]);
				}

				if (mana != null)
				{
					mana[i]++;
					Console.WriteLine("{0}: process mana", rows.Entities[i]);
				}
			}
		}

		public static void Run(World world)
		{
			var healthType = Caches.AddComponentTypedef<float>(world, "Health");
			var staminaType = Caches.AddComponentTypedef<float>(world, "Stamina");
			var manaType = Caches.AddComponentTypedef<float>(world, "Mana");

			ECS_SYSTEM(world, Regenerate, SystemKind.OnUpdate, "?Health, ?Stamina, ?Mana");

			/* Create three entities that will all match with the Regenerate system */
			ecs.new_entity(world, healthType);
			ecs.new_entity(world, staminaType);
			ecs.new_entity(world, manaType);

			/* Run systems */
			ecs.progress(world, 0);
		}
	}
}