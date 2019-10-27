using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine
{
    public class TimerManager
    {
        private List<Timer> timerList;
        public Dictionary<string, Timer> timers;
        
        public TimerManager()
        {
            timerList = new List<Timer>();
            timers = new Dictionary<string, Timer>();
        }

        public void TimerCheck()
        {
            foreach(var b in timerList)
            {
                b.ResetCheck();
                b.AddTime();
                b.TriggerCheck();
            }
        }

        public void RemoveTimer(string name)
        {
            timerList.Remove(timers[name]);
        }

        public void DestroyTimer(string name)
        {
            timerList.Remove(timers[name]);
            timers.Remove(name);
        }

        public void SetTimer(string name, float maxTime, int layer)
        {
            if (timers.ContainsKey(name)) return;
            timers.Add(name, new Timer(maxTime));
            timerList.Add(timers[name]);
        }
        

    }
}
