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
        private const int MapSize = 10;
        private int PlayerPosition = 5;

        // Move the player on the map
        public void MovePlayer(int direction)
        {
            PlayerPosition += direction;

            if (PlayerPosition < 0) 
            { 
                PlayerPosition = 0;
                Console.WriteLine("\nYou can't go further left.");
            }

            if (PlayerPosition >= MapSize)
            {
                PlayerPosition = MapSize - 1;
                Console.WriteLine("\nYou can't go further right.");
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
