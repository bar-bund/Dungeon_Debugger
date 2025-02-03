using System;
using System.Security.AccessControl;
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

        // ??????? Maybe
        Random random = new Random();



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
                Console.WriteLine("You have perished on your journey. Game over...");
                isRunning = false; // Exit game loop
            }
        }


        
        // Handle movement and determines encounter
        private void HandleEncounter(int direction)
        {
            MovePlayer(direction);
            int randomNr = random.Next(1, 5); // Generate random nr. between 1-4

            // 75% chance of getting attacked (randomNr = 1, 2, or 3)
            if (randomNr <= 3)
            {
                Console.WriteLine("A hostile enemy appears!");

                // Array of different enemy types
                Character[] enemyTypes = { new Bug(), 
                                           new Serpent(), 
                                           new Ogre() };
                Character enemy = enemyTypes[random.Next(enemyTypes.Length)];

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



        // Handles combat
        private void Battle(Character enemy)
        {
            Console.WriteLine($"You encounter a {enemy.Name} with {enemy.Health} health!");

            while (enemy.Health > 0 && CurrentPlayer.Health > 0)
            {
                Console.WriteLine("Choose an action:\n" +
                                  "1: Attack\n" +
                                  "2: Run\n" +
                                  "3: Inventory?\n");

                int action = Convert.ToInt32(Console.ReadLine());

                // ???????? MAYBE
                if (!int.TryParse(Console.ReadLine(), out action))
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 3.");
                    continue;
                }

                switch (action)
                {
                    case 1: // Attack
                        int damage = CurrentPlayer.Attack(); // Player attacks
                        enemy.Health -= damage;
                        Console.WriteLine($"You dealt {damage} damage to the {enemy.Name}!");
                        
                        if (enemy.Health > 0)
                        {
                            int enemyDamage = enemy.Attack(); // Enemy counterattacks
                            CurrentPlayer.Health -= enemyDamage;
                            Console.WriteLine($"The {enemy.Name} dealt {enemyDamage} damage to you!");
                        }
                        break;

                    case 2: // Run
                        if (random.Next(2) == 0) // 0 or 1, so 50/50 chance
                        {
                            Console.WriteLine("You fled the battle!");
                            return; // Break out of the method
                        }
                        else
                        {
                            Console.WriteLine("You failed to flee! The enemy caught you!");
                            break; // Continue the battle
                        }

                    case 3: // Check Inventory
                        Console.WriteLine("Checking your inventory...");
                        break;

                    default:
                        Console.WriteLine("Invalid action. Please choose a valid option (1-3).");
                        break;
                }
            }


            if (CurrentPlayer.Health <= 0)
            {
                Console.WriteLine("You have been defeated in battle...");
            }
            else if (enemy.Health <= 0)
            {
                Console.WriteLine($"You defeated the {enemy.Name}!");
                Item droppedItem = enemy.DropItem();
                if (droppedItem != null)
                {
                    CurrentPlayer.AddToInventory(droppedItem);
                }
            }

            GameLoop();
        }
    }
}
