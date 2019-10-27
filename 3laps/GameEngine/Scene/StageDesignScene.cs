#if LevelDesign

using GameEngine;
using GameEngine.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class StageDesignScene : Scene
    {
        bool isPlay;
        GameObject camera;
        Vector2 befCameraPos,rclickPos;
        EditModeUI editModeUI;
        public StageDesignScene()
        {
            camera = gameObjectManager.Instantiate(new Camera(new Transform(new Vector2(-32, -28)), 1));
            editModeUI = new EditModeUI(this);
        }
        public override Scene Clone()
        {
            throw new NotImplementedException();
        }

        public override Scene Create()
        {
            throw new NotImplementedException();
        }
        public void EditModeCameraWork()
        {
            if (Input.IsMouseRButtonDown())
            {
                befCameraPos = camera.transform.position;
                rclickPos = Input.GetMousePosition();
            }
            else if (Input.IsMouseRButton()) camera.transform.position = (befCameraPos + (rclickPos - Input.GetMousePosition()));

        }
        public void Export()
        {
            ((LevelDesignMap)nowMap).Export(((LevelDesignMap)nowMap).GetNowMap());
        }
        public override void Load()
        {
            gameObjectManager = new GameObjectManager();
            nowMap = new LevelDesignMap("map0.csv", 64,gameObjectManager);

            
            isPlay = true;
        }
        public void ChangeYSize(int size)
        {
            nowMap.ChangeYSize(size);
        }
        public void ChangeXSize(int size)
        {
            nowMap.ChangeXSize(size);
        }
        public Camera GetCamera()
        {
            return (Camera)camera;
        }

        public void PlaySwitch()
        {
            isPlay = !isPlay;

        }
        public override void UpdateScene()
        {
            editModeUI.Draw();
            if (Input.IsMouseLButtonDown())
                editModeUI.PlayButtonClick(Input.GetMousePoint());
            
            
            if(!isPlay)
            Update();
            else
            gameObjectManager.UpdateObjects();
            
        }
        public override void UnLoad()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            nowMap.DrawMap();

            EditMap();

            

            EditModeCameraWork();
        }
        public void EditMap()
        {
            if (Input.IsMouseLButton() && ScreenInformations.PlayWindow.Contains(Input.GetMousePoint()))
            {
                Point clickedPoint = new Point((int)camera.transform.position.X + 32 + Input.GetMousePoint().X, (int)camera.transform.position.Y + 28 + Input.GetMousePoint().Y);
                nowMap.ChangeGrid(clickedPoint, editModeUI.GetMapchipNum());
            }
            else if (Input.IsMouseLButtonDown())
            {
                editModeUI.Click(Input.GetMousePoint());
            }

            if (Input.GetKeyDown(Keys.Up))
            {
                editModeUI.MapYChange(-1);
            }else if (Input.GetKeyDown(Keys.Down))
            {
                editModeUI.MapYChange(1);
            }else if (Input.GetKeyDown(Keys.Left))
            {
                editModeUI.MapXChange(-1);
            }
            else if (Input.GetKeyDown(Keys.Right))
            {
                editModeUI.MapYChange(1);
            }
            if (Input.GetKeyDown(Keys.X)) editModeUI.Export();
        }
    }
}
#endif