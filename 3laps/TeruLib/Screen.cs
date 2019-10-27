using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameEngine
{ 
    public class Screen
    {
        public static Vector2 screenSize;
        public Screen(GraphicsDeviceManager graphics)
        {
            screenSize.X = graphics.PreferredBackBufferWidth;
            screenSize.Y = graphics.PreferredBackBufferHeight;
        }

        public Vector2 GetScreenSize()
        {
            return screenSize;
        }
    }
}
