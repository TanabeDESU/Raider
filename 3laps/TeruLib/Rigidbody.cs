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
    public class Rigidbody:GameComponent
    {

        public Vector2 velocity = Vector2.Zero;
        private Transform transform;
        private float gravityScale = 1;
        public RectangleCollider rectangleCollider;
        public bool isTrigger;
        public bool simurated = true;
        public static float gravity = 0.1f;

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
                GameManager.instance.nowScene.map.MapCollision(this, true);
                transform.position.Y += velocity.Y;
                GameManager.instance.nowScene.map.MapCollision(this, false);
            }
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
    }
}
