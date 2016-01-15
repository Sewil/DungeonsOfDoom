using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom {
    class Dungeon {
        public Cell[,] Cells { get; set; }
        public int[] SpawnPoint { get; }

        public Dungeon() {
            string[] chart = File.ReadAllLines("Dungeon.txt");

            int longestRow = 0;
            for (int i = 0; i < chart.Length; i++) {
                if (longestRow < chart[i].Length) {
                    longestRow = chart[i].Length;
                }
            }
            Cells = new Cell[longestRow, chart.Length];

            char icon;
            for (int x = 0; x < Cells.GetLength(0); x++) {
                for (int y = 0; y < Cells.GetLength(1); y++) {
                    try {
                        icon = chart[y][x];
                    } catch {
                        icon = '\0';
                    }
                    Cells[x, y] = new Cell(icon);

                    if (Cells[x, y].Icon == Player.Icon) {
                        Cells[x, y].Icon = ' ';
                        SpawnPoint = new int[] { x, y };
                    }
                }
            }

            Item[] itemsFromFile = Item.GetItems(File.ReadAllLines("Items.txt"));
            PlaceItems(itemsFromFile);
        }
        
        private int[] GetDimensions(string[] chart) {
            int longestRow = 0;
            for (int i = 0; i < chart.Length; i++) {
                if (longestRow < chart[i].Length)
                    longestRow = chart[i].Length;
            }
            return new int[] { longestRow, chart.Length };
        }

        private void GenerateCells(string[] text) {
            
        }

        private void PlaceItems(Item[] items) {
            Random random = new Random();
            int itemsLeft = items.Length;
            for (int iX = 0; iX < Cells.GetLength(0); iX++) {
                for (int iY = 0; iY < Cells.GetLength(1); iY++) {
                    if (random.Next(1, 101) <= 10 && itemsLeft > 0 && Cells[iX, iY].IsRoom()) { // delar ut denna Item array till kartan, 10% chans att ett rum får ett item
                        Cells[iX, iY].HasItem = true;
                        Cells[iX, iY].Item = items[items.Length - (itemsLeft--)];
                    }
                }
            }
        }

        public void Print(Player player) {
            for (int y = 0; y < Cells.GetLength(1); y++) {
                for (int x = 0; x < Cells.GetLength(0); x++) {
                    if (Cells[x, y].Icon == ' ' && x == player.X && y == player.Y) {
                        Console.Write(Player.Icon);
                    }
                    else {
                        Console.Write(Cells[x, y].Icon);
                    }
                }

                Console.WriteLine();
            }
        }
    }
}