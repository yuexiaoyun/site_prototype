using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.Text;
using Core.Cache;
using Core.Cache.Settings;
using Core.Data;
using Core.Interceptor;
using Core.Ioc;
using Core.Util;
using Microsoft.Practices.Unity;
using Web.Session.Cache;

namespace Web
{
    public abstract class BaseWebInitializer
    {
        public abstract string[] CacheAssemblyNames { get; }

        public void Init(Container container)
        {
            BootstrapLogger.Debug("Start initializer");

            InitCore(container);
            InitSite(container);

            BootstrapLogger.Debug("End initializer");
        }

        protected abstract void InitSite(Container container);

        private void InitCore(Container container)
        {
            BootstrapLogger.Debug("Start Init Core");

            RegisterDatabase(container);
            RegisterParamsInConfig(container);
            RegisterCache(container);
            RegisterCacheInterceptor(container);
            RegisterInterceptor(container);

            BootstrapLogger.Debug("End Init Core");
        }

        private static void RegisterDatabase(Container container)
        {
            container.RegisterInstance(typeof(DatabaseDelegator), new DatabaseDelegator());
            container.RegisterInstance(typeof(DatabaseDelegator), "CustomedDb", new DatabaseDelegator("CustomedDb"));
        }

        private static void RegisterParamsInConfig(Container container)
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
            container.RegisterType(typeof(ICacheHelper), typeof(SessionCacheHelper), CacheContainerKey.Session_Cache, new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(ICacheHelper), typeof(RequestCacheHelper), CacheContainerKey.Request_Cache, new ContainerControlledLifetimeManager());
            
            string remoteCacheMode = ConfigurationManager.AppSettings["RemoteCacheMode"];
            switch (remoteCacheMode)
            {
                case "localcache":
                    container.RegisterType(typeof(ICacheHelper), typeof(LocalCacheHelper), CacheContainerKey.Remote_Cache, new ContainerControlledLifetimeManager());
                    break;
                case "memcache":
                    container.RegisterType(typeof(ICacheHelper), typeof(MemCacheHelper), CacheContainerKey.Remote_Cache, new ContainerControlledLifetimeManager());
                    break;
                default:
                    container.RegisterType(typeof(ICacheHelper), typeof(MemCacheHelper), CacheContainerKey.Remote_Cache, new ContainerControlledLifetimeManager());
                    break;
            }
        }

        private void RegisterCacheInterceptor(Container container)
        {
            foreach (string assemblyName in CacheAssemblyNames)
            {
                Assembly assembly = Assembly.Load(assemblyName);
                foreach (Type type in assembly.GetTypes())
                {
                    foreach (MethodInfo method in type.GetMethods())
                    {
                        if (method.IsDefined(typeof (CacheAttribute), true))
                        {
                            container.AddInterceptor(type);
                            break;
                        }
                    }
                }
            }
        }

        private void RegisterInterceptor(Container container)
        {
            container.RegisterType(typeof(RetryCallHandler), new ContainerControlledLifetimeManager());
            container.RegisterType(typeof(TransactionCallHandler), new ContainerControlledLifetimeManager());
        }

        private void RegisterGlobalStopWatch(Container container)
        {
            try
            {
                string globalStopWatchAssembly = ConfigurationManager.AppSettings["GlobalStopWatchAssembly"];
                if (string.IsNullOrEmpty(globalStopWatchAssembly)) return;

                string[] assemblies = globalStopWatchAssembly.Split(',');

                foreach (string assembly in assemblies)
                {
                    //container
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
