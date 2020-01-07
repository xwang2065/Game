using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XWangFinalProject.Enums;
using XWangFinalProject.MenuContent;

namespace XWangFinalProject.Scenes
{
    class MenuScene : Scene
    {
        public bool changSceneUsingKeaboard = false;
        private KeyboardState oldState;

        public MenuScene(Game game) : base(game)
        {
        }

        /// <summary>
        /// to load the menu buttons and the button text
        /// </summary>
        public override void Initialize()
        {
            for (int i = 0; i < FIRST_GAMEOVER_BUTTON_INDEX; i++)
            {
                MenuButton b = new MenuButton(Game, Game.Content.Load<Texture2D>("Button"), 95.0f, i, this);
                this.AddComponent(b);

                MenuButtonText bt = new MenuButtonText(Game, b, (MenuItem)i, this);
                buttonTextList.Add(bt);
                this.AddComponent(bt);
            }

            this.Show();

            base.Initialize();
        }

        /// <summary>
        /// to check the key board input then change the index accordingly
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                if (SelectedIndex != -1)
                {
                    buttonTextList[SelectedIndex++].ActiveColor = regularColor;
                }
                else
                {
                    SelectedIndex++;
                }

                if (SelectedIndex == FIRST_GAMEOVER_BUTTON_INDEX)
                {
                    SelectedIndex = 0;
                }
                ButtonTextList[SelectedIndex].ActiveColor = hilightColor;
                changSceneUsingKeaboard = true;
            }
            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                if (SelectedIndex != -1)
                {
                    buttonTextList[SelectedIndex--].ActiveColor = regularColor;
                }
                else
                {
                    SelectedIndex--;
                }
                if (SelectedIndex <= -1)
                {
                    SelectedIndex = FIRST_GAMEOVER_BUTTON_INDEX - 1;
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

