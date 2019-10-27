using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using OssanAndUmbrella;

namespace GameEngine {
    public class GameObjectManager{

        public List<GameObject> objectList = new List<GameObject>();
        public GameObject slash, player;
        public List<GameObject> enemyList = new List<GameObject>(), enemySlashList = new List<GameObject>();
        public List<GameObject> addObjects = new List<GameObject>();
        bool canAdd = true;

        public GameObjectManager()
        {
        }

        public void StartObjects()
        {
            RemoveObjects();
            foreach (var b in objectList)
            {
                b.StartObject();

            }
        }

        public void UpdateObjects()
        {
            RemoveObjects();
            AddObjects();
            canAdd = false;
            foreach(var b in objectList)
            {       
                b.UpdateObject();
                
            }
            foreach(var b in enemySlashList)
            {
                RectangleCollider a, c;
                a = (RectangleCollider)b.GetComponent("RectangleCollider");
                c = (RectangleCollider)Player.instance.GetComponent("RectangleCollider");
                if (a.square.Intersects(c.square))
                {
                    a.gameObject.Hit(c);
                    c.gameObject.Hit(a);
                }

            }
            foreach (var b in enemyList)
            {
                RectangleCollider a, c;
                a = (RectangleCollider)b.GetComponent("RectangleCollider");
                if (GameManager.instance.nowScene.gameObjectManager.slash == null) break;
                c = (RectangleCollider)GameManager.instance.nowScene.gameObjectManager.slash.GetComponent("RectangleCollider");
                if (a.square.Intersects(c.square))
                {
                    a.gameObject.Hit(c);
                    c.gameObject.Hit(a);
                }

            }
            canAdd = true;
        }

        private void AddObjects()
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

        private void RemoveObjects()
        {
            objectList.RemoveAll(b => b.isDead);
            enemySlashList.RemoveAll(b => b.isDead);
            enemyList.RemoveAll(b => b.isDead);
            if (slash == null) return;
            if (slash.isDead)
            {
                slash = null;
            }
        }

        public GameObject Instantiate(GameObject gameObject)
        {
            addObjects.Add(gameObject);
            return gameObject;
        }

        public void Destroy(GameObject gameObject)
        {
            gameObject = null;
        }

    }
}
