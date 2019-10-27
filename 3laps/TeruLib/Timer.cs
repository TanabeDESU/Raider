using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class Timer
    {
        public float maxTime;
        public float time;
        public bool trigger;
        public Timer(float maxTime)
        {
            this.maxTime = maxTime;
            time = 0;
            trigger = false;
        }

        public void ResetCheck()
        {
            if (trigger)
            {
                trigger = false;
                time = 0;
            }
        }
        public void AddTime()
        {
            time += (float)GameManager.instance.gameTime.ElapsedGameTime.TotalSeconds;
        }
        public void TriggerCheck()
        {
            if (time > maxTime)
            {
                trigger = true;
            }
        }
    }
}
