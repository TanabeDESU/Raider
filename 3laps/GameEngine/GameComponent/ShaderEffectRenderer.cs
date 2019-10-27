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
    public class ShaderEffectRenderer : SpriteRenderer//画像の描画
    {
        string shaderEffectName;
        
        public ShaderEffectRenderer(string assetName, Transform transform, int drawLayer,string effectName):base(assetName,transform,drawLayer)
        {
            Renderer.instance.LoadEffect(effectName);
            shaderEffectName = effectName;
        }
        public ShaderEffectRenderer(string assetName, Transform transform, int drawLayer, int glidSize, string effectName) : base(assetName, transform, drawLayer,glidSize)
        {
            shaderEffectName = effectName;
            Renderer.instance.LoadEffect(effectName);
        }
        public ShaderEffectRenderer(string assetName, int width, int height, Transform transform, int drawLayer, string effectName) :base(assetName,width,height,transform,drawLayer)
        {
            shaderEffectName = effectName;
            Renderer.instance.LoadEffect(effectName);
        }

        public ShaderEffectRenderer(string assetName, int width, int height, Transform transform, int drawLayer, int spriteNum, string effectName) : base(assetName, transform, drawLayer,spriteNum)
        {
            shaderEffectName = effectName;
            Renderer.instance.LoadEffect(effectName);
        }
        public ShaderEffectRenderer(string assetName, Transform transform, int drawLayer, int glidSize, int spriteNum, string effectName) : base(assetName, transform, drawLayer,glidSize,spriteNum)
        {
            shaderEffectName = effectName;
            Renderer.instance.LoadEffect(effectName);
        }
        public ShaderEffectRenderer(string assetName, Transform transform, int drawLayer, float alpha, string effectName) : base(assetName, transform, drawLayer,alpha)
        {
            shaderEffectName = effectName;
            Renderer.instance.LoadEffect(effectName);
        }
        public ShaderEffectRenderer(string assetName, Transform transform, int drawLayer, float alpha, int glidSize, string effectName) : base(assetName, transform, drawLayer,alpha,glidSize)
        {
            shaderEffectName = effectName;
            Renderer.instance.LoadEffect(effectName);
        }
        public ShaderEffectRenderer(string assetName, Transform transform, int drawLayer, Color color, string effectName) : base(assetName, transform, drawLayer,color)
        {
            shaderEffectName = effectName;
            Renderer.instance.LoadEffect(effectName);
        }
        public ShaderEffectRenderer(string assetName, Transform transform, int drawLayer, Color color, int glidSize, string effectName) : base(assetName, transform, drawLayer,color,glidSize)
        {
            shaderEffectName = effectName;
            Renderer.instance.LoadEffect(effectName);
        }
        public ShaderEffectRenderer(string assetName, Transform transform, int drawLayer, Color color, float alpha, string effectName) : base(assetName, transform, drawLayer,color,alpha)
        {
            shaderEffectName = effectName;
            Renderer.instance.LoadEffect(effectName);
        }
        public ShaderEffectRenderer(string assetName, Transform transform, int drawLayer, Color color, float alpha, int glidSize, string effectName) : base(assetName, transform, drawLayer,color,glidSize)
        {
            shaderEffectName = effectName;
            Renderer.instance.LoadEffect(effectName);
        }
        
        public override void Start()
        {
            if (width == 0 && height == 0)
            {
                if (glidSize == 0) glidSize = Renderer.instance.GetTextue(assetName).Height;
                width = glidSize;
                height = glidSize;
            }

        }

        public override void Update()
        {
            Renderer.instance.AddDrawList(this);
        }

        public override string ToString()
        {
            return "ShaderRenderer";
        }

        public override void Draw()
        {
            Renderer.instance.DrawTexture(assetName, transform.position - Renderer.instance.camera.transform.position, new Rectangle(width * spriteNum, 0, width, height), color * alpha, transform.rotationZ, new Vector2(width / 2, height / 2), transform.scall, effects, (float)DrawLayer / 10f,shaderEffectName);
        }

        public override GameComponent Clone(GameObject obj)
        {
            ShaderEffectRenderer spriteRenderer = (ShaderEffectRenderer)MemberwiseClone();
            spriteRenderer.transform = obj.transform;
            return spriteRenderer;
        }

    }
}
