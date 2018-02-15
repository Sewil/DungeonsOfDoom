using DungeonsOfDoom.Enums;
using DungeonsOfDoom.GameObjects;
using DungeonsOfDoom.Services;
using System;
using System.Collections.Generic;
using System.Threading;

namespace DungeonsOfDoom
{
    class Game
    {
        public Queue<ControlKey> QueuedKeys { get; set; }
        public Dungeon Dungeon { get; set; }
        public Player Player { get; set; }
        public IList<Monster> Monsters { get; set; }
        public GameWindow Window { get; set; }
        public Cell CurrentCell { get { return Dungeon.Cells[Player.X, Player.Y]; } }
        private bool Rewrite { get; set; }

        public Game()
        {
            QueuedKeys = new Queue<ControlKey>();
            Monsters = new List<Monster>();
        }

        public void Start()
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.Clear();
            ConsoleService.WriteLogo();

            Window = GameWindow.NameCharacter;
            string playerName = ConsoleService.AskForName();
            Player = new Player(playerName);

            Window = GameWindow.PickClass;
            string playerClass = ConsoleService.AskForClass();
            Player.SetClass(playerClass);
            ConsoleService.WriteAttributes(Player);
            ConsoleService.Pause();

            Window = GameWindow.Dungeon;
            Dungeon = new Dungeon();
            Player.X = Dungeon.SpawnPoint[0];
            Player.Y = Dungeon.SpawnPoint[1];
            SpawnMonsters();

            StartKeyboardListener();
            StartMonsterMover();
            ControlKey key = ControlKey.None;
            QueuedKeys.Enqueue(key);
            while (Player.Health > 0)
            {
                key = ControlKey.None;
                if (QueuedKeys.Count == 0 && !Rewrite)
                {
                    continue;
                }
                if (Rewrite)
                {
                    Rewrite = false;
                }
                if (QueuedKeys.Count > 0)
                {
                    key = QueuedKeys.Dequeue();
                }
                if (Window == GameWindow.Inventory && key == ControlKey.ToggleInventory)
                {
                    Window = GameWindow.Dungeon;
                }
                else if (Window == GameWindow.Dungeon && key == ControlKey.ToggleInventory)
                {
                    Window = GameWindow.Inventory;
                }

                if (Window == GameWindow.Inventory)
                {
                    if (key == ControlKey.UseItem)
                    {
                        UseItem();
                    }
                    else if (key == ControlKey.SelectItemDown || key == ControlKey.SelectItemUp)
                    {
                        Player.SelectedInventoryIndex += key == ControlKey.SelectItemDown ? 1 : -1;
                    }
                    else if (key == ControlKey.DropItem && !CurrentCell.HasItem)
                    {
                        DropItem();
                    }
                    ConsoleService.WriteInventory(this);
                }
                else if (Window == GameWindow.Dungeon)
                {
                    MovePlayer(key);
                    ConsoleService.WriteDungeon(this);
                    if (key == ControlKey.PickUp)
                    {
                        LootItem();
                        ConsoleService.WriteDungeon(this);
                    }
                    else if (CurrentCell.HasItem)
                    {
                        SoundService.PlayFoundItem();
                        ConsoleService.WriteFoundItem(CurrentCell.Item);
                    }
                    else
                    {
                        ConsoleService.PadLines(2);
                    }
                }
                Thread.Sleep(10);
            };
        }

        public void UseItem()
        {
            Player.Inventory.RemoveAt(Player.SelectedInventoryIndex);
        }

        public void DropItem()
        {
            Item item = Player.SelectedItem;
            CurrentCell.Item = item;
            Player.Inventory.RemoveAt(Player.SelectedInventoryIndex);
        }

        public void LootItem()
        {
            if (CurrentCell.HasItem)
            {
                SoundService.PlayPickUpItem();
                Player.Inventory.Add(CurrentCell.Item);
                CurrentCell.Item = null;
            }
        }

        public void MovePlayer(ControlKey key)
        {
            try
            {
                switch (key)
                {
                    case ControlKey.MoveUp: MovePlayer(Player.X, Player.Y - 1); break;
                    case ControlKey.MoveLeft: MovePlayer(Player.X - 1, Player.Y); break;
                    case ControlKey.MoveRight: MovePlayer(Player.X + 1, Player.Y); break;
                    case ControlKey.MoveDown: MovePlayer(Player.X, Player.Y + 1); break;
                    default: break;
                }
            }
            catch { }
        }

        private void SpawnMonsters()
        {
            Random r = new Random();
            for (int x = 0; x < Dungeon.Cells.GetLength(0); x++)
            {
                for (int y = 0; y < Dungeon.Cells.GetLength(1); y++)
                {
                    Cell cell = Dungeon.Cells[x, y];
                    if (r.Next(100) <= 1 && cell.IsRoom() && !cell.HasItem)
                    {
                        Monsters.Add(new Monster
                        {
                            X = x,
                            Y = y,
                            Name = "Monster " + x + "," + y,
                            Health = 10,
                            Type = MonsterType.Orc
                        });
                    }
                }
            }
        }

        private void MovePlayer(int newX, int newY)
        {
            try
            {
                if (Dungeon.Cells[newX, newY].IsRoom())
                {
                    Player.X = newX;
                    Player.Y = newY;
                    SoundService.PlayMove();
                }
            }
            catch { }
        }

        private void StartMonsterMover()
        {
            new Thread(() =>
            {
                Random r = new Random();
                DateTime now = DateTime.UtcNow;
                foreach (var monster in Monsters)
                {
                    monster.LastMoved = now;
                }
                while (true)
                {
                    foreach (var monster in Monsters)
                    {
                        int delay = 1000 + r.Next(20000);
                        if (now > monster.LastMoved.AddMilliseconds(delay))
                        {
                            MoveMonsterRandomly(r, monster);
                        }
                    }
                    Thread.Sleep(200);
                    now = DateTime.UtcNow;
                }
            }).Start();
        }

        private void MoveMonsterRandomly(Random r, Monster monster)
        {
            int x = r.Next(3) - 1;
            int y = r.Next(3) - 1;
            if (x == 0 && y == 0)
            {
                return;
            }
            x = monster.X + x;
            y = monster.Y + y;
            try
            {
                if (Dungeon.Cells[x, y].IsRoom())
                {
                    monster.X = x;
                    monster.Y = y;
                }
                monster.LastMoved = DateTime.UtcNow;
                Rewrite = true;
            }
            catch { }
        }

        private void StartKeyboardListener()
        {
            new Thread(() =>
            {
                while (true)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;
                    if (Enum.IsDefined(typeof(ControlKey), (int)key))
                    {
                        QueuedKeys.Enqueue((ControlKey)key);
                    }
                }
            })
            {
                IsBackground = true
            }.Start();
        }
    }
}
