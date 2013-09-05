using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Web.Utils
{
    public class RewriteHelper
    {
        public static void Rewrite(string url, string reason)
        {
            HttpContext.Current.Response.AppendHeader("RewriteReason", reason);
            HttpContext.Current.Response.AppendHeader("RewriteReason", "Redirect to " + HttpUtility.HtmlEncode(url));

            HttpContext.Current.RewritePath(url, false);
        }
    }
}
