using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Core.Ioc;
using Microsoft.Practices.Unity;
using Web.UrlEngine;
using log4net;
using HttpUtility = Web.Utils.HttpUtility;

namespace Web.Module
{
    public class UrlRewriteModule : IHttpModule
    {
        private readonly ILog logger = LogManager.GetLogger(typeof (UrlRewriteModule));

        private UrlRewriteEngine urlRewriteEngine;
        [Dependency]
        public UrlRewriteEngine UrlRewriteEngine
        {
            set { urlRewriteEngine = value; }
        }

        public void Init(HttpApplication context)
        {
            Container container = context.Application[Container.CONTAINER] as Container;
            container.BuildUp(this);
            context.BeginRequest += Application_BeginRequest;
        }

        private void Application_BeginRequest(object sender, EventArgs e)
        {
            try
            {
                logger.Debug("Start url rewrite");

                HttpApplication application = sender as HttpApplication;

                if(IsBlockFilePath(application.Context.Request.FilePath))
                    return;

                if (HttpUtility.IsAjaxRequest()) 
                    return;

                urlRewriteEngine.Rewrite(application.Context);

                logger.Debug("End url rewrite");
            }
            catch (Exception ex)
            {
                logger.Error("Rewrite url failed", ex);
            }
            
        }

        private bool IsBlockFilePath(string filePath)
        {
            if (filePath.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            if (filePath.EndsWith(".png", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            if (filePath.EndsWith(".gif", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            if (filePath.EndsWith(".js", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            if (filePath.EndsWith(".css", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            if (filePath.EndsWith(".bmp", StringComparison.CurrentCultureIgnoreCase))
            {
                return true;
            }
            return false;
        }

        public void Dispose()
        {
        }
    }
}
