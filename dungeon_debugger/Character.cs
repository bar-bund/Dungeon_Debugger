using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dungeon_debugger
{
    // Base class for all characters
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

    // Player class
    public class Player : Character
    {
        private List<Item> Inventory { get; set; }

        public Player(string name) : base(name, 100)
        {
            Inventory = new List<Item>();
        }

        public int Attack(Item item)
        {
            Random random = new Random();
            int baseDamage = random.Next(5, 15); // Random damage between 5 and 15

            if (item is Vial)
            {
                baseDamage += ((Vial)item).DamageBoost;
                Console.WriteLine("The Vial boosts your attack damage!");
            }

            return baseDamage;
        }

        public void Rest()
        {
            int healAmount = 10;
            Health += healAmount;
            Console.WriteLine($"You rest at the bonfire and recover {healAmount} health. Current health: {Health}.");
        }

        public void AddToInventory(Item item)
        {
            Inventory.Add(item);
            Console.WriteLine($"{item.Name} has been added to your inventory.");
        }

        public void ViewInventory()
        {
            Console.WriteLine("Your Inventory:");
            foreach (var item in Inventory)
            {
                Console.WriteLine($"- {item.Name}");
            }
        }
    }

    // Enemy class
    public class Enemy : Character
    {
        private static Random random = new Random();

        public Enemy(string name, int health) : base(name, health) { }

        public int Attack()
        {
            return random.Next(5, 10); // Random damage between 5 and 10
        }

        public Item DropItem()
        {
            List<Item> possibleItems = new List<Item>
            {
                new Vial(),
                new Bandage(),
                new Shield()
            };

            if (random.Next(2) == 0) // 50% chance of dropping an item
            {
                Item droppedItem = possibleItems[random.Next(possibleItems.Count)];
                Console.WriteLine($"The {Name} dropped a {droppedItem.Name}!");
                return droppedItem;
            }

            return null;
        }
    }
}
