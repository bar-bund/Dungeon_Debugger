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

        private GameManager() { } // Private constructor

        public Player CurrentPlayer { get; private set; }
        private int PlayerPosition { get; set; } = 5;
        private const int MapSize = 10;
        private Random random = new Random();


        // FINAL
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
            Console.ReadLine();
            Console.Clear();

            GameLoop(); // Starts the main game loop
        }


        // FINAL
        // Main game loop
        private void GameLoop()
        {
            bool isRunning = true;

            while (isRunning && CurrentPlayer.Health > 0)
            {
                Console.Clear();
                PrintMap();

                Console.WriteLine($"Player health: {CurrentPlayer.Health}");
                
                Console.WriteLine("\nChoose a direction:" +
                                  "\n1: Left" +
                                  "\n2: Right" +
                                  "\n3: Quit the game");

                // Use int.TryParse to parse the user input into choice int
                if (int.TryParse(Console.ReadLine(), out int choice))
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
                            Console.WriteLine("\nYou have chosen to abandon your journey...");
                            isRunning = false; // Exit game loop
                            break;

                        default:
                            Console.WriteLine("\nInvalid choice. Please enter a number between 1 and 3.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid choice. Please enter a number between 1 and 3.");
                }
            }

            // Game over
            if (CurrentPlayer.Health <= 0)
            {
                Console.Clear();
                Art.DisplayDefeat();
                Console.WriteLine("\nYou have perished on your journey. Game over...");
                Console.WriteLine("\nPress 'Enter' to end the game...");
                Console.ReadLine();
                isRunning = false; // Exit game loop
            }
        }


        
        // Handle movement and determines encounter
        private void HandleEncounter(int direction)
        {
            // Pass direction to MovePlayer() method to update map
            MovePlayer(direction);

            int randomNr = random.Next(1, 5);
            // 75% chance of getting attacked
            if (randomNr <= 3)
            {
                // Array of different enemy types
                Enemy enemy = GenerateRandomEnemy();
                Console.WriteLine($"\nA {enemy.Name} appears!");
                Thread.Sleep(1000);
                Battle(enemy);
            }
            // 25% chance of getting a safe bonfire
            else
            {
                Console.WriteLine("\nYou find a safe bonfire to rest at.");
                CurrentPlayer.Rest();
                Console.WriteLine("\nPress 'Enter' to continue...");
                Console.ReadKey();
            }
        }


        // FINAL
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


        // FINAL
        // Moves the player
        private void MovePlayer(int direction)
        {
            // Update player position
            PlayerPosition += direction;

            // Prevents player from moving out of bounds - left
            if (PlayerPosition < 0)
            {
                PlayerPosition = 0;
                Console.WriteLine("\nYou can't go further left.");
                return;
            }

            // Prevents player from moving out of bounds - right
            if (PlayerPosition >= MapSize)
            {
                PlayerPosition = MapSize - 1;
                Console.WriteLine("\nYou can't go further right.");
                return;
            }
        }


        // FINAL
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
            Console.WriteLine($"\nYou encounter a {enemy.Name} with {enemy.Health} health!");

            while (enemy.Health > 0 && CurrentPlayer.Health > 0)
            {
                // Console.Clear();
                Console.WriteLine($"\nPlayer health: {CurrentPlayer.Health}" +
                                  $"\nEnemy health:  {enemy.Health}");

                Console.WriteLine("\nChoose an action:" +
                                  "\n1: Attack" +
                                  "\n2: Run" +
                                  "\n3: Inventory");

                int action = Convert.ToInt32(Console.ReadLine());

                switch (action)
                {
                    case 1: // Attack
                        int damage = CurrentPlayer.Attack(); // Player attacks
                        enemy.Health -= damage;
                        Console.WriteLine($"\nYou dealt {damage} damage to the {enemy.Name}!");
                        
                        if (enemy.Health > 0)
                        {
                            int enemyDamage = enemy.Attack(); // Enemy counterattacks
                            CurrentPlayer.Health -= enemyDamage;
                            Console.WriteLine($"\nThe {enemy.Name} dealt {enemyDamage} damage to you!");
                        }
                        break;

                    case 2: // Run
                        if (random.Next(2) == 0) // 0 or 1, so 50/50 chance
                        {
                            Console.WriteLine("\nYou fled the battle!");
                            Console.WriteLine("\nPress 'Enter' to continue...");
                            Console.ReadLine();
                            return; // Break out of the method
                        }
                        else
                        {
                            Console.WriteLine("\nYou failed to flee! The enemy caught you!");
                            break; // Continue the battle
                        }

                    case 3: // Check Inventory
                        Console.WriteLine("\nChecking your inventory...");
                        break;

                    default:
                        Console.WriteLine("\nInvalid action. Please enter a number between 1 and 3.");
                        break;
                }
            }


            if (CurrentPlayer.Health <= 0)
            {
                Console.WriteLine("\nYou have been defeated in battle...");
            }

            else if (enemy.Health <= 0)
            {
                Console.WriteLine($"\nYou defeated the {enemy.Name}!");
                Item droppedItem = enemy.DropItem();

                if (droppedItem != null) CurrentPlayer.AddToInventory(droppedItem);

                Console.WriteLine("\nPress 'Enter' to continue...");
                Console.ReadLine();
            }

            GameLoop();
        }
    }
}
