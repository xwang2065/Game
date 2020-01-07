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
    class MovingComponent : PlayComponent
    {
        protected Vector2 velocity;

        public MovingComponent(Game game, Texture2D texture, Vector2 position, Scene currentScene) : base(game, texture, currentScene)
        {
            this.position = position;
        }

        /// <summary>
        /// to keep the component moving in a certain velocity and remove the component if it moves out the screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            position += velocity;

            if(Position.Y >= GraphicsDevice.Viewport.Height)
            {
                currentScene.RemoveComponent(this);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// to check if collisoned with player
        /// </summary>
        /// <param name="typeName"></param>
        protected void CheckCollisionWithPlayer(string typeName)
        {
            Rectangle boundry = this.texture.Bounds;
            boundry.Location = this.position.ToPoint();

            PlayerShip player;
            Rectangle playerBoundry;
            foreach (GameComponent gc in Game.Components)
            {
                if (gc is PlayerShip p)
                {
                    player = p;
                    playerBoundry = player.Texture.Bounds;
                    playerBoundry.Location = player.Position.ToPoint();

                    if (playerBoundry.Intersects(boundry))
                    {
                        currentScene.RemoveComponent(this);
                        player.PlayerShipCollided(typeName);
                    }
                    break;
                }
            }
        }
    }
}
