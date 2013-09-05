using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using Business.Schedule;
using Core.Ioc;
using QuaintHouse.Scheduler;
using Web;
using site.Base;

namespace site
{
    public class Global : BaseWebApplication
    {
        public override void InitWebSite()
        {
            //WebInitializer webInitializer = new WebInitializer();
            //webInitializer.Init(Container);

            SchedulerInitializer.Start();
            SchedulerInitializer.LoadActionConfigurations(new SampleActionConfiguration());
            SchedulerInitializer.LoadActionConfigurations(new ExceptionMailActionConfiguration());
        }

        public override void StopWebSite()
        {
            SchedulerInitializer.Stop();
        }
    }
}
