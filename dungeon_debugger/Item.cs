using System;
using System.Collections.Generic;

namespace dungeon_debugger
{
    // Base class for all items
    public abstract class Item
    {
        public string Name { get; }

        protected Item(string name) 
        { 
            Name = name;
        }

        public abstract void Use(Player player);
    }


    // Healing item - Bandage
    public class Bandage : Item
    {
        public Bandage() : base("Bandage") { }

        public override void Use(Player player)
        {
            int healAmount = 30;
            player.Health = player.Health + healAmount;
            Console.WriteLine($"You used a {Name} and restored {healAmount} health.");
        }
    }


    // Damage-boosting item - Vial
    public class Vial : Item
    {
        public Vial() : base("Vial") { }

        public override void Use(Player player)
        {
            int attackBoost = 15;
            player.bonusAttack += attackBoost;
            Console.WriteLine($"You used a {Name} and gained {attackBoost} extra damage for the next attack.");
        }
    }


    // Defensive item - Shield
    public class Shield : Item
    {
        public Shield() : base("Shield") { }

        public override void Use(Player player)
        {
            Console.WriteLine($"You used {Name}. Your defense increases, reducing the next attack by 50%!");
            player.reduceNextDamage = true;
        }
    }
}
