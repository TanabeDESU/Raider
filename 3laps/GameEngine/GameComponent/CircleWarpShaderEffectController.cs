using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class CircleWarpShaderEffectController :IRenderer
    {
        Vector2 circlePos;
        float radius,thickness,pase,maxRadius,maxThickness,firstRadius;
        public bool isPlay;int layer;
        public CircleWarpShaderEffectController(float maxradius,float firstThickness,float expasionPase, float firstRadius,int layer)
        {
            this.layer = layer;
            circlePos = new Vector2(ScreenInformations.ScreenWidth / 2, ScreenInformations.ScreenHeight / 2);
            radius = firstRadius+firstThickness;
            this.firstRadius = radius;
            maxRadius = maxradius;
            thickness = firstThickness;maxThickness = thickness;
            pase = expasionPase;
        }
        public void SetCenterPosition(Vector2 center)
        {
            isPlay = true;
            circlePos = center;
            thickness = maxThickness;
            radius = firstRadius;
        }
        public  GameComponent Clone(GameObject obj)
        {
            throw new NotImplementedException();
        }

        public  void Start()
        {
            //throw new NotImplementedException();
        }

        public override string ToString()
        {
            return "CircleWarpShaderGenerater";
        }

        public  void Update()
        {
            if (isPlay) {
                radius += pase;
                thickness -= maxThickness / (maxRadius / pase); }
            if (thickness < 0 || radius > maxRadius)
            {
                isPlay = false;
                return;
            }
            Renderer.instance.AddEffectList(this);
        }
        private void SetEffect()
        {
            Renderer.instance.SetCircleWarpPosition(circlePos-Renderer.instance.camera.transform.position);
            Renderer.instance.SetCircleWarpRadius(radius / ScreenInformations.ScreenHeight);
            Renderer.instance.SetCircleWarpThickness(thickness / ScreenInformations.ScreenHeight);
        }

        public void Draw()
        {
            SetEffect();
            Renderer.instance.DrawEffect("CircleWarp",layer);
        }

        public int GetLayerNum()
        {
            return layer;
        }
    }
}
