using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Web.UrlEngine.UrlRuleEngine
{
    public interface IUrlRuleEngine
    {
        void Execute(HttpContext httpContext);
    }
}
