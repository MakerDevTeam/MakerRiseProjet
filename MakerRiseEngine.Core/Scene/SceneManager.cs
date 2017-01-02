﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.RiseEngine.Core.Scene
{
    public struct SceneManager
    {

        public static int CurrentScene = -1;
        public static MainMenu MainMn;

        static WorldGeneratorTest w;

        static Point MouseXY = Point.Zero;

        public static void Initialize()
        {
            MainMn = new MainMenu();
            w = new WorldGeneratorTest();
        }

        //0 = MainMenu
        //1 = Option
        //2 = WorldManager
        //3 = loading

        
        public static void Update(MouseState Mouse, KeyboardState KeyBoard, GameTime gameTime)
        {

            MouseXY = Mouse.Position;
           
            switch (CurrentScene)
            {
                case -1:
                    //do nothing
                    break;
                case 0:
                    //Update Main Menu
                    MainMn.Update(Mouse, KeyBoard, gameTime);

                    break;
;
                case 5:
                    w.Update(Mouse, KeyBoard, gameTime);
                    break;
            }
            

        }

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            

            switch (CurrentScene)
            {
                case -1:
                    //do nothing
                    break;
                case 0:
                    //Draw Main menu 
                    MainMn.Draw(spriteBatch, gameTime);

                    break;
                case 5:
                    w.Draw(spriteBatch, gameTime);
                    break;
            }
            

        }

    }
}
