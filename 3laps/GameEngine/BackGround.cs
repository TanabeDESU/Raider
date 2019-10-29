using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class BackGround : GameObject
    {
        public BackGround() : base("BackGround")
        {
            transform = new Transform(new Vector2(ScreenInformations.ScreenWidth / 2, ScreenInformations.ScreenHeight / 2), 0, 
                new Vector2(ScreenInformations.ScreenWidth, ScreenInformations.ScreenHeight));
            AddComponent(new ShaderEffectRenderer("BackGround",transform,0,"None"));
        }
        public override GameObject Clone()
        {
            throw new NotImplementedException();
        }

        public override void Hit(RectangleCollider rectangleCollider)
        {
        }

        public override void Start()
        {
        }

        public override void Update()
        {
           transform.position= Renderer.instance.camera.transform.position;
        }
    }
}
