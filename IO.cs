using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Media;
using System.Resources;
namespace DungeonsOfDoom {
    static class IO {
        static SoundPlayer soundPlayer = new SoundPlayer();
        public static void PlayMove() {
            soundPlayer.SoundLocation = "Walk.wav";
            soundPlayer.Play();
        }
        public static void PlayFoundItem() {
            soundPlayer.SoundLocation = "Found item.wav";
            soundPlayer.Play();
        }
        public static void PlayPickUpItem() {
            soundPlayer.SoundLocation = "Pick up item.wav";
            soundPlayer.Play();
        }
        public static void PlayTooMuchWeight() {
            soundPlayer.SoundLocation = "Too much weight.wav";
            soundPlayer.Play();
        }
        static void WriteLogo() {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"
         __ \  |   |  \  |  ___| ____|  _ \   \  |  ___|     _ \  ____|
         |   | |   |   \ | |     __|   |   |   \ |\___ \    |   | |    
         |   | |   | |\  | |   | |     |   | |\  |      |   |   | __|  
        ____/ \___/ _| \_|\____|_____|\___/ _| \_|_____/   \___/ _|    
                                                                       
                             __ \   _ \   _ \   \  |
                             |   | |   | |   | |\/ |
                             |   | |   | |   | |   |
                            ____/ \___/ \___/ _|  _|
                                                    ");
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static string AskForName() {
            string userName;

            do {
                CleanConsole();
                Console.Write("Character name: ");
                userName = Console.ReadLine();

            } while (!Player.ValidName(userName));

            return userName;
        }
        public static string AskForClass() {
            ConsoleKey choosenClass;
            do {
                IO.CleanConsole();
                Console.WriteLine("Choose your class: (W)arrior, (R)ogue or (S)orcerer?");
                choosenClass = Console.ReadKey().Key;

            } while (choosenClass != ConsoleKey.W && choosenClass != ConsoleKey.R && choosenClass != ConsoleKey.S);

            return choosenClass.ToString();
        }
        public static void WriteAttributes(Player player) {
            IO.CleanConsole();
            Console.WriteLine("ATTRIBUTES");
            Console.WriteLine("Strength: " + player.Strength + "%");
            Console.WriteLine("Dexterity: " + player.Dexterity + "%");
            Console.WriteLine("Health: " + player.Health + "%");
            Console.WriteLine("Intelligence: " + player.Intelligence + "%");
        }
        public static bool PickUp(ConsoleKey key) {
            return key == ConsoleKey.P;
        }
        public static bool MoveUp(ConsoleKey key) {
            return key == ConsoleKey.W;
        }
        public static bool MoveLeft(ConsoleKey key) {
            return key == ConsoleKey.A;
        }
        public static bool MoveDown(ConsoleKey key) {
            return key == ConsoleKey.S;
        }
        public static bool MoveRight(ConsoleKey key) {
            return key == ConsoleKey.D;
        }
        public static void WriteFoundItem(this Item item) {
            Console.WriteLine("You found " + item.Name + " (" + (double.Parse(item.Weight.ToString()) / 1000) + "kg)! Press P to pick it up.");
            Console.WriteLine("\"" + item.Description + "\"");
        }
        public static void WriteTooMuchWeight() {
            Console.WriteLine("This item weighs too much!");
        }
        public static void WriteWeightStatus(this Player player) {
            Console.WriteLine("Carrying "
                + (double.Parse(player.CurrentWeight.ToString()) / 1000)
                + "/"
                + (double.Parse(Player.MaxWeight.ToString()) / 1000) + "kg");
        }
        public static void PadLines(int lines) {
            for (int i = 0; i < lines; i++) {
                Console.WriteLine();
            }
        }
        public static void PadText(int spaces) {
            for (int i = 0; i < spaces; i++) {
                Console.Write(" ");
            }
        }
        public static void CleanConsole() {
            Console.Clear();
            WriteLogo();
        }
        public static void Pause(string text = "Press anywhere to continue . . . ") {
            Console.Write(text);
            Console.ReadKey();
        }

    }
}