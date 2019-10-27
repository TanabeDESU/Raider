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
    public abstract class GameObject
    {
        private string name;
        public bool isStatic =false;
        public bool isDead = false;
        public string tag;
        public Transform transform;
        
        protected Dictionary<string, GameComponent> gameComponents = new Dictionary<string, GameComponent>();
        public string Name { get => name; set => name = value; }  

        public GameObject(string name)
        {
            this.Name = name;
            this.transform = new Transform(new Vector2(0, 0));
        }

        public GameObject(string name, Transform transform)
        {
            this.Name = name;
            this.transform = transform;
        }

        public GameObject(string name, Transform transform, RectangleCollider collider)
        {
            this.Name = name;
            this.transform = transform;

        }

        public void AddComponent(GameComponent gameComponent)
        {
            if (gameComponents.ContainsKey(gameComponent.ToString()))
            {
                int i = 1;
                while (true)
                {
                    if (!gameComponents.ContainsKey(gameComponent.ToString() + i))
                    {
                        gameComponents.Add(gameComponent.ToString() + i, gameComponent);
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }

            }
            else
            {
                gameComponents.Add(gameComponent.ToString(), gameComponent);
            }
        }

        public void AddComponent(GameComponent gameComponent, string assetName)
        {
#if DEBUG
            if (gameComponents.ContainsKey(assetName))
            {
                Console.WriteLine(name + "には既に同じ名前のGameComponentが含まれています。");
                return;
            }
#endif
            gameComponents.Add(assetName, gameComponent);
        }

        public GameComponent GetComponent(string componentName)
        {
            return gameComponents[componentName];
        }

        public void StartObject()
        {
            if (!isStatic)
            {
                Start();
                foreach (var b in gameComponents.Values)
                {
                    b.Start();
                }
                
            }
        }

        public void UpdateObject()
        {
            if (!isStatic)
            {
                Update();
                foreach (var b in gameComponents.Values)
                {
                    b.Update();
                }
                
            }
        }

        public abstract void Start();
        public abstract void Update();
        public abstract void Hit(RectangleCollider rectangleCollider);
    }
}
