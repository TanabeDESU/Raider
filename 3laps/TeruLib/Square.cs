using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    public class Square
    {
        public int width, height;
        public Transform transform;

        public Square(Transform transform, int size)
        {
            this.transform = transform;
            width = size;
            height = size;
        }

        public Square(Transform transform, int width, int height)
        {
            this.transform = transform;
            this.width = width;
            this.height = height;
        }

        public bool Intersects(Square square)
        {
            float xDis = Math.Abs(transform.position.X - square.transform.position.X);
            float yDis = Math.Abs(transform.position.Y - square.transform.position.Y);
            if((width / 2) + (square.width / 2) > xDis)
            {
                if((height / 2) + (square.height / 2) > yDis)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
