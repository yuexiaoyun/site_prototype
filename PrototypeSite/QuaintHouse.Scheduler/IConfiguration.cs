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
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using QuaintHouse.Scheduler.Action;
using QuaintHouse.Scheduler.Action.Interceptor;
using QuaintHouse.Scheduler.Action.Log;
using IInterceptor = QuaintHouse.Scheduler.Action.Interceptor.IInterceptor;

namespace QuaintHouse.Scheduler
{
    public interface IConfiguration
    {
        void Configure(Container container);

        string ActionId();

        Type ActionType();
    }

    public abstract class BaseConfiguration : IConfiguration
    {
        public virtual void Configure(Container container)
        {
            RegisterAction(container);

            RegisterParamsInWebConfig(container);

            RegisterDataBase(container);

            RegisterInterceptor(container);

            RegisterCache(container);
        }

        private void RegisterAction(Container container)
        {
            container.RegisterType(typeof(IAction), ActionType());
            container.RegisterInstance(typeof(string), "ActionId", ActionId());
            container.RegisterType(typeof(ActionProxy), new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(ActionLogData), new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(ILog), typeof(FileLogger), new PerThreadLifetimeManager());
        }

        public virtual void RegisterInterceptor(Container container)
        {
            container.RegisterType(typeof(IInterceptor), typeof(LoggingInterceptor), "LoggingInterceptor");
            container.RegisterType(typeof(IInterceptor), typeof(ExceptionInterceptor), "ExceptionInterceptor");
        }

        public virtual void RegisterDataBase(Container container)
        {
            container.RegisterInstance(typeof(DatabaseDelegator), new DatabaseDelegator());
            //container.RegisterInstance(typeof(DatabaseDelegator), "SomeOther", new DatabaseDelegator("SomeOtherConnectionString"));
        }

        public virtual void RegisterParamsInWebConfig(Container container)
        {
            NameValueCollection appSettings = ConfigurationManager.AppSettings;
            foreach (string key in appSettings)
            {
                container.RegisterInstance(typeof(string), key, appSettings.Get(key));
            }
        }

        private void RegisterCache(Container container)
        {
            container.RegisterType(typeof(CacheSettingManager), new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(CacheCallHandler), new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(CacheMetaLoader), new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(ICacheHelper), typeof(LocalCacheHelper), CacheContainerKey.Local_Cache, new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(ICacheHelper), typeof(LocalCacheHelper), CacheContainerKey.Remote_Cache, new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(ICacheHelper), typeof(LocalCacheHelper), CacheContainerKey.Session_Cache, new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(ICacheHelper), typeof(LocalCacheHelper), CacheContainerKey.Request_Cache, new ContainerControlledLifetimeManager());

            Type[] cacheClasses = CacheClassTypes();
            if (cacheClasses == null || cacheClasses.Length <= 0)
                return;

            foreach (Type cacheClass in cacheClasses)
            {
                if (!cacheClass.IsClass) continue;

                MethodInfo[] methods = cacheClass.GetMethods();
                if (methods.Length <= 0) continue;

                foreach (MethodInfo method in cacheClass.GetMethods())
                {
                    if (method.IsDefined(typeof (CacheAttribute), false))
                    {
                        container.AddInterceptor(cacheClass);
                        break;
                    }
                }
            }
        }
        
        protected abstract Type[] CacheClassTypes();

        public abstract string ActionId();

        public abstract Type ActionType();
    }
}
