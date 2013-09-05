using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace Web.Utils
{
    public class HttpUtility
    {
        public static bool IsAjaxRequest()
        {
            string requestWith = HttpContext.Current.Request.Headers["X-Requested-With"];
            return "XMLHttpRequest".Equals(requestWith, StringComparison.OrdinalIgnoreCase);
        }

        public static string HtmlEncode(string plaintText)
        {
            return System.Web.HttpUtility.HtmlEncode(plaintText);
        }
    }
}
