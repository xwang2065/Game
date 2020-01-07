using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XWangFinalProject.Enums;
using XWangFinalProject.MenuContent;
using XWangFinalProject.Scenes;

namespace XWangFinalProject
{
    class ButtonText : Text
    {
        protected SpriteFont hilightFont;

        protected Color regularColor = Color.Black;
        protected Color hilightColor = Color.GreenYellow;

        protected readonly Button button;

        protected SpriteFont activeFont;
        public Color ActiveColor { get; set; }

        public ButtonText(Game game,
                          Button button,
                          MenuItem menuItem) : base(game)
        {
            this.button = button;
            if (menuItem == MenuItem.HighScore 
                || menuItem == MenuItem.MainMenu || menuItem == MenuItem.PlayAgain)
            {
                this.text = menuItem.ToString().Insert(4, " ");
            }
            else
            {
                this.text = menuItem.ToString();
            }
        }

        /// <summary>
        /// to load the two type of spriteFont and set the activeColor's default as regularColor
        /// </summary>
        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>("RegularFont");
            hilightFont = Game.Content.Load<SpriteFont>("HilightFont");

            ActiveColor = regularColor;

            base.LoadContent();
        }

        /// <summary>
        /// to draw the font to screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();

            if (this.ActiveColor == hilightColor)
            {
                activeFont = hilightFont;
            }
            else
            {
                activeFont = font;
            }
            Vector2 textSize = activeFont.MeasureString(text);
            this.position = new Vector2((button.Position.X + (button.Texture.Width - textSize.X) / 2),
                                        (button.Position.Y + (button.Texture.Height - textSize.Y) / 2));
            sb.DrawString(activeFont, text, position, ActiveColor);

            sb.End();

            base.Draw(gameTime);
        }
    }
}
