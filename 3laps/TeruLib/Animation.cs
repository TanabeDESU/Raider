using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Animation
    {   
        public bool isLoop;
        public float playerTime;
        public int imageSum, num = 0;
        public SpriteRenderer spriteRenderer;
        public bool exitAnimation;

        public Animation(int imageSum, bool isLoop, SpriteRenderer spriteRenderer)
        {
            this.spriteRenderer = spriteRenderer;
            this.imageSum = imageSum;
            this.isLoop = isLoop;
            spriteRenderer.Start();
        }

        public void ChangeImage()
        {
            if (exitAnimation) return;
            if (!isLoop && spriteRenderer.SpriteNum >= imageSum - 1)
            {
                exitAnimation = true;
                return;
            }
            spriteRenderer.SpriteNum++;
            if(isLoop && spriteRenderer.SpriteNum >= imageSum)
            {
                spriteRenderer.SpriteNum = 0;
            }
        }
    }
}
