using System;
using System.Collections.Generic;

namespace dungeon_debugger
{
    // Base class for all items
    public abstract class Item
    {
        public string Name { get; }

        // Constructor to initialize the name of the item
        protected Item(string name) 
        { 
            Name = name;
        }

        // Abstract method that forces derived classes to implement their own 'Use' method
        public abstract void Use(Player player);
    }


    // Healing item - Bandage
    public class Bandage : Item
    {
        // Constructor calling the base class with the name "Bandage"
        public Bandage() : base("Bandage") { }

        // Override the Use method to implement specific behavior for the Bandage
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
        // Constructor calling the base class with the name "Vial"
        public Vial() : base("Vial") { }

        // Override the Use method to implement specific behavior for the Vial
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
        // Constructor calling the base class with the name "Shield"
        public Shield() : base("Shield") { }

        // Override the Use method to implement specific behavior for the Shield
        public override void Use(Player player)
        {
            Console.WriteLine($"You used {Name}. Your defense increases, reducing the next attack by 50%!");
            player.reduceNextDamage = true;
        }
    }
}
