using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom {
    class Character {
        public static char Icon { get { return '☻'; } }
        public static int MaxWeight { get { return 50000; } }
        public int CurrentWeight { get; set; }
        public List<Item> Inventory { get; set; }
        string name;

        public string Name {
            get { return name; }
            set {
                if (ValidName(value)) {
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
        public static bool ValidName(string value) {
            return value.Length >= 2 && value.Length <= 12;
        }
    }
    class Player : Character {
        public Player(string name) {

        }

        public void SetClass(string playerClass) {
            Random random = new Random();
            switch (playerClass) {
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
        public void Loot(Cell cell) {
            if (cell.HasItem) {
                int weightAfterPickup = CurrentWeight + cell.Item.Weight;
                if (weightAfterPickup <= MaxWeight) { //plockar upp
                    IO.PlayPickUpItem();
                    Inventory.Add(cell.Item);
                    CurrentWeight += cell.Item.Weight;
                    cell.HasItem = false;
                    IO.PadLines(2);
                } else if (weightAfterPickup > MaxWeight) {// rummet har ett item men spelaren orkar inte bära det
                    IO.PlayTooMuchWeight();
                    IO.WriteTooMuchWeight();
                    IO.PadLines(1);
                }
            } else {
                IO.PadLines(2);
            }
        }

        public void Move(Dungeon dungeon, ConsoleKey key) {
            if (IO.MoveUp(key)) {
                try {
                    if (dungeon.Cells[X, Y - 1].IsRoom()) {
                        Y -= 1;
                        IO.PlayMove();
                    }
                } catch { }
            } else if (IO.MoveLeft(key)) {
                try {
                    if (dungeon.Cells[X - 1, Y].IsRoom()) {
                        X -= 1;
                        IO.PlayMove();
                    }
                } catch { }
            } else if (IO.MoveDown(key)) {
                try {
                    if (dungeon.Cells[X, Y + 1].IsRoom()) {
                        Y += 1;
                        IO.PlayMove();
                    }
                } catch { }
            } else if (IO.MoveRight(key)) {
                try {
                    if (dungeon.Cells[X + 1, Y].IsRoom()) {
                        X += 1;
                        IO.PlayMove();
                    }
                } catch { }
            }
        }
        
    }
    enum MonsterType {
        Human,
        Orc
    }
    class Monster {
        public MonsterType Type { get; set; }
    }
}
