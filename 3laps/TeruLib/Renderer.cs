using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;//Assert用

namespace GameEngine
{
    public class Renderer
    {
        private ContentManager contentManager;
        public GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;
        public static Renderer instance;
        public Camera camera;

        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private LinkedList<SpriteRenderer>[] drawLists = new LinkedList<SpriteRenderer>[] { new LinkedList<SpriteRenderer>(), new LinkedList<SpriteRenderer>(), new LinkedList<SpriteRenderer>(), new LinkedList<SpriteRenderer>(), };
        public static Dictionary<char, int> numbers = new Dictionary<char, int>();

        public Renderer (ContentManager content, GraphicsDevice graphics)
        {
            contentManager = content;
            graphicsDevice = graphics;
            spriteBatch = new SpriteBatch(graphicsDevice);
            numbers.Add('0', 0);
            numbers.Add('1', 1);
            numbers.Add('2', 2);
            numbers.Add('3', 3);
            numbers.Add('4', 4);
            numbers.Add('5', 5);
            numbers.Add('6', 6);
            numbers.Add('7', 7);
            numbers.Add('8', 8);
            numbers.Add('9', 9);
            numbers.Add('.', 10);
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

        public void DrawSmallNumber(string number, Vector2 position, float alpha)
        {
            for (int i = 0; i < number.Length; i++)
            {
                spriteBatch.Draw(textures["SmallNumber"], position + new Vector2(i * 17, 0), new Rectangle(16 * numbers[number[i]], 0, 16, 32), Color.White * alpha);
            }
        }

        public void DrawNumber(string number, Vector2 position, float alpha)
        {
            for (int i = 0; i < number.Length; i++)
            {
                spriteBatch.Draw(textures["Numbers"], position + new Vector2(i * 34, 0), new Rectangle(32 * numbers[number[i]], 0, 32, 64), Color.White * alpha);
            }
        }

        public void DrawObjects()
        {
            
            for (int i = 0; i < drawLists.Length; i++)
            {
                //Console.WriteLine(i + ", " + drawLists.Length);
                //Console.WriteLine(drawLists[i].Count);
                foreach (var b in drawLists[i])
                {
                    b.Draw();
                }
                drawLists[i].Clear();
            }
        }
    }
}
