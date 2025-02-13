﻿using System;
using System.Threading.Channels;

namespace dungeon_debugger
{
    public class Art
    {
        // Intro - Art
        public static void DisplayIntro()
        {
            Console.WriteLine("\n" +
                              "______                                     ______     _                                          \r\n" +
                              "|  _  \\                                    |  _  \\   | |                                       \r\n" +
                              "| | | |_   _ _ __   __ _  ___  ___  _ __   | | | |___| |__  _   _  __ _  __ _  ___ _ __          \r\n" +
                              "| | | | | | | '_ \\ / _` |/ _ \\/ _ \\| '_ \\  | | | / _ \\ '_ \\| | | |/ _` |/ _` |/ _ \\ '__|  \r\n" +
                              "| |/ /| |_| | | | | (_| |  __/ (_) | | | | | |/ /  __/ |_) | |_| | (_| | (_| |  __/ |            \r\n" +
                              "|___/  \\__,_|_| |_|\\__, |\\___|\\___/|_| |_| |___/ \\___|_.__/ \\__,_|\\__, |\\__, |\\___|_|   \r\n" +
                              "                    __/ |                                          __/ | __/ |                   \r\n" +
                              "                   |___/                                          |___/ |___/                    \r\n");
        }


        // Player - Art
        public static void DisplayPlayer()
        {
            Console.WriteLine("\n" +
                              "   .                      \r\n" +
                              "  / \\                    \r\n" +
                              "  | |                     \r\n" +
                              "  |.|                     \r\n" +
                              "  |.|                     \r\n" +
                              "  |:|      __             \r\n" +
                              ",_|:|_,   /  )            \r\n" +
                              "  (Oo    / _I_            \r\n" +
                              "   +\\ \\  || __|         \r\n" +
                              "      \\ \\||___|         \r\n" +
                              "        \\ /.:.\\-\\      \r\n" +
                              "         |.:. /-----\\    \r\n" +
                              "         |___|::oOo::|    \r\n" +
                              "         /   |:<_T_>:|    \r\n" +
                              "        |_____\\ ::: /    \r\n" +
                              "         | |  \\ \\:/     \r\n" +
                              "         | |   | |        \r\n" +
                              "         \\ /   | \\___   \r\n" +
                              "         / |   \\_____\\  \r\n" +
                              "         `-'              \r\n");
        }
        
        // Bug - Art
        public static void DisplayBug()
        {
            Console.WriteLine("\n" +
                              "       ,_    /) (\\    _,        \r\n" +
                              "        >>  <<,_,>>  <<          \r\n" +
                              "       //   _0.-.0_   \\\\       \r\n" +
                              "       \\'._/       \\_.'/       \r\n" +
                              "        '-.\\.--.--./.-'         \r\n" +
                              "        __/ : :Y: : \\ _         \r\n" +
                              "';,  .-(_| : : | : : |_)-.  ,:'  \r\n" +
                              "  \\\\/.'  |: : :|: : :|  `.\\// \r\n" +
                              "   (/    |: : :|: : :|    \\)    \r\n" +
                              "         |: : :|: : :;           \r\n" +
                              "        /\\ : : | : : /\\        \r\n" +
                              "       (_/'.: :.: :.'\\_)        \r\n" +
                              "        \\\\  `\"\"`\"\"`  //    \r\n" +
                              "         \\\\         //         \r\n" +
                              "          ':.     .:'            \r\n");
        }

        // Serpent - Art
        public static void DisplaySerpent()
        {
            Console.WriteLine("\n" +
                              "       ---_ ......._-_--.            \r\n" +
                              "      (|\\ /      / /| \\  \\        \r\n" +
                              "      /  /     .'  -=-'   `.         \r\n" +
                              "     /  /    .'             )        \r\n" +
                              "   _/  /   .'        _.)   /         \r\n" +
                              "  / o   o        _.-' /  .'          \r\n" +
                              "  \\          _.-'    / .'*|         \r\n" +
                              "   \\______.-'//    .'.' \\*|        \r\n" +
                              "    \\|  \\ | //   .'.' _ |*|        \r\n" +
                              "     `   \\|//  .'.'_ _ _|*|         \r\n" +
                              "      .  .// .'.' | _ _ \\*|         \r\n" +
                              "      \\`-|\\_/ /    \\ _ _ \\*\\    \r\n" +
                              "       `/'\\__/      \\ _ _ \\*\\    \r\n" +
                              "      /^|            \\ _ _ \\*      \r\n" +
                              "     '  `             \\ _ _ \\      \r\n" +
                              "                       \\_           \r\n");
        }

