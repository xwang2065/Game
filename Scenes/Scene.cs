using System;
using System.Collections.Generic;
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
    class Scene : GameComponent
    {
        public int SelectedIndex { get; set; } = -1;

        public const int FIRST_GAMEOVER_BUTTON_INDEX = 5;

        protected Color regularColor = Color.Black;
        protected Color hilightColor = Color.GreenYellow;

        List<GameComponent> SceneComponents { get; set; }

        protected static List<ButtonText> buttonTextList = new List<ButtonText>();
        internal static List<ButtonText> ButtonTextList { get => buttonTextList; }

        public Scene(Game game) : base(game)
        {
            SceneComponents = new List<GameComponent>();
        }

        /// <summary>
        /// to add component to both the game and the SceneComponents 
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent(GameComponent component)
        {
            this.SceneComponents.Add(component);
            Game.Components.Add(component);
        }

        /// <summary>
        /// to remove the component from both the game components and the SceneComponents list
        /// </summary>
        /// <param name="component"></param>
        public void RemoveComponent(GameComponent component)
        {
            this.SceneComponents.Remove(component);
            Game.Components.Remove(component);
        }

        /// <summary>
        /// to show the wanted scene and make its drawable component visible
        /// </summary>
        public virtual void Show()
        {
            this.Enabled = true;
            foreach (GameComponent component in SceneComponents)
            {
                component.Enabled = true;
                if (component is DrawableGameComponent dgc)
                {
                    dgc.Visible = true;
                }
            }
        }

        /// <summary>
        /// to hide the wanted scene and make the drawable components hidden
        /// </summary>
        public virtual void Hide()
        {
            this.Enabled = false;
            foreach (GameComponent component in SceneComponents)
            {
                component.Enabled = false;
                if (component is DrawableGameComponent dgc)
                {
                    dgc.Visible = false;
                }
            }
        }

        /// <summary>
        /// to change to menu scene
        /// </summary>
        public void ChangeToMenuScene()
        {
            ((Game1)Game).HideAllScenes();
                
            RemovePlayContentAndPlayScene();

            MenuScene menuScene = Game.Services.GetService<MenuScene>();
            if (menuScene.SelectedIndex > -1)
            {
                Scene.ButtonTextList[menuScene.SelectedIndex].ActiveColor = Color.Black;
                menuScene.SelectedIndex = -1;
            }

            menuScene.Show();
        }

        /// <summary>
        /// to change to the play scene
        /// </summary>
        public void ChangeToPlayScene()
        {
            PlayScene playScene = new PlayScene(Game);
            Game.Components.Add(playScene);
            Game.Services.AddService<PlayScene>(playScene);
            playScene.Show();
        }

        /// <summary>
        /// to remove the play scene and content on the scene
        /// </summary>
        public void RemovePlayContentAndPlayScene()
        {
            for (int i = 0; i < Game.Components.Count; i++)
            {
                if (Game.Components[i] is PlayComponent pc)
                {
                    RemoveComponent(pc);
                }
            }

            HP hp = Game.Services.GetService<HP>();
            if (hp != null)
            {
                RemoveComponent(hp);
                Game.Services.RemoveService(hp.GetType());
            }

            Score score = Game.Services.GetService<Score>();
            if (score != null)
            {
                RemoveComponent(score);
                Game.Services.RemoveService(score.GetType());
            }

            PlayScene playScene = Game.Services.GetService<PlayScene>();
            if (playScene != null)
            {
                Game.Components.Remove(playScene);
                Game.Services.RemoveService(playScene.GetType());
            }

        }

        /// <summary>
        /// to change scene according to the selectIndex
        /// </summary>
        public void ChangeScene()
        {
            switch (SelectedIndex)
            {
                case 0:
                    MediaPlayer.Stop();
                    BackgroundMusic.ChangBkgdMusic(((Game1)Game).bkgdMusic_Play);
                    ChangeToPlayScene();
                    break;
                case 1:
                    Game.Services.GetService<HelpScene>().Show();
                    break;
                case 2:
                    Game.Services.GetService<HighScoreScene>().Show();
                    break;
                case 3:
                    Game.Services.GetService<AboutScene>().Show();
                    break;
                case 4:
                    Game.Exit();
                    break;
                case 5:
                    MediaPlayer.Stop();
                    BackgroundMusic.ChangBkgdMusic(((Game1)Game).bkgdMusic_Play);
                    ChangeToPlayScene();
                    break;
                case 6:
                    MediaPlayer.Stop();
                    BackgroundMusic.ChangBkgdMusic(((Game1)Game).bkgdMusic_NonPlay);
                    ChangeToMenuScene();
                    break;
                    //default:
                    //    MediaPlayer.Stop();
                    //    BackgroundMusic.ChangBkgdMusic(((Game1)Game).bkgdMusic_NonPlay);
                    //    Game.Services.GetService<MenuScene>().Show();
                    //    break;
            }
        }
    }
}
