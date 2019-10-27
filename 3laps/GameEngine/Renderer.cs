using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;//Assert用
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    public class Renderer//描画管理
    {
        private ContentManager contentManager;
        public GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;
        public static Renderer instance;
        public Camera camera;
        public Effect none;

        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private Dictionary<string, Effect> shaderEffects = new Dictionary<string, Effect>();
        private LinkedList<SpriteRenderer>[] drawLists = new LinkedList<SpriteRenderer>[] { new LinkedList<SpriteRenderer>(), new LinkedList<SpriteRenderer>(), new LinkedList<SpriteRenderer>(), new LinkedList<SpriteRenderer>(), new LinkedList<SpriteRenderer>(), new LinkedList<SpriteRenderer>() };

        public Renderer (ContentManager content, GraphicsDevice graphics)
        {
            contentManager = content;
            graphicsDevice = graphics;
            spriteBatch = new SpriteBatch(graphicsDevice);
            none = content.Load<Effect>("./Shader/ScrewyColors");
        }
        public void LoadEffect(string assetName,string filePath = "./Shader/")
        {
            if (shaderEffects.ContainsKey(assetName))
            {
                return;
            }
            shaderEffects.Add(assetName, contentManager.Load<Effect>(filePath + assetName));
        }
        public void LoadContent(string assetName, string filepath = "./")
        {
            if (textures.ContainsKey(assetName))
            {
                return;
            }
            Console.WriteLine(assetName + "をロード");
            textures.Add(assetName, contentManager.Load<Texture2D>(filepath + assetName));
        }
        public Texture2D GetTextue(string assetName)
        {
            if (textures.ContainsKey(assetName) == false)
            {
                Console.WriteLine("画像が見つかりません, " + assetName);
                return null;
            }
            return textures[assetName];

        }

        public void AddDrawList(SpriteRenderer spriteRendrer)
        {
            drawLists[spriteRendrer.DrawLayer].AddLast(spriteRendrer);
        }

        public void UnLoad()
        {
            textures.Clear();
        }
        public void Begin()
        {
            spriteBatch.Begin();
        }
        public void Begin(SpriteSortMode spriteSortMode,BlendState blendState,Effect shaderEffect)
        {
            spriteBatch.Begin(spriteSortMode, blendState, effect: shaderEffect);
        }
        public void End()
        {
            spriteBatch.End();
        }

        public void DrawTexture(string assetName, Vector2 position)
        {
            Debug.Assert(textures.ContainsKey(assetName),
                "描画時にアセット名の名前を間違えたか、画像の読み込み自体できていません。");

            spriteBatch.Draw(textures[assetName], position, Color.White);
        }
        public void DrawTexture(string assetName, Vector2 position, float alpha = 1.0f)
        {
            Debug.Assert(textures.ContainsKey(assetName),
                "描画時にアセット名の名前を間違えたか、画像の読み込み自体できていません。");

            spriteBatch.Draw(textures[assetName], position, Color.White * alpha);
        }
        public void DrawTexture(string assetName, Vector2 position, Rectangle rectangle)
        {
            Debug.Assert(textures.ContainsKey(assetName),
                "描画時にアセット名の名前を間違えたか、画像の読み込み自体できていません。");

            spriteBatch.Draw(textures[assetName], position, rectangle, Color.White);
        }
        public void DrawTexture(string assetName, Vector2 position, Rectangle rectangle, Color color)
        {
            Debug.Assert(textures.ContainsKey(assetName),
                "描画時にアセット名の名前を間違えたか、画像の読み込み自体できていません。");

            spriteBatch.Draw(textures[assetName], position, rectangle, color);
        }
        public void DrawTexture(string assetName, Vector2 position, Rectangle rectangle, float alpha = 1.0f)
        {
            Debug.Assert(textures.ContainsKey(assetName),
                "描画時にアセット名の名前を間違えたか、画像の読み込み自体できていません。");

            spriteBatch.Draw(textures[assetName], position, rectangle,  Color.White * alpha);
        }
        public void DrawTexture(string assetName, Vector2 position, Color color,  float alpha = 1.0f)
        {
            Debug.Assert(textures.ContainsKey(assetName),
                "描画時にアセット名の名前を間違えたか、画像の読み込み自体できていません。");

            spriteBatch.Draw(textures[assetName], position, color * alpha);
        }

        public void DrawTexture(string assetName, Vector2 position, Rectangle rectangle, Color color, float alpha = 1.0f)
        {
            Debug.Assert(textures.ContainsKey(assetName),
                "描画時にアセット名の名前を間違えたか、画像の読み込み自体できていません。");
            spriteBatch.Draw(textures[assetName], position, rectangle, color * alpha);
        }

        public void DrawTexture(string assetName, Vector2 position, Rectangle rectangle, Color color, float rotation, Vector2 origin, Vector2 scall, SpriteEffects effects, float layerDepth)
        {
            
            Debug.Assert(textures.ContainsKey(assetName),
                "描画時にアセット名の名前を間違えたか、画像の読み込み自体できていません。");
            spriteBatch.Draw(textures[assetName], position, rectangle, color, rotation, origin, scall, effects, layerDepth);
        }
        public void DrawTexture(string assetName, Vector2 position, Rectangle rectangle, Color color, float rotation, Vector2 origin, Vector2 scall, SpriteEffects effects, float layerDepth,string effectName)
        {
            

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, effect: shaderEffects[effectName]);
            //shaderEffects[effectName].Parameters["delta"].SetValue((0.1f));
            Debug.Assert(textures.ContainsKey(assetName),
                "描画時にアセット名の名前を間違えたか、画像の読み込み自体できていません。");
            spriteBatch.Draw(textures[assetName], position, rectangle, color, rotation, origin, scall, effects, layerDepth);
            spriteBatch.End();
            spriteBatch.Begin();
            
        }
        public void DrawObjects()
        {
            
            for (int i = 0; i < drawLists.Length; i++)
            {
                foreach (var b in drawLists[i])
                {
                    b.Draw();
                }
                drawLists[i].Clear();
            }
        }
    }
}