        // Ogre - Art
        public static void DisplayOgre()
        {
            Console.WriteLine("\n" +
                              "           _......._                 \r\n" +
                              "       .-'.'.'.'.'.'.`-.             \r\n" +
                              "     .'.'.'.'.'.'.'.'.'.`.           \r\n" +
                              "    /.'.'               '.\\         \r\n" +
                              "    |.'    _.--...--._     |         \r\n" +
                              "    \\    `._.-.....-._.'   /        \r\n" +
                              "    |     _..- .-. -.._   |          \r\n" +
                              " .-.'    `.   ((@))  .'   '.-.       \r\n" +
                              "( ^ \\      `--.   .-'     / ^ )     \r\n" +
                              " \\  /         .   .       \\  /     \r\n" +
                              " /          .'     '.  .-    \\      \r\n" +
                              "( _.\\    \\ (_`-._.-'_)    /._\\)   \r\n" +
                              " `-' \\   ' .--.          / `-'      \r\n" +
                              "     |  / /|_| `-._.'\\   |          \r\n" +
                              "     |   |       |_| |   /-.._       \r\n" +
                              " _..-\\   `.--.______.'  |           \r\n" +
                              "      \\       .....     |           \r\n" +
                              "       `.  .'      `.  /             \r\n" +
                              "         \\           .'             \r\n" +
                              "          `-..___..-`                \r\n");
        }


        // Bonfire - Art
        public static void DisplayBonfire()
        {
            Console.WriteLine("\n" +
                              "            (                \r\n" +
                              "             )               \r\n" +
                              "            (  (             \r\n" +
                              "                )            \r\n" +
                              "          (    (  ,          \r\n" +
                              "           ) /\\ -((         \r\n" +
                              "         (  // | (`'         \r\n" +
                              "       _ -.;_/ \\\\--._      \r\n" +
                              "      (_;-// | \\ \\-'.\\    \r\n" +
                              "      ( `.__ _  ___,')       \r\n" +
                              "       `'(_ )_)(_)_)'        \r\n");
        }

        // Defeat - Art
        public static void DisplayDefeat()
        {
            Console.WriteLine("\n" +
                              "                            ,--.         \r\n" +
                              "                           {    }        \r\n" +
                              "                           K,   }        \r\n" +
                              "                          /  ~Y`         \r\n" +
                              "                     ,   /   /           \r\n" +
                              "                    {_'-K.__/            \r\n" +
                              "                      `/-.__L._          \r\n" +
                              "                      /  ' /`\\_}        \r\n" +
                              "                     /  ' /              \r\n" +
                              "             ____   /  ' /               \r\n" +
                              "      ,-'~~~~    ~~/  ' /_               \r\n" +
                              "    ,'             ``~~~  ',             \r\n" +
                              "   (                        Y            \r\n" +
                              "  {                         I            \r\n" +
                              " {      -                    `,          \r\n" +
                              " |       ',                   )          \r\n" +
                              " |        |   ,..__      __. Y           \r\n" +
                              " |    .,_./  Y ' / ^Y   J   )|           \r\n" +
                              " \\           |' /   |   |   ||          \r\n" +
                              "  \\          L_/    . _ (_,.'(          \r\n" +
                              "   \\,   ,      ^^\"\"' / |      )       \r\n" +
                              "     \\_  \\          /,L]     /         \r\n" +
                              "       '-_~-,       ` `   ./`            \r\n" +
                              "          `'{_            )              \r\n" +
                              "              ^^\\..___,.--`             \r\n");
        }

        // Abandon Journey - Art
        public static void DisplayAbandon()
        {
            Console.WriteLine("\n" +
                              "            ^^                   @@@@@@@@@                                   \r\n" +
                              "       ^^       ^^            @@@@@@@@@@@@@@@                                \r\n" +
                              "                            @@@@@@@@@@@@@@@@@@              ^^               \r\n" +
                              "                           @@@@@@@@@@@@@@@@@@@@                              \r\n" +
                              " ~~~~ ~~ ~~~~~ ~~~~~~~~ ~~ &&&&&&&&&&&&&&&&&&&& ~~~~~~~ ~~~~~~~~~~~ ~~~      \r\n" +
                              " ~         ~~   ~  ~       ~~~~~~~~~~~~~~~~~~~~ ~       ~~     ~~ ~          \r\n" +
                              "   ~      ~~      ~~ ~~ ~~  ~~~~~~~~~~~~~ ~~~~  ~     ~~~    ~ ~~~  ~ ~~     \r\n" +
                              "   ~  ~~     ~         ~      ~~~~~~  ~~ ~~~       ~~ ~ ~~  ~~ ~             \r\n" +
                              " ~  ~       ~ ~      ~           ~~ ~~~~~~  ~      ~~  ~             ~~      \r\n" +
                              "       ~             ~        ~      ~      ~~   ~             ~             \r\n");
        }
    }
}
