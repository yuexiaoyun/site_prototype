using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Data;

namespace QuaintHouse.Scheduler.Action.Log
{
    public class ActionLogData : BaseData
    {
        public virtual void CreateActionLog(ActionLog actionLog)
        {
            //throw new NotImplementedException();
        }

        public void CompleteActionLog(ActionLog actionLog)
        {
            //throw new NotImplementedException();
        }
    }
}
