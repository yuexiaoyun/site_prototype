using System;
using System.Collections.Generic;
using System.Web;
using Business;
using Core.Ioc;
using Microsoft.Practices.Unity;
using Web;

namespace site.Base
{
    public class WebInitializer : BaseWebInitializer 
    {
        protected override void InitSite(Container container)
        {
            RegisterLifeCycle(container);
            RegisterUrlRewrite(container);
            RegisterSearchService(container);
        }

        private void RegisterLifeCycle(Container container)
        {
            container.RegisterLifeCycle("Business", "DataAccess");
        }

        private void RegisterUrlRewrite(Container container)
        {
            //todo
        }

        private void RegisterSearchService(Container container)
        {
            //todo elastic search
        }

        public override string[] CacheAssemblyNames
        {
            get { return new[] {"Core", "Web", "Business", "DataAccess"}; }
        }
    }
}