﻿using Maker.RiseEngine.Core.Config;
using Maker.RiseEngine.Core.Content;
using Maker.RiseEngine.Core.Input;
using Maker.RiseEngine.Core.Plugin;
using Maker.RiseEngine.Core.Rendering;
using Maker.RiseEngine.Core.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
using System.Windows.Forms;
using static Maker.RiseEngine.Core.Rendering.SpriteFontDraw;

namespace Maker.RiseEngine.Core.Scenes.Scenes
{
    public class EngineLoading : Scene
    {

        public string Message = "Loading...";

        public override void OnLoad()
        {
            show();
            ThreadStart GenHandle = new ThreadStart(delegate
            {
                EngineDebug.DebugLogs.WriteLog("Initializing...", EngineDebug.LogType.Info, "Engine");
                Thread.Sleep(Engine.engineConfig.Engine_SplashTime);

                Message = "Loading config...";

                // load binary config file.
                if (System.IO.File.Exists("config.bin"))
                    Engine.engineConfig = SerializationHelper.LoadFromBin<EngineConfig>("config.bin");
                else
                    SerializationHelper.SaveToBin(Engine.engineConfig, "config.bin");
                this.Message = "Loading Plugins...";
                PluginLoader<IPlugin> p = new PluginLoader<IPlugin>("Plugins");
                p.initializePlugin();
                Engine.Plugins = p.Plugins;
  

                Engine.IsLoaded = true;

                RiseEngine.sceneManager.RemoveScene(this);
            });
            Thread t = new Thread(GenHandle);
            t.Start();
        }

        public override void OnUnload()
        {
            //setting up screen
            if (Engine.engineConfig.GFX_FullScreen == true)
            {
                // Set full screen.
                Engine.graphics.PreferredBackBufferWidth = Screen.PrimaryScreen.Bounds.Width;
                Engine.graphics.PreferredBackBufferHeight = Screen.PrimaryScreen.Bounds.Height;
                Engine.graphics.ToggleFullScreen();
                Engine.graphics.ApplyChanges();
            }
            else
            {
                // Set window setting.
                Engine.graphics.PreferredBackBufferWidth = 1366;
                Engine.graphics.PreferredBackBufferHeight = 768;
                Engine.graphics.ApplyChanges();
            }
        }

        public override void OnUpdate(GameInput playerInput, GameTime gameTime)
        {

        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(ContentEngine.Texture2D("Engine", "MakerLogo"), new Rectangle(0,0, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight), Color.White);
            spriteBatch.DrawString(ContentEngine.SpriteFont("Engine", "Consolas_16pt"), Message, new Rectangle(0, 16, Engine.graphics.PreferredBackBufferWidth, Engine.graphics.PreferredBackBufferHeight), Alignment.Top, Style.DropShadow, Color.White);
        }
    }
}
