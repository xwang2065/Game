using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XWangFinalProject.Enums;
using XWangFinalProject.Scenes;

namespace XWangFinalProject
{
    class Text : DrawableGameComponent
    {
        protected SpriteFont font;
        protected string text;
        protected Vector2 position;

        public Text(Game game) : base(game)
        {
        }
    }
}
