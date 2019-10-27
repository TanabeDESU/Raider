using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using Microsoft.Xna.Framework;

namespace OssanAndUmbrella
{
    class Title : Scene
    {
        public override void Load()
        {
            gameObjectManager = new GameObjectManager();
            gameObjectManager.Instantiate(new Image("Title"));
            Transform a = new Transform(new Vector2(640, 360));
            gameObjectManager.Instantiate(new Camera(a, 1));
        }

        public override void UnLoad()
        {
        }

        public override void Update()
        {
            if (Input.GetButtonDown(Microsoft.Xna.Framework.Input.Buttons.A))
            {
                GameManager.instance.ChangeScene(new SampleScene());
            }
        }
    }
}
