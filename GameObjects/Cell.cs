using DungeonsOfDoom.Enums;

namespace DungeonsOfDoom.GameObjects
{
    class Cell
    {
        private CellIcon icon;
        public CellIcon Icon
        {
            get
            {
                if (HasItem)
                {
                    return CellIcon.Item;
                }
                else
                {
                    return icon;
                }
            }
        }
        public Item Item { get; set; }
        public bool HasItem
        {
            get
            {
                return Item != null;
            }
        }
        public Cell(CellIcon icon)
        {
            this.icon = icon;
        }
        public bool IsRoom()
        {
            return Icon == CellIcon.Room || Icon == CellIcon.Item;
        }
    }
}
