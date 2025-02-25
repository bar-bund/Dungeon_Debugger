using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

// Handles the battles
namespace dungeon_debugger
{
    public class BattleManager
    {
        // Shared RNG (Random Number Generator) instance to generate random outcomes
        protected static readonly Random random = new();


        // Determines whether a battle should start, based on a 75% chance
        public bool ShouldStartBattle()
        {
            return random.Next(1, 5) <= 3; // 75% chance for a battle to start
        }


        // Generates a random enemy type (Bug, Serpent, or Ogre)
        public Enemy GenerateRandomEnemy()
        {
            return random.Next(3) switch // Generate a random number from 0 to 2
            {
                0 => new Bug(),
                1 => new Serpent(),
                _ => new Ogre()
            };
        }


        // Starts a battle between the player and an enemy
        public void StartBattle(Player player, Enemy enemy)
        {
            Console.WriteLine($"\nYou encounter a {enemy.Name} with {enemy.Health} health!");
            Console.WriteLine("Press 'Enter' to continue...");
            Console.ReadLine();

            // Main battle loop, runs until either the enemy or player is defeated
            while (enemy.Health > 0 && player.Health > 0)
            {
                // Clear screen and display enemy after every action
                Console.Clear();
                enemy.DisplayEnemyArt();

                // Display player's health and attack damage
                Console.WriteLine("\n-----------------------------------------------------------------");
                Console.WriteLine($"\nPlayer health: {player.Health}");
                Console.WriteLine($"Player attack damage: {player.Attack()}");

                // Display enemy's health
                Console.WriteLine($"\nEnemy health:  {enemy.Health}");

                // Provide action choices for the player
                Console.WriteLine($"\nChoose an action:" +
                                  $"\n1: Attack" +
                                  $"\n2: Run" +
                                  $"\n3: Inventory");


                // Check if the player's input is a valid integer
                if (!int.TryParse(Console.ReadLine(), out int action))
                {
                    Console.WriteLine("Invalid input.");
                    continue;
                }

                // Handle the action based on the player's choice
                switch (action)
                {
                    case 1:
                        Attack(player, enemy);
                        break;
                    case 2:
                        if (TryEscape()) return;
                        break;
                    case 3:
                        Console.WriteLine("\nChecking your inventory...");
                        player.UseItem();
                        break;
                    default:
                        Console.WriteLine("\nInvalid action.");
                        break ;
                }
            }

            // After the loop ends, handle the outcome of the battle (whether player or enemy wins)
            HandleBattleOutcome(player, enemy);
        }


        // Handles the attack phase: player attacks the enemy, and enemy counterattacks if alive
        private void Attack(Player player, Enemy enemy)
        {
            int playerDamage = player.Attack();
            enemy.Health -= playerDamage;
            Console.WriteLine($"\nYou dealt: {playerDamage} damage!");
            Thread.Sleep(1500);

            // If the enemy is still alive, the enemy counterattacks
            if (enemy.Health > 0)
            {
                int enemyDamage = enemy.Attack();
                player.Health -= enemyDamage;
                Console.WriteLine($"{enemy.Name} dealt: {enemyDamage} damage!");
                Thread.Sleep(1500);
            }
        }


        // Tries to escape the battle with a 50% chance of success
        private bool TryEscape()
        {
            if (random.Next(2) == 0) // 50% chance (0 or 1)
            {
                Console.WriteLine("\nYou fled the battle!");
                Console.WriteLine("Press 'Enter' to continue...");
                Console.ReadLine();
                return true;
            }

            Console.WriteLine("\nYou failed to flee! The enemy caught you!");
            Thread.Sleep(1500);
            return false;
        }


        // Handles the outcome of the battle (win or lose)
        private void HandleBattleOutcome(Player player, Enemy enemy)
        {
            // If the player is defeated
            if (player.Health <= 0)
            {
                Console.WriteLine("\nYou have been defeated...");
            }

            // If the enemy is defeated
            else if (enemy.Health <= 0)
            {
                Console.WriteLine($"\nYou defeated the {enemy.Name}!");

                // Handle item drop from enemy
                Item? droppedItem = enemy.DropItem();
                if (droppedItem != null)
                {
                    player.AddToInventory(droppedItem);
                }
                else
                {
                    Console.WriteLine("\nThe enemy dropped no loot...");
                }

                Console.WriteLine("Press 'Enter' to continue...");
                Console.ReadLine();
            }
        }
    }
}
