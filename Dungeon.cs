using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DungeonsOfDoom
{
    class Dungeon
    {
        public Cell[,] Cells { get; set; }
        public int[] SpawnPoint { get; }

        public Dungeon()
        {
            string[] chart = File.ReadAllLines("Dungeon.txt");

            int longestRow = 0;
            for (int i = 0; i < chart.Length; i++)
            {
                if (longestRow < chart[i].Length)
                {
                    longestRow = chart[i].Length;
                }
            }
            Cells = new Cell[longestRow, chart.Length];

            char icon;
            for (int x = 0; x < Cells.GetLength(0); x++)
            {
                for (int y = 0; y < Cells.GetLength(1); y++)
                {
                    try
                    {
                        icon = chart[y][x];
                    }
                    catch
                    {
                        icon = '\0';
                    }
                    Cells[x, y] = new Cell(icon);

                    if (Cells[x, y].Icon == Icon.PLAYER)
                    {
                        Cells[x, y].Icon = Icon.ROOM;
                        SpawnPoint = new int[] { x, y };
                    }
                }
            }

            using (FileStream stream = new FileStream("Items.xml", FileMode.Open, FileAccess.Read))
            {
                var serializer = new XmlSerializer(typeof(Items));
                var itemCollection = (Items)serializer.Deserialize(stream);
                PlaceItems(itemCollection.Item);
            }
        }

        private int[] GetDimensions(string[] chart)
        {
            int longestRow = 0;
            for (int i = 0; i < chart.Length; i++)
            {
                if (longestRow < chart[i].Length)
                    longestRow = chart[i].Length;
            }
            return new int[] { longestRow, chart.Length };
        }

        private void PlaceItems(Item[] items)
        {
            Random random = new Random();
            int itemsLeft = items.Length;
            for (int x = 0; x < Cells.GetLength(0); x++)
            {
                for (int y = 0; y < Cells.GetLength(1); y++)
                {
                    if (random.Next(1, 101) <= 10 && itemsLeft > 0 && Cells[x, y].IsRoom())
                    { // delar ut denna Item array till kartan, 10% chans att ett rum får ett item
                        Cells[x, y].HasItem = true;
                        Cells[x, y].Item = items[items.Length - (itemsLeft--)];
                    }
                }
            }
        }

        public void Print(Player player)
        {
            for (int y = 0; y < Cells.GetLength(1); y++)
            {
                for (int x = 0; x < Cells.GetLength(0); x++)
                {
                    if (Cells[x, y].Icon == Icon.ROOM && x == player.X && y == player.Y)
                    {
                        Console.Write(Icon.PLAYER);
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