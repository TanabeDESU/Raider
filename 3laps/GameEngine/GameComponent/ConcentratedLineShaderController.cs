using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    class ConcentratedLineShaderController : GameComponent,IRenderer
    {
        float pase, radius, alpha,minRadius,maxRadius,maxAlpha;
        bool isOn;short d = 1;int layer;
        public ConcentratedLineShaderController(int layerNum)
        {
            layer = layerNum;
            maxRadius = 1.1f;
            radius = maxRadius;
        }
        public override GameComponent Clone(GameObject obj)
        {
            throw new NotImplementedException();
        }

        public override void Start()
        {
        }
        public void On(float minR,float maxA,float pase)
        {
            d = 1;
            isOn = true;
            minRadius = minR;
            maxAlpha = maxA;
            this.pase = pase;
        }
        public void Off(float pase)
        {
            d = -1;
            isOn = true;
        }
        public override string ToString()
        {
            return "ConcentratedLineShaderController";
        }
        public void SetEffectParameter()
        {
            Renderer.instance.SetShaderParameter("Blur", "alpha", alpha);
            Renderer.instance.SetShaderParameter("Blur", "radius", radius);
        }
        public void Draw()
        {
            SetEffectParameter();
            Renderer.instance.DrawEffect("Blur", layer);
        }
        public override void Update()
        {
            if (isOn&&!(radius <= minRadius || radius > maxRadius)) ParamUpdate();
        }
        public void ParamUpdate()
        {
            alpha += maxAlpha / ((maxRadius-minRadius) / pase) * d;
            radius -= pase*d;
            Renderer.instance.AddEffectList(this);
        }

        public int GetLayerNum()
        {
            return layer;
        }
    }
}
