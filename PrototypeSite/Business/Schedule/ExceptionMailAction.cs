using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuaintHouse.Scheduler;
using QuaintHouse.Scheduler.Action;

namespace Business.Schedule
{
    public class ExceptionMailAction : IAction
    {
        public void Execute(ActionContext actionContext)
        {
            throw new Exception("Exception Mail Test");
        }
    }
}
