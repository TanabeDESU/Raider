using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GameEngine;

namespace GameEngine
{
    class TestScene : Scene
    {
        public override Scene Clone()
        {
            return null;
        }

        public override Scene Create()
        {
            return new TestScene();
        }

        public override void Load()
        {
            gameObjectManager = new GameObjectManager();
#if LevelDesign
#else
            nowMap = new Map("Map.csv", 64);
#endif
            gameObjectManager.Instantiate(new Camera(new Transform(new Vector2(0, 31)), 1, true));
            gameObjectManager.Instantiate(new Player(new Transform()));
        }

        public override void UnLoad()
        {
        }

        public override void Update()
        {
        }
    }
}
