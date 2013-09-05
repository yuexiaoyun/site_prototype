using System;
using System.Collections.Generic;
using System.Text;
using Core.Cache;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Core.Interceptor
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheAttribute : HandlerAttribute
    {
        private CacheMode cacheMode;
        private CacheAction cacheAction = CacheAction.FETCH;
        private string groupName;
        private string name;

        public CacheAttribute(string groupName, string name,CacheMode cacheMode, CacheAction cacheAction)
        {
            this.groupName = groupName;
            this.name = name;
            this.cacheMode = cacheMode;
            this.cacheAction = cacheAction;
        }

        public CacheAttribute(string groupName, string name, CacheMode cacheMode)
        {
            this.cacheMode = cacheMode;
            this.groupName = groupName;
            this.name = name;
        }

        public CacheMode CacheMode
        {
            get { return cacheMode; }
        }

        public CacheAction CacheAction
        {
            get { return cacheAction; }
        }

        public string GroupName
        {
            get { return groupName; }
        }

        public string Name
        {
            get { return name; }
        }

        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return container.Resolve<CacheCallHandler>();
        }
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public class CacheKeyAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Parameter)]
    public class CacheValueAttribute : Attribute
    {
        
    }
}
