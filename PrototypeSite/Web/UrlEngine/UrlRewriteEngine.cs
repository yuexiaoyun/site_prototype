using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Microsoft.Practices.Unity;
using Web.UrlEngine.UrlRuleEngine;

namespace Web.UrlEngine
{
    public class UrlRewriteEngine
    {
        private readonly List<IUrlRuleEngine> urlRuleEngines = new List<IUrlRuleEngine>();
        [Dependency]
        public IUrlRuleEngine[] UrlRuleEngines
        {
            set { urlRuleEngines.AddRange(value); }
            get { return urlRuleEngines.ToArray(); }
        }

        public void Rewrite(HttpContext httpContext)
        {
            foreach (IUrlRuleEngine urlRuleEngine in UrlRuleEngines)
            {
                urlRuleEngine.Execute(httpContext);
            }
        }
    }
}
