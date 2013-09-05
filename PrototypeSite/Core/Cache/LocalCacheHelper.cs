using System;
using System.Collections.Generic;
using System.Text;
using Core.Cache.Settings;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.BackingStoreImplementations;
using Microsoft.Practices.EnterpriseLibrary.Caching.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.Unity;

namespace Core.Cache
{
    public class LocalCacheHelper : ICacheHelper
    {
        private CacheSettingManager cacheSettingManager;
        
        [Dependency]
        public CacheSettingManager CacheSettingManager
        {
            set { cacheSettingManager = value; }
        }

        private readonly Dictionary<string, CacheManager>  caches = new Dictionary<string, CacheManager>();

        #region CacheHelper Members

        public void Add(string groupName, string key, object value)
        {
            CacheManager cacheManager = GetCacheManagerByGroupName(groupName);
            if (cacheManager != null)
            {
                LocalCachePolicy policy = cacheSettingManager.GetLocalCachePolicy(groupName);
                if (policy != null && policy.AbsoluteExpirationTimeInSecond > 0)
                {
                    cacheManager.Add(key, value, CacheItemPriority.Normal, null,
                                     new AbsoluteTime(DateTime.Now.AddSeconds(policy.AbsoluteExpirationTimeInSecond)));
                }
                else
                {
                    cacheManager.Add(key, value);
                }
            }
        }

        public void Remove(string groupName, string key)
        {
            CacheManager cacheManager = GetCacheManagerByGroupName(groupName);
            if (cacheManager != null)
            {
                cacheManager.Remove(key);
            }
        }

        public object Get(string groupName, string key)
        {
            CacheManager cacheManager = GetCacheManagerByGroupName(groupName);
            if (cacheManager != null)
            {
                return cacheManager.GetData(key);
            }
            return null;
        }

        public bool Contains(string groupName, string key)
        {
            CacheManager cacheManager = GetCacheManagerByGroupName(groupName);
            if (cacheManager != null)
            {
                return cacheManager.Contains(key);
            }
            return false;
        }

        public long Size(string groupName)
        {
            CacheManager cacheManager = GetCacheManagerByGroupName(groupName);
            if (cacheManager != null)
            {
                return cacheManager.Count;
            }
            return 0;
        }

        public void Flush(string groupName)
        {
            CacheManager cacheManager = GetCacheManagerByGroupName(groupName);
            if (cacheManager != null)
            {
                cacheManager.Flush();
            }
        }

        public void Update(string groupName, string key, object value)
        {
            Remove(groupName, key);
            Add(groupName, key, value);
        }

        #endregion

        public List<CacheManager> GetCaches()
        {
            List<CacheManager> cacheManagers = new List<CacheManager>();

            lock (caches)
            {
                foreach (KeyValuePair<string, CacheManager> pair in caches)
                {
                    cacheManagers.Add(pair.Value);
                }
            }

            return cacheManagers;
        }

        public CacheManager GetCacheManagerByGroupName(string groupName)
        {
            lock (caches)
            {
                if (caches.ContainsKey(groupName))
                    return caches[groupName];

                CacheManagerFactory cacheManagerFactory = new CacheManagerFactory(GetCacheConfiguration(groupName));
                CacheManager cacheManager = cacheManagerFactory.Create(groupName);
                if (cacheManager != null)
                    caches.Add(groupName, cacheManager);
                return cacheManager;
            }
        }

        internal DictionaryConfigurationSource GetCacheConfiguration(string policyName)
        {
            DictionaryConfigurationSource configurationSource = new DictionaryConfigurationSource();

            CacheManagerSettings cacheConfiguration = new CacheManagerSettings();
            cacheConfiguration.DefaultCacheManager = policyName;

            Type nullBackingStoreType = Type.GetType(new NullBackingStore().GetType().AssemblyQualifiedName);
            CacheStorageData cacheStorageData = new CacheStorageData("MyCache", nullBackingStoreType);
            cacheConfiguration.BackingStores.Add(cacheStorageData);

            LocalCachePolicy policy = cacheSettingManager.GetLocalCachePolicy(policyName);

            CacheManagerData cacheManagerData = new CacheManagerData(policyName, policy.ExpiredSeconds,
                                                                     policy.MaxElements,
                                                                     policy.NumToRemoveWhileScavenging, "MyCache");
            cacheConfiguration.CacheManagers.Add(cacheManagerData);

            configurationSource.Add(CacheManagerSettings.SectionName, cacheConfiguration);
            return configurationSource;
        }

        public void Stop()
        {
            lock (caches)
            {
                foreach (string key in caches.Keys)
                {
                    caches[key].Dispose();
                }

                caches.Clear();
            }
        }
    }
}
