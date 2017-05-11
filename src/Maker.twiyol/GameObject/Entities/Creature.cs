﻿
using Maker.RiseEngine.Core.Input;
using Maker.Twiyol.GameObject.Event;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Maker.Twiyol.GameObject.Entities
{
    public class Creature : Entity
    {
        public AI.AIbase IA { get; set; }

        public Creature(AI.AIbase _IA, string[] _SpriteVariant, int spriteSheetID, Vector2 _SpriteLocation) : base(_SpriteVariant, spriteSheetID, _SpriteLocation)
        {
            IA = _IA;
        }

        public override void Tick(GameObjectEventArgs e, GameTime gametime) { }

        public override void Update(GameObjectEventArgs e, GameInput playerInput, GameTime gametime)
        {
            IA.Tick(e, playerInput, gametime);
            IA.ExecuteAction(e, gametime);
        }

    }
}