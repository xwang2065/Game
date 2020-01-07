using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XWangFinalProject.Enums;
using XWangFinalProject.HUD_Items;
using XWangFinalProject.MenuContent;
using XWangFinalProject.PlayContent;

namespace XWangFinalProject.Scenes 
{
    class PlayScene : Scene
    {
        public PlayScene(Game game) : base(game)
        {
        }

        /// <summary>
        /// to add enemys, the player ship and the hud strings
        /// </summary>
        public override void Initialize()
        {
            PlayerShip player = new PlayerShip(Game, Game.Content.Load<Texture2D>("spaceShip"), this);
            AddComponent(player);
            AddComponent(new EnemyShipManager(Game, this, 0.5));
            AddComponent(new Score(Game, "RegularFont", HudLocation.BottomRight));
            AddComponent(new HP(Game, "RegularFont", HudLocation.TopRight));
            AddComponent(new HudString(Game, "RegularFont", HudLocation.TopLeft));

            base.Initialize();
        }

        /// <summary>
        /// to change the background music and to the menu scene
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            if (Enabled)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    MediaPlayer.Stop();
                    BackgroundMusic.ChangBkgdMusic(((Game1)Game).bkgdMusic_NonPlay);
                    ChangeToMenuScene();
                }                
            }

            base.Update(gameTime);
        }
    }
}
