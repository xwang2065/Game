using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XWangFinalProject.Enums;
using XWangFinalProject.Scenes;

namespace XWangFinalProject
{
    class AboutContentText : Text
    {
        public AboutContentText(Game game) : base(game)
        {
        }

        /// <summary>
        /// to load the sprite font
        /// </summary>
        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("HilightFont");

            base.LoadContent();
        }

        /// <summary>
        /// to draw the about text to screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sp = Game.Services.GetService<SpriteBatch>();

            sp.Begin();
            text = "    Created by\r\nXiaoyang Wang";
            Vector2 textSize = font.MeasureString(text);


            sp.DrawString(font,
                                   text,
                                   new Vector2((GraphicsDevice.Viewport.Width - textSize.X) / 2, (GraphicsDevice.Viewport.Height - textSize.Y) / 2),
                                   Color.GreenYellow);
            sp.End();

            base.Draw(gameTime);
        }
    }
}
