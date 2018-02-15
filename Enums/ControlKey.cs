using System;

namespace DungeonsOfDoom.Enums
{
    enum ControlKey
    {
        None = 0,
        PickUp = ConsoleKey.P,
        ToggleInventory = ConsoleKey.E,
        MoveUp = ConsoleKey.UpArrow,
        MoveLeft = ConsoleKey.LeftArrow,
        MoveDown = ConsoleKey.DownArrow,
        MoveRight = ConsoleKey.RightArrow,
        SelectItemDown = ConsoleKey.DownArrow,
        SelectItemUp = ConsoleKey.UpArrow,
        DropItem = ConsoleKey.D,
        UseItem = ConsoleKey.Enter
    }
}
