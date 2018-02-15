using DungeonsOfDoom.Utils;
using System;

namespace DungeonsOfDoom.GameObjects
{
    class Player : Character
    {
        private int selectedInventoryIndex;
        public int SelectedInventoryIndex
        {
            get
            {
                selectedInventoryIndex = Inventory.Count == 0 ? 0 : Maths.Mod(selectedInventoryIndex, Inventory.Count);
                return selectedInventoryIndex;
            }
            set
            {
                selectedInventoryIndex = value;
            }
        }
        public Item SelectedItem
        {
            get
            {
                return Inventory.Count > 0 ? Inventory[SelectedInventoryIndex] : null;
            }
        }

        public Player(string name)
        {
            Name = name;
        }

        public void SetClass(string playerClass)
        {
            Random random = new Random();
            switch (playerClass)
            {
                case "W": //Warrior
                    Strength = random.Next(40, 46);
                    Dexterity = random.Next(16, 21);
                    Health = random.Next(27, 31);
                    Intelligence = 100 - (Strength + Dexterity + Health);
                    break;
                case "R": //Rogue
                    Strength = random.Next(26, 31);
                    Dexterity = random.Next(40, 46);
                    Health = random.Next(20, 24);
                    Intelligence = 100 - (Strength + Dexterity + Health);
                    break;
                case "S": //Sorcerer
                    Strength = random.Next(6, 13);
                    Dexterity = random.Next(22, 28);
                    Health = random.Next(14, 25);
                    Intelligence = 100 - (Strength + Dexterity + Health);
                    break;
            }
        }
    }
}
