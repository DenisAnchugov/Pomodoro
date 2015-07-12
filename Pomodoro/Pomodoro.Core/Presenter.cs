namespace Pomodoro.Core
{
    public class Presenter
    {
        IPomodoroView view;
        PomodoroTimer timer;

        public Presenter(IPomodoroView view)
        {
            this.view = view;
            timer = new PomodoroTimer();
            timer.TimerTicked += Timer_TimerTicked;
        }

        void Timer_TimerTicked(object sender, TimerTickedEventArgs e)
        {
            view.UpdateTime(e.ElapsedTime);
        }

        public bool SwitchTimerState()
        {
            if (timer.IsRunning)
            {
                timer.Pause();
            }
            else
            {
                timer.Start();
            }
            return timer.IsRunning;
        }
    }
}
