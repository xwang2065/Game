using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XWangFinalProject.MenuContent;
using XWangFinalProject.PlayContent;
using XWangFinalProject.Scenes;

namespace XWangFinalProject.PlayContent
{
    class PlayerBulletManager : PlayComponentManager
    {
        PlayerShip player;

        public PlayerBulletManager(Game game, Scene currentScene, double creationInterval, PlayerShip player) : base(game, currentScene, creationInterval)
        {
            this.player = player;
        }

        /// <summary>
        /// to create a player bullet as the player shoot
        /// </summary>
        public override void Initialize()
        {
            Texture2D bullet = Game.Content.Load<Texture2D>("PlayerBullet");
            currentScene.AddComponent(new PlayerBullet(Game,
                                                            bullet,
                                                            new Vector2(player.Position.X + (player.Texture.Width - bullet.Width) / 2,
                                                                           player.Position.Y),
                                                            currentScene));
            base.Initialize();
        }

        /// <summary>
        /// to create continuous bullet when player enter space continuously
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalSeconds;
            if (timer >= creationInterval)
            {
                timer = 0;

                Texture2D bullet = Game.Content.Load<Texture2D>("PlayerBullet");
                currentScene.AddComponent(new PlayerBullet(Game,
                                                            bullet,
                                                            new Vector2(player.Position.X + (player.Texture.Width - bullet.Width) / 2,
                                                                           player.Position.Y),
                                                            currentScene));
            }

            base.Update(gameTime);
        }
    }
}
