using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XWangFinalProject.MenuContent;
using XWangFinalProject.PlayContent;
using XWangFinalProject.Scenes;

namespace XWangFinalProject.PlayContent
{
    class EnemyShip : MovingComponent
    {
        const double FRAME_DURATION = 0.1;
        const int TILE_SIZE = 100;
        const int TILE_ROW_COUNT = 5;
        const int TILE_COL_COUNT = 10;

        const float SPARK_SCALE = 1.2f;
        const int SPARK_TILE_SIZE = 56;
        const int SPARK_TILE_ROW_COUNT = 2;
        const int SPARK_TILE_COL_COUNT = 4;

        Texture2D activeTexture;
        Color activeColor;

        SoundEffect enemyExplosionSound;
        bool isAlive = true;

        Texture2D explosionTexture;
        Rectangle acriveTextureRect;
        double frameTimer = 0.0;
        int currentFrame = 0;

        bool addSpark = false;
        Texture2D sparkTexture;
        private Rectangle sparkRect;

        SoundEffect sparkSound;

        public EnemyShip(Game game, Texture2D texture, Vector2 position, Scene currentScene) : base(game, texture, position, currentScene)
        {
            activeTexture = texture;
            activeColor = Color.White;
        }

        /// <summary>
        /// to load the explosion texture, the explosion sound, sparkTexture and sparkSound
        /// and initialize hte rectangles of the textures
        /// </summary>
        protected override void LoadContent()
        {
            enemyExplosionSound = Game.Content.Load<SoundEffect>("EnemyExplosionSound");

            explosionTexture = Game.Content.Load<Texture2D>("explosion1");

            acriveTextureRect = new Rectangle(0, 0, texture.Width, texture.Height);

            sparkTexture = Game.Content.Load<Texture2D>("SparkSheet");
            sparkRect = new Rectangle(64, 0, 64 , 64);

            sparkSound = Game.Content.Load<SoundEffect>("Enemy_1HittedSound");
            base.LoadContent();
        }

        /// <summary>
        /// to check collision with the player if alive or change the active texture if not
        /// and to add spark texture if enemy_1 got first hit
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                if (isAlive)
                {
                    CheckCollisionWithPlayer(this.GetType().Name);
                }

                if (!isAlive)
                {
                    activeTexture = explosionTexture;
                    if (this is EnemyShipType1)
                    {
                        activeColor = Color.DarkSalmon;
                    }
                    else if (this is EnemyShipType2)
                    {
                        activeColor = Color.Green;
                    }

                    acriveTextureRect.Y = TILE_SIZE * 2;
                    frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
                    if (frameTimer >= FRAME_DURATION)
                    {
                        frameTimer = 0;

                        if (TILE_COL_COUNT <= ++currentFrame)
                        {
                            acriveTextureRect.Y += TILE_SIZE;
                            currentFrame = 0;
                        }
                        if (acriveTextureRect.Y == TILE_SIZE * (TILE_ROW_COUNT - 2))
                        {
                            currentScene.RemoveComponent(this);
                        }
                        acriveTextureRect.X = TILE_SIZE * currentFrame;
                    }
                }

                if (addSpark)
                {
                    frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
                    if (frameTimer >= FRAME_DURATION)
                    {
                        frameTimer = 0;

                        if (SPARK_TILE_COL_COUNT <= ++currentFrame)
                        {
                            sparkRect.Y += SPARK_TILE_SIZE;
                            currentFrame = 0;
                        }
                        if (sparkRect.Y == SPARK_TILE_SIZE * SPARK_TILE_ROW_COUNT)
                        {
                            addSpark = false;
                        }
                        sparkRect.X = SPARK_TILE_SIZE * currentFrame;
                    }
                }
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// to draw the active texture and spark texture if enemy_1 got first hit
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {

            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();

            sb.Draw(activeTexture,
                    position,
                    acriveTextureRect,
                    activeColor,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0f);

            if (addSpark)
            {
                sb.Draw(sparkTexture,
                        new Vector2(position.X + (texture.Width - SPARK_TILE_SIZE * SPARK_SCALE) / 2, 
                                    position.Y + (texture.Height - SPARK_TILE_SIZE * SPARK_SCALE) / 2),
                        sparkRect,
                        Color.White,
                        0f,
                        Vector2.Zero,
                        SPARK_SCALE,
                        SpriteEffects.None,
                        0f);
            }

            sb.End();
        }

        /// <summary>
        /// to deduct enemy_1's HP, if enmy's HP is 0, call EnemyShipDied
        /// or for enemy_2, directly call EnemyShipDied
        /// </summary>
        /// <param name="enemyShip"></param>
        internal void EnemyShipCollided(EnemyShip enemyShip)
        {
            if (isAlive)
            {
                if (enemyShip is EnemyShipType1 type1)
                {
                    type1.SetEnemy1HP(10);
                    if (type1.Enemy1HP > 0)
                    {
                        sparkSound.Play();
                        addSpark = true;
                    }
                    else if(type1.Enemy1HP == 0)
                    {
                        EnemyShipDied(type1);
                    }
                }
                else
                {
                    EnemyShipDied(enemyShip);
                }
            }
        }

        /// <summary>
        /// if enemy 1 died, remove its bullet manager to stop produce bullet from it
        /// and to add player's score
        /// </summary>
        /// <param name="enemyShip"></param>
        internal void EnemyShipDied(EnemyShip enemyShip)
        {
            isAlive = false;
            enemyExplosionSound.Play();

            if (enemyShip is EnemyShipType1)
            {
                for (int j = 0; j < Game.Components.Count; j++)
                {
                    if (Game.Components[j] is EnemyBulletType1Manager manager && manager.enemyShip == enemyShip)
                    {
                        currentScene.RemoveComponent(manager);
                    }
                }
            }

            PlayerShip player;
            foreach (GameComponent gc in Game.Components)
            {
                if (gc is PlayerShip p)
                {
                    player = p;
                    player.GetScore(enemyShip.GetType().Name);
                }
            }
        }
    }
}

