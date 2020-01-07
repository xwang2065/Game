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
    class HelpContent : DrawableGameComponent
    {
        Texture2D texture;

        public HelpContent(Game game) : base(game)
        {
        }

        /// <summary>
        /// to load the help texture
        /// </summary>
        public override void Initialize()
        {
            texture = Game.Content.Load<Texture2D>("Help");
            base.Initialize();
        }

        /// <summary>
        /// to draw the texture to screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.Draw(texture,
                    new Vector2((GraphicsDevice.Viewport.Width - texture.Width) / 2,
                                GraphicsDevice.Viewport.Height - texture.Height),
                    Color.White);

            sb.End();

            base.Draw(gameTime);
        }
    }
}
