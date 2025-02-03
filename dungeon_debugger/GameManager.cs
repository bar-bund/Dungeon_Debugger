using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace dungeon_debugger
{
    // Singleton pattern for managing the game state
    public class GameManager
    {
        private static GameManager _instance; // Holds the single instance of GameManager
        public static GameManager Instance => _instance ??= new GameManager(); // Ensures only one instance exists

        private GameManager() { } // Private constructor to enforce Singleton pattern

        public Player CurrentPlayer { get; set; }
        private int PlayerPosition { get; set; } = 5;
        private const int MapSize = 10;



        // Starts the game and initializes the player
        public void StartGame()
        {
            Console.WriteLine("Welcome to the Adventure Game!");

            // Player info and welcome message
            Console.Write("Enter your name: ");
            string playerName = Console.ReadLine();
            CurrentPlayer = new Player(playerName);
            Console.WriteLine($"Hello, {CurrentPlayer.Name}! Your journey begins now.\n");

            GameLoop(); // Starts the main game loop
        }



        // Main game loop
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
                        HandleEncounter(-1); // Move player left
                        break;

                    case "right":
                        HandleEncounter(1); // Move player right
                        break;

                    case "quit":
                        Console.WriteLine("Goodbye!");
                        isRunning = false; // Exit game loop
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
            // Game over
            if (CurrentPlayer.Health <= 0)
            {
                Console.WriteLine("You have perished on your journey. Game over.");
            }
        }


        
        // Handle movement and determines encounter
        private void HandleEncounter(int direction)
        {
            MovePlayer(direction);
            Random random = new Random();
            int randomNr = random.Next(1, 5); // Generate random nr. between 1-4

            // 75% chance of getting attacked (randomNr = 1, 2, or 3)
            if (randomNr <= 3)
            {
                Console.WriteLine("A hostile enemy appears!");
                Enemy enemy = new Enemy("Goblin", 10);
                Battle(enemy);
            }
            // 25% chance of getting a safe bonfire (randomNr = 4)
            else
            {
                Console.WriteLine("You find a safe bonfire to rest at.");
                CurrentPlayer.Rest();
            }

            CheckInventory();
        }


        // Allows player to check inventory
        private void CheckInventory()
        {
            Console.WriteLine("Would you like to check your inventory? (yes/no)");
            string response = Console.ReadLine()?.ToLower();
            if (response == "yes")
            {
                CurrentPlayer.ViewInventory();
            }
        }



        // Moves the player and determines encounters
        private void MovePlayer(int direction)
        {
            // Update player position
            PlayerPosition += direction;

            // Prevents player from moving out of bounds - left
            if (PlayerPosition < 0)
            {
                PlayerPosition = 0;
                Console.WriteLine("You can't go further left.");
                return;
            }

            // Prevents player from moving out of bounds - right
            if (PlayerPosition >= MapSize)
            {
                PlayerPosition = MapSize - 1;
                Console.WriteLine("You can't go further right.");
                return;
            }
        }



        // Prints the map with player position
        private void PrintMap()
        {
            Console.WriteLine("\nMap:");
            for (int i = 0; i < MapSize; i++)
            {
                if (i == PlayerPosition)
                {
                    Console.Write("P "); // Marks the player's position
                }
                else
                {
                    Console.Write("- "); // Marks empty spaces
                }
            }
            Console.WriteLine("\n");
        }



        // Handles combat between player and enemy
        private void Battle(Enemy enemy)
        {
            Console.WriteLine($"You encounter a {enemy.Name} with {enemy.Health} health!");

            // Enemy may drop an item upon defeat
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
                    int damage = CurrentPlayer.Attack(droppedItem); // Player attacks
                    enemy.Health -= damage;
                    Console.WriteLine($"You dealt {damage} damage to the {enemy.Name}.");

                    if (enemy.Health > 0)
                    {
                        int enemyDamage = enemy.Attack(); // Enemy counterattacks
                        CurrentPlayer.Health -= enemyDamage;
                        Console.WriteLine($"The {enemy.Name} dealt {enemyDamage} damage to you.");
                    }
                }
                else if (action == "run")
                {
                    Console.WriteLine("You fled the battle!");
                    break; // Exits battle loop
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
