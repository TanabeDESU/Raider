using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;

namespace OssanAndUmbrella
{
    class Image : GameObject
    {
        public Image(string assetName) : base("aa", new Transform(new Microsoft.Xna.Framework.Vector2(640, 360)))
        {
            
        }

        public override void Hit(RectangleCollider rectangleCollider)
        {
        }

        public override void Start()
        {
        }

        public override void Update()
        {
        }
    }
}
