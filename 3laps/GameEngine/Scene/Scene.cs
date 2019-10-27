using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    public abstract class  Scene//シーン
    {   
        public  GameObjectManager gameObjectManager;
#if LevelDesign
        public LevelDesignMap nowMap;
#else
        public Map nowMap;
#endif
        public Scene()
        {
            gameObjectManager = new GameObjectManager();

        }

        public void LoadScene()
        {
            Load();
        }

        public virtual void UpdateScene()
        {
            Update();
            gameObjectManager.UpdateObjects();
        }

        public void UnLoadScene()
        {
            UnLoad();
        }

        public abstract Scene Clone();

        public abstract Scene Create();

        public abstract void Load();

        public abstract void Update();
        public abstract void UnLoad();

    }
}
