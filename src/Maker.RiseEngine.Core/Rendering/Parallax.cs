﻿
using Maker.RiseEngine.Input;
using Maker.RiseEngine.Storage;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Globalization;

namespace Maker.RiseEngine.Rendering
{
    public class Parallax
    {

        ParallaxLayer[] Layers;
        public Rectangle DestinationRectangle;
        float[] LayersPos;

        public Parallax(ParallaxLayer[] layers, Rectangle destination)
        {

            DestinationRectangle = destination;
            Layers = layers;
            LayersPos = new float[layers.Length];

        }

        public void Update(GameInput playerInput, GameTime gameTime)
        {

            for (int i = 0; i < Layers.Length; i++)
            {

                LayersPos[i] = (LayersPos[i] + Layers[i].Speed);

                double Factor = (double)DestinationRectangle.Width / Layers[i].Sprite.Bounds.Width;

                if (LayersPos[i] > Layers[i].Sprite.Bounds.Width * Factor)
                {
                    LayersPos[i] = 0;
                }

            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            for (int i = 0; i < Layers.Length; i++)
            {
                double Factor = (double)DestinationRectangle.Width / Layers[i].Sprite.Bounds.Width;
                spriteBatch.Draw(Layers[i].Sprite, new Rectangle(DestinationRectangle.X + (int)LayersPos[i] - (int)(Layers[i].Sprite.Bounds.Width * Factor), DestinationRectangle.Y, DestinationRectangle.Width, DestinationRectangle.Height), Color.White);
                spriteBatch.Draw(Layers[i].Sprite, new Rectangle(DestinationRectangle.X + (int)LayersPos[i], DestinationRectangle.Y, DestinationRectangle.Width, DestinationRectangle.Height), Color.White);

            }


        }

    }

    public class ParallaxLayer
    {

        public float Speed;
        public Texture2D Sprite;

        public ParallaxLayer(Texture2D sprite, float speed)
        {

            Speed = speed;
            Sprite = sprite;

        }

    }

    public static class ParallaxParse
    {
        public static Parallax Parse(string pluginName, string name, Rectangle destination)
        {

            var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.NumberDecimalSeparator = ".";

            System.IO.StreamReader sr = new System.IO.StreamReader("Plugins\\" + pluginName + "\\assets\\images\\parallax\\" + name + ".rise");
            string f = sr.ReadToEnd().ToDosLineEnd();
            sr.Close();
            f = f.Replace(System.Environment.NewLine, "");
            f = f.Replace("\n", "");
            string[] Ls = f.Split(';');

            List<ParallaxLayer> paralaxeLayerList = new List<ParallaxLayer>();

            for (int i = 0; i < Ls.Length; i++)
            {

                string[] sub = Ls[i].Split(':');
                if (sub.Length == 2)
                    paralaxeLayerList.Add(new ParallaxLayer(Rise.Engine.ressourceManager.GetTexture2D(pluginName, "parallax\\" + name + "\\" + sub[0]), float.Parse(sub[1], culture)));
            }

            Parallax p = new Parallax(paralaxeLayerList.ToArray(), destination);
            return p;
        }

    }
}
