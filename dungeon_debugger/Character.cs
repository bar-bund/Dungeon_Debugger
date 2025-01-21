namespace dungeon_debugger;

// Base class for all characters
public class Character
{
    public Character(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public string Name { get; set; }
    public int Health { get; set; }
}

// Player class
public class Player : Character
{
    public Player(string name) : base(name, 100)
    {
        Inventory = new List<Item>();
    }

    private List<Item> Inventory { get; }

    public int Attack(Item item)
    {
        var random = new Random();
        var baseDamage = random.Next(5, 15); // Random damage between 5 and 15

        if (item is Vial)
        {
            baseDamage += ((Vial)item).DamageBoost;
            Console.WriteLine("The Vial boosts your attack damage!");
        }

        return baseDamage;
    }

    public void Rest()
    {
        var healAmount = 10;
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
        foreach (var item in Inventory) Console.WriteLine($"- {item.Name}");
    }
}

// Enemy class
public class Enemy : Character
{
    private static readonly Random random = new();

    public Enemy(string name, int health) : base(name, health)
    {
    }

    public int Attack()
    {
        return random.Next(5, 10); // Random damage between 5 and 10
    }

    public Item DropItem()
    {
        List<Item> possibleItems = new()
        {
            new Vial(),
            new Bandage(),
            new Shield()
        };

        if (random.Next(2) == 0) // 50% chance of dropping an item
        {
            var droppedItem = possibleItems[random.Next(possibleItems.Count)];
            Console.WriteLine($"The {Name} dropped a {droppedItem.Name}!");
            return droppedItem;
        }

        return null;
    }
}