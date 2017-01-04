﻿using Maker.RiseEngine.Core;
using Maker.RiseEngine.Core.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Maker.twiyol.Game
{
    public class GameUIScene : Scene
    {

        GameScene G;
        Scenes.Menu.MenuMain MainMenu;
        bool IsPause = false;

        public GameUIScene(GameScene _gameScene)
        {
            G = _gameScene;
        }

        public override void OnLoad()
        {

        }

        public override void OnUnload()
        {

        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        KeyboardState oldKeyBoard;
        public override void OnUpdate(MouseState mouse, KeyboardState keyBoard, GameTime gameTime)
        {
            if (keyBoard.IsKeyDown(Engine.engineConfig.Input_ShowMenu) && oldKeyBoard.IsKeyUp(Engine.engineConfig.Input_ShowMenu))
            {
                if (!IsPause)
                    PauseGame();

            }

            oldKeyBoard = keyBoard;
        }

        public void PauseGame()
        {
            IsPause = true;
            G.PauseSimulation = true;
            MainMenu = new Scenes.Menu.MenuMain(G);
            RiseEngine.sceneManager.AddScene(MainMenu);
            MainMenu.show();
        }

        public void GoBackToGame()
        {

            RiseEngine.sceneManager.RemoveScene(MainMenu);
            G.PauseSimulation = false;
            IsPause = false;

        }
    }
}