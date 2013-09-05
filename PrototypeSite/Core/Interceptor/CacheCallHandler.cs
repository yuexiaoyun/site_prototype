using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Core.Cache;
using Core.Cache.Exceptions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using log4net;

namespace Core.Interceptor
{
    public class CacheCallHandler : ICallHandler
    {
        private readonly static ILog logger = LogManager.GetLogger("Cache");

        private CacheMetaLoader cacheMetaLoader;
        
        [Dependency]
        public CacheMetaLoader CacheMetaLoader
        {
            set { cacheMetaLoader = value; }
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            CacheMeta cacheMeta = cacheMetaLoader.Load(input.MethodBase);

            if(cacheMeta == null)
            {
                logger.Error("CacheMeta is null", new ArgumentNullException());
                return getNext()(input, getNext);
            }

            CacheMode cacheMode = cacheMeta.CacheMode;

            ICacheHelper cacheHelper = ChooseCacheHelper(cacheMode);
            if (cacheHelper == null)
            {
                logger.Error(cacheMode + "cache type not found", new CacheModeNotFoundException());
                return getNext()(input, getNext);
            }

            cacheHelper = new CacheProxy(cacheMode, cacheHelper);
            CacheAction cacheAction = cacheMeta.CacheAction;
            string cacheKey = BuildCacheKey(cacheMeta, input.Arguments);
            string groupName = cacheMeta.CacheGroup;
            string cacheType = cacheHelper.GetType().Name;

            logger.Debug(string.Format("Choose {0} as cache helper, cacheGroup={1}, cacheKey={2}", cacheType, cacheMeta.CacheGroup, cacheKey));

            if(cacheAction == CacheAction.FETCH)
            {
                object obj;
                try
                {
                    obj = cacheHelper.Get(groupName, cacheKey);
                }
                catch (Exception ex)
                {
                    logger.Error("Get cache failed, cacheType=" + cacheType, ex);
                    return getNext()(input, getNext);
                }

                if(obj != null)
                {
                    logger.Debug(string.Format("Hitting cache, cacheGroup={0}, cacheKey={1}", groupName, cacheKey));
                    return input.CreateMethodReturn(obj, null);
                }
                else
                {
                    IMethodReturn methodReturn = getNext()(input, getNext);
                    if (methodReturn.Exception == null && methodReturn.ReturnValue != null)
                    {
                        cacheHelper.Add(groupName, cacheKey, methodReturn.ReturnValue);
                        logger.Info(string.Format("Adding to cache, cacheGroup={0}, cacheKey={1}", groupName, cacheKey));
                    }
                    return methodReturn;
                }
            }

            else if (cacheAction == CacheAction.UPDATE)
            {
                object cacheValue = GetCacheValue(input.Arguments);
                if(cacheValue != null)
                {
                    cacheHelper.Update(groupName, cacheKey, cacheValue);
                    logger.Info(string.Format("Update cache, cacheGroup={0}, cacheKey={1}", groupName, cacheKey));
                }
                return getNext()(input, getNext);
            }

            else if (cacheAction == CacheAction.CLEAR)
            {
                IMethodReturn methodReturn = getNext()(input, getNext);
                cacheHelper.Remove(groupName, cacheKey);
                logger.Info(string.Format("Remove cache, cacheGroup={0}, cacheKey={1}", groupName, cacheKey));
                return methodReturn;
            }

            return getNext()(input, getNext);
        }

        object GetCacheValue(IParameterCollection inputs)
        {
            for (int i = 0; i < inputs.Count; i++)
            {
                ParameterInfo parameterInfo = inputs.GetParameterInfo(i);
                CacheValueAttribute[] cacheValueAttributes = parameterInfo.ParameterType.GetCustomAttributes(typeof (CacheValueAttribute), false) as CacheValueAttribute[];
                if (cacheValueAttributes == null || cacheValueAttributes.Length <= 0) continue;
                return inputs[i];
            }

            return null;
        }

        string BuildCacheKey(CacheMeta cacheMeta, IParameterCollection inputs)
        {
            StringBuilder cacheKeyBuilder = new StringBuilder();
            cacheKeyBuilder.Append(cacheMeta.Name);

            for (int i = 0; i < inputs.Count; i++)
            {
                ParameterInfo parameterInfo = inputs.GetParameterInfo(0);
                PropertyInfo[] propertyInfos = cacheMeta.GetCacheKey(parameterInfo);

                if(propertyInfos != null)
                {
                    if(propertyInfos.Length == 0)
                    {
                        cacheKeyBuilder.Append(";");
                        cacheKeyBuilder.Append(parameterInfo.Name).Append("=").Append(inputs[i]);
                    }
                    else
                    {
                        foreach (PropertyInfo propertyInfo in propertyInfos)
                        {
                            cacheKeyBuilder.Append(";");
                            cacheKeyBuilder.Append(propertyInfo.Name).Append("=").Append(propertyInfo.GetValue(
                                inputs[i], null));
                        }
                    }
                }
            }

            return cacheKeyBuilder.ToString().Replace(" ", "").ToUpper();
        }

        public int Order { get; set; }

        private ICacheHelper ChooseCacheHelper(CacheMode mode)
        {
            switch (mode)
            {
                case CacheMode.LOCAL:
                    return localCacheHelper;
                case CacheMode.REMOTE:
                    return remoteCacheHelper;
                case CacheMode.SESSION:
                    return sessionCacheHelper;
                case CacheMode.REQUEST:
                    return requestCacheHelper;
            }

            return null;
        }

        #region cache helpers
        private ICacheHelper localCacheHelper;
        private ICacheHelper remoteCacheHelper;
        private ICacheHelper sessionCacheHelper;
        private ICacheHelper requestCacheHelper;

        [Dependency(CacheContainerKey.Local_Cache)]
        public ICacheHelper LocalCacheHelper
        {
            get { return localCacheHelper; }
            set { localCacheHelper = value; }
        }

        [Dependency(CacheContainerKey.Remote_Cache)]
        public ICacheHelper RemoteCacheHelper
        {
            get { return remoteCacheHelper; }
            set { remoteCacheHelper = value; }
        }

        //[Dependency(CacheContainerKey.Session_Cache)]
        //public ICacheHelper SessionCacheHelper
        //{
        //    get { return sessionCacheHelper; }
        //    set { sessionCacheHelper = value; }
        //}

        //[Dependency(CacheContainerKey.Request_Cache)]
        //public ICacheHelper RequestCacheHelper
        //{
        //    get { return requestCacheHelper; }
        //    set { requestCacheHelper = value; }
        //}
        #endregion
    }
}
