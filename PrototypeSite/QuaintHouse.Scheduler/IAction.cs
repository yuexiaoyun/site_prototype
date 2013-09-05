using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuaintHouse.Scheduler.Action;

namespace QuaintHouse.Scheduler
{
    public interface IAction
    {
        void Execute(ActionContext actionContext);
    }
}
