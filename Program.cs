using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom {
    class Program {
        static Player player;

        static void Main(string[] args) {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            IO.CleanConsole();
            StartGame();
            PlayGame();
        }

        static void StartGame() {
            string playerName = IO.AskForName();
            player = new Player(playerName);

            string playerClass = IO.AskForClass();
            player.SetClass(playerClass);

            IO.WriteAttributes(player);
            IO.Pause();
        }
        static void PlayGame() {
            Dungeon dungeon = new Dungeon();
            player.X = dungeon.SpawnPoint[0];
            player.Y = dungeon.SpawnPoint[1];

            Cell currentRoom;
            ConsoleKey key = new ConsoleKey();
            do {
                IO.CleanConsole();
                //Console.WriteLine(player.X + ", " + player.Y);

                dungeon.Print(player);
                currentRoom = dungeon.Cells[player.X, player.Y];


                if (IO.PickUp(key)) {
                    player.Loot(currentRoom);
                }
                else {
                    if (currentRoom.HasItem) {
                        IO.PlayFoundItem();
                        currentRoom.Item.WriteFoundItem();
                    } else {
                        IO.PadLines(2);
                    }
                }

                key = Console.ReadKey().Key;

                player.Move(dungeon, key);

            } while (player.Health > 0);
        }
    }
}