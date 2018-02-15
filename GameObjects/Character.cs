using System.Collections.Generic;

namespace DungeonsOfDoom.GameObjects
{
    abstract class Character
    {
        public List<Item> Inventory { get; set; }

        string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (ValidName(value))
                {
                    name = value;
                }
            }
        }

        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Health { get; set; }
        public int Intelligence { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Character()
        {
            Inventory = new List<Item>();
        }

        public static bool ValidName(string value)
        {
            return value.Length >= 2 && value.Length <= 12;
        }
    }
}
