using System;
using System.ComponentModel.Design;

namespace dungeon_debugger
{
    // Base class
    // Character
    public abstract class Character
    {
        public string Name { get; set; } // Character name
        public int Health { get; set; } // Character health
        
        public virtual int Attack() => 1000000; // Default attack (overridden in subclasses)

        public Character(string name, int health)
        {
            Name = name;
            Health = health;
        }

        // Shared RNG instance for randomness in attacks, drops, etc.
        protected static readonly Random random = new Random();
    }


    // Sub class
    // Player : Character
    public class Player : Character
    {
        private const int maxHealth = 100; // Max player health
        private int playerAttackDamage = 25; // Default attack damage


        // Additional stats for power-ups
        public int bonusAttack { get; set; } = 0;
        public bool reduceNextDamage { get; set; } = false; // Shield effect


        private readonly List<Item> inventory = new(); // Player's inventory


        // Constructor: Initializes player
        public Player(string name) : base(name, name.ToLower() == "god" ? 1000 : maxHealth)
        {
            // Enables god mode if the player's name is "god"
            if (name.ToLower() == "god")
            {
                Console.WriteLine("\nYou have unlocked GOD MODE!");
                playerAttackDamage = 1000;

                // Add 10 of each item to inventory
                for (int i = 0; i < 10; i++)
                {
                    inventory.Add(new Vial());
                    inventory.Add(new Bandage());
                    inventory.Add(new Shield());
                }

                Console.WriteLine("You received 10 of each item!");
            }

            Console.WriteLine("Press 'Enter' to continue...");
            Console.ReadLine();
        }

        // Override Attack() method to include bonus attack damage
        public override int Attack() => playerAttackDamage + bonusAttack;


        // Rest at a bonfire to regain health
        public void Rest()
        {
            const int healAmount = 50; // Amount of health restored
            Health = Health + healAmount;
            Console.WriteLine($"You rest at the bonfire and recover {healAmount} health. Current health: {Health}.");
            Console.WriteLine("Press 'Enter' to continue...");
            Console.ReadLine();
        }


        // Adds an item to the player's inventory
        public void AddToInventory(Item item)
        {
            inventory.Add(item);
            Console.WriteLine($"\n{item.Name} has been added to your inventory.");
        }


        // Uses an item from the inventory
        public void UseItem()
        {
            if (inventory.Count == 0)
            {
                Console.WriteLine("\nYour inventory is empty.");
                return;
            }

            // Count unique item occurrences
            Dictionary<string, int> itemCounts = new();
            List<Item> uniqueItems = new();

            foreach (var item in inventory)
            {
                if (itemCounts.ContainsKey(item.Name))
                {
                    itemCounts[item.Name]++;
                }
                else
                {
                    itemCounts[item.Name] = 1;
                    uniqueItems.Add(item);
                }
            }

            // Display inventory
            Console.WriteLine("\nSelect an item to use:");
            for (int i = 0; i < uniqueItems.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {uniqueItems[i].Name} x{itemCounts[uniqueItems[i].Name]}");
            }

            // Get player choice
            if (int.TryParse(Console.ReadLine(), out int choice) && choice > 0 && choice <= uniqueItems.Count)
            {
                Item selectedItem = uniqueItems[choice - 1];

                // Use item and remove one instance of it
                selectedItem.Use(this);
                inventory.Remove(inventory.First(i => i.Name == selectedItem.Name)); // Remove one instance

                Console.WriteLine($"{selectedItem.Name} used!");
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
        }


        // Absorbs half of enemy damage if shield is used
        public void AbsorbDamage(Enemy enemy)
        {
            int damage = enemy.Attack();
            if (reduceNextDamage)
            {
                damage /= 2; // Reduce damage if shield is active
                Console.WriteLine("Shield absorbed half of the damage!");
                reduceNextDamage = false; // Deactivate shield after use
            }
            Health -= damage;
        }
    }


    // Enemy base class
    public abstract class Enemy : Character
    {
        public EnemyType Type { get; set; }
        public enum EnemyType { Bug, Serpent, Ogre }

        public Enemy(EnemyType type, string name, int health) : base(name, health)
        {
            Type = type;
            DisplayEnemyArt(); // Display ASCII art on spawn
        }


        // Method displaying enemy art
        public void DisplayEnemyArt()
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


        // Enforces each enemy class to define their attack logic
        public abstract override int Attack();


        // Item drop method
        public Item? DropItem()
        {
            // List of items that an enemy can drop
            List<Item> possibleItems = new()
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


    // Subclasses for different enemy types
    // Bug : Enemy
    public class Bug : Enemy
    {
        public Bug() : base(EnemyType.Bug, "Buggy Bug", 75) { }

        public const int enemyAttackDamage = 15;

        // Attack method
        public override int Attack()
        {
            if (random.Next(4) == 1) // 50% hit chance on player
            {
                return enemyAttackDamage;
            }
            else
            {
                Console.WriteLine("The enemy missed!");
                return 0;
            }
        }
    }


    // Serpent : Enemy
    public class Serpent : Enemy
    {
        public Serpent() : base(EnemyType.Serpent, "Syntax Serpent", 100) { }

        public const int enemyAttackDamage = 25;

        // Attack method
        public override int Attack()
        {
            if (random.Next(4) <= 2) // 75% hit chance on player
            {
                return enemyAttackDamage; 
            }
            else
            {
                Console.WriteLine("The enemy missed!");
                return 0;
            }
        }
    }


    // Ogre : Enemy
    public class Ogre : Enemy
    {
        public Ogre() : base(EnemyType.Ogre, "OutOfBounds Ogre", 150) { }

        public const int enemyAttackDamage = 35;

        // Attack method
        public override int Attack()
        {
            if (random.Next(4) <= 0) // 25% hit chance on player
            {
                return enemyAttackDamage;
            }
            else
            {
                Console.WriteLine("The enemy missed!");
                return 0;
            }
        }
    }
}