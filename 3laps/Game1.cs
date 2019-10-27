using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameEngine;

namespace _3laps
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Input input;
        RenderTarget2D m_renderTarget;
        int screenShotCount = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            //graphics.IsFullScreen = true;
            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            IsMouseVisible = true;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            Renderer.instance = new Renderer(Content, GraphicsDevice);
            Audio.instance = new Audio(Content);
            input = new Input();
#if LevelDesign
            GameManager.instance.GameStart(new StageDesignScene());
#else
            GameManager.instance.GameStart(new DammyScene());
#endif
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            m_renderTarget = new RenderTarget2D(GraphicsDevice, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight);
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            input.InputUpdate();
            GameManager.instance.GameUpdate();
            if (Input.GetKeyDown(Keys.S))
            {
                TakeScreenShot();
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        private void TakeScreenShot()
        {
            // 描画するレンダーターゲットを設定する
            GraphicsDevice.SetRenderTarget(m_renderTarget);

            // 描画先のレンダーターゲットの内容を消す
            GraphicsDevice.Clear(Color.Transparent);

            // ゲームの内用を描画する
            GameManager.instance.GameDraw();

            // 描画先を、画面に変える
            GraphicsDevice.SetRenderTarget(null);

            // ファイルストリームを開く
            string path = "Stage" + screenShotCount +".png";
            using (System.IO.Stream stream = System.IO.File.OpenWrite(path))
            {
                // レンダーターゲットの内用をpngファイルとして保存
                m_renderTarget.SaveAsPng(stream, m_renderTarget.Width, m_renderTarget.Height);
            }

            screenShotCount++;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            
            GameManager.instance.GameDraw();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
