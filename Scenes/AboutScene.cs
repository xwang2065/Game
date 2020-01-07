using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XWangFinalProject.Enums;
using XWangFinalProject.HUD_Items;
using XWangFinalProject.MenuContent;

namespace XWangFinalProject.Scenes
{
    class AboutScene : Scene
    {
        public AboutScene(Game game) : base(game)
        {
        }

        /// <summary>
        /// to load the content text and hud string
        /// </summary>
        public override void Initialize()
        {
            AddComponent(new HudString(Game, "RegularFont", HudLocation.TopLeft));
            AddComponent(new AboutContentText(Game));

            base.Initialize();
        }

        /// <summary>
        /// to change to menu scene if escape pressed
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    ChangeToMenuScene();
                }
            }

            base.Update(gameTime);
        }
    }
}
