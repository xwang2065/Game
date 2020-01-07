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
    class HighScoreScene : Scene
    {
        public bool IsHighScoreTextRemoved { get; set; } = false;

        public HighScoreScene(Game game) : base(game)
        {
        }

        /// <summary>
        /// to add the hud string and the high score content text
        /// </summary>
        public override void Initialize()
        {
            AddComponent(new HudString(Game, "RegularFont", HudLocation.TopLeft));
            AddComponent(new HighScoreSceneContentText(Game));
            base.Initialize();
        }

        /// <summary>
        /// to add high score text if the old one got removed(high score text got remove when high score list got updated
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                if (IsHighScoreTextRemoved)
                {
                    AddComponent(new HighScoreSceneContentText(Game));
                    IsHighScoreTextRemoved = false;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    ChangeToMenuScene();
                }
            }

            base.Update(gameTime);
        }
    }
}
