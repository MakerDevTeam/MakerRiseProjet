﻿using Maker.RiseEngine.Core.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Maker.RiseEngine.Core.Audio
{
    public static class SongEngine
    {


        static float FadeVolume;
        static bool IsFading = false;
        static bool Play = false;
        static string NextSong;
        static string PluginName;

        public static void SwitchSong(string _PluginName, string _Name)
        {
            FadeVolume = 1f;
            IsFading = true;
            NextSong = _Name;
            PluginName = _PluginName;
            MediaPlayer.IsRepeating = true;
        }


        public static void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
        {

            MediaPlayer.Volume = (((Engine.engineConfig.Sound_Master_Level * Engine.engineConfig.Sound_Song_Level) / 2) * FadeVolume);

            if (Play == true)
            {
                if (IsFading == true)
                {

                    FadeVolume -= 0.01f;
                    if (FadeVolume <= 0.1)
                    {

                        MediaPlayer.Stop();
                        Play = false;
                        FadeVolume = 1f;

                    }
                }
            }
            else
            {

                if (!(PluginName == null || NextSong == null))
                {

                    MediaPlayer.Play(ContentEngine.Song(PluginName, NextSong));
                    Play = true;
                    IsFading = false;

                }
            }
        }
    }
}
