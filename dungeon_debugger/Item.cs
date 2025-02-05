using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dungeon_debugger
{
    // Item base class
    public abstract class Item
    {
        public string? Name { get; set; }
        public abstract void Use(Player player);
    }

    // Vial class
    public class Vial : Item
    {
        public int DamageBoost { get; } = 5;

        public Vial()
        {
            Name = "Vial";
        }

        public override void Use(Player player)
        {
            //player.Heal(HealAmount);
            Console.WriteLine($"You used a {Name} and restored {HealAmount} health.");
        }
    }

    // Bandage class
    public class Bandage : Item
    {
        public int HealAmount { get; } = 15;

        public Bandage()
        {
            //player.IncreaseDamage(DamageBoost);
            Console.WriteLine($"You used a {Name} and gained {DamageBoost} extra damage for the next attack.");
        }
    }

    // Shield class
    public class Shield : Item
    {
        public int TemporaryHealthBoost { get; } = 10;

        public Shield()
        {
            Name = "Shield";
        }

        public override void Use(Player player)
        {
            //player.IncreaseTemporaryHealth(TemporaryHealthBoost);
            Console.WriteLine($"You used a {Name} and gained {TemporaryHealthBoost} temporary health.");
        }
    }
}
