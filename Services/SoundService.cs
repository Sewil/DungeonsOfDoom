using System.Media;

namespace DungeonsOfDoom.Services
{
    static class SoundService
    {
        static SoundPlayer soundPlayer = new SoundPlayer();
        public static void PlayMove()
        {
            soundPlayer.SoundLocation = "Walk.wav";
            soundPlayer.Play();
        }
        public static void PlayFoundItem()
        {
            soundPlayer.SoundLocation = "Found item.wav";
            soundPlayer.Play();
        }
        public static void PlayPickUpItem()
        {
            soundPlayer.SoundLocation = "Pick up item.wav";
            soundPlayer.Play();
        }
    }
}
