using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Practices.Unity;
using Core.Util;

namespace Core.Cache.Settings
{
    public class CacheSettingManager
    {
        private Dictionary<string, LocalCachePolicy> localCachePolicys;
        private Dictionary<string, RemoteCachePolicy> remoteCachePolicys;

        [Dependency("CacheSettingFile")]
        public string CacheSettingFile
        {
            set
            {
                string xml = File.ReadAllText(string.Format("{0}\\{1}", AppDomain.CurrentDomain.BaseDirectory, value));
                CachePolicys policys = (CachePolicys)XMLUtility.Deserialize(xml, typeof(CachePolicys));
                localCachePolicys = new Dictionary<string, LocalCachePolicy>();
                foreach (LocalCachePolicy localCachePolicy in policys.LocalCachePolicys)
                {
                    localCachePolicys.Add(localCachePolicy.PolicyName.ToLower(), localCachePolicy);
                }
                remoteCachePolicys = new Dictionary<string, RemoteCachePolicy>();
                foreach (RemoteCachePolicy remoteCachePolicy in policys.RemoteCachePolicys)
                {
                    remoteCachePolicys.Add(remoteCachePolicy.PolicyName.ToLower(), remoteCachePolicy);
                }
            }
        }

        public virtual LocalCachePolicy GetLocalCachePolicy(string policyName)
        {
            string key = (!string.IsNullOrEmpty(policyName)) && localCachePolicys.ContainsKey(policyName.ToLower()) ? policyName.ToLower() : "default";
            return localCachePolicys[key];
        }

        public virtual RemoteCachePolicy GetRemoteCachePolicy(string policyName)
        {
            string key = (!string.IsNullOrEmpty(policyName)) && remoteCachePolicys.ContainsKey(policyName.ToLower()) ? policyName.ToLower() : "default";
            return remoteCachePolicys[key];
        }
    }
}
