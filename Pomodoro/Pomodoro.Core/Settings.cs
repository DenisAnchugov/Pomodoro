using System;

namespace Pomodoro
{
    static class Settings
    {
        public static TimeSpan WorkPeriod { get { return new TimeSpan(0, 25, 0); } }
        public static TimeSpan ShortBreak { get { return new TimeSpan(0, 3, 0); } }
        public static TimeSpan LongBreak { get { return new TimeSpan(0, 15, 0); } }
        public static int WorkPeriodCount { get { return 10; } }
    }
}
