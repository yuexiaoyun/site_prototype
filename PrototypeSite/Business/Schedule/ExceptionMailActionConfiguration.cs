using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuaintHouse.Scheduler;

namespace Business.Schedule
{
    public class ExceptionMailActionConfiguration : BaseConfiguration
    {
        protected override Type[] CacheClassTypes()
        {
            return null;
        }

        public override string ActionId()
        {
            return "ExceptionMailAction";
        }

        public override Type ActionType()
        {
            return typeof (ExceptionMailAction);
        }
    }
}
