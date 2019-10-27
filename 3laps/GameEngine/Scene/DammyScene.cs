using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    class DammyScene : Scene
    {
        public override void Load()
        {
            //Camera生成
            //気にせんでいい
            gameObjectManager.Instantiate(new Camera(new Transform(), 1, false));

            //↓GameObject生成の流れ

            //GameObjectを生成する座標
            Vector2 pos = new Vector2(960, 540);

            //GameObjectに渡すTransform、座標と角度とスケールが入ってる
            Transform transform = new Transform(pos);

            //GameObject本体
            //GameObjectを継承して作ってる
            GameObject gameObject = new DammyObject(transform);

            //GameObjectを生成(シーンに追加)
            //戻り値は生成したGameObject
            GameObject dammyObject = gameObjectManager.Instantiate(gameObject);

            //スケールを横2倍、縦3倍
            //誤字は許して
            dammyObject.transform.scall.X *= 2;
            dammyObject.transform.scall.Y *= 3;

            //角度を変更(ラジアン)
            //多分時計回り
            dammyObject.transform.rotationZ = 1f / 3f * (float)Math.PI;

            //説明のために長く書いたけど一行で生成できる
            //左上に新しくDammyObject生成
            gameObjectManager.Instantiate(new DammyObject(new Transform(new Vector2(64, 64))));
        }

        public override void UnLoad()
        {
        }

        public override void Update()
        {
        }

        public override Scene Clone()
        {
            return null;
        }

        public override Scene Create()
        {
            return new DammyScene();
        }
    }
}
