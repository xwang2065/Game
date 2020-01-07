using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace XWangFinalProject
{
    public class BackgroundMusic : GameComponent
    {
        Song bkgdMusic;

        public BackgroundMusic(Game game, Song bkgdMusic) : base(game)
        {
            this.bkgdMusic = bkgdMusic;
        }

        /// <summary>
        /// to set mediaPlayer and play music
        /// </summary>
        public override void Initialize()
        {
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.5f;
            MediaPlayer.Play(bkgdMusic);

            base.Initialize();
        }
        
        /// <summary>
        /// to change the background music
        /// </summary>
        /// <param name="song">the song wanted to be played</param>
        public static void ChangBkgdMusic(Song song)
        {
            MediaPlayer.Play(song);
        }
    }
}
