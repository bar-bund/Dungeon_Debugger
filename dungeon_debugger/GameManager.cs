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

        private readonly MapManager mapManager;
        private readonly BattleManager battleManager;
        private readonly UIManager uiManager;

        private GameManager()
        {
            mapManager = new MapManager();
            battleManager = new BattleManager();
            uiManager = new UIManager();
        }

        public Player CurrentPlayer { get; private set; }


        // Starts the game and initializes the player
        public void StartGame()
        {
            uiManager.ShowGameTitle();

            // Player info and welcome message
            string playerName = uiManager.GetPlayerName();
            CurrentPlayer = new Player(playerName);

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

            while (isRunning && CurrentPlayer.Health > 0)
            {
                Console.Clear();
                mapManager.PrintMap();

                uiManager.ShowPlayerStats(CurrentPlayer.Health, CurrentPlayer.Attack());
                int choice = uiManager.GetPlayerChoice();

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
                        uiManager.ShowInvalidChoice();
                        break;
                }
            }

            // Game over
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
            // Pass direction to MovePlayer() method to update map
            mapManager.MovePlayer(direction);

            if (battleManager.ShouldStartBattle())
            {
                Enemy enemy = battleManager.GenerateRandomEnemy();
                battleManager.StartBattle(CurrentPlayer, enemy);
            }
            else
            {
                uiManager.ShowBonfireRest();
                CurrentPlayer.Rest();
            }
        }
    }
}