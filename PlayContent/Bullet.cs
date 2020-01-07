using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using XWangFinalProject.MenuContent;
using XWangFinalProject.PlayContent;
using XWangFinalProject.Scenes;

namespace XWangFinalProject.PlayContent
{
    class Bullet : MovingComponent
    {
        public Bullet(Game game, Texture2D texture, Vector2 position, Scene currentScene) : base(game, texture, position, currentScene)
        {
            this.velocity = new Vector2(0, 8);
        }

    }
}
