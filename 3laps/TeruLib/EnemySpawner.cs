using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using Microsoft.Xna.Framework;

namespace OssanAndUmbrella
{
    public class EnemySpawner
    {   
        public void SpawnEnemy(bool right, int type)
        {
            int pos;
            if (right)
            {
                pos = 640;
            }
            else
            {
                pos = -640;
            }
            if (type == 0)
            {
                GameManager.instance.nowScene.gameObjectManager.Instantiate(new WalkEnemy(new Transform(new Vector2(Player.instance.transform.position.X + pos, Player.instance.transform.position.Y))));
            }
            else if (type == 1)
            {
                GameManager.instance.nowScene.gameObjectManager.Instantiate(new SpireEnemy(new Transform(new Vector2(Player.instance.transform.position.X + pos, Player.instance.transform.position.Y))));
            }
            else
            {
                GameManager.instance.nowScene.gameObjectManager.Instantiate(new DashEnemy(new Transform(new Vector2(Player.instance.transform.position.X + pos, Player.instance.transform.position.Y))));

            }
        }
    }
}
