using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuaintHouse.Scheduler;

namespace Business.Schedule
{
    public class ExceptionMailJob : Job
    {
        public override string ActionId()
        {
            return "ExceptionMailAction";
        }
    }
}
