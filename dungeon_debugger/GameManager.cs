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
            Art.DisplayIntro();

            // Player info and welcome message
            Console.Write("Enter your name: ");
            string playerName = Console.ReadLine();
            CurrentPlayer = new Player(playerName);

            Console.WriteLine($"Welcome, Knight {CurrentPlayer.Name}!\r\n" +
                              $"Your journey begins in these desolate lands where shadows creep, and danger lurks at every turn. \r\n" +
                              $"As a lone wanderer, you must navigate this unforgiving world, \r\n" +
                              $"battling fierce enemies and seeking solace at rare bonfires. \r\n" +
                              $"Your courage and choices will determine whether you survive the journey or \r\n" +
                              $"succumb to the darkness!");

            Art.DisplayPlayer();

            Console.WriteLine("Press 'Enter' to continue...");
            Console.ReadKey();
            Console.Clear();

            GameLoop(); // Starts the main game loop
        }



        // Main game loop
        private void GameLoop()
        {
            bool isRunning = true;

            while (isRunning && CurrentPlayer.Health > 0)
            {
                Console.Clear();
                PrintMap();
                Console.WriteLine("Choose a direction:\n" +
                                  "1: Left\n" +
                                  "2: Right\n" +
                                  "3: Quit the game\n");

                string input = Console.ReadLine();  // Read user input as a string
                int choice;

                // Use int.TryParse to safely parse the input
                if (int.TryParse(input, out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            HandleEncounter(-1); // Move player left
                            break;

                        case 2:
                            HandleEncounter(1); // Move player right
                            break;

                        case 3:
                            Console.WriteLine("You have given up on your journey...");
                            isRunning = false; // Exit game loop
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Try again.");
                            break;
                    }
                }
                else
                {
                    // If input is not a valid integer, inform the user
                    Console.WriteLine("Invalid input. Please enter a number (1-3).");
                }
            }

            // Game over
            if (CurrentPlayer.Health <= 0)
            {
                Console.Clear();
                Art.DisplayDefeat();
                Console.WriteLine("You have perished on your journey. Game over...");
                Console.WriteLine("Press 'Enter' to continue...");
                Console.ReadKey();
                isRunning = false; // Exit game loop
            }
        }


        
        // Handle movement and determines encounter
        private void HandleEncounter(int direction)
        {
            // Pass direction to moveplayer method to update map
            MovePlayer(direction);
            int randomNr = random.Next(1, 5); // Generate random nr. between 1-4

            // 75% chance of getting attacked
            if (randomNr <= 3)
            {
                // Array of different enemy types
                Enemy enemy = GenerateRandomEnemy();
                Console.WriteLine($"A {enemy.Name} appears!");
                Battle(enemy);
            }
            // 25% chance of getting a safe bonfire
            else
            {
                Console.WriteLine("You find a safe bonfire to rest at.");
                Art.DisplayBonfire();
                CurrentPlayer.Rest();
                Console.WriteLine("Press 'Enter' to continue...");
                Console.ReadKey();
            }
        }


        // Generates random enemy during every encounter
        private Enemy GenerateRandomEnemy()
        {
            int enemyNr = random.Next(3);
            switch (enemyNr)
            {
                case 0:
                    return new Bug();

                case 1:
                    return new Serpent();

                default:
                    return new Ogre();
            };
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
        private void Battle(Enemy enemy)
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
                //if (!int.TryParse(Console.ReadLine(), out action))
                //{
                //    Console.WriteLine("Invalid input. Please enter a number between 1 and 3.");
                //    continue;
                //}

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
