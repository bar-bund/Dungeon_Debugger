using System;

// Responsibilities: Represents items that the player can pick up or use
// (e.g., potions, weapons, shields, etc.). This class can include properties
// like name, effects, and how they interact with the player.

// Example Responsibilities: Define the types of items,
// how they modify the player’s stats or abilities
// (e.g., healing items, damage-dealing items),
// and how they can be added to the inventory.


namespace dungeon_debugger
{
    // Item base class
    public abstract class Item
    {
        public string Name { get; set; }
    }

    // Vial class
    public class Vial : Item
    {
        public int DamageBoost { get; } = 5;

        public Vial()
        {
            Name = "Vial";
        }
    }

    // Bandage class
    public class Bandage : Item
    {
        public int HealAmount { get; } = 15;

        public Bandage()
        {
            Name = "Bandage";
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
    }
}
