using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XWangFinalProject.Enums;
using XWangFinalProject.MenuContent;
using XWangFinalProject.PlayContent;

namespace XWangFinalProject.HUD_Items
{
    class Score : HudString
    {
        private int playerScore;
        public int PlayerScore { get => playerScore; }
        public Score(Game game, string fontName, HudLocation screenLocation) : base(game, fontName, screenLocation)
        {
            playerScore = 0;
            if (Game.Services.GetService<Score>() == null)
            {
                Game.Services.AddService<Score>(this);
            }
        }

        /// <summary>
        /// to update the score during playing
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            displayString = $"Score: {playerScore}";
            base.Update(gameTime);
        }

        /// <summary>
        /// to add the player's score 
        /// </summary>
        /// <param name="value">the adding value</param>
        public void AddPlayerScore(int value)
        {
            playerScore += value;
        }
    }
}
