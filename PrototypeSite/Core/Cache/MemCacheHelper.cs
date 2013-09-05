using System;
using System.Collections.Generic;
using System.Text;
using BeIT.MemCached;
using Core.Cache.Settings;
using Microsoft.Practices.Unity;

namespace Core.Cache
{
    public class MemCacheHelper : ICacheHelper
    {
        private object lockObj;

        private string[] servers;

        private CacheSettingManager cacheSettingManager;

        [Dependency("MemcachedServers")]
        public string Server
        {
            set { servers = value.Split(','); }
        }

        [Dependency]
        public CacheSettingManager CacheSettingManager
        {
            set { cacheSettingManager = value; }
        }

        public void Add(string groupName, string key, object value)
        {
            MemcachedClient client = GetMemcachedClient(groupName);

            RemoteCachePolicy policy = cacheSettingManager.GetRemoteCachePolicy(groupName);

            if(policy != null && policy.AbsoluteExpirationTimeInSecond > 0)
            {
                client.Add(key, value, DateTime.Now.AddSeconds(policy.AbsoluteExpirationTimeInSecond));
            }
            else
            {
                client.Add(key, value);
            }
        }

        public void Remove(string groupName, string key)
        {
            MemcachedClient client = GetMemcachedClient(groupName);
            client.Delete(key);
        }

        public object Get(string groupName, string key)
        {
            MemcachedClient client = GetMemcachedClient(groupName);
            return client.Get(key);
        }

        public bool Contains(string groupName, string key)
        {
            MemcachedClient client = GetMemcachedClient(groupName);
            return client.Get(key) != null;
        }

        public long Size(string groupName)
        {
            return 0;
        }

        public void Flush(string groupName)
        {
            MemcachedClient client = GetMemcachedClient(groupName);
            client.FlushAll();
        }

        public void Update(string groupName, string key, object value)
        {
            MemcachedClient client = GetMemcachedClient(groupName);

            RemoteCachePolicy policy = cacheSettingManager.GetRemoteCachePolicy(groupName);

            if (policy != null && policy.AbsoluteExpirationTimeInSecond > 0)
            {
                client.Set(key, value, DateTime.Now.AddSeconds(policy.AbsoluteExpirationTimeInSecond));
            }
            else
            {
                client.Set(key, value);
            }
        }

        internal MemcachedClient GetMemcachedClient(string groupName)
        {
            lock (lockObj)
            {
                if (!MemcachedClient.Exists(groupName))
                    MemcachedClient.Setup(groupName, servers);

                MemcachedClient client = MemcachedClient.GetInstance(groupName);
                client.KeyPrefix = groupName + "_";
                client.ConnectTimeout = 5000;
                client.SendReceiveTimeout = 10000;
                client.MinPoolSize = 1;
                client.MaxPoolSize = 1;

                return client;
            }
        }
    }
}
