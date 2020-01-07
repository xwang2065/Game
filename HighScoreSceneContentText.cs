using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XWangFinalProject.Enums;
using XWangFinalProject.Scenes;

namespace XWangFinalProject
{
    class HighScoreSceneContentText : Text
    {
        SpriteFont regularFont;

        string titleText;

        public HighScoreSceneContentText(Game game) : base(game)
        {
        }

        /// <summary>
        /// to read file and set as the text in high score scene if the file exist
        /// to set tile text of the scene
        /// </summary>
        public override void Initialize()
        {
            StringBuilder textBuilder = new StringBuilder();
            if(((Game1)Game).highScoreList.Count > 0)
            {
                for(int i = 0; i < ((Game1)Game).highScoreList.Count; i++)
                {
                    if(i < 9)
                    {
                        textBuilder.AppendLine($"0{i + 1}   :   {((Game1)Game).highScoreList[i]}");
                    }
                    else
                    {
                        textBuilder.AppendLine($"{i + 1}   :   {((Game1)Game).highScoreList[i]}");

                    }
                }

                titleText = "High Score Top 10\r\n\r\n";
            }
            else
            {
                titleText = "No High Score Now!";
            }

            text = textBuilder.ToString();

            base.Initialize();
        }

        /// <summary>
        /// to draw the title and hight scores to screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sp = Game.Services.GetService<SpriteBatch>();

            sp.Begin();
            Vector2 titleTextSize = font.MeasureString(titleText);
            Vector2 textSize = font.MeasureString(text);

            sp.DrawString(font,
                          titleText,
                          new Vector2((GraphicsDevice.Viewport.Width - titleTextSize.X) / 2, 100),
                          Color.GreenYellow);
            sp.DrawString(font,
                        text,
                        new Vector2((GraphicsDevice.Viewport.Width - textSize.X) / 2, 200),
                        Color.White);
            sp.End();

            base.Draw(gameTime);
        }

        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("HilightFont");
            regularFont = Game.Content.Load<SpriteFont>("RegularFont");

            base.LoadContent();
        }
    }
}
