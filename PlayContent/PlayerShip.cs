using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XWangFinalProject.GameOverContent;
using XWangFinalProject.HUD_Items;
using XWangFinalProject.MenuContent;
using XWangFinalProject.Scenes;

namespace XWangFinalProject.PlayContent
{
    class PlayerShip : PlayComponent
    {
        const int PLAYER_DISPLACEMENT = 10;

        const int TILE_SIZE = 100;
        const int TILE_ROW_COUNT = 5;
        const int TILE_COL_COUNT = 10;
        const double FRAME_DURATION = 0.1;

        bool ShootOrNot = true;

        bool isAlive = true;
        double dieingTimer = 0.0;
        const double DIEING_DURATION = 1.0;

        Texture2D activeTexture;

        SoundEffect playerExplosionSound;
        Texture2D explodeTexture;
        Rectangle sourceRect;

        double frameTimer = 0.0;
        int currentFrame = 0;

        public PlayerShip(Game game, Texture2D texture, Scene currentScene) : base(game, texture, currentScene)
        {
            activeTexture = texture;
        }

        /// <summary>
        /// to load explosion texture and sound, and set the start positon
        /// </summary>
        protected override void LoadContent()
        {
            playerExplosionSound = Game.Content.Load<SoundEffect>("PlayerExplosionSound");

            explodeTexture = Game.Content.Load<Texture2D>("explosion1");

            this.position = new Vector2((GraphicsDevice.Viewport.Width - texture.Width) / 2,
                                         GraphicsDevice.Viewport.Height - texture.Height);
            sourceRect = new Rectangle(0, 0, texture.Width, texture.Height);

            base.LoadContent();
        }

        /// <summary>
        /// to follow the keys to move or shoot if alive
        /// to change to the explosion status if not
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            ShootBullet(gameTime);

            if (isAlive)
            {
                UpdatePositionAsPressDirectionKeys();
            }

            if (!isAlive)
            {
                activeTexture = explodeTexture;

                sourceRect.Y = TILE_SIZE * 2;
                frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (frameTimer >= FRAME_DURATION)
                {
                    frameTimer = 0;

                    if (TILE_COL_COUNT <= ++currentFrame)
                    {
                        sourceRect.Y += TILE_SIZE;
                        currentFrame = 0;
                    }
                    if (sourceRect.Y == TILE_SIZE * (TILE_ROW_COUNT - 2))
                    {
                        currentScene.RemoveComponent(this);
                    }
                    sourceRect.X = TILE_SIZE * currentFrame;
                }

                dieingTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if (dieingTimer >= DIEING_DURATION)
                {
                    ChangeToGameOverScene();
                }               
            }

            base.Update(gameTime);
        }


        /// <summary>
        /// to draw the active texture to screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();

            sb.Draw(activeTexture,
                    position,
                    sourceRect,
                    Color.White,
                    0f,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0f);

            sb.End();
        }

        /// <summary>
        /// to shoot bullet if space key got pressed
        /// </summary>
        /// <param name="gameTime"></param>
        private void ShootBullet(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Space) && ShootOrNot && isAlive)
            {
                ShootOrNot = false;

                PlayerBulletManager playerBullet = new PlayerBulletManager(Game, currentScene, 0.2, this);
                currentScene.AddComponent(playerBullet);
            }
            if (ks.IsKeyUp(Keys.Space) && !ShootOrNot)
            {
                ShootOrNot = true;

                for (int i = 0; i < Game.Components.Count; i++)
                {
                    if (Game.Components[i] is PlayerBulletManager pb)
                    {
                        currentScene.RemoveComponent(pb);
                    }
                }
            }
        }

        /// <summary>
        /// to move the player ship if the direction keys got pressed
        /// </summary>
        private void UpdatePositionAsPressDirectionKeys()
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Up) || ks.IsKeyDown(Keys.W))
            {
                position.Y -= PLAYER_DISPLACEMENT;
            }
            else if (ks.IsKeyDown(Keys.Down) || ks.IsKeyDown(Keys.S))
            {
                position.Y += PLAYER_DISPLACEMENT;
            }

            if (ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D))
            {
                position.X += PLAYER_DISPLACEMENT;
            }
            else if (ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.A))
            {
                position.X -= PLAYER_DISPLACEMENT;
            }

            position.X = MathHelper.Clamp(Position.X, 0, GraphicsDevice.Viewport.Width - texture.Width);
            position.Y = MathHelper.Clamp(Position.Y, 0, GraphicsDevice.Viewport.Height - texture.Height);
        }

        /// <summary>
        /// to deduct HP if player ship got collied and change to not alive if HP is 0
        /// </summary>
        /// <param name="typeName"></param>
        internal void PlayerShipCollided(string typeName)
        {
            HP hp = Game.Services.GetService<HP>();
            if (isAlive)
            {
                if (typeName == "EnemyBulletType1" || typeName == "EnemyShipType2")
                {
                    hp.DecreasePlayerHP(10);
                }
                if (typeName == "EnemyBulletType2" || typeName == "EnemyShipType1")
                {
                    hp.DecreasePlayerHP(20);
                }

                if (hp.PlayerDiedOrNot())
                {
                    playerExplosionSound.Play();
                    isAlive = false;

                    for (int i = 0; i < Game.Components.Count; i++)
                    {
                        if (Game.Components[i] is PlayerBulletManager pb)
                        {
                            currentScene.RemoveComponent(pb);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// to add score if enemys got shooted
        /// </summary>
        /// <param name="name"></param>
        internal void GetScore(string name)
        {
            Score score = Game.Services.GetService<Score>();
            if (name == "EnemyShipType1")
            {
                score.AddPlayerScore(20);
            }
            if (name == "EnemyShipType2")
            {
                score.AddPlayerScore(10);
            }
        }

        /// <summary>
        /// to change to game over scene after player died
        /// </summary>
        private void ChangeToGameOverScene()
        {
            MediaPlayer.Stop();
            Game.Components.Add(new BackgroundMusic(Game, ((Game1)Game).bkgdMusic_Gameover));

            ((Game1)Game).HideAllScenes();

            Score score = Game.Services.GetService<Score>();
            GameOverText gameOverText = Game.Services.GetService<GameOverText>();
            gameOverText.SetHighScoreList(score.PlayerScore);
            gameOverText.SetFinalScore(score.PlayerScore);

            GameOverScene gameOverScene = Game.Services.GetService<GameOverScene>();
            if (gameOverScene.SelectedIndex > 4)
            {
                Scene.ButtonTextList[gameOverScene.SelectedIndex].ActiveColor = Color.Black;
                gameOverScene.SelectedIndex = -1;
            }
            gameOverScene.Show();
        }
    }
}
