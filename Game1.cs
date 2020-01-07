using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.IO;
using XWangFinalProject.Enums;
using XWangFinalProject.Scenes;

namespace XWangFinalProject
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        public List<int> highScoreList = new List<int>(10);
        public string filename = "TopTenScore.txt";

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Song bkgdMusic_NonPlay;
        public Song bkgdMusic_Play;
        public Song bkgdMusic_Gameover;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 960;
            graphics.PreferredBackBufferWidth = 850;
            this.IsMouseVisible = true;

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            if (File.Exists(filename))
            {
                string[] fileLines = File.ReadAllLines(filename);

                foreach(string element in fileLines)
                {
                    highScoreList.Add(int.Parse(element));
                }
            }
            bkgdMusic_NonPlay = this.Content.Load<Song>("BkgdMusic1_NonPlayScene");
            bkgdMusic_Play = this.Content.Load<Song>("BkgdMusic2_PlayScene");
            bkgdMusic_Gameover = this.Content.Load<Song>("BkgdMusic3_GameOver");

            this.Components.Add(new Background(this, this.Content.Load<Texture2D>("Background"), new Vector2(0, -1)));
            this.Components.Add(new BackgroundMusic(this, bkgdMusic_NonPlay));

            MenuScene menuScene = new MenuScene(this);
            this.Components.Add(menuScene);
            Services.AddService<MenuScene>(menuScene);

            HelpScene helpScene = new HelpScene(this);
            this.Components.Add(helpScene);
            Services.AddService<HelpScene>(helpScene);

            HighScoreScene highScoreScene = new HighScoreScene(this);
            this.Components.Add(highScoreScene);
            Services.AddService<HighScoreScene>(highScoreScene);

            AboutScene aboutScene = new AboutScene(this);
            this.Components.Add(aboutScene);
            Services.AddService<AboutScene>(aboutScene);

            GameOverScene gameOverScene = new GameOverScene(this);
            this.Components.Add(gameOverScene);
            Services.AddService<GameOverScene>(gameOverScene);

            base.Initialize();

            // hide all then show our first scene
            // this has to be done after the initialize methods are called
            // on all our components
            HideAllScenes();
            menuScene.Show();
        }

        public void HideAllScenes()
        {
            foreach (GameComponent item in Components)
            {
                if (item is Scene s)
                {
                    s.Hide();
                }
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService<SpriteBatch>(spriteBatch);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        public void RemoveBackgroundMusic()
        {
            for (int i = 0; i < this.Components.Count; i++)
            {
                if (Components[i] is BackgroundMusic bkgdM)
                {
                    Components.Remove(bkgdM);
                }
            }
        }
    }
}
