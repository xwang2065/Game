using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XWangFinalProject.Enums;
using XWangFinalProject.Scenes;

namespace XWangFinalProject.GameOverContent
{
    class GameOverText : Text
    {
        int finalScore;
        string congratsText = "";
        public GameOverText(Game game) : base(game)
        {
            if (Game.Services.GetService<GameOverText>() == null)
            {
                Game.Services.AddService<GameOverText>(this);
            }
        }

        /// <summary>
        /// to load the SpritFont
        /// </summary>
        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("HilightFont");

            base.LoadContent();
        }

        /// <summary>
        /// to show game over and player's score in game over scene
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sp = Game.Services.GetService<SpriteBatch>();

            sp.Begin();

            text = $"Game Over!\r\nYour Score: {finalScore}";
            Vector2 textSize = font.MeasureString(text);

            sp.DrawString(font,
                        text,
                        new Vector2((GraphicsDevice.Viewport.Width - textSize.X) / 2, GraphicsDevice.Viewport.Height / 2 - textSize.Y * 2),
                        Color.White);

            if (congratsText != "")
            {
                Vector2 congratsTextSize = font.MeasureString(congratsText);

                sp.DrawString(font,
                           congratsText,
                           new Vector2((GraphicsDevice.Viewport.Width - congratsTextSize.X) / 2, GraphicsDevice.Viewport.Height / 2 + textSize.Y),
                           Color.White);
            }
            sp.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// to set the final score the player got
        /// </summary>
        /// <param name="value"></param>
        public void SetFinalScore(int value)
        {
            finalScore = value;
        }

        /// <summary>
        /// to update the high score list and save to file if the final score is get into top 10 of the high score list
        /// </summary>
        /// <param name="value"></param>
        public void SetHighScoreList(int value)
        {
            if (((Game1)Game).highScoreList.Count < 10)
            {
                ((Game1)Game).highScoreList.Add(value);
                SortListDescending();
                SetCongratsTextOnGameOverScene(((Game1)Game).highScoreList.IndexOf(value) + 1);
                SaveListToFile();
                RenewHighScoreScene();
            }
            else if(value > ((Game1)Game).highScoreList[((Game1)Game).highScoreList.Count - 1])
            {
                ((Game1)Game).highScoreList.RemoveAt(((Game1)Game).highScoreList.Count - 1);
                ((Game1)Game).highScoreList.Add(value);
                SortListDescending();
                SetCongratsTextOnGameOverScene(((Game1)Game).highScoreList.IndexOf(value) + 1);
                SaveListToFile();
                RenewHighScoreScene();
            }
            else
            {
                SetCongratsTextOnGameOverScene(0);
            }
        }

        /// <summary>
        /// to set a congrats text with the place of the final score in the high score list if the final score is get in top 10
        /// </summary>
        /// <param name="v"></param>
        private void SetCongratsTextOnGameOverScene(int v)
        {
            if (v == 0)
            {
                congratsText = "";
            }
            else
            {
                string text = "";
                if (v == 1)
                {
                    text = "1st";
                }
                else if(v == 2)
                {
                    text = "2nd";
                }
                else if(v == 3)
                {
                    text = "3rd";
                }
                else
                {
                    text = $"{v}th";
                }
                congratsText = $"Congratulations!\r\nYou won the {text} place!";
            }
        }

        /// <summary>
        /// to renew the high score scene
        /// </summary>
        private void RenewHighScoreScene()
        {
            HighScoreScene highScoreScene = Game.Services.GetService<HighScoreScene>();

            for (int i = 0; i < Game.Components.Count; i++)
            {
                if(Game.Components[i] is HighScoreSceneContentText ht)
                {
                    highScoreScene.RemoveComponent(ht);
                    highScoreScene.IsHighScoreTextRemoved = true;
                    break;
                }
            }
        }

        /// <summary>
        /// to save the high score list if it got changed
        /// </summary>
        private void SaveListToFile()
        {
            using(StreamWriter writer = new StreamWriter(((Game1)Game).filename))
            {
                foreach(int score in ((Game1)Game).highScoreList)
                {
                    writer.WriteLine(score);
                }
            }
        }

        /// <summary>
        /// to sort the high score list in descending order
        /// </summary>
        private void SortListDescending()
        {
            ((Game1)Game).highScoreList.Sort();
            ((Game1)Game).highScoreList.Reverse();
        }
    }
}
