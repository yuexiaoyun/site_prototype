using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Text;
using System.Web;

namespace Web.Utils
{
    public class RedirectHelper
    {
        private static bool AddStackTraceToRedirectHeader
        {
            get
            {
                string addStackTraceToRedirectHeader = ConfigurationManager.AppSettings["AddStackTraceToRedirectHeader"];
             
                if (string.IsNullOrEmpty(addStackTraceToRedirectHeader)) return false;

                return bool.Parse(addStackTraceToRedirectHeader);
            }
        }

        public static void Do301Redirect(string targetUrl, string reason)
        {
            HttpResponse httpResponse = HttpContext.Current.Response;
            httpResponse.Clear();
            httpResponse.StatusCode = 301;
            httpResponse.Status = "301 Moved Permanently";
            httpResponse.Headers.Add("Location", targetUrl);
            httpResponse.Headers.Add("RedirectReason", reason);

            if(AddStackTraceToRedirectHeader)
                httpResponse.AddHeader("RedirectReason", new StackTrace(false).ToString());

            httpResponse.End();
        }

        public static void Do302Redirect(string targetUrl, string reason)
        {
            HttpResponse httpResponse = HttpContext.Current.Response;
            httpResponse.Clear();
            httpResponse.Headers.Add("RedirectReason", reason);

            if (AddStackTraceToRedirectHeader)
                httpResponse.AddHeader("RedirectReason", new StackTrace(false).ToString());

            httpResponse.Redirect(targetUrl);
        }

        public static void Redirect(string targetUrl, string reason)
        {
            Do302Redirect(targetUrl, reason);
        }
    }
}
