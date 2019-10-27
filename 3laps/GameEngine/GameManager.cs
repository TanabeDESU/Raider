using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using _3laps;
namespace GameEngine
{
    public class GameManager//シーン、ゲームを管理、更新、描画する
    {
        public GameTime gameTime;
        public Scene nowScene;//現在のシーン
        public Dictionary<string, Scene> scences;//シーン管理
        public static Random random = new Random();

        public static GameManager instance = new GameManager();

        public static int timeCount;

        public GameManager()
        {
            gameTime = new GameTime();
            
        }

        public void GameStart (Scene scene)//最初に行う処理
        {
            nowScene = scene;
            nowScene.Load();
        } 

        public void GameUpdate()//毎フレーム行う処理
        {
            nowScene.UpdateScene();
        }
        public void GameDraw()//描画処理
        {
            Renderer.instance.Begin();
            Renderer.instance.DrawObjects();
            Renderer.instance.End();


        }

        public void HoldScene()
        {
        }

        public void CloneScene()
        {
        }

        public void ChangeScene(Scene scene)//シーン切り替え
        {
            nowScene.UnLoadScene();
            nowScene = scene;
            nowScene.LoadScene();
        }
    }
}