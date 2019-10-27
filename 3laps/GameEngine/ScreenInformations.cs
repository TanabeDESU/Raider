using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    static class ScreenInformations
    {
        public static int ScreenWidth = 1920;
        public static int ScreenHeight = 1080;
        public static int ButtonWidth = 64;
        public static int ButtonHeight = 64;
        public static int ButtonSpace = 8;
        public static Rectangle PlayWindow = new Rectangle(0, ButtonHeight + ButtonSpace * 2, 
            ScreenWidth - (ButtonSpace * 3 + ButtonWidth * 3), ScreenHeight - (ButtonHeight + ButtonSpace * 2));
    }
}
