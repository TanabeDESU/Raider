using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using Microsoft.Xna.Framework;


namespace OssanAndUmbrella
{   
    public class SampleScene : Scene
    {

        private float time, maxSpawnDelayTime = 300;
        public int range = 1;
        EnemySpawner spawner = new EnemySpawner();
        public SampleScene()
        {   
            gameObjectManager = new GameObjectManager();
            
        }

        public override void Load()
        {
            map = new Map("Map1.csv", 64);
            gameObjectManager.Instantiate(new Player());
            if (Player.instance != null && !flg)
            {
                Transform a = new Transform(new Vector2(Player.instance.transform.position.X - 640, Player.instance.transform.position.Y - 500));
                gameObjectManager.Instantiate(new Camera(a, 1));
            }
            
            
        }

        public override void UnLoad()
        {
            
        }
        bool flg = false;
        float time2 = 0;
        Random random = new Random();
        public static int enemyCount = 0;
        public override void Update()
        {   
            if(Player.instance.isDead == true)
            {
                GameManager.instance.ChangeScene(new SampleScene());
            }
            if(enemyCount > 10)
            {
                enemyCount = 0;
                GameManager.instance.ChangeScene(new SampleScene());

            }
            time++;
            if(time > 300)
            {
                time = 0;
                spawner.SpawnEnemy(true, random.Next(0, 3));
            }
            if(time == 2)
            {
                spawner.SpawnEnemy(false, random.Next(0, 3));
            }
        }
    }
}
