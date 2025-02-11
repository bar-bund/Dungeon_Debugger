using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Handles the input/output

namespace dungeon_debugger
{
    public class UIManager
    {
        public string GetPlayerName()
        {
            Console.WriteLine("Enter your name: ");
            string input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? "Rider" : input;

        }


        public void ShowWelcomeMessage(string playerName)
        {
            Console.WriteLine($"\nWelcome, Knight {playerName}!\r\n" +
                              $"Your journey begins in these desolate lands where shadows creep, and danger lurks at every turn. \r\n" +
                              $"As a lone wanderer, you must navigate this unforgiving world, \r\n" +
                              $"battling fierce enemies and seeking solace at rare bonfires. \r\n" +
                              $"Your courage and choices will determine whether you survive the journey or \r\n" +
                              $"succumb to the darkness!");
        }


        public void ShowPlayerStats(int health)
        {
            Console.WriteLine($"\nPlayer health: {health}");
        }


        public int GetPlayerChoice()
        {
            Console.WriteLine("\nChoose a direction:" +
                              "\n1: Left" +
                              "\n2: Right" +
                              "\n3: Quit the game");

            return int.TryParse(Console.ReadLine(), out int choice) ? choice : -1;
        }


        public void ShowEnemyEncounter(Enemy enemy)
        {
            Console.WriteLine($"\nA {enemy.Name} appears!");
        }


        public void ShowBonfireRest()
        {
            Console.WriteLine("\nYou find a safe bonfire to rest at...");
        }


        public void ShowQuitMessage()
        {
            Console.WriteLine("\nYou have chosen to abandon your journey...");
        }


        public void ShowInvalidChoice()
        {
            Console.WriteLine("\nInvalid choice. Please enter a valid number.");
        }
    }
}
