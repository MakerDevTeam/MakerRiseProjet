﻿
using Maker.RiseEngine.Rendering;
using Maker.RiseEngine.Rendering.SpriteSheets;
using Maker.RiseEngine.Ressources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.RiseEngine.UserInterface.Controls
{
    public class Button : Control
    {

        // Sprites.
        // Button idle
        Sprite ButtonMid = Common.UserInterface.GetSprite("ButM");
        Sprite ButtonLeft = Common.UserInterface.GetSprite("ButL");
        Sprite ButtonRight = Common.UserInterface.GetSprite("ButR");

        // Button down.
        Sprite ButtonMidDown = Common.UserInterface.GetSprite("ButMD");
        Sprite ButtonLeftDown = Common.UserInterface.GetSprite("ButLD");
        Sprite ButtonRightDown = Common.UserInterface.GetSprite("ButRD");

        public Button(string text, Rectangle rect, Color color) {

            Text = text;
            Bound = rect;
            ControlColor = color;
            TextColor = Color.Black;

        }

        public override void OnDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (MouseState == MouseStats.Over || MouseState == MouseStats.None)
            {
                // Draw button body.
                DrawSprite(spriteBatch, ButtonLeft, new Rectangle(0, 0, 64, 64), ControlColor, gameTime);
                DrawSprite(spriteBatch, ButtonMid, new Rectangle(64, 0, Bound.Width - 128, 64), ControlColor, gameTime);
                DrawSprite(spriteBatch, ButtonRight, new Rectangle(Bound.Width - 64, 0, 64, 64), ControlColor, gameTime);
            }
            else
            {
                DrawSprite(spriteBatch, ButtonLeftDown, new Rectangle(0, 0, 64, 64), ControlColor, gameTime);
                DrawSprite(spriteBatch, ButtonMidDown, new Rectangle(64, 0, Bound.Width - 128, 64), ControlColor, gameTime);
                DrawSprite(spriteBatch, ButtonRightDown, new Rectangle(Bound.Width - 64, 0, 64, 64), ControlColor, gameTime);
            }

            DrawText(spriteBatch, Rise.Engine.ressourceManager.GetSpriteFont("Engine", "segoeUI_16pt"),
                     Text, new Rectangle(0, 0, Bound.Width, Bound.Height),
                     TextColor, Alignment.Center, Style.Regular);
        }
    }
}
