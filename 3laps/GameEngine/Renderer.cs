using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;//Assert用
using Microsoft.Xna.Framework.Input;

namespace GameEngine
{
    public struct WarpShaderParam
    {
        public float start, range;
        public int rangeDelta;
    }
    public class Renderer//描画管理
    {
        private ContentManager contentManager;
        public GraphicsDevice graphicsDevice;
        public SpriteBatch spriteBatch;
        public static Renderer instance;
        public Camera camera;
        public Effect currentEffect, defaultEffect, screenEffect;
        WarpShaderParam shaderParam;

        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        private Dictionary<string, Effect> shaderEffects = new Dictionary<string, Effect>();
        private LinkedList<IRenderer>[] drawLists = new LinkedList<IRenderer>[] { new LinkedList<IRenderer>(), new LinkedList<IRenderer>(), new LinkedList<IRenderer>(), new LinkedList<IRenderer>(), new LinkedList<IRenderer>(), new LinkedList<IRenderer>() };
        private RenderTarget2D[] renderTargets;
        private List<IRenderer> renderEffects;
        RenderTarget2D mainRenderTarget;
        public Renderer(ContentManager content, GraphicsDevice graphics)
        {

            contentManager = content;
            graphicsDevice = graphics;
            renderTargets = new RenderTarget2D[drawLists.Length];
            for (int i = 0; i < renderTargets.Length; i++)
            {
                renderTargets[i] = new RenderTarget2D(graphicsDevice, ScreenInformations.ScreenWidth, ScreenInformations.ScreenHeight);
            }
            mainRenderTarget = new RenderTarget2D(graphicsDevice, ScreenInformations.ScreenWidth, ScreenInformations.ScreenHeight);
            spriteBatch = new SpriteBatch(graphicsDevice);
            LoadContent("warpSample");
            LoadEffect("Blue");
            LoadEffect("None");
            LoadEffect("CircleWarp");
            LoadEffect("AllBlackToInvisible");
            shaderEffects["Blue"].Parameters["warp"].SetValue(textures["warpSample"]);
            screenEffect = shaderEffects["Blue"];
            currentEffect = shaderEffects["None"];
            defaultEffect = shaderEffects["Blue"];
            shaderParam.rangeDelta = 1;
            renderEffects = new List<IRenderer>();
        }
        public void LoadEffect(string assetName, string filePath = "./Shader/")
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

