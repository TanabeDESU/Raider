using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameEngine;
using OssanAndUmbrella;

namespace GameEngine
{
    public class Camera : GameObject
    {
        private float zoomSize;
        public float ZoomSize { get => zoomSize; set => zoomSize = value; }

        public Camera(Transform transform, float zoomSize):base("Camera", transform)
        {
            this.ZoomSize = zoomSize;
            Renderer.instance.camera = this;
        }

        public override void Start()
        {
            
        }

        public override void Update()
        {
            if (Player.instance == null) return;
            if (Player.instance.transform.position.X < 1280 || Player.instance.transform.position.X > 2560)
            {
                return;
            }
            transform.position = Player.instance.transform.position;
            transform.position -= new Microsoft.Xna.Framework.Vector2(640, 500);
        }

        public override void Hit(RectangleCollider rectangleCollider)
        {
            
        }
    }
}
