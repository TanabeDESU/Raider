using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    abstract class Easing
    {
        public abstract float Move(float time, float start, float distance, float maxTime);

        public static float ExpoIn(float time, float start, float distance, float maxTime)
        {
            return distance * (float)Math.Pow(2, 10 * (time / maxTime - 1)) + start;
        }

        public static float ExpoOut(float time, float start, float distance, float maxTime)
        {
            return distance * (float)(-Math.Pow(2, -10 * time / maxTime) + 1) + start;
        }

        public static float ExpoInOut(float time, float start, float distance, float maxTime)
        {
            time /= maxTime / 2;
            if (time < 1) return distance / 2 * (float)Math.Pow(2, 10 * (time - 1)) + start;
            time--;
            return distance / 2 * (-(float)Math.Pow(2, -10 * time) + 2) + start;
        }

        public static float QuadIn(float time, float start, float distance, float maxTime)
        {
            time /= maxTime;
            return distance * time * time + start;
        }

        public static float QuadOut(float time, float start, float distance, float maxTime)
        {
            time /= maxTime;
            return -distance * time * (time - 2) + start;
        }

        public static float QuadInOut(float time, float start, float distance, float maxTime)
        {
            time /= maxTime / 2;
            if (time < 1) return distance / 2 * time * time + start;
            time--;
            return -distance / 2 * (time * (time - 2) - 1) + start;
        }

        public Easing Clone()
        {
            return (Easing)MemberwiseClone();
        }
    }
}
