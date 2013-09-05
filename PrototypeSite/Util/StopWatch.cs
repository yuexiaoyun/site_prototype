using System;

namespace Util
{
    public class StopWatch
    {
        private readonly DateTime startTime;
        private DateTime endTime;

        public StopWatch()
        {
            startTime = DateTime.Now;
        }

        public void Stop()
        {
            endTime = DateTime.Now;
        }

        public long ElapsedMs()
        {
            return (long) endTime.Subtract(startTime).TotalMilliseconds;
        }
    }
}