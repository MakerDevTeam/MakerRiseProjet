﻿using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.Drawing;

namespace Maker.RiseEngine.Core
{
   public struct ContentEngine
    {

        public static ContentManager Content;

        static Dictionary<string, Texture2D> ColectionTexture2D = new Dictionary<string, Texture2D>();
        static Dictionary<string, SoundEffect> ColectionSoundEffect = new Dictionary<string, SoundEffect>();
        static Dictionary<string, Song> ColectionSong = new Dictionary<string, Song>();
        static Dictionary<string, SpriteFont> ColectionFont = new Dictionary<string, SpriteFont>();


        public static Texture2D Texture2D(string PluginName, string contentname)
        {

            if (ColectionTexture2D.ContainsKey(contentname))
            { return ColectionTexture2D[contentname]; }
            else
            {
                EngineDebug.DebugLogs.WriteInLogs("Load <Texture2D>" + PluginName + "." + contentname, EngineDebug.LogType.Info, "ContentEngine");
                ColectionTexture2D.Add(contentname, Content.Load<Texture2D>(PluginName + "/Textures2D/" + contentname));
                return ColectionTexture2D[contentname];
            }
            
        }

        public static Texture2D GetDefaultTexture2D() {

            System.Drawing.Bitmap bitmap = new Bitmap(2, 2);
            bitmap.SetPixel(0, 0, System.Drawing.Color.Fuchsia);
            bitmap.SetPixel(1, 1, System.Drawing.Color.Fuchsia);
            bitmap.SetPixel(1, 0, System.Drawing.Color.Black);
            bitmap.SetPixel(0, 1, System.Drawing.Color.Black);

            return Core.Rendering.BitmapHelper.BitmapToTexture2D(Common.GraphicsDevice ,bitmap);

        }

        public static SoundEffect SoundEffect(string PluginName, string contentname)
        {

            if (ColectionSoundEffect.ContainsKey(contentname))
            { return ColectionSoundEffect[contentname]; }
            else
            {
                EngineDebug.DebugLogs.WriteInLogs("Load <SoundEffect>" + PluginName + "." + contentname, EngineDebug.LogType.Info, "ContentEngine");
                ColectionSoundEffect.Add(contentname, Content.Load<SoundEffect>(PluginName + "/SoundsEffects/" + contentname));
                return ColectionSoundEffect[contentname];
            }

        }

        public static Song Song(string PluginName, string contentname)
        {

            if (ColectionSong.ContainsKey(contentname))
            { return ColectionSong[contentname]; }
            else
            {
                EngineDebug.DebugLogs.WriteInLogs("Load <Song>" + PluginName + "." + contentname, EngineDebug.LogType.Info, "ContentEngine");
                ColectionSong.Add(contentname, Content.Load<Song>(PluginName + "/Songs/" + contentname));
                return ColectionSong[contentname];
            }

        }

        public static SpriteFont SpriteFont(string PluginName, string contentname)
        {

            if (ColectionFont.ContainsKey(contentname))
            { return ColectionFont[contentname]; }
            else
            {
                EngineDebug.DebugLogs.WriteInLogs("Load <SpriteFont>" + PluginName + "." + contentname, EngineDebug.LogType.Info, "ContentEngine");
                ColectionFont.Add(contentname, Content.Load<SpriteFont>(PluginName + "/Fonts/" + contentname));
                return ColectionFont[contentname];
            }

        }

        public static void ReloadContent() {

            EngineDebug.DebugLogs.WriteInLogs("Reloading...", EngineDebug.LogType.Info, "ContentEngine");
            ColectionTexture2D.Clear();
            ColectionSoundEffect.Clear();
            ColectionSong.Clear();
            ColectionFont.Clear();

        }
       

    }
}
