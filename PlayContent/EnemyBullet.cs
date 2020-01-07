using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XWangFinalProject.MenuContent;
using XWangFinalProject.PlayContent;
using XWangFinalProject.Scenes;

namespace XWangFinalProject.PlayContent
{
    class EnemyBullet : Bullet
    {
        public EnemyBullet(Game game, Texture2D texture, Vector2 position, Scene currentScene) : base(game, texture, position, currentScene)
        {
        }

        /// <summary>
        /// to check collision with player if enabled
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                CheckCollisionWithPlayer(this.GetType().Name);
            }

            base.Update(gameTime);
        }
    }
}
