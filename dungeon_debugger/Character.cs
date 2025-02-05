using System;

namespace dungeon_debugger
{
    // Base class
    // Character
    public abstract class Character
    {
        public string Name { get; set; }
        public int Health { get; set; }

        public Character(string name, int health)
        {
            Name = name;
            Health = health;
        }
    }


    // Sub class
    // Player : Character
    public class Player : Character
    {
        // Constructor: Initializes player with a name and 100 health
        public Player(string name) : base(name, 100) { }

        // Fixed base damage
        public int Attack() => 25;


        // FINAL
        // Method to rest at a bonfire and heal the player
        public void Rest()
        {
            int healAmount = 50; // Amount of health restored
            Health = Math.Min(Health + healAmount, 100); // Prevent overhealing
            Art.DisplayBonfire();
            Console.WriteLine($"You rest at the bonfire and recover {healAmount} health. Current health: {Health}.");
        }


        // Item list
        private List<Item> Inventory { get; } = new List<Item>();


        // FINAL
        // Method to add an item to the player's inventory
        public void AddToInventory(Item item)
        {
            Inventory.Add(item); // Adds item to inventory
            Console.WriteLine($"{item.Name} has been added to your inventory.");
        }


        // FINAL
        // Method to display the player's inventory
        public void ViewInventory()
        {
            Console.WriteLine("\nYour Inventory:");

            if (Inventory.Count == 0)
            {
                Console.WriteLine("- Empty");
                return;
            }

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


    // Sub class
    // Enemy : Character
    public abstract class Enemy : Character
    {
        public EnemyType Type { get; private set; }

        public Enemy(EnemyType type, string name, int health) : base(name, health)
        {
            Type = type;
            DisplayEnemyArt(); // Call ASCII art when the enemy appears
        }

        public enum EnemyType { Bug, Serpent, Ogre }


        //FINAL
        // Method displaying enemy art
        private void DisplayEnemyArt()
        {
            switch (Type)
            {
                case EnemyType.Bug:
                    Art.DisplayBug();
                    break;

                case EnemyType.Serpent:
                    Art.DisplaySerpent();
                    break;

                case EnemyType.Ogre:
                    Art.DisplayOgre();
                    break;

                default:
                    Console.WriteLine("Unknown enemy appeared!");
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


    // Sub sub class
    // Bug : Enemy
    public class Bug : Enemy
    {
        public Bug() : base(EnemyType.Bug, "Buggy Bug", 75) { }

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


    // Sub sub class
    // Serpent : Enemy
    public class Serpent : Enemy
    {
        public Serpent() : base(EnemyType.Serpent, "Syntax Serpent", 100) { }

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


    // Sub sub class
    // Ogre : Enemy
    public class Ogre : Enemy
    {
        public Ogre() : base(EnemyType.Ogre, "OutOfBounds Ogre", 150) { }

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