using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dungeon_debugger
{
    // Singleton pattern for managing the game state
    public class GameManager
    {
        private static GameManager _instance;
        public static GameManager Instance => _instance ??= new GameManager();

        private GameManager() { }

        public Player CurrentPlayer { get; set; }
        private int PlayerPosition { get; set; } = 5; // Starting position on the map
        private const int MapSize = 10; // Map size (0 to 9)

        public void StartGame()
        {
            Console.WriteLine("Welcome to the Adventure Game!");
            Console.Write("Enter your name: ");
            string playerName = Console.ReadLine();
            CurrentPlayer = new Player(playerName);

            Console.WriteLine($"Hello, {CurrentPlayer.Name}! Your journey begins now.\n");
            GameLoop();
        }

        private void GameLoop()
        {
            bool isRunning = true;
            while (isRunning && CurrentPlayer.Health > 0)
            {
                PrintMap();
                Console.WriteLine("Choose a direction: Left or Right (or Quit to exit)");
                string choice = Console.ReadLine()?.ToLower();

                switch (choice)
                {
                    case "left":
                        MovePlayer(-1);
                        break;
                    case "right":
                        MovePlayer(1);
                        break;
                    case "quit":
                        Console.WriteLine("Goodbye!");
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }

            if (CurrentPlayer.Health <= 0)
            {
                Console.WriteLine("You have perished on your journey. Game over.");
            }
        }

        private void MovePlayer(int direction)
        {
            PlayerPosition += direction;

            if (PlayerPosition < 0)
            {
                PlayerPosition = 0;
                Console.WriteLine("You can't go further left.");
                return;
            }

            if (PlayerPosition >= MapSize)
            {
                PlayerPosition = MapSize - 1;
                Console.WriteLine("You can't go further right.");
                return;
            }

            Random random = new Random();
            if (random.Next(2) == 0) // 50/50 chance
            {
                Console.WriteLine("You find a safe bonfire to rest at.");
                CurrentPlayer.Rest();
            }
            else
            {
                Console.WriteLine("A hostile enemy appears!");
                Enemy enemy = new Enemy("Goblin", 10);
                Battle(enemy);
            }
        }

        private void PrintMap()
        {
            Console.WriteLine("\nMap:");
            for (int i = 0; i < MapSize; i++)
            {
                if (i == PlayerPosition)
                {
                    Console.Write("P "); // Player's position
                }
                else
                {
                    Console.Write("- ");
                }
            }
            Console.WriteLine("\n");
        }

        private void Battle(Enemy enemy)
        {
            Console.WriteLine($"You encounter a {enemy.Name} with {enemy.Health} health!");

            // Generate a random item from the enemy
            Item droppedItem = enemy.DropItem();
            if (droppedItem != null)
            {
                CurrentPlayer.AddToInventory(droppedItem);
            }

            while (enemy.Health > 0 && CurrentPlayer.Health > 0)
            {
                Console.WriteLine("Choose an action: Attack or Run?");
                string action = Console.ReadLine()?.ToLower();

                if (action == "attack")
                {
                    int damage = CurrentPlayer.Attack(droppedItem);
                    enemy.Health -= damage;
                    Console.WriteLine($"You dealt {damage} damage to the {enemy.Name}.");

                    if (enemy.Health > 0)
                    {
                        int enemyDamage = enemy.Attack();
                        CurrentPlayer.Health -= enemyDamage;
                        Console.WriteLine($"The {enemy.Name} dealt {enemyDamage} damage to you.");
                    }
                }
                else if (action == "run")
                {
                    Console.WriteLine("You fled the battle!");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid action. Try again.");
                }
            }

            if (CurrentPlayer.Health <= 0)
            {
                Console.WriteLine("You have been defeated. Game over.");
            }
            else if (enemy.Health <= 0)
            {
                Console.WriteLine($"You defeated the {enemy.Name}!");
            }
        }
    }
}
