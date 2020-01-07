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
    class PlayerBullet : Bullet
    {
        public PlayerBullet(Game game, Texture2D texture, Vector2 position, Scene currentScene) : base(game, texture, position, currentScene)
        {
            this.velocity = new Vector2(0, -8);
        }

        /// <summary>
        /// to check collision with enemy if the bullet is in the screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                if (position.X >= 0 && position.X <= GraphicsDevice.Viewport.Width - texture.Width
             && position.Y >= 0 && position.Y <= GraphicsDevice.Viewport.Height - texture.Height)
                {
                    CheckCollisionWithEnemys(gameTime);
                }
                else
                {
                    currentScene.RemoveComponent(this);
                }
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// to check if the bullet collision with enemys if the enemy is in screen
        /// </summary>
        protected void CheckCollisionWithEnemys(GameTime gameTime)
        {
            List<EnemyShip> enemyShips = new List<EnemyShip>();
            foreach (GameComponent g in Game.Components)
            {
                if (g is EnemyShip es)
                {
                    enemyShips.Add(es);
                }
            }

            for (int i = 0; i < enemyShips.Count; i++)
            {
                if ( enemyShips[i].Position.Y > 0)
                {
                    Rectangle enemyShipBoundry = enemyShips[i].Texture.Bounds;
                    enemyShipBoundry.Location = enemyShips[i].Position.ToPoint();

                    Rectangle boundry = this.texture.Bounds;
                    boundry.Location = this.position.ToPoint();

                    if (enemyShipBoundry.Intersects(boundry))
                    {
                        enemyShips[i].EnemyShipCollided(enemyShips[i]);

                        currentScene.RemoveComponent(this);
                    }
                }
            }
        }
    }
}
