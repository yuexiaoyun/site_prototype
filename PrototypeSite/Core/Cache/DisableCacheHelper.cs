using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Cache
{
    public class DisableCacheHelper : ICacheHelper
    {
        public void Add(string groupName, string key, object value)
        {
            
        }

        public void Remove(string groupName, string key)
        {
            
        }

        public object Get(string groupName, string key)
        {
            return null;
        }

        public bool Contains(string groupName, string key)
        {
            return false;
        }

        public long Size(string groupName)
        {
            return 0;
        }

        public void Flush(string groupName)
        {
            
        }

        public void Update(string groupName, string key, object value)
        {
            
        }
    }
}
