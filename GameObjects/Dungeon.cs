using DungeonsOfDoom.Enums;
using System;
using System.IO;
using System.Xml.Serialization;

namespace DungeonsOfDoom.GameObjects
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

            CellIcon icon = CellIcon.None;
            for (int x = 0; x < Cells.GetLength(0); x++)
            {
                for (int y = 0; y < Cells.GetLength(1); y++)
                {
                    try
                    {
                        icon = (CellIcon)chart[y][x];
                    }
                    catch
                    {
                        icon = CellIcon.Unwalkable;
                    }

                    if (icon == CellIcon.Player)
                    {
                        Cells[x, y] = new Cell(CellIcon.Room);
                        SpawnPoint = new int[] { x, y };
                    }
                    else
                    {
                        Cells[x, y] = new Cell(icon);
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
                    {
                        Cells[x, y].Item = items[items.Length - (itemsLeft--)];
                    }
                }
            }
        }
    }
}
