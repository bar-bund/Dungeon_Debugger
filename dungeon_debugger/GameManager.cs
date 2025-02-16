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
        private static GameManager? _instance; // Holds the single instance of GameManager
        public static GameManager Instance => _instance ??= new GameManager(); // Ensures only one instance exists


        // Declares instances of other managers required for the game: Map, Battle, and UI
        private readonly MapManager mapManager;
        private readonly BattleManager battleManager;
        private readonly UIManager uiManager;


        // Initialize the different managers
        private GameManager()
        {
            mapManager = new MapManager();
            battleManager = new BattleManager();
            uiManager = new UIManager();
        }


        // Current player in the game
        public Player CurrentPlayer { get; private set; }


        // Starts the game and initializes the player
        public void StartGame()
        {
            uiManager.ShowGameTitle();

            // Prompt user for player name and initialize the player with it
            string playerName = uiManager.GetPlayerName();
            CurrentPlayer = new Player(playerName); // Create a new player object

            uiManager.ShowWelcomeMessage(CurrentPlayer.Name);

            Console.WriteLine("Press 'Enter' to continue...");
            Console.ReadLine();
            Console.Clear();

            GameLoop(); // Starts the main game loop
        }


        // Main game loop
        private void GameLoop()
        {
            bool isRunning = true;

            // Loop continues until player dies or chooses to quit
            while (isRunning && CurrentPlayer.Health > 0)
            {
                Console.Clear();
                mapManager.PrintMap();

                uiManager.ShowPlayerStats(CurrentPlayer.Health, CurrentPlayer.Attack()); // Display player stats (health, attack)
                int choice = uiManager.GetPlayerChoice(); // Get player's choice of action

                switch (choice)
                {
                    case 1:
                        HandleEncounter(-1); // Move player left
                        break;

                    case 2:
                        HandleEncounter(1); // Move player right
                        break;

                    case 3:
                        uiManager.ShowQuitMessage();
                        isRunning = false; // Exit game loop
                        break;

                    default:
                        uiManager.ShowInvalidChoice(); // Display an error message if the choice is invalid
                        break;
                }
            }

            // Game over if player health reaches zero
            if (CurrentPlayer.Health <= 0)
            {
                Console.Clear();
                uiManager.ShowDefeatMesseage();
                Console.WriteLine("\nPress 'Enter' to end the game...");
                Console.ReadLine();
                isRunning = false; // Exit game loop
            }
        }


        // Handle movement and determines encounter
        private void HandleEncounter(int direction)
        {
            // Pass direction to MovePlayer() method to update map based on direction
            mapManager.MovePlayer(direction);

            // Check if a battle should start (75% chance)
            if (battleManager.ShouldStartBattle())
            {
                // Generate a random enemy and start the battle
                Enemy enemy = battleManager.GenerateRandomEnemy();
                battleManager.StartBattle(CurrentPlayer, enemy);
            }
            else
            {
                // If no battle, show a message for resting by a bonfire and restore player health
                uiManager.ShowBonfireRest();
                CurrentPlayer.Rest();
            }
        }
    }
}