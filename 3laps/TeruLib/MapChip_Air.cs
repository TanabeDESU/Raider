using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using Microsoft.Xna.Framework;


namespace OssanAndUmbrella
{
    public class MapChip_Air : GameObject
    {
        public MapChip_Air(Transform transform) : base("MapChip_Block", transform)
        {
            tag = "Map";
            AddComponent(new SpriteRenderer("Air", transform, 0, Color.White, 1, 64));
            AddComponent(new RectangleCollider(this, new Square(transform, 64)));
            RectangleCollider rectangleCollider =  (RectangleCollider)GetComponent("RectangleCollider");
            rectangleCollider.isTrigger = true;
        }

        public override void Start()
        {
        }

        public override void Update()
        {

        }

        public override void Hit(RectangleCollider rectangleCollider)
        {
        }
    }
}
