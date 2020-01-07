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
    class EnemyShipManager : PlayComponentManager
    {
        readonly List<Texture2D> enemyShipTextures = new List<Texture2D>();
        readonly List<Texture2D> bulletTexures = new List<Texture2D>();
        readonly Random random = new Random();
        int randomIndex = 0;

        public EnemyShipManager(Game game, Scene currentScene, double creationInterval) : base(game,currentScene, creationInterval)
        {
            enemyShipTextures.Clear();
            enemyShipTextures.Add(Game.Content.Load<Texture2D>("Enemy_1"));
            enemyShipTextures.Add(Game.Content.Load<Texture2D>("Enemy_2"));

            bulletTexures.Clear();
            bulletTexures.Add(Game.Content.Load<Texture2D>("Bullet_1"));
            bulletTexures.Add(Game.Content.Load<Texture2D>("Bullet_2"));
        }

        /// <summary>
        /// to generate random enemy ship type in random x location
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalSeconds;
            if (timer >= creationInterval)
            {
                timer = 0;

                randomIndex = GetRandomEnemyIndex();
                Texture2D shipTexture = enemyShipTextures[randomIndex];
                Vector2 location = GetRandomLocation();

                EnemyShip enemyShip;

                switch (randomIndex)
                {
                    case 0:
                        enemyShip = new EnemyShipType1(Game, shipTexture, location, currentScene);
                        currentScene.AddComponent((EnemyShipType1)enemyShip);
                        currentScene.AddComponent(new EnemyBulletType1Manager(Game, currentScene, 1.0, (EnemyShipType1)enemyShip));
                        break;
                    case 1:
                        enemyShip = new EnemyShipType2(Game, shipTexture, location, currentScene);
                        currentScene.AddComponent((EnemyShipType2)enemyShip);
                        Texture2D bullet2 = Game.Content.Load<Texture2D>("Bullet_2");
                        currentScene.AddComponent(new EnemyBulletType2(Game,
                                                                       bullet2,
                                                                       new Vector2(enemyShip.Position.X + enemyShip.Texture.Width / 2 - bullet2.Width / 2,
                                                                           enemyShip.Position.Y + enemyShip.Texture.Height),
                                                                       currentScene));
                        break;
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// to get random x location
        /// </summary>
        /// <returns></returns>
        public virtual Vector2 GetRandomLocation()
        {
            int randomX = random.Next(0, Game.GraphicsDevice.Viewport.Width - enemyShipTextures[randomIndex].Width);
            return new Vector2(randomX, 0);
        }

        /// <summary>
        /// to get random enemy type index
        /// </summary>
        /// <returns></returns>
        protected int GetRandomEnemyIndex()
        {
            return random.Next(0, enemyShipTextures.Count);
        }
    }
}