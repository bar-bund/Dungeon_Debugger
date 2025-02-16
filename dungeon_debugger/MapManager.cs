using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

// Handles the map and player's position

namespace dungeon_debugger
{
    public class MapManager
    {
        private const int MapSize = 11; // Defines the size of the map (11 tiles)
        private int PlayerPosition = 5; // Starting position of the player at index 5 (middle of the map)


        // Method that moves the player in the specified direction (left or right)
        public void MovePlayer(int direction)
        {
            PlayerPosition += direction; // Update player's position

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


        // Method to print the map, showing the player's position with a "P"
        public void PrintMap()
        {
            Console.WriteLine("\nMap:");
            // Loop through each tile in the map (size of the map defined by MapSize)
            for (int i = 0; i < MapSize; i++)
            {
                // If the tile corresponds to the player's position, print "P"
                // Otherwise, print "-"
                Console.Write(i == PlayerPosition ? "P " : "- ");
            }
            Console.WriteLine("\n");
        }
    }
}
