using System;

namespace Pomodoro.Core
{
    public interface IPomodoroView
    {
        void UpdateTime(TimeSpan elapsedTime);
        void NotifyTimerElapsed();
    }
}