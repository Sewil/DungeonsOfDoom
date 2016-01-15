using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom
{
    class Item
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int Weight { get; private set; }

        public Item(string name, string description, int weight)
        {
            Name = name;
            Description = description;
            Weight = weight;
        }

        public static Item[] GetItems(string[] text) {
            Item[] items = new Item[text.Length];
            for (int i = 0; i < text.Length; i++) //lägger in allt från filen till en Items array
            {//"Magical Sword" "A very heavy sword that is magical and glows blue" 24
                items[i] = new Item(text[i].Split('"')[1], text[i].Split('"')[3], int.Parse(text[i].Split('"')[4]));
            }
            return items;
        }
    }
}
