using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class CircleWarpShaderEffectGenerater : GameComponent
    {
        List<CircleWarpShaderEffectController> circleWarps;
        //int layer;
        public CircleWarpShaderEffectGenerater()
        {
            //layer = layerNum;
            circleWarps = new List<CircleWarpShaderEffectController>();
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
            return "CircleWarpGenerater";
        }
        public void AddWarp(Vector2 pos,float maxRadius,float thickness,float pase,int layer,float firstRadius=0)
        {
            CircleWarpShaderEffectController newWarp= new CircleWarpShaderEffectController(maxRadius,thickness,pase,firstRadius,layer);
            newWarp.SetCenterPosition(pos);
            circleWarps.Add(newWarp);
        }
        public override void Update()
        {
            circleWarps.RemoveAll(cw => !cw.isPlay);
            circleWarps.ForEach(cw => cw.Update());
        }
    }
}
