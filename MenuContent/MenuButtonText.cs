using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XWangFinalProject.Enums;
using XWangFinalProject.Scenes;

namespace XWangFinalProject.MenuContent
{
    class MenuButtonText : ButtonText
    {
        readonly MenuScene currentScene;

        public MenuButtonText(Game game, MenuButton button, MenuItem menuItem, MenuScene currentScene)
            : base(game, button, menuItem)
        {
            this.currentScene = currentScene;
        }

        /// <summary>
        /// to check if the button under this button text got clicked, change the font color if so
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                MouseState ms = Mouse.GetState();

                if (currentScene.SelectedIndex != -1/* && ms.LeftButton == ButtonState.Pressed*/)
                {
                    for (int i = 0; i < Scene.ButtonTextList.Count; i++)
                    {
                        if (i == currentScene.SelectedIndex)
                        {
                            Scene.ButtonTextList[i].ActiveColor = hilightColor;
                        }
                    }
                }



                //base.Update(gameTime);
            }
        }
    }
}
