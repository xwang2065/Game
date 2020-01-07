using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XWangFinalProject.Enums;
using XWangFinalProject.Scenes;

namespace XWangFinalProject
{
    class Button : DrawableGameComponent
    {
        protected Texture2D texure;
        public Texture2D Texture { get => texure; }

        protected Vector2 positon;
        public Vector2 Position { get => positon;}

        protected int buttonIndex;

        public Button(Game game, Texture2D texture, int buttonIndex) : base(game)
        {
            this.texure = texture;
            this.buttonIndex = buttonIndex;
        }

        /// <summary>
        /// to draw the bottons to screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();

            sb.Draw(texure, Position, Color.White);

            sb.End();

            base.Draw(gameTime);
        }

        protected Rectangle GetTextureRectangle()
        {
            Rectangle boundry = texure.Bounds;
            boundry.Location = positon.ToPoint();
            return boundry;
        }
    }
}

