﻿using Maker.RiseEngine;
using Maker.RiseEngine.Input;
using Maker.RiseEngine.Scenes;
using Maker.RiseEngine.UserInterface;
using Maker.RiseEngine.UserInterface.Controls;

using Maker.Twiyol.Game.WorldDataStruct;
using Maker.Twiyol.Inventory;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using System.Collections.Generic;

namespace Maker.Twiyol.Game.GameUI
{
    public class Inventory : Scene
    {

        Panel rootPanel;
        List<ItemSlot> Slots;
        DataInventory PlayerInventory;
        GameScene G;

        public Inventory(DataEntity Player, GameScene game) {

            G = game;
            Slots = new List<ItemSlot>();
            rootPanel = new Panel(new Rectangle(-256, -128, 512, 256), Color.White);
            rootPanel.Anchor = Anchors.Center;
            PlayerInventory = Player.Tags.GetTag("inventory", new DataInventory(32));

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    ItemSlot s = new ItemSlot(new Point(x * 64, y * 64), PlayerInventory, y * 8 + x);
                    Slots.Add(s);
                    rootPanel.AddChild(s);
                }
            }
        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            rootPanel.Draw(spriteBatch, gameTime);
        }

        public override void OnLoad()
        {

        }

        public override void OnUnload()
        {

        }

        public override void OnUpdate(GameInput playerInput, GameTime gameTime)
        {
            rootPanel.Update(playerInput, gameTime);

            if (playerInput.IsKeyBoardKeyPress(Engine.userConfig.InputShowMainMenu)) {

                G.GameUIScene.GoBackToGame(this);
            }
        }

    }
}
