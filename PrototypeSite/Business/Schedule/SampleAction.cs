using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using QuaintHouse.Scheduler;
using QuaintHouse.Scheduler.Action;
using log4net;

namespace Business.Schedule
{
    public class SampleAction : IAction
    {
        private UserManager userManager;
        [Dependency]
        public UserManager UserManager
        {
            set { userManager = value; }
        }

        private ILog logger = LogManager.GetLogger("Scheduler");

        public void Execute(ActionContext actionContext)
        {
            logger.Info("Start Sample Action");
            logger.Info("Param: " + actionContext.Get("param1"));
            logger.Info("Action Result, user count:" + userManager.GetUserCount());
            logger.Info("End Sample Action");
        }
    }
}
