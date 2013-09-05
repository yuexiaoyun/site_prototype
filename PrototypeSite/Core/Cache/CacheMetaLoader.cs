using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Core.Interceptor;

namespace Core.Cache
{
    public class CacheMetaLoader
    {
        private static readonly Hashtable CacheMetas = Hashtable.Synchronized(new Hashtable());

        internal CacheMeta Load(MethodBase methodBase)
        {
            lock (CacheMetas.SyncRoot)
            {
                string key = methodBase.DeclaringType.Module.Name + methodBase.MetadataToken;

                if (CacheMetas.ContainsKey(key))
                    return CacheMetas[key] as CacheMeta;

                CacheAttribute[] cacheAttribute = methodBase.GetCustomAttributes(typeof(CacheAttribute), false) as CacheAttribute[];
                if (cacheAttribute == null || cacheAttribute.Length <= 0)
                    return null;

                CacheMeta cacheMeta = new CacheMeta();
                cacheMeta.Name = cacheAttribute[0].Name;
                cacheMeta.CacheGroup = cacheAttribute[0].GroupName;
                cacheMeta.CacheMode = cacheAttribute[0].CacheMode;
                cacheMeta.CacheAction = cacheAttribute[0].CacheAction;

                ParameterInfo[] parameterInfos = methodBase.GetParameters();
                foreach (ParameterInfo parameterInfo in parameterInfos)
                {
                    CacheKeyAttribute[] cacheKeyAttributes = parameterInfo.GetCustomAttributes(typeof(ParameterInfo), false) as CacheKeyAttribute[];
                    if (cacheKeyAttributes == null || cacheKeyAttributes.Length <= 0)
                        continue;

                    if(parameterInfo.ParameterType.IsPrimitive || parameterInfo.ParameterType == typeof(Type))
                    {
                        cacheMeta.AddCacheKey(parameterInfo, new PropertyInfo[0]);
                    }
                    else
                    {
                        PropertyInfo[] propertyInfos = parameterInfo.ParameterType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);
                        cacheMeta.AddCacheKey(parameterInfo, 
                            Array.FindAll(propertyInfos, delegate(PropertyInfo p) { return p.GetCustomAttributes(typeof(CacheKeyAttribute), false).Length > 0; }));
                    }
                }

                CacheMetas.Add(key, cacheMeta);
                return cacheMeta;
            }
        }
    }
}
