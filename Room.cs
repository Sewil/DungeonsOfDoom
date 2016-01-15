using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsOfDoom {
    class Cell {
        public char Icon { get; set; }
        public Item Item { get; set; }
        public bool HasItem { get; set; }
        public Cell(char icon) {
            Icon = icon;
        }
        public bool IsRoom() {
            return Icon == DungeonsOfDoom.Icon.ROOM || Icon == DungeonsOfDoom.Icon.PLAYER;
        }
    }
}
