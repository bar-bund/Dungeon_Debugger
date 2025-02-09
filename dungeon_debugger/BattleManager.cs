﻿using System;
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
        private Random random = new();


        public bool ShouldStartBattle()
        {
            return random.Next(1, 5) <= 3; // 75%
        }


        public Enemy GenerateRandomEnemy()
        {
            return random.Next(3) switch
            {
                0 => new Bug(),
                1 => new Serpent(),
                _ => new Ogre()
            };
        }


        public void StartBattle(Player player, Enemy enemy)
        {
            Console.WriteLine($"\nYou encounter a {enemy.Name} with {enemy.Health} health!");

            while (enemy.Health > 0 && player.Health > 0)
            {
                Console.WriteLine($"\nPlayer health: {player.Health}");
                Console.WriteLine($"\nEnemy health:  {enemy.Health}");

                Console.WriteLine($"\nChoose an action:" +
                                  $"\n1: Attack" +
                                  $"\n2: Run" +
                                  $"\n3: Inventory");

                int action = Convert.ToInt32(Console.ReadLine());

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
                        break;
                    default:
                        Console.WriteLine("\nInvalid action.");
                        break ;
                }
            }

            HandleBattleOutcome(player, enemy);
        }


        private void Attack(Player player, Enemy enemy)
        {
            int playerDamage = player.Attack();
            enemy.Health -= playerDamage;
            Console.WriteLine($"\nYou dealt {playerDamage} damage to the {enemy.Name}!");

            if (enemy.Health > 0)
            {
                int enemyDamage = enemy.Attack();
                player.Health -= enemyDamage;
                Console.WriteLine($"\nThe {enemy.Name} dealt {enemyDamage} damage to you!");
            }
        }


        private bool TryEscape()
        {
            if (random.Next(2) == 0)
            {
                Console.WriteLine("\nYou fled the battle!");
                Console.WriteLine("Press 'Enter' to continue...");
                Console.ReadLine();
                return true;
            }

            Console.WriteLine("\nYou failed to flee! The enemy caught you!");
            return false;
        }


        private void HandleBattleOutcome(Player player, Enemy enemy)
        {
            if (player.Health <= 0)
            {
                Console.WriteLine("\nYou have been defeated...");
            }

            else if (enemy.Health <= 0)
            {
                Console.WriteLine($"\nYou defeated the {enemy.Name}!");

                Item droppedItem = enemy.DropItem();
                if (droppedItem != null)
                {
                    player.AddToInventory(droppedItem);
                }

                Console.WriteLine("Press 'Enter' to continue...");
                Console.ReadLine();
            }
        }
    }
}
