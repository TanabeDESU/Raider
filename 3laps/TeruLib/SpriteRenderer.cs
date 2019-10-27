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
    public class SpriteRenderer:GameComponent
    {

        int drawLayer;
        string assetName;
        Transform transform;
        public float alpha = 1;
        public Color color = Color.White;
        public SpriteEffects effects = SpriteEffects.None;
        int width, height;
        int glidSize = 0;
        int spriteNum = 0;

        public SpriteRenderer(string assetName, Transform transform, int drawLayer) 
        {
            Renderer.instance.LoadContent(assetName);
            this.assetName = assetName;
            this.drawLayer = drawLayer;
            this.transform = transform;
        }
        public SpriteRenderer(string assetName, Transform transform, int drawLayer, int glidSize)
        {
            Renderer.instance.LoadContent(assetName);
            this.glidSize = glidSize;
            this.assetName = assetName;
            this.drawLayer = drawLayer;
            this.transform = transform;
        }
        public SpriteRenderer(string assetName, Transform transform, int drawLayer, int glidSize, int spriteNum)
        {
            Renderer.instance.LoadContent(assetName);
            this.spriteNum = spriteNum;
            this.glidSize = glidSize;
            this.assetName = assetName;
            this.drawLayer = drawLayer;
            this.transform = transform;
        }
        public SpriteRenderer(string assetName, Transform transform, int drawLayer, float alpha)
        {
            Renderer.instance.LoadContent(assetName);
            this.assetName = assetName;
            this.alpha = alpha;
            if (this.alpha > 1) this.alpha = 1;
            else if (this.alpha < 0) this.alpha = 0;
            this.drawLayer = drawLayer;
            this.transform = transform;
        }
        public SpriteRenderer(string assetName, Transform transform, int drawLayer, float alpha, int glidSize)
        {
            Renderer.instance.LoadContent(assetName);
            this.assetName = assetName;
            this.alpha = alpha;
            if (this.alpha > 1) this.alpha = 1;
            else if (this.alpha < 0) this.alpha = 0;
            this.drawLayer = drawLayer;
            this.transform = transform;
            this.glidSize = glidSize;
        }
        public SpriteRenderer(string assetName, Transform transform, int drawLayer, Color color)
        {
            Renderer.instance.LoadContent(assetName);
            this.assetName = assetName;
            this.drawLayer = drawLayer;
            this.transform = transform;
            this.color = color;
        }
        public SpriteRenderer(string assetName, Transform transform, int drawLayer, Color color, int glidSize)
        {
            Renderer.instance.LoadContent(assetName);
            this.glidSize = glidSize;
            this.assetName = assetName;
            this.drawLayer = drawLayer;
            this.transform = transform;
            this.color = color;
        }
        public SpriteRenderer(string assetName, Transform transform, int drawLayer, Color color, float alpha)
        {
            Renderer.instance.LoadContent(assetName);
            this.assetName = assetName;
            this.drawLayer = drawLayer;
            this.transform = transform;
            this.color = color;
            this.alpha = alpha;
            if (this.alpha > 1) this.alpha = 1;
            else if (this.alpha < 0) this.alpha = 0;
        }
        public SpriteRenderer(string assetName, Transform transform, int drawLayer, Color color, float alpha, int glidSize)
        {
            Renderer.instance.LoadContent(assetName);
            this.assetName = assetName;
            this.glidSize = glidSize;
            this.drawLayer = drawLayer;
            this.transform = transform;
            this.color = color;
            this.alpha = alpha;
            if (this.alpha > 1) this.alpha = 1;
            else if (this.alpha < 0) this.alpha = 0;
        }

        public int DrawLayer { get => drawLayer; set => drawLayer = value; }
        public int SpriteNum { get => spriteNum; set => spriteNum = value; }

        public override void Start()
        {
            width = glidSize;
            height = glidSize;
            if (glidSize == 0) glidSize = Renderer.instance.GetTextue(assetName).Height;
            
        }

        public override void Update()
        {
            Renderer.instance.AddDrawList(this);
        }

        public override string ToString()
        {
            return "SpriteRenderer";
        }

        public void Draw()
        {
            Renderer.instance.DrawTexture(assetName, transform.position - Renderer.instance.camera.transform.position, new Rectangle(glidSize * spriteNum, 0, glidSize, glidSize), color * alpha, transform.rotationZ, new Vector2(width / 2, height / 2), transform.scall, effects, drawLayer);
        }
        
    }
}
