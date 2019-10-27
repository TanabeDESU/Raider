using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class DammyObject : GameObject
    {
        public DammyObject(Transform transform) : base("DammyName", transform)
        {
            //GameComponetsにSpriteRendererを追加 描画されるようになる
           AddComponent(new SpriteRenderer("TempPlayer", transform, 0));
        }

        public override GameObject Clone()
        {
            return new DammyObject(new Transform(transform.position));
        }

        public override void Hit(RectangleCollider rectangleCollider)
        {
            //何かと衝突した時に呼ばれる
        }

        public override void Start()
        {
            //生成した時に一度呼ばれる
            //ここでGameObject生成するとエラー吐きますごめんなさい
        }

        public override void Update()
        {
            //毎フレーム呼ばれる処理
            Easing.ExpoOut(1f,2f,3f,4f);
        }
    }
}
