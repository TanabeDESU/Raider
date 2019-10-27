using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{
    public class Rigidbody:GameComponent//重力、衝突判定
    {

        public Vector2 velocity = Vector2.Zero;//速度
        private Transform transform;//
        public float gravityScale = 1;//重力にかける倍率
        public RectangleCollider rectangleCollider;
        public bool isTrigger;//すりぬけるか
        public bool simurated = true;//falseにすると無効になる
        public static float gravity = 0.5f;//重力
        public bool controllale;

        public Rigidbody(Transform transform, RectangleCollider rectangleCollider)
        {
            this.transform = transform;
            this.rectangleCollider = rectangleCollider;
        }

        public override void Start()
        {
            
        }

        public override void Update()
        {
            if (simurated)
            {
                velocity.Y += gravity * gravityScale;
                transform.position.X += velocity.X;
                if (GameManager.instance.nowScene.nowMap != null)
                GameManager.instance.nowScene.nowMap.MapCollision(this, true);
                transform.position.Y += velocity.Y;
                if (GameManager.instance.nowScene.nowMap != null)
                GameManager.instance.nowScene.nowMap.MapCollision(this, false);
            }
        }

        public void Collision()
        {

        }

        public void VerticalHitBack(RectangleCollider hit)
        {
            if(hit.isTrigger == false)
            {
                if (velocity.Y > 0)
                {
                    transform.position.Y = hit.gameObject.transform.position.Y - ((hit.square.height / 2) + (rectangleCollider.square.height / 2));
                }
                else if (velocity.Y < 0)
                {
                    transform.position.Y = hit.gameObject.transform.position.Y + (hit.square.height / 2) + (rectangleCollider.square.height / 2);
                }
                velocity.Y = 0;
            }
        }

        public void HorizontalHitBack(RectangleCollider hit)
        {
            if (hit.isTrigger == false)
            {
                if (velocity.X > 0)
                {
                    transform.position.X = hit.gameObject.transform.position.X - ((hit.square.width / 2) + (rectangleCollider.square.width / 2));
                }
                else if (velocity.X < 0)
                {
                    transform.position.X = hit.gameObject.transform.position.X + ((hit.square.width / 2) + (rectangleCollider.square.width / 2));
                }
                velocity.X = 0;
            }
        }

        public override string ToString()
        {
            return "Rigidbody";
        }

        public override GameComponent Clone(GameObject obj)
        {
            Rigidbody rigidbody = (Rigidbody)MemberwiseClone();
            rigidbody.transform = obj.transform;
            rigidbody.rectangleCollider = (RectangleCollider)obj.GetComponent("RectangleCollider");
            return rigidbody;
        }
    }
}
