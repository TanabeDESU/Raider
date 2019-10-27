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
    public class SpriteRenderer : GameComponent//画像の描画
    {

        int drawLayer;//描画順
        public string assetName;//名前
        protected Transform transform;
        public float alpha = 1;//透明度
        public Color color = Color.White;//色
        public SpriteEffects effects = SpriteEffects.None;//忘れた
        public int width, height;//画像の縦横
        public int glidSize = 0;//縦横一気に
        public int spriteNum = 0;//連番管理

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
        public SpriteRenderer(string assetName, int width, int height, Transform transform, int drawLayer)
        {
            Renderer.instance.LoadContent(assetName);
            this.width = width;
            this.height = height;
            this.assetName = assetName;
            this.drawLayer = drawLayer;
            this.transform = transform;
        }

        public SpriteRenderer(string assetName, int width, int height, Transform transform, int drawLayer, int spriteNum)
        {
            Renderer.instance.LoadContent(assetName);
            this.width = width;
            this.height = height;
            this.assetName = assetName;
            this.drawLayer = drawLayer;
            this.transform = transform;
            this.spriteNum = spriteNum;
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
            return "SpriteRenderer";
        }

        public virtual void Draw()
        {
            Renderer.instance.DrawTexture(assetName, transform.position - Renderer.instance.camera.transform.position, new Rectangle(width * spriteNum, 0, width, height), color * alpha, transform.rotationZ, new Vector2(width / 2, height / 2), transform.scall, effects, (float)drawLayer / 10f);
        }

        public override GameComponent Clone(GameObject obj)
        {
            SpriteRenderer spriteRenderer = (SpriteRenderer)MemberwiseClone();
            spriteRenderer.transform = obj.transform;
            return spriteRenderer;
        }

    }
}