using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XWangFinalProject.Enums;
using XWangFinalProject.Scenes;

namespace XWangFinalProject.GameOverContent
{
    class GameOverButton : Button
    {
        GameOverScene currentScene;

        public GameOverButton(Game game, Texture2D texture, int buttonIndex, GameOverScene currentScene) 
            : base(game, texture, buttonIndex)
        {
            if (buttonIndex == 5)
            {
                positon.X = (GraphicsDevice.Viewport.Width / 2 - texture.Width) / 2;
            }
            else
            {
                positon.X = (3 *GraphicsDevice.Viewport.Width / 2 - texture.Width) / 2;
            }
            positon.Y = (GraphicsDevice.Viewport.Height - texture.Height) / 2;
            this.currentScene = currentScene;
        }

        /// <summary>
        /// to check if the mouse clicked on the button, and change the scene if so
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            MouseState ms = Mouse.GetState();

            if (ms.LeftButton == ButtonState.Pressed)
            {
                if (GetTextureRectangle().Contains(ms.Position))
                {
                    currentScene.SelectedIndex = this.buttonIndex;
                }
            }
            if (ms.LeftButton == ButtonState.Released 
                && currentScene.SelectedIndex > 4
                && !currentScene.changSceneUsingKeaboard)
            {
                ((Game1)Game).HideAllScenes();
                currentScene.RemovePlayContentAndPlayScene();
                currentScene.ChangeScene();
            }
            base.Update(gameTime);
        }
    }
}
