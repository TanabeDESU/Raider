using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
namespace GameEngine
{
    class Image:GameObject
    {
        public SpriteRenderer spriteRenderer;

        public Image(string assetName, int width, int height, int drawLayer, Transform transform):base(assetName, transform)
        {
            AddComponent(new SpriteRenderer(assetName, width, height, transform, drawLayer));
            spriteRenderer = (SpriteRenderer)GetComponent("SpriteRenderer");
        }

        public override GameObject Clone()
        {
            return null;
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
