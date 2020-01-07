using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XWangFinalProject.Enums;
using XWangFinalProject.GameOverContent;
using XWangFinalProject.MenuContent;

namespace XWangFinalProject.Scenes
{
    class GameOverScene : Scene
    {
        public bool changSceneUsingKeaboard = false;
        private KeyboardState oldState;

        public GameOverScene(Game game) : base(game)
        {
        }

        /// <summary>
        /// to load the buttons and button text
        /// </summary>
        public override void Initialize()
        {
            AddComponent(new GameOverText(Game));

            for (int i = FIRST_GAMEOVER_BUTTON_INDEX; i < Enum.GetNames(typeof(MenuItem)).Length; i++)
            {
                GameOverButton b = new GameOverButton(Game, Game.Content.Load<Texture2D>("Button"), i, this);
                this.AddComponent(b);

                GameOverButtonText bt = new GameOverButtonText(Game, b, (MenuItem)i, this);
                buttonTextList.Add(bt);
                this.AddComponent(bt);
            }

            this.Show();

            base.Initialize();
        }

        /// <summary>
        /// to change the bkgd music and change to menu scene if escape pressed
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                KeyboardState ks = Keyboard.GetState();

                if (ks.IsKeyDown(Keys.Escape))
                {
                    MediaPlayer.Stop();
                    BackgroundMusic.ChangBkgdMusic(((Game1)Game).bkgdMusic_NonPlay);

                    ChangeToMenuScene();
                }

                if (ks.IsKeyDown(Keys.Right) && oldState.IsKeyUp(Keys.Right))
                {
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = FIRST_GAMEOVER_BUTTON_INDEX;
                    }
                    else
                    {
                        buttonTextList[SelectedIndex++].ActiveColor = regularColor;
                    }

                    if (SelectedIndex == buttonTextList.Count)
                    {
                        SelectedIndex = FIRST_GAMEOVER_BUTTON_INDEX;
                    }
                    ButtonTextList[SelectedIndex].ActiveColor = hilightColor;
                    changSceneUsingKeaboard = true;
                }
                if (ks.IsKeyDown(Keys.Left) && oldState.IsKeyUp(Keys.Left))
                {
                    if (SelectedIndex == -1)
                    {
                        SelectedIndex = buttonTextList.Count - 1;
                    }
                    else
                    {
                        buttonTextList[SelectedIndex--].ActiveColor = regularColor;
                    }
                    if (SelectedIndex == FIRST_GAMEOVER_BUTTON_INDEX - 1)
                    {
                        SelectedIndex = buttonTextList.Count - 1;
                    }
                    ButtonTextList[SelectedIndex].ActiveColor = hilightColor;
                    changSceneUsingKeaboard = true;
                }
                oldState = ks;

                if (ks.IsKeyDown(Keys.Enter) && SelectedIndex != -1)
                {
                    changSceneUsingKeaboard = false;
                }

                base.Update(gameTime);
            }
        }
    }
}
