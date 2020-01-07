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
    class HP : HudString
    {
        int hp;

        public HP(Game game, string fontName, HudLocation screenLocation) : base(game, fontName, screenLocation)
        {
            hp = 100;
            if (Game.Services.GetService<HP>() == null)
            {
                Game.Services.AddService<HP>(this);
            }
        }

        /// <summary>
        /// to update the HP during playing
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            displayString = $"HP: {hp}";
            base.Update(gameTime);
        }

        /// <summary>
        /// To deduct the player's HP
        /// </summary>
        /// <param name="value">the deducting value</param>
        public void DecreasePlayerHP(int value)
        {
            hp -= value;
            if (hp < 0)
            {
                hp = 0;
            }
        }

        /// <summary>
        /// to check if player is alive
        /// </summary>
        /// <returns></returns>
        public bool PlayerDiedOrNot()
        {
            return hp == 0;
        }
    }
}
