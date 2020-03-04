using System;

namespace WPFDesktopSimple.Events
{
    public class RaceEventArg
    {
        public DateTime EventTime;
        public RaceEventArg(DateTime time)
        {
            EventTime = time;
        }
    }

    public delegate void RaceEventHandler(object sender, RaceEventArg e);
}
