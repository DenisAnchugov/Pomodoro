using System;
using System.Threading;

namespace Pomodoro.Core
{
    public class PomodoroTimer
    {
        Timer timer;
        public bool IsRunning;

        TimeSpan currentPeriod = new TimeSpan(0,25,0);
        public event EventHandler<TimerTickedEventArgs> TimerTicked;

        public void Start()
        {
            timer = new Timer(Tick, null, 0, 1000);
            IsRunning = true;
        }

        void Tick(object state)
        {
            currentPeriod = currentPeriod.Subtract(new TimeSpan(0, 0, 1));
            TimerTicked.Invoke(this, new TimerTickedEventArgs { ElapsedTime = currentPeriod }); 
        }

        public void Pause()
        {
            timer.Dispose();
            IsRunning = false;
        }
    }

    public class TimerTickedEventArgs
    {
        public TimeSpan ElapsedTime { get; set; }
    }
}
