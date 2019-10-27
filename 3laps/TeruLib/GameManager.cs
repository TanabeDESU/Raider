using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using OssanAndUmbrella;

namespace GameEngine
{
    public class GameManager
    {
        public GameTime gameTime;
        public Scene nowScene;
        public Dictionary<string, Scene> scences;

        public static GameManager instance = new GameManager();
        public enum GameState
        {
            Title,
            Play,
            Result
        }

        public GameState gameState;
        public static int timeCount;

        public GameManager()
        {
            gameState = GameState.Title;
            gameTime = new GameTime();
            
        }

        public void GameStart (Scene scene)
        {
            nowScene = scene;
            nowScene.Load();
        } 


        public void GameUpdate()
        {
            nowScene.UpdateScene();
            switch (gameState)
            {
                case GameState.Title:
                    break;
                case GameState.Play:
                    break;
                case GameState.Result:
                    break;
            }
        }
        public void GameDraw()
        {
            Renderer.instance.Begin();
            Renderer.instance.DrawObjects();
            switch (gameState)
            {
                case GameState.Title:
                    break;
                case GameState.Play:;
                    break;
                case GameState.Result:
                    break;
            }
            Renderer.instance.End();


        }

        public void ChangeScene(Scene scene)
        {
            nowScene.UnLoadScene();
            nowScene = scene;
            nowScene.LoadScene();
        }
    }
}