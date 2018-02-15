using DungeonsOfDoom.Enums;
using System;

namespace DungeonsOfDoom.GameObjects
{
    class Monster : Character
    {
        public DateTime LastMoved { get; set; }
        public MonsterType Type { get; set; }
    }
}
