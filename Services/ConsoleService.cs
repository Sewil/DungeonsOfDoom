using DungeonsOfDoom.Enums;
using DungeonsOfDoom.GameObjects;
using System;
using System.Linq;

namespace DungeonsOfDoom.Services
{
    static class ConsoleService
    {
        public static void WriteLogo()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
         __ \  |   |  \  |  ___| ____|  _ \   \  |  ___|     _ \  ____|
         |   | |   |   \ | |     __|   |   |   \ |\___ \    |   | |    
         |   | |   | |\  | |   | |     |   | |\  |      |   |   | __|  
        ____/ \___/ _| \_|\____|_____|\___/ _| \_|_____/   \___/ _|    
                                                                       
                             __ \   _ \   _ \   \  |
                             |   | |   | |   | |\/ |
                             |   | |   | |   | |   |
                            ____/ \___/ \___/ _|  _|
                                                    ");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void WriteDungeon(Game game)
        {
            Console.Clear();
            WriteLogo();

            for (int y = 0; y < game.Dungeon.Cells.GetLength(1); y++)
            {
                for (int x = 0; x < game.Dungeon.Cells.GetLength(0); x++)
                {
                    Cell cell = game.Dungeon.Cells[x, y];
                    if (cell.IsRoom() && x == game.Player.X && y == game.Player.Y)
                    {
                        Console.Write((char)CellIcon.Player);
                    }
                    else if (game.Monsters.Any(i => i.X == x && i.Y == y))
                    {
                        Console.Write((char)CellIcon.Monster);
                    }
                    else
                    {
                        Console.Write((char)cell.Icon);
                    }
                }

                Console.WriteLine();
            }
            WriteControls(game);
        }

        public static string AskForName()
        {
            string userName;

            do
            {
                Console.Clear();
                WriteLogo();
                Console.Write("Character name: ");
                userName = Console.ReadLine();

            } while (!Character.ValidName(userName));

            return userName;
        }
        public static string AskForClass()
        {
            ConsoleKey choosenClass;
            do
            {
                Console.Clear();
                WriteLogo();
                Console.WriteLine("Choose your class: (W)arrior, (R)ogue or (S)orcerer?");
                choosenClass = Console.ReadKey().Key;

            } while (choosenClass != ConsoleKey.W && choosenClass != ConsoleKey.R && choosenClass != ConsoleKey.S);

            return choosenClass.ToString();
        }
        public static void WriteAttributes(Player player)
        {
            Console.Clear();
            WriteLogo();
            Console.WriteLine("ATTRIBUTES");
            Console.WriteLine("Strength: " + player.Strength + "%");
            Console.WriteLine("Dexterity: " + player.Dexterity + "%");
            Console.WriteLine("Health: " + player.Health + "%");
            Console.WriteLine("Intelligence: " + player.Intelligence + "%");
        }
        public static void WriteFoundItem(Item item)
        {
            Console.WriteLine($"You found {item.Name}!");
            Console.WriteLine($"\"{item.Description}\"");
        }

        public static void WriteControls(Game game)
        {
            Player player = game.Player;
            Cell currentCell = game.Dungeon.Cells[player.X, player.Y];
            switch (game.Window)
            {
                case GameWindow.Dungeon:
                    string inventoryCount = player.Inventory.Count > 0 ? "(" + player.Inventory.Count + ")" : "";
                    Console.WriteLine("(↑→↓←) Move\t(E) Inventory " + inventoryCount + (currentCell.HasItem ? "\t(P) Pick up" : ""));
                    break;
                case GameWindow.Inventory:
                    bool itemSelected = player.SelectedItem != null;
                    string useText = player.SelectedItem?.UseText ?? "";
                    Console.WriteLine("(↑↓) Select item\t(E) Close inventory" + (itemSelected ? "\t(D) Drop" : "") + (itemSelected ? "\t(Enter) " + useText : ""));
                    break;
                default: break;
            }
        }

        public static void PadLines(int lines)
        {
            for (int i = 0; i < lines; i++)
            {
                Console.WriteLine();
            }
        }
        public static void PadText(int spaces)
        {
            for (int i = 0; i < spaces; i++)
            {
                Console.Write(" ");
            }
        }
        public static void Pause(string text = "Press anywhere to continue . . . ")
        {
            Console.Write(text);
            Console.ReadKey();
        }

        public static void WriteInventory(Game game)
        {
            Console.Clear();
            WriteLogo();
            Console.WriteLine("INVENTORY");
            for (int i = 0; i < game.Player.Inventory.Count; i++)
            {
                Item item = game.Player.Inventory[i];
                Console.WriteLine($"{(i == game.Player.SelectedInventoryIndex ? ">" : " ")} {item.Name}");
                Console.WriteLine($"    \"{item.Description}\"");
                Console.WriteLine($"    {item.Value} grams");
                Console.WriteLine();
            }
            WriteControls(game);
        }
    }
}
