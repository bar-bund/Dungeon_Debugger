using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dungeon_debugger
{
    // Item base class
    public abstract class Item
    {
        public string Name { get; set; }
    }

    // Vial class
    public class Vial : Item
    {
        public int DamageBoost { get; } = 5;

        public Vial()
        {
            Name = "Vial";
        }
    }

    // Bandage class
    public class Bandage : Item
    {
        public int HealAmount { get; } = 15;

        public Bandage()
        {
            Name = "Bandage";
        }
    }

    // Shield class
    public class Shield : Item
    {
        public int TemporaryHealthBoost { get; } = 10;

        public Shield()
        {
            Name = "Shield";
        }
    }
}
