using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    public abstract class  Scene
    {   
        public  GameObjectManager gameObjectManager;
        public Camera camera;
        public Map map;

        public Scene()
        {


        }

        public void LoadScene()
        {
            Load();
        }

        public void UpdateScene()
        {
            Update();
            gameObjectManager.UpdateObjects();
        }

        public void UnLoadScene()
        {
            UnLoad();
        }

        public abstract void Load();

        public abstract void Update();
        public abstract void UnLoad();

    }
}
