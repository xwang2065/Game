using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using XWangFinalProject.Scenes;

namespace XWangFinalProject.PlayContent
{
    class EnemyBulletType1 : EnemyBullet
    {
        public EnemyBulletType1(Game game, Texture2D texture, Vector2 position, Scene currentScene) : base(game, texture, position, currentScene)
        {
        }
    }
}
