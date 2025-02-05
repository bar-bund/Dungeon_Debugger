using System;
using System.Collections.Generic;

namespace dungeon_debugger
{
    // Base class for all items
    public abstract class Item
    {
        public string? Name { get; set; }
        public abstract void Use(Player player);
    }


    // Healing item - Bandage
    public class Bandage : Item
    {
        private const int HealAmount = 15;

        public Bandage()
        {
            Name = "Bandage";
        }

        public override void Use(Player player)
        {
            //player.Heal(HealAmount);
            Console.WriteLine($"You used a {Name} and restored {HealAmount} health.");
        }
    }


    // Damage-boosting item - Vial
    public class Vial : Item
    {
        private const int DamageBoost = 5;

        public Vial()
        {
            Name = "Vial";
        }

        public override void Use(Player player)
        {
            //player.IncreaseDamage(DamageBoost);
            Console.WriteLine($"You used a {Name} and gained {DamageBoost} extra damage for the next attack.");
        }
    }


    // Defensive item - Shield
    public class Shield : Item
    {
        private const int TemporaryHealthBoost = 10;

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
