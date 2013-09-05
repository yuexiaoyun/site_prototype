using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using Core.Cache;
using Core.Cache.Settings;
using Core.Data;
using Core.Interceptor;
using Core.Ioc;
using Core.Util;
using Microsoft.Practices.Unity;
using QuaintHouse.Scheduler.Action;
using QuaintHouse.Scheduler.Schedule;

namespace QuaintHouse.Scheduler
{
    public class SchedulerInitializer
    {
        private static Container container;

        public static void Start()
        {
            BootstrapLogger.Debug("Start initializer");

            Init();

            Schedule.Scheduler scheduler = container.GetInstance<Schedule.Scheduler>();
            scheduler.Start(10);

            BootstrapLogger.Debug("End initializer");
        }

        public static void LoadActionConfigurations(params IConfiguration[] configurations)
        {
            ActionFramework actionFramework = container.GetInstance<ActionFramework>();
            actionFramework.LoadConfiguration(configurations);
        }

        private static void Init()
        {
            InitContainer();
            InitCore();
            InitJobs();
        }

        private static void InitContainer()
        {
            container = ContainerFactory.GetContainer();
            container.RegisterInstance(typeof(Container), container);
        }

        private static void InitCore()
        {
            RegisterDataBase();
            RegisterCache();
            RegisterParamsInWebConfig();
        }

        private static void InitJobs()
        {
            container.RegisterType(typeof(Schedule.Scheduler), new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(JobLoader), new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(ActionFramework), new ContainerControlledLifetimeManager());
        }

        private static void RegisterDataBase()
        {
            container.RegisterInstance(typeof(DatabaseDelegator), new DatabaseDelegator());
            //container.RegisterInstance(typeof(DatabaseDelegator), "SomeOther", new DatabaseDelegator("SomeOtherConnectionString"));
        }

        private static void RegisterParamsInWebConfig()
        {
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            foreach (string key in appSettings)
            {
                container.RegisterInstance(typeof(string), key, appSettings.Get(key));
            }
        }

        private static void RegisterCache()
        {
            container.RegisterType(typeof(CacheSettingManager), new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(CacheCallHandler), new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(CacheMetaLoader), new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(ICacheHelper), typeof(LocalCacheHelper), CacheContainerKey.Local_Cache, new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(ICacheHelper), typeof(LocalCacheHelper), CacheContainerKey.Remote_Cache, new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(ICacheHelper), typeof(LocalCacheHelper), CacheContainerKey.Session_Cache, new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(ICacheHelper), typeof(LocalCacheHelper), CacheContainerKey.Request_Cache, new ContainerControlledLifetimeManager());
        }

        public static void Stop()
        {
            BootstrapLogger.Debug("Stop Scheduler");

            Schedule.Scheduler scheduler = container.GetInstance<Schedule.Scheduler>();
            scheduler.Stop();

            BootstrapLogger.Debug("Stop Local Cache");

            StopLocalCache();

            BootstrapLogger.Debug("Dispose Container");

            container.Dispose();
        }

        private static void StopLocalCache()
        {
            try
            {
                if (container != null)
                {
                    LocalCacheHelper localCache = container.GetInstance<ICacheHelper>(CacheContainerKey.Local_Cache) as LocalCacheHelper;
                    if (localCache != null)
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
    }
}
