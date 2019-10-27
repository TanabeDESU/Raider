using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.UI
{
    interface IButton
    {
        void On();
        void Off();
        void Draw();
        void OnClick();
        Rectangle GetRect();
        void Update();
    }
}
