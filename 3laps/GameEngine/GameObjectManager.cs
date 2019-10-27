using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using _3laps;

namespace GameEngine {
    public class GameObjectManager{//GameObjectの管理、更新、生成、破棄するやつ

        public List<GameObject> objectList = new List<GameObject>();
        public List<GameObject> addObjects = new List<GameObject>();
        bool canAdd = true;

        public GameObjectManager()
        {
        }

        public void StartObjects()//GameObjectのStartを呼び出す
        {
            RemoveObjects();
            foreach (var b in objectList)
            {
                b.StartObject();

            }
        }

        public void UpdateObjects()//GameObjectのUpdateを呼び出す
        {
            RemoveObjects();
            AddObjects();
            canAdd = false;
            foreach(var b in objectList)
            {       
                b.UpdateObject();
                
            }
            canAdd = true;
        }

        private void AddObjects()//GameObjectを追加する
        {   
            if (canAdd)
            {
                objectList.AddRange(addObjects);
                foreach(var b in addObjects)
                {
                    b.StartObject();
                }
                addObjects.Clear();
            }
        }

        private void RemoveObjects()//GameObjectを破棄する
        {
            objectList.RemoveAll(b => b.isDead);

        }
        public void RemoveObj()
        {
            objectList.RemoveAll(b => b.isDead);
        }
        public GameObject Instantiate(GameObject gameObject)//GameObjectの生成リクエスト
        {
            addObjects.Add(gameObject);
            return gameObject;
        }

        public void Destroy(GameObject gameObject)//GameObjectの破壊
        {
            gameObject = null;
        }

        public GameObjectManager Clone()
        {
            return null;
        }
    }
}
