using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    //SpriteRendererを使って画像をアニメーションさせるクラス
    public class Animation
    {   
        public bool isLoop;//繰り返し再生するか
        public float playerTime;
        public int imageSum, num = 0;//連番の数
        public SpriteRenderer spriteRenderer;//引数で受け取る
        public bool exitAnimation;//アニメーションが終了したか

        public Animation(int imageSum, bool isLoop, SpriteRenderer spriteRenderer)
        {
            this.spriteRenderer = spriteRenderer;
            this.imageSum = imageSum;
            this.isLoop = isLoop;
            spriteRenderer.Start();
        }

        public void ChangeImage()//表示画像変更
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

        public Animation Clone(GameObject gameObject)
        {
            Animation animation = (Animation)MemberwiseClone();
            animation.spriteRenderer = (SpriteRenderer)gameObject.GetComponent("SpriteRenderer");
            return animation;
        }
    }
}
