using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuaintHouse.Scheduler.Action;
using QuaintHouse.Scheduler.Action.Interceptor;
using log4net;

namespace Business.Schedule.Interceptor
{
    public class SampleActionInterceptor : IInterceptor
    {
        private ILog logger = LogManager.GetLogger("Scheduler");

        public void Intercepte(ActionProxy actionProxy)
        {
            logger.Info("SampleActionInterceptor, order:" + Order);
            actionProxy.Execute();
        }

        public int Order
        {
            get { return 0; }
        }
    }
}
