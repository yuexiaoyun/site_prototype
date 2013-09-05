using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Schedule.Interceptor;
using Core.Interceptor;
using Core.Ioc;
using DataAccess;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using QuaintHouse.Scheduler;
using log4net;
using IInterceptor = QuaintHouse.Scheduler.Action.Interceptor.IInterceptor;

namespace Business.Schedule
{
    public class SampleActionConfiguration : BaseConfiguration
    {
        private ILog logger = LogManager.GetLogger("Scheduler");

        public override void Configure(Container container)
        {
            logger.Info("SampleActionConfiguration");

            base.Configure(container);

            container.RegisterType(typeof(UserManager), new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(UserData), new ContainerControlledLifetimeManager());
        }

        public override void RegisterInterceptor(Container container)
        {
            base.RegisterInterceptor(container);
            container.RegisterType(typeof(IInterceptor), typeof(SampleActionInterceptor), "SampleActionInterceptor", new ContainerControlledLifetimeManager());
        }

        protected override Type[] CacheClassTypes()
        {
            return new [] {typeof (UserManager), typeof (UserData)};
        }

        public override string ActionId()
        {
            return "SampleAction";
        }

        public override Type ActionType()
        {
            return typeof (SampleAction);
        }
    }
}
