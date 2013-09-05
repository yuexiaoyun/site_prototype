using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QuaintHouse.Scheduler.Action;
using Quartz;
using log4net;

namespace QuaintHouse.Scheduler
{
    public abstract class Job : IStatefulJob
    {
        private ILog logger = LogManager.GetLogger("Scheduler");

        public virtual void Execute(JobExecutionContext context)
        {
            logger.Debug("Execute Job: " + context.MergedJobDataMap.Get("JobName"));

            ActionContext actionContext = new ActionContext(context.MergedJobDataMap);

            ActionFramework actionFramework = ContainerFactory.GetContainer().GetInstance<ActionFramework>();
            
            actionFramework.Execute(ActionId(), actionContext);
        }

        public abstract string ActionId();
    }

    
}
