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
        public Player(string name) : base(name, 5)
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
            int healAmount = 50; // Amount of health restored
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

    public abstract class Enemy : Character
    {
        public Art.EnemyType Type { get; private set; }
        
        public Enemy(Art.EnemyType type, string name, int health) : base(name, health)
        {
            Type = type;
            DisplayEnemyArt(); // Call ASCII art when the enemy appears
        }


        // Display Enemy Art method
        private void DisplayEnemyArt()
        {
            switch (Type)
            {
                case Art.EnemyType.Bug:
                    Art.DisplayBug();
                    break;

                case Art.EnemyType.Serpent:
                    Art.DisplaySerpent();
                    break;

                case Art.EnemyType.Ogre:
                    Art.DisplayOgre();
                    break;

                default:
                    Console.WriteLine("Unknown Enemy Appeared!");
                    break;
            }
        }


        // Virtual method so derived class can overwrite the value
        public virtual int Attack()
        {
            return 10000; // Default attack value if non is overwritten
        }


        // Item drop method
        protected static Random random = new Random();
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



    public class Bug : Enemy
    {
        public Bug() : base(Art.EnemyType.Bug, "Buggy Bug", 75) { }

        // Attack method
        public override int Attack()
        {
            int baseDamage;
            int attackHitChance = random.Next(1, 5);
            if (attackHitChance <= 2) // 50% hit chance
            {
                baseDamage = 15;
            }
            else
            {
                baseDamage = 0;
                Console.WriteLine("The enemy missed!");
            }
            return baseDamage;
        }
    }


    public class Serpent : Enemy
    {
        public Serpent() : base(Art.EnemyType.Serpent, "Syntax Serpent", 100) { }

        // Attack method
        public override int Attack()
        {
            int baseDamage;
            int attackHitChance = random.Next(1, 5);
            if (attackHitChance <= 3) // 75% hit chance
            {
                baseDamage = 25; 
            }
            else
            {
                baseDamage = 0;
                Console.WriteLine("The enemy missed!");
            }
            return baseDamage;
        }
    }


    public class Ogre : Enemy
    {
        public Ogre() : base(Art.EnemyType.Ogre, "OutOfBounds Ogre", 150) { }

        // Attack method
        public override int Attack()
        {
            int baseDamage;
            int attackHitChance = random.Next(1, 5);
            if (attackHitChance <= 1) // 25% hit chance
            {
                baseDamage = 35;
            }
            else
            {
                baseDamage = 0;
                Console.WriteLine("The enemy missed!");
            }
            return baseDamage;
        }
    }
}