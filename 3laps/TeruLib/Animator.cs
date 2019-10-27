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
    public class Animator:GameComponent
    {
        public Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
        public Animation nowAnimation;
        public string nowAnimationName;
        public int count = 0, maxCount = 6;

        public void SetAnimation(string key)
        {
            if (animations[key] == nowAnimation) return;
            if (nowAnimation != null)
            {
                nowAnimation.spriteRenderer.SpriteNum = 0;
                nowAnimation.exitAnimation = false;
            }
            nowAnimation = animations[key];
            nowAnimationName = key;
        }

        public override void Start()
        {
            
        }

        public override string ToString()
        {
            return "Animator";
        }

        public override void Update()
        {
            count++;
            if(count > maxCount)
            {   
                nowAnimation.ChangeImage();
                count = 0;
            }
            nowAnimation.spriteRenderer.Update();
        }
    }
}
