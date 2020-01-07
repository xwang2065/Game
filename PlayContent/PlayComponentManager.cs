using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XWangFinalProject.MenuContent;
using XWangFinalProject.PlayContent;
using XWangFinalProject.Scenes;

namespace XWangFinalProject.PlayContent
{
    class PlayComponentManager : GameComponent
    {
        protected double creationInterval;
        protected double timer = 0.0;

        protected Scene currentScene;

        public PlayComponentManager(Game game, Scene currentScene, double creationInterval) : base(game)
        {
            this.currentScene = currentScene;
            this.creationInterval = creationInterval;
        }
    }
}
