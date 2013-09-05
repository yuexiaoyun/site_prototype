using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Core.Cache;

namespace Web.Session.Cache
{
    public class RequestCacheHelper : ICacheHelper
    {
        public void Add(string groupName, string key, object value)
        {
            HttpContext.Current.Items[groupName + key] = value;
        }

        public void Remove(string groupName, string key)
        {
            HttpContext.Current.Items.Remove(groupName + key);
        }

        public object Get(string groupName, string key)
        {
            return HttpContext.Current.Items[groupName + key];
        }

        public bool Contains(string groupName, string key)
        {
            return HttpContext.Current.Items.Contains(groupName + key);
        }

        public long Size(string groupName)
        {
            return 0;
        }

        public void Flush(string groupName)
        {
            //do nothing
        }

        public void Update(string groupName, string key, object value)
        {
            HttpContext.Current.Items[groupName + key] = value;
        }
    }
}
