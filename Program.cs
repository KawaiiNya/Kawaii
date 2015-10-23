using System;
using System.IO;
using System.Media;
using LeagueSharp;
using LeagueSharp.Common;
using TreesKawaii.Properties;

namespace TreesKawaii
{
    internal class Program
    {
        public static SoundObject Kawaii;

        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        private static void Game_OnGameLoad(EventArgs args)
        {
            Kawaii = new SoundObject(Resources.Kawaii);
            Game.OnNotify += Game_OnNotify;
        }

        private static void Game_OnNotify(GameNotifyEventArgs args)
        {
            if (args.NetworkId.Equals(ObjectManager.Player.NetworkId) && args.EventId.Equals(GameEventId.OnChampionKill))
            {
                Kawaii.Play();
            }
        }
    }

    internal class SoundObject
    {
        public static float LastPlayed;
        private static SoundPlayer _sound;

        public SoundObject(Stream sound)
        {
            _sound = new SoundPlayer(sound);
        }

        public void Play()
        {
            if (Utils.TickCount - LastPlayed < 1500)
            {
                return;
            }
            _sound.Play();
            LastPlayed = Utils.TickCount;
        }
    }
}