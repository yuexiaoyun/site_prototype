using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Web.Ioc
{
    public class PageHttpHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }

        public bool IsReusable
        {
            get { throw new NotImplementedException(); }
        }
    }
}
