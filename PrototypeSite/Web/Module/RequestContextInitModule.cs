using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Core;

namespace Web.Module
{
    public class RequestContextInitModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += Context_BeginRequest;
        }

        void Context_BeginRequest(object sender, EventArgs e)
        {
            RequestContext.GetContext().Clear();
        }

        public void Dispose()
        {
            //do nothing
        }
    }
}
