using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Util
{
    public class StopWatch
    {
        private DateTime _startTime;
        private DateTime _endTime;

        public StopWatch()
        {
            _startTime = DateTime.Now;
        }

        public void End()
        {
            _endTime = DateTime.Now;
        }

        public long ElapsedMs()
        {
            return (long)_endTime.Subtract(_startTime).TotalMilliseconds;
        }
    }
}
