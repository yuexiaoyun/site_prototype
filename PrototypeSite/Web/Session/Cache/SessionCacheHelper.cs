using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Core.Cache;

namespace Web.Session.Cache
{
    public class SessionCacheHelper : ICacheHelper
    {
        public void Add(string groupName, string key, object value)
        {
            HttpContext.Current.Session.Add(groupName + key, value);
        }

        public void Remove(string groupName, string key)
        {
            HttpContext.Current.Session.Remove(groupName + key);
        }

        public object Get(string groupName, string key)
        {
            return HttpContext.Current.Session[groupName + key];
        }

        public bool Contains(string groupName, string key)
        {
            return HttpContext.Current.Session[groupName + key] != null;
        }

        public long Size(string groupName)
        {
            return HttpContext.Current.Session.Keys.Count;
        }

        public void Flush(string groupName)
        {
            //do nothing
        }

        public void Update(string groupName, string key, object value)
        {
            HttpContext.Current.Session[groupName + key] = value;
        }
    }
}
