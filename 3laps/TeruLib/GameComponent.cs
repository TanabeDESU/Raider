using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public abstract class GameComponent
    {
        public GameComponent(){}

        public abstract void Start();

        public abstract void Update();

        public abstract string ToString();

    }
}
