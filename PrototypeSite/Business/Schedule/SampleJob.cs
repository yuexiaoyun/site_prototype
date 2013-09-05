using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuaintHouse.Scheduler;

namespace Business.Schedule
{
    public class SampleJob : Job
    {
        public override string ActionId()
        {
            return "SampleAction";
        }
    }
}
