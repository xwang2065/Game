using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XWangFinalProject.Enums;
using XWangFinalProject.Scenes;

namespace XWangFinalProject.MenuContent
{
    class MenuButton : Button
    {
        MenuScene currentScene;
        public MenuButton(Game game, Texture2D texture, float buttonYOffset, int buttonIndex, MenuScene currentScene) 
            : base(game, texture, buttonIndex)
        {
            positon.X = (GraphicsDevice.Viewport.Width - texure.Width) / 2;
            positon.Y = buttonYOffset + (texure.Height + buttonYOffset) * buttonIndex;
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

            KeyboardState ks = Keyboard.GetState();

            if (ms.LeftButton == ButtonState.Released 
                && currentScene.SelectedIndex > -1
                &&!currentScene.changSceneUsingKeaboard)
            {
                ((Game1)Game).HideAllScenes();
                currentScene.RemovePlayContentAndPlayScene();
                currentScene.ChangeScene();
            }
            base.Update(gameTime);
        }
    }
}
