using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XWangFinalProject.Scenes;

namespace XWangFinalProject.PlayContent
{
    class PlayComponent : DrawableGameComponent
    {
        protected Texture2D texture;
        public Texture2D Texture { get => texture; }

        protected Vector2 position;
        public Vector2 Position { get => position;}

        protected Scene currentScene;

        public PlayComponent(Game game, Texture2D texture, Scene currentScene) : base(game)
        {
            this.texture = texture;
            this.currentScene = currentScene;
        }

        /// <summary>
        /// to draw to component to screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            sb.Draw(Texture, Position, Color.White);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
