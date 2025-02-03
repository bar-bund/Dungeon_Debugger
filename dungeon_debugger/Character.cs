using System;

namespace dungeon_debugger
{
    // Base class for characters
    public class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }

        public Character(string name, int health)
        {
            Name = name;
            Health = health;
        }
    }




    // Player class - inherits from Character
    public class Player : Character
    {
        // Item list
        private List<Item> Inventory { get; set; }

        // Constructor: Initializes player with a name and 100 health
        public Player(string name) : base(name, 100)
        {
            Inventory = new List<Item>(); // Initializes an empty inventory
        }


        // Method for attacking an enemy, with optional item effects
        public int Attack()
        {
            int baseDamage = 25;
            return baseDamage; // Returns final attack damage
        }



        // Method to rest at a bonfire and heal the player
        public void Rest()
        {
            int healAmount = 10; // Amount of health restored
            Health += healAmount;
            Console.WriteLine($"You rest at the bonfire and recover {healAmount} health. Current health: {Health}.");
        }



        // Method to add an item to the player's inventory
        public void AddToInventory(Item item)
        {
            Inventory.Add(item); // Adds item to inventory
            Console.WriteLine($"{item.Name} has been added to your inventory.");
        }



        // Method to display the player's inventory
        public void ViewInventory()
        {
            Console.WriteLine("Your Inventory:");

            foreach (var item in Inventory)
            {
                Console.WriteLine($"- {item.Name}");
            }
        }


        // Method to use an item in inventory
        public void UseItem()
        {

        }
    }




    public class Bug : Character
    {
        public Bug() : base("Buggy Bug", 75) { }

        public int Attack()
        {
            return 15;
        }

        public Item DropItem()
        {
            // List of items that an enemy can drop
            List<Item> possibleItems = new List<Item>
            {
                new Vial(),
                new Bandage(),
                new Shield()
            };

            // 50% chance of dropping an item
            private static Random random = new Random();

            if (random.Next(2) == 0)
            {
                // Selects a random item from the list
                Item droppedItem = possibleItems[random.Next(possibleItems.Count)];
                Console.WriteLine($"The {Name} dropped a {droppedItem.Name}!");
                return droppedItem;
            }
            return null; // Returns null if no item is dropped
        }
    }




    public class Serpent : Character
    {
        public Serpent() : base("Syntax Serpent", 100) { }

        public int Attack()
        {
            return 25;
        }
    }




    public class Ogre : Character
    {
        public Ogre() : base("OutOfBounds Ogre", 150) { }

        public int Attack()
        {
            return 35;
        }
    }



    // Enemy class - inherits from Character
    public class Enemy : Character
    {
        private static Random random = new Random();
        public Enemy(string name, int health) : base(name, health) { }
    }
}
