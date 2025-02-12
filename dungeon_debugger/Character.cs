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

        // Shared RNG instance
        protected static readonly Random random = new Random();

        // Default attack damage
        public virtual int Attack() => 1000000000;
    }


    // Sub class
    // Player : Character
    public class Player : Character
    {
        // Base stats for player
        private const int maxHealth = 25;
        private int attackDamage = 25;

        // Bonus stats from items used
        public int BonusAttack { get; set; } = 0;
        public bool ReduceNextDamage { get; set; } = false;

        private readonly List<Item> inventory = new();

        // Constructor: Initializes player
        public Player(string name) : base(name, name.ToLower() == "god" ? 1000 : maxHealth)
        {
            // Enables god mode
            if (name.ToLower() == "god")
            {
                Console.WriteLine("\nYou have unlocked GOD MODE!");
                attackDamage = 1000;

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


        // FINAL
        // Attack method. Added bonus attack damage if item is used.
        // If god mode is enabled check Player() constructor.
        public override int Attack() => attackDamage + BonusAttack;


        // FINAL
        // Method to rest at a bonfire and heal the player
        public void Rest()
        {
            const int healAmount = 50; // Amount of health restored
            Health = Health + healAmount;
            Console.WriteLine($"You rest at the bonfire and recover {healAmount} health. Current health: {Health}.");
            Console.WriteLine("Press 'Enter' to continue...");
            Console.ReadLine();
        }


        // FINAL
        // Method to add an item to the player's inventory
        public void AddToInventory(Item item)
        {
            inventory.Add(item); // Adds item to inventory list
            Console.WriteLine($"\n{item.Name} has been added to your inventory.");
        }


        // Method for using an item from inventory list
        public void UseItem()
        {
            if (inventory.Count == 0)
            {
                Console.WriteLine("\nYour inventory is empty.");
                return;
            }

            // Dictionary to count item occurrences
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

            // Display inventory with item counts
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
                inventory.Remove(inventory.First(i => i.Name == selectedItem.Name)); // Remove only one instance

                Console.WriteLine($"{selectedItem.Name} used!");
            }
            else
            {
                Console.WriteLine("Invalid selection.");
            }
        }


        public void AbsorbDamage(int damage)
        {
            if (ReduceNextDamage)
            {
                damage /= 2;
                Console.WriteLine("Shield absorbed half of the damage!");
                ReduceNextDamage = false;
            }
            Health -= damage;
        }
    }


    // Sub class
    // Enemy : Character
    public abstract class Enemy : Character
    {
        public EnemyType Type { get; }
        
        protected Enemy(EnemyType type, string name, int health) : base(name, health)
        {
            Type = type;
            DisplayEnemyArt(); // Call ASCII art when the enemy appears
        }

        public enum EnemyType { Bug, Serpent, Ogre }


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


        // Forces each enemy to define their own attack
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


    // Sub sub class
    // Bug : Enemy
    public class Bug : Enemy
    {
        public Bug() : base(EnemyType.Bug, "Buggy Bug", 75) { }

        private const int baseAttackDamage = 15;

        // Attack method
        public override int Attack()
        {
            if (random.Next(2) == 0) // 50% hit chance
            {
                return baseAttackDamage;
            }
            else
            {
                Console.WriteLine("The enemy missed!");
                return 0;
            }
        }
    }


    // Sub sub class
    // Serpent : Enemy
    public class Serpent : Enemy
    {
        public Serpent() : base(EnemyType.Serpent, "Syntax Serpent", 100) { }

        private const int baseAttackDamage = 25;

        // Attack method
        public override int Attack()
        {
            if (random.Next(4) <= 2) // 75% hit chance
            {
                return baseAttackDamage; 
            }
            else
            {
                Console.WriteLine("The enemy missed!");
                return 0;
            }
        }
    }


    // Sub sub class
    // Ogre : Enemy
    public class Ogre : Enemy
    {
        public Ogre() : base(EnemyType.Ogre, "OutOfBounds Ogre", 150) { }

        private const int baseAttackDamage = 35;

        // Attack method
        public override int Attack()
        {
            if (random.Next(4) <= 0) // 25% hit chance
            {
                return baseAttackDamage;
            }
            else
            {
                Console.WriteLine("The enemy missed!");
                return 0;
            }
        }
    }
}