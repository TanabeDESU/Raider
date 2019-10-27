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
    public abstract class GameObject//ゲームで動かす物体
    {
        private string name;
        public bool isStatic = false;//停止かどうか
        public bool isDead = false;//trueで破棄
        public string tag;//タグ付け
        public Transform transform;//Positionとか入ってる
        
        public Dictionary<string, GameComponent> gameCompoents = new Dictionary<string, GameComponent>();
        public string Name { get => name; set => name = value; }  //名前

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

        public void AddComponent(GameComponent gameComponent)//コンポーネントの追加
        {
            if (gameCompoents.ContainsKey(gameComponent.ToString()))
            {
                int i = 1;
                while (true)
                {
                    if (!gameCompoents.ContainsKey(gameComponent.ToString() + i))
                    {
                        gameCompoents.Add(gameComponent.ToString() + i, gameComponent);
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
                gameCompoents.Add(gameComponent.ToString(), gameComponent);
            }
        }

        public void AddComponent(GameComponent gameComponent, string assetName)
        {
#if DEBUG
            if (gameCompoents.ContainsKey(assetName))
            {
                Console.WriteLine(name + "には既に同じ名前のGameComponentが含まれています。");
                return;
            }
#endif
            gameCompoents.Add(assetName, gameComponent);
        }

        public GameComponent GetComponent(string componentName)//名前からコンポーネントを取得
        {
            return gameCompoents[componentName];
        }

        public void StartObject()//ここはいじらんでいい
        {
            if (!isStatic)
            {
                Start();
                foreach (var b in gameCompoents.Values)
                {
                    b.Start();
                }
                
            }
        }

        public void UpdateObject()//ここはいじらんでいい
        {
            if (!isStatic)
            {
                Update();
                foreach (var b in gameCompoents.Values)
                {
                    b.Update();
                }
                
            }
        }

        public Dictionary<string, GameComponent> CloneComponets(GameObject clone)
        {
            Dictionary<string, GameComponent> cloneComponents = new Dictionary<string, GameComponent>();
            foreach (var b in gameCompoents)
            {
                Console.WriteLine(clone.name + b.ToString());
                cloneComponents.Add(b.Key, b.Value.Clone(clone));
            }
            return cloneComponents;
        }

        public abstract GameObject Clone();

        public abstract void Start();//生成時の処理
        public abstract void Update();//毎フレーム呼ばれる処理
        public abstract void Hit(RectangleCollider rectangleCollider);//何かとぶつかった時に呼ばれる処理
    }
}
