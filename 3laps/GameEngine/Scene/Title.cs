using GameEngine;
using Microsoft.Xna.Framework;
using System;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    class Title : Scene//タイトルシーン
    {
        public bool trigger, triggerFlg;
        GameObject player;
        bool isUp = true;
        float targetHeight;
        float distance;
        GameObject[] stageIcons =
        {
            new StageIcon(0, 4),
            new StageIcon(1, 4),
            new StageIcon(2, 4),
            new StageIcon(3, 4),
        };
        int menuNum = 0;
        
        public override void Load()
        {
            gameObjectManager = new GameObjectManager();
            gameObjectManager.Instantiate(new Camera(new Transform(new Vector2(0, 0)), 1, false));
            gameObjectManager.Instantiate(new Image("TitleImage", 1920, 1080, 0, new Transform(new Vector2(960, 540)))); 
            gameObjectManager.Instantiate(new Image("MenuImage", 1920, 1080, 0, new Transform(new Vector2(960, 540 + 1080)))); 

        }

        public override void UnLoad()
        {
            
        }

        public override void Update()
        {
            if (isUp)
            {
                targetHeight = 0;
            }
            else
            {
                targetHeight = 1080;
            }
            distance = Math.Abs(targetHeight - Renderer.instance.camera.transform.position.Y);
            Renderer.instance.camera.transform.position.Y = MathHelper.Lerp(Renderer.instance.camera.transform.position.Y, targetHeight, 0.2f);
            if (Input.GetButtonDown(Buttons.A) || Input.GetButtonDown(Buttons.DPadDown))
            {
                isUp = false;
            }
            else if(Input.GetButtonDown(Buttons.DPadUp))
            {
                isUp = true;
            }
        }

        public override Scene Clone()
        {
            return null;
        }

        public override Scene Create()
        {
            return new Title();
        }
    }
}
