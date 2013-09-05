using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using log4net;

namespace Web.Log
{
    public class LoggingModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.Error += LogException_ContextError;
        }

        public void Dispose()
        {
        }

        public void LogException_ContextError(object sender, EventArgs e)
        {
            HttpApplication context = sender as HttpApplication;
            Exception exception = context.Server.GetLastError();
            if (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }

            ILog logger = LogManager.GetLogger("GlobalLog");
            logger.Error(exception.Message, exception);
        }
    }
}
