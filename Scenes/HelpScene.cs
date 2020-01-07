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
    class HelpScene : Scene
    {
        public HelpScene(Game game) : base(game)
        {
        }

        /// <summary>
        /// to load the hud string and help content
        /// </summary>
        public override void Initialize()
        {
            AddComponent(new HudString(Game, "RegularFont", HudLocation.TopLeft));
            AddComponent(new HelpContent(Game));

            base.Initialize();
        }

        /// <summary>
        /// to change to menu scene if escape got pressed
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
