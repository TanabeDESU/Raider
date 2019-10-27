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
    public class RectangleCollider:GameComponent//枡形の当たり判定
    {
        public GameObject gameObject;
        public Square square;
        public Vector2 position;
        public bool isTrigger;
        
        public RectangleCollider(GameObject gameObject, Square square)
        {
            this.gameObject = gameObject;
            this.square = square;
            position = Vector2.Zero;
            RectangleUpdate();
        }

        public RectangleCollider(GameObject gameObject, Vector2 position, Square square)
        {
            this.gameObject = gameObject;
            this.square = square;
            this.position = position;
            RectangleUpdate();
        }

        public override string ToString()
        {
            return "RectangleCollider";
        }


        public override void Start()
        {
            
        }

        public override void Update()
        {

        }

        public void RectangleUpdate()
        {
        }

        public bool CollisionCheck(RectangleCollider rectangleCollider)
        {
            if (square.Intersects(rectangleCollider.square))
            {
                gameObject.Hit(rectangleCollider);
                rectangleCollider.gameObject.Hit(this);
                return true;
            }
            return false;
        }

        public override GameComponent Clone(GameObject obj)
        {
            RectangleCollider rectangleCollider = (RectangleCollider)MemberwiseClone();
            rectangleCollider.gameObject = obj;
            rectangleCollider.square = square.Clone(obj.transform);
            return rectangleCollider;
        }
    }
}
