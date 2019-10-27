using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using GameEngine;

namespace OssanAndUmbrella
{
    public class MapChip_Block:GameObject
    {

        public MapChip_Block(Transform transform):base("MapChip_Block", transform)
        {
            tag = "Map";
            AddComponent(new SpriteRenderer("Renga", transform, 0, Color.White, 1, 64));
            AddComponent(new RectangleCollider(this, new Square(transform, 64)));
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
