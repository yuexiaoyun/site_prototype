using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Quartz.Spi;

namespace QuaintHouse.Scheduler.Schedule
{
    public class JobFactory : IJobFactory
    {
        public IJob NewJob(TriggerFiredBundle bundle)
        {
            return (IJob)ContainerFactory.GetContainer().GetInstance(bundle.JobDetail.JobType);
        }
    }
}
