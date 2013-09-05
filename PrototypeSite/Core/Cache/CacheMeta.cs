using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Core.Cache
{
    public class CacheMeta
    {
        private string cacheGroup;
        private string name;
        private CacheMode cacheMode;
        private CacheAction cacheAction;

        private readonly Dictionary<string, PropertyInfo[]> cacheKeys = new Dictionary<string, PropertyInfo[]>();

        public string CacheGroup
        {
            get { return cacheGroup; }
            set { cacheGroup = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public CacheMode CacheMode
        {
            get { return cacheMode; }
            set { cacheMode = value; }
        }

        public CacheAction CacheAction
        {
            get { return cacheAction; }
            set { cacheAction = value; }
        }

        public void AddCacheKey(ParameterInfo parameterInfo, PropertyInfo[] propertyInfos)
        {
            string key = parameterInfo.ParameterType.Module.Name + parameterInfo.MetadataToken;
            cacheKeys.Add(key, propertyInfos);
        }

        public PropertyInfo[] GetCacheKey(ParameterInfo parameterInfo)
        {
            string key = parameterInfo.ParameterType.Module.Name + parameterInfo.MetadataToken;

            if (!cacheKeys.ContainsKey(key))
                return null;

            return cacheKeys[key];
        }
    }
}
