using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.Http
{
    public class HttpConstans
    {
        public static int DEFAULT_REQUEST_TIMEOUT = 5000;
        
        public static string CONTENT_TYPE_JSON = "application/json";
        public static string CONTENT_TYPE_XML = "text/xml";
        public static string CONTENT_TYPE_HTML = "text/html";
        public static string CONTENT_TYPE_PLAIN = "text/plain";
        public static string CONTENT_TYPE_FORM = "application/x-www-form-urlencoded;charset=utf-8";

        public static string METHOD_GET = "GET";
        public static string METHOD_POST = "POST";
        public static string METHOD_PUT = "PUT";
        public static string METHOD_DELETE = "DELETE";
        public static string METHOD_HEAD = "HEAD";
    }
}