        public void AddDrawList(IRenderer render)
        {
            drawLists[render.GetLayerNum()].AddLast(render);
        }
        public void AddEffectList(IRenderer renderer)
        {
            renderEffects.Add(renderer);
        }
        public void UnLoad()
        {
            textures.Clear();
        }
        public void Begin()
        {

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, effect: currentEffect);
        }
        public void Begin(Effect shaderEffect)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, effect: shaderEffect);
        }
        public void CheckShaderEffect(Effect shaderEffect)
        {
            if (shaderEffect == currentEffect)
            {
                return;
            }
            spriteBatch.End();
            currentEffect = shaderEffect;
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, effect: shaderEffect);
        }
        public void ChangeDefaultEffect(string effectName)
        {
            defaultEffect = shaderEffects[effectName];
        }
        public void ChangeScreenEffect(string effectName)
        {
            screenEffect = shaderEffects[effectName];
        }
        public void End()
        {
            spriteBatch.End();
        }

        public void SetCircleWarpPosition(Vector2 pos)
        {
            shaderEffects["CircleWarp"].Parameters["circlePosition"].SetValue(new Vector2(pos.X / ScreenInformations.ScreenWidth, pos.Y / ScreenInformations.ScreenHeight));
        }
        public void SetCircleWarpThickness(float thickness)
        {
            shaderEffects["CircleWarp"].Parameters["thickness"].SetValue(thickness);
        }
        public void SetCircleWarpRadius(float radius)
        {
            shaderEffects["CircleWarp"].Parameters["radius"].SetValue(radius);
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

            spriteBatch.Draw(textures[assetName], position, rectangle, Color.White * alpha);
        }
        public void DrawTexture(string assetName, Vector2 position, Color color, float alpha = 1.0f)
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

            CheckShaderEffect(defaultEffect);
            Debug.Assert(textures.ContainsKey(assetName),
                "描画時にアセット名の名前を間違えたか、画像の読み込み自体できていません。");
            spriteBatch.Draw(textures[assetName], position, rectangle, color, rotation, origin, scall, effects, layerDepth);
        }
        public void DrawTexture(string assetName, Vector2 position, Rectangle rectangle, Color color, float rotation, Vector2 origin, Vector2 scall, SpriteEffects effects, float layerDepth, string effectName)
        {
            var se = shaderEffects[effectName].Clone();
            CheckShaderEffect(se);
            //shaderEffects[effectName].Parameters["delta"].SetValue((0.1f));
            Debug.Assert(textures.ContainsKey(assetName),
                "描画時にアセット名の名前を間違えたか、画像の読み込み自体できていません。");
            spriteBatch.Draw(textures[assetName], position, rectangle, color, rotation, origin, scall, effects, layerDepth);
            se.Dispose();
        }
        public void ScreenDraw(Texture2D Screen)
        {
            if (screenEffect == shaderEffects["Blue"]) SinWarpRenderer(ref shaderParam);
            graphicsDevice.SetRenderTarget(null);
            var se = screenEffect.Clone();
            Begin(se);
            spriteBatch.Draw(mainRenderTarget, new Vector2(0, 0), Color.White);
            End();
            se.Dispose();
        }
        public void SinWarpRenderer(ref WarpShaderParam shaderParam)
        {
            shaderParam.start += 2f;
            if (shaderParam.start > 360) shaderParam.start = 0;
            shaderParam.range += 0.01f * shaderParam.rangeDelta;
            if (shaderParam.range > 0.05f || shaderParam.range < -0.05f) shaderParam.rangeDelta *= -1;
            screenEffect.Parameters["start"].SetValue(shaderParam.start);
            screenEffect.Parameters["range"].SetValue(shaderParam.range);
        }
        public void DrawEffect(string effectName, int layer)
        {
            var screen = renderTargets[layer];

            var newScreen = new RenderTarget2D(graphicsDevice, ScreenInformations.ScreenWidth, ScreenInformations.ScreenHeight);

            graphicsDevice.SetRenderTarget(newScreen);
            graphicsDevice.Clear(Color.Black);
            //End();
            var se = shaderEffects[effectName].Clone();
            Begin(se);
            spriteBatch.Draw(screen, new Vector2(0, 0), Color.White);
            End();

            se.Dispose();

            renderTargets[layer].Dispose();
            renderTargets[layer] = newScreen;

        }
        public void SetShaderParameter(string shaderName,string parameterName,float value)
        {
            shaderEffects[shaderName].Parameters[parameterName].SetValue(value);
        }
        public void SetRenderTargets(RenderTarget2D target)
        {
            mainRenderTarget = target;
        }
        public void ShadersReload()
        {
            //if (!shaderEffects.ContainsKey(key)) return;
            string[] keys = new string[shaderEffects.Count];

            shaderEffects.Keys.CopyTo(keys, 0);

            //shaderEffects.Keys;

            foreach (var k in keys)
            {
                shaderEffects.Remove(k);
                LoadEffect(k);
            }
        }
        public List<IRenderer> GetRenderEffects()
        {
            return renderEffects;
        }
        public void DrawObjects()
        {

            for (int i = 0; i < drawLists.Length; i++)
            {
                graphicsDevice.SetRenderTargets(renderTargets[i]);
                graphicsDevice.Clear(Color.Black);
                Begin();
                foreach (var b in drawLists[i])
                {
                    b.Draw();
                }
                drawLists[i].Clear();
                End();
            }
            for (int i = 0; i < renderEffects.Count; i++)
            {
                renderEffects[i].Draw();
            }
            renderEffects.Clear();
            graphicsDevice.SetRenderTarget(mainRenderTarget);
            var se = shaderEffects["AllBlackToInvisible"].Clone();
            Begin(se);
            for (int i = 0; i < renderTargets.Length; i++)
            {
                spriteBatch.Draw(renderTargets[i], new Vector2(), Color.White);
            }
            End();
            se.Dispose();
            //ShadersReload();
        }
    }
}
