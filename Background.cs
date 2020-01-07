using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XWangFinalProject
{
    class Background : DrawableGameComponent
    {
        const int BKGD_DRAWORDER = 0;

        static Texture2D texture;
        Vector2 velocity;

        Vector2 position = Vector2.Zero;

        List<Rectangle> textureRects;

        public static Texture2D Texture { get => texture; set => texture = value; }

        public Background(Game game, Texture2D texture, Vector2 velocity) : base(game)
        {
            Texture = texture;
            this.velocity = velocity;

            DrawOrder = BKGD_DRAWORDER;

            textureRects = CalculateBackgroundRectangleList();
        }

        /// <summary>
        /// to move the background
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            Rectangle firstRect1 = textureRects[0];
            if (firstRect1.Top == 0)
            {
                Rectangle lastRect = textureRects[textureRects.Count - 1];
                textureRects.Remove(lastRect);
                lastRect.Y = -lastRect.Height;
                textureRects.Insert(0, lastRect);
            }

            for (int i = 0; i < textureRects.Count; i++)
            {
                Rectangle rect = textureRects[i];
                rect.Location -= velocity.ToPoint();
                textureRects[i] = rect;
            }

            Rectangle firstRect2 = textureRects[0];
            if (firstRect2.Bottom <= 0)
            {
                textureRects.RemoveAt(0);
                Rectangle lastRect = textureRects[textureRects.Count - 1];
                firstRect2.Y = lastRect.Bottom;

                // add it to the back of the list 
                textureRects.Add(firstRect2);
            }
            
            base.Update(gameTime);
        }

        /// <summary>
        /// to draw all rectangles to screen
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            // iterate through all needed rectangles and draw them
            // to the screen
            foreach (Rectangle rect in textureRects)
            {
                sb.Draw(Texture, rect, Color.White);

            }
            sb.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// to calculate how many rectangles needed to make the background keep moving without blank
        /// </summary>
        /// <returns></returns>
        private List<Rectangle> CalculateBackgroundRectangleList()
        {
            List<Rectangle> neededRectangles = new List<Rectangle>();

            int rectangleCount = Game.GraphicsDevice.Viewport.Width / Texture.Width + 3;

            for (int i = 0; i < rectangleCount; i++)
            {
                neededRectangles.Add(new Rectangle(0, Texture.Height * i, Texture.Width, Texture.Height));
            }

            return neededRectangles;
        }
    }
}
