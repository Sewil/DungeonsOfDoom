namespace DungeonsOfDoom.Utils
{
    static class Maths
    {
        public static int Mod(int x, int m)
        {
            return (x % m + m) % m;
        }
    }
}
