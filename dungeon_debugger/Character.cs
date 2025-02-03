namespace dungeon_debugger;

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
        // List to store collected items
        private List<Item> Inventory { get; set; }



        // Constructor: Initializes player with a name and 100 health
        public Player(string name) : base(name, 100)
        {
            Inventory = new List<Item>(); // Initializes an empty inventory
        }



        // Method for attacking an enemy, with optional item effects
        public int Attack(Item item)
        {
            Random random = new Random();
            int baseDamage = random.Next(5, 15);


            // Checks if the player is using a Vial, which boosts attack damage
            if (item is Vial)
            {
                baseDamage += ((Vial)item).DamageBoost; // Adds extra damage from Vial
                Console.WriteLine("The Vial boosts your attack damage!");
            }

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


        public void UseItem()
        {

        }
    }



    // Enemy class - inherits from Character
    public class Enemy : Character
    {
        private static Random random = new Random();
        public Enemy(string name, int health) : base(name, health) { }

        // Method to perform an attack
        public int Attack()
        {
            return random.Next(5, 10);
        }

        // Method to determine if the enemy drops an item
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

    }

    public class Serpent : Enemy
    {

    }

    public class Ogre : Enemy
    {

    }

}
