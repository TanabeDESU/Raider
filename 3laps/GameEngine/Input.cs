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
    class Input//入力を受け取るクラス
    {
        static KeyboardState keyboardState, previewKeyboardState;
        static GamePadState gamePadState, previewGamePadState;
        //マウス
        private static MouseState currentMouse;//現在のマウスの状態
        private static MouseState previousMouse;//1フレーム前のマウスの状態
        public Input()
        {
            keyboardState = Keyboard.GetState();
            gamePadState = GamePad.GetState(PlayerIndex.One);
        }

        public void InputUpdate()
        {
            previewKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();
            previewGamePadState = gamePadState;
            gamePadState = GamePad.GetState(PlayerIndex.One);
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();
        }
        public static Point GetMousePoint()
        {
            return currentMouse.Position;
        }
        public static Vector2 GetMousePosition()
        {
            return currentMouse.Position.ToVector2();
        }
        public static bool IsMouseLButtonDown()
        {
            return currentMouse.LeftButton == ButtonState.Pressed &&
                previousMouse.LeftButton == ButtonState.Released;
        }
        public static bool IsMouseLButtonUp()
        {
            return currentMouse.LeftButton == ButtonState.Released &&
                previousMouse.LeftButton == ButtonState.Pressed;
        }
        public static bool IsMouseLButton()
        {
            return currentMouse.LeftButton == ButtonState.Pressed;
        }
        public static bool IsMouseRButtonDown()
        {
            return currentMouse.RightButton == ButtonState.Pressed &&
                previousMouse.RightButton == ButtonState.Released;
        }
        public static bool IsMouseRButtonUp()
        {
            return currentMouse.RightButton == ButtonState.Released &&
                previousMouse.RightButton == ButtonState.Pressed;
        }
        public static bool IsMouseRButton()
        {
            return currentMouse.RightButton == ButtonState.Pressed;
        }
        static public Vector2 VerticalInput()//上下の取得
        {
            Vector2 vector2 = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.Up)) vector2.Y += -1;
            if (keyboardState.IsKeyDown(Keys.Down)) vector2.Y += 1;
            return vector2;
        }

        static public Vector2 HorizontalInput()//左右の取得
        {
            Vector2 vector2 = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.Right)) vector2.X += 1;
            if (keyboardState.IsKeyDown(Keys.Left)) vector2.X += -1;
            return vector2;
        }
       
        static public bool GetKeyDown(Keys key)//キーを押した瞬間
        {
            if (previewKeyboardState.IsKeyDown(key)) return false;
            if (keyboardState.IsKeyDown(key)) return true;
            return false;
        }

        static public bool GetKey(Keys key)//キーを押している間
        {
           return keyboardState.IsKeyDown(key);
        }

        static public bool GetButtonDown(Buttons button)//ボタンを押した瞬間
        {
            if (previewGamePadState.IsButtonDown(button)) return false;
            if (gamePadState.IsButtonDown(button)) return true;
            return false;
        }

        static public bool GetButton(Buttons button)//ボタンを押している間
        {
            return gamePadState.IsButtonDown(button);
        }

        static public Vector2 GetLeftStickAxis()//左スティック傾き有
        {
            return new Vector2(gamePadState.ThumbSticks.Left.X, gamePadState.ThumbSticks.Left.Y);
        }

        static public Vector2 GetLeftStickAxisRaw()//左スティック傾き無
        {
            Vector2 result = Vector2.Zero;
            if(gamePadState.ThumbSticks.Left.X > 0.1)
            {
                result.X += 1;
            }
            else if(gamePadState.ThumbSticks.Left.X < -0.1)
            {
                result.X += -1;
            }
            if (gamePadState.ThumbSticks.Left.Y > 0.1)
            {
                result.Y += 1;
            }
            else if(gamePadState.ThumbSticks.Left.Y < -0.1f)
            {
                result.Y += -1;
            }
            return result;
        }

    }
}
