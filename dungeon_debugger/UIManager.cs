using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Handles the input/output (UI-related tasks)
namespace dungeon_debugger
{
    public class UIManager
    {
        // Displays the game title/artwork
        public void ShowGameTitle()
        {
            Art.DisplayIntro();
        }


        // Prompts the player for their name, returns the name entered or "Rider" as default
        public string GetPlayerName()
        {
            Console.WriteLine("Enter your name: ");
            string input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? "Rider" : input;
        }


        // Displays a welcome message to the player with their name
        public void ShowWelcomeMessage(string playerName)
        {
            Art.DisplayPlayer();
            Console.WriteLine($"\nWelcome, Knight {playerName}!\r\n" +
                              $"Your journey begins in these desolate lands where shadows creep, and danger lurks at every turn. \r\n" +
                              $"As a lone wanderer, you must navigate this unforgiving world, \r\n" +
                              $"battling fierce enemies and seeking solace at rare bonfires. \r\n" +
                              $"Your courage and choices will determine whether you survive the journey or \r\n" +
                              $"succumb to the darkness!");
        }


        // Displays player stats like health and attack damage
        public void ShowPlayerStats(int health, int attackDamage)
        {
            Console.WriteLine($"\nPlayer health: {health}");
            Console.WriteLine($"Player attack damage: {attackDamage}");
        }


        // Prompts the player to choose an action and returns their choice
        public int GetPlayerChoice()
        {
            Console.WriteLine("\nChoose a direction:" +
                              "\n1: Left" +
                              "\n2: Right" +
                              "\n3: Quit the game");

            return int.TryParse(Console.ReadLine(), out int choice) ? choice : -1;
        }


        // Displays a message when the player finds a bonfire to rest at
        public void ShowBonfireRest()
        {
            Console.WriteLine("\nYou find a safe bonfire to rest at...");
            Art.DisplayBonfire();
        }


        // Displays a message when the player chooses to quit the game
        public void ShowQuitMessage()
        {
            Art.DisplayAbandon();
            Console.WriteLine("\nYou have chosen to abandon your journey...");
        }


        // Displays a message when the player is defeated
        public void ShowDefeatMesseage()
        {
            Art.DisplayDefeat();
            Console.WriteLine("\nYou have perished on your journey. Game over...");
        }


        // Displays an error message when the player enters an invalid choice
        public void ShowInvalidChoice()
        {
            Console.WriteLine("\nInvalid choice. Please enter a valid number.");
            Thread.Sleep(1000);
        }
    }
}
