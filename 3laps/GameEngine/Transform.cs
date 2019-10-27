using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameEngine;


namespace GameEngine
{   

    public class Transform//position、大きさ、角度とかが入ってる
    {
        public Vector2 position, scall;
        public float rotationZ;
        public Transform parent;
        

        public Transform()
        {
            this.position = Vector2.Zero;
            this.scall = new Vector2(1, 1);
            this.rotationZ = 0;
            //parent = new Transform();
        }
        public Transform(Vector2 position)
        {
            this.position = position;
            this.scall = new Vector2(1, 1);
            this.rotationZ = 0;
        }
        public Transform(Vector2 position, float rotationZ)
        {
            this.position = position;
            this.scall = new Vector2(1, 1);
            this.rotationZ = rotationZ;
            //parent = new Transform();
        }
        public Transform(Vector2 position, float rotationZ, Vector2 scall)
        {
            this.position = position;
            this.scall = scall;
            this.rotationZ = rotationZ;
            //parent = new Transform();
        }

        public Transform(Vector2 position, float rotationZ, Vector2 scall, Transform parent)
        {
            this.position = position;
            this.scall = scall;
            this.rotationZ = rotationZ;
            //this.parent = parent;
        }

        public Transform Clone()
        {
            return (Transform)MemberwiseClone();
        }
    }
}
