using DungeonsOfDoom.Enums;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace DungeonsOfDoom.GameObjects
{
    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public class Items
    {
        [XmlElement("Item")]
        public Item[] Item { get; set; }
    }

    [Serializable]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    public class Item
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Description { get; set; }

        [XmlAttribute]
        public ushort Value { get; set; }

        [XmlAttribute]
        public int Type { get; set; }

        public string UseText
        {
            get
            {
                switch ((ItemType)Type)
                {
                    case ItemType.Useable: return "Use";
                    case ItemType.Equipment: return "Equip";
                    default: return string.Empty;
                }
            }
        }
    }
}
