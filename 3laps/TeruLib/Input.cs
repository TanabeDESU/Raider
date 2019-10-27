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
    class Input
    {
        static KeyboardState keyboardState, previewKeyboardState;
        static GamePadState gamePadState, previewGamePadState;
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
        }

        static public Vector2 VerticalInput()
        {
            Vector2 vector2 = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.Up)) vector2.Y += -1;
            if (keyboardState.IsKeyDown(Keys.Down)) vector2.Y += 1;
            return vector2;
        }

        static public Vector2 HorizontalInput()
        {
            Vector2 vector2 = Vector2.Zero;
            if (keyboardState.IsKeyDown(Keys.Right)) vector2.X += 1;
            if (keyboardState.IsKeyDown(Keys.Left)) vector2.X += -1;
            return vector2;
        }
       
        static public bool GetKeyDown(Keys key)
        {
            if (previewKeyboardState.IsKeyDown(key)) return false;
            if (keyboardState.IsKeyDown(key)) return true;
            return false;
        }

        static public bool GetKey(Keys key)
        {
           return keyboardState.IsKeyDown(key);
        }

        static public bool GetButtonDown(Buttons button)
        {
            if (previewGamePadState.IsButtonDown(button)) return false;
            if (gamePadState.IsButtonDown(button)) return true;
            return false;
        }

        static public bool GetButtton(Buttons button)
        {
            return gamePadState.IsButtonDown(button);
        }

        static public Vector2 GetLeftStickAxis()
        {
            return new Vector2(gamePadState.ThumbSticks.Left.X, gamePadState.ThumbSticks.Left.Y);
        }

        static public Vector2 GetLeftStickAxisRaw()
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
