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
    class EnemyShipType1 : EnemyShip
    {
        private int enemy1HP = 20;
        public int Enemy1HP { get => enemy1HP; }

        public EnemyShipType1(Game game, Texture2D texture, Vector2 position, Scene currentScene) : base(game, texture, position, currentScene)
        {
            this.velocity = new Vector2(0, 2);
        }

        /// <summary>
        /// to deduct the enemy_1's HP
        /// </summary>
        /// <param name="value">the value want to be deducted</param>
        public void SetEnemy1HP(int value)
        {
            enemy1HP -= value;
        }
    }
}
