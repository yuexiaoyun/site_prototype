using System;
using System.Collections.Generic;
using System.Text;
using Core.Cache.Exceptions;
using Microsoft.Practices.Unity;
using log4net;

namespace Core.Cache
{
    public class CacheProxy : ICacheHelper
    {
        private static readonly ILog Logger = LogManager.GetLogger("Cache");
        
        private ICacheHelper cacheHelper;
        private CacheMode cacheMode;

        public CacheProxy(CacheMode cacheMode, ICacheHelper cacheHelper)
        {
            this.cacheMode = cacheMode;
            this.cacheHelper = cacheHelper;
        }

        #region cache helper member
        public void Add(string groupName, string key, object value)
        {
            try
            {
                cacheHelper.Add(groupName, key, value);
            }
            catch (Exception ex)
            {
                Logger.Error(cacheMode + "Cache Error", ex);
            }
        }

        public void Remove(string groupName, string key)
        {
            try
            {
                cacheHelper.Remove(groupName, key);
            }
            catch (Exception ex)
            {
                Logger.Error(cacheMode + "Cache Error", ex);
            }
        }

        public object Get(string groupName, string key)
        {
            try
            {
                return cacheHelper.Get(groupName, key);
            }
            catch (Exception ex)
            {
                Logger.Error(cacheMode + "Cache Error", ex);
                return null;
            }
        }

        public bool Contains(string groupName, string key)
        {
            try
            {
                return cacheHelper.Contains(groupName, key);
            }
            catch (Exception ex)
            {
                Logger.Error(cacheMode + "Cache Error", ex);
                return false;
            }
        }

        public long Size(string groupName)
        {
            try
            {
                return cacheHelper.Size(groupName);
            }
            catch (Exception ex)
            {
                Logger.Error(cacheMode + "Cache Error", ex);
                return 0;
            }
        }

        public void Flush(string groupName)
        {
            try
            {
                cacheHelper.Flush(groupName);
            }
            catch (Exception ex)
            {
                Logger.Error(cacheMode + "Cache Error", ex);
            }
        }

        public void Update(string groupName, string key, object value)
        {
            try
            {
                cacheHelper.Update(groupName, key, value);
            }
            catch (Exception ex)
            {
                Logger.Error(cacheMode + "Cache Error", ex);
            }
        }

        #endregion

    }
}
