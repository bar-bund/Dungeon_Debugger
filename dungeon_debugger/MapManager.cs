using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Handles the map and players position

namespace dungeon_debugger
{
    public class MapManager
    {
        private const int MapSize = 11;
        private int PlayerPosition = 5;

        // Move player method
        public void MovePlayer(int direction)
        {
            PlayerPosition += direction;

            // Returns player to map from left
            if (PlayerPosition < 0)
            {
                PlayerPosition = 0;
                Console.WriteLine("\nYou can't go further left.");
                Thread.Sleep(1500);
            }

            // Returns player to map from right
            if (PlayerPosition >= MapSize)
            {
                PlayerPosition = MapSize - 1;
                Console.WriteLine("\nYou can't go further right.");
                Thread.Sleep(1500);
            }
        }

        // Print map
        public void PrintMap()
        {
            Console.WriteLine("\nMap:");
            for (int i = 0; i < MapSize; i++)
            {
                Console.Write(i == PlayerPosition ? "P " : "- ");
            }
            Console.WriteLine("\n");
        }
    }
}
