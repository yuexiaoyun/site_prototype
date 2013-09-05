using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Core.Cache;
using Core.Ioc;
using Core.Util;

namespace Web
{
    public abstract class BaseWebApplication : HttpApplication
    {
        private Container container;

        public Container Container
        {
            get { return container; }
        }

        public abstract void InitWebSite();

        public abstract void StopWebSite();

        public void Application_Start(object sender, EventArgs e)
        {
            BootstrapLogger.Debug("Begin Application_Start");

            Start();

            BootstrapLogger.Debug("End Application_Start");
        }

        public void Application_End(object sender, EventArgs e)
        {
            BootstrapLogger.Debug("Begin Application_End");

            Stop();

            BootstrapLogger.Debug("End Application_End");
        }

        private void Start()
        {
            // Code that runs on application startup
            log4net.Config.XmlConfigurator.Configure(new FileInfo(Server.MapPath("~/Config/log4net.xml.config")));

            container = new Container();

            InitWebSite();

            Application.Add(Container.CONTAINER, container);
        }

        private void Stop()
        {
            //  Code that runs on application shutdown
            StopWebSite();

            StopLocalCache();

            DisposeContainer();
        }

        private void StopLocalCache()
        {
            try
            {
                if(container != null)
                {
                    LocalCacheHelper localCache = container.GetInstance<ICacheHelper>(CacheContainerKey.Local_Cache) as LocalCacheHelper;
                    if(localCache != null)
                    {
                        BootstrapLogger.Debug("Begin Stop Local Cache");
                        localCache.Stop();
                        BootstrapLogger.Debug("End Stop Local Cache");
                    }
                }
            }
            catch (Exception ignore)
            {
                BootstrapLogger.Error(ignore.Message, ignore);
            }
            
        }

        private void DisposeContainer()
        {
            if (container != null)
            {
                container.Dispose();
            }

            Application[Container.CONTAINER] = null;
        }

        public void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occursed
        }

        public void Session_Start(object sender, EventArgs e)
        {
        }

        public void Session_End(object sender, EventArgs e)
        {
        }
    }
}
