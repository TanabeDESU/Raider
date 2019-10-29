using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class UnderWorldShaderController : GameComponent
    {
        float alpha,pase;
        bool isUnder;
        int changeDirection;
        public UnderWorldShaderController(float changePase)
        {
            alpha = 0;
            isUnder = false;
            pase = changePase;
            changeDirection = -1;
        }
        public override GameComponent Clone(GameObject obj)
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
        }

        public override string ToString()
        {
            return "UnderWorldShaderController";
        }
        public void On()
        {
            isUnder = true;
            changeDirection = -1;
        }
        public void Off()
        {
            isUnder = false;
            changeDirection = 1;
        }
        public void Switch()
        {
            isUnder = !isUnder;
            changeDirection = -1 * changeDirection;
        }
        public void AlphaUpdate()
        {
            alpha += pase * changeDirection;
        }
        public void SetShaderParam()
        {
            Renderer.instance.SetShaderParameter("Blue", "alpha", alpha);
            Renderer.instance.SetShaderParameter("SinWarpAlpha", "alpha", alpha);
        }
        public override void Update()
        {
            if ((alpha>=0&&isUnder)|| (alpha <= 1 && !isUnder))
                AlphaUpdate();
            if (alpha < 0 ) alpha = 0;
            if (alpha > 1) alpha = 1;
            SetShaderParam();
        }
    }
}
