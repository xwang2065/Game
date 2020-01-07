using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XWangFinalProject.Scenes;

namespace XWangFinalProject.PlayContent
{
    class EnemyBulletType1Manager : PlayComponentManager
    {
        public EnemyShipType1 enemyShip;

        public EnemyBulletType1Manager(Game game, Scene currentScene, double creationInterval, EnemyShipType1 enemyShip) : base(game, currentScene, creationInterval)
        {
            this.enemyShip = enemyShip;
        }

        /// <summary>
        /// to create bullet in certain time interval
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.TotalSeconds;
            if (timer >= creationInterval)
            {
                timer = 0;

                Texture2D bullet1 = Game.Content.Load<Texture2D>("Bullet_1");
                currentScene.AddComponent(new EnemyBulletType1(Game,
                                                               bullet1,
                                                               new Vector2(enemyShip.Position.X + (enemyShip.Texture.Width - bullet1.Width) / 2,
                                                                           enemyShip.Position.Y + enemyShip.Texture.Height),
                                                               currentScene));
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// to add the bullet as the manager created
        /// </summary>
        public override void Initialize()
        {
            Texture2D texture = Game.Content.Load<Texture2D>("Bullet_1");
            currentScene.AddComponent(new EnemyBulletType1(Game,
                                                            texture,
                                                            new Vector2(enemyShip.Position.X + (enemyShip.Texture.Width - texture.Width) / 2,
                                                                        enemyShip.Position.Y + enemyShip.Texture.Height),
                                                            currentScene));
            base.Initialize();
        }
    }
}
