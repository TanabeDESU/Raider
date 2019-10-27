using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public abstract class GameComponent//GameObjectのgameComponents持たせて使う
    {
        public GameComponent(){}

        public abstract void Start();//生成時の処理

        public abstract void Update();//毎フレーム行う処理

        public abstract string ToString();//コンポーネント名を返す

        public abstract GameComponent Clone(GameObject obj);
    }
}
