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
    public class Line
    {
        public Vector2 start, end;
        public Line(Vector2 start, Vector2 end)
        {
            this.start = start;
            this.end = end;
        }
    }
}
