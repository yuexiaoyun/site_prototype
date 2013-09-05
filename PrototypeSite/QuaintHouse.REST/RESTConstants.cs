using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.REST
{
    public static class RESTConstants
    {
        public static string HEADER_REQUEST_ID = "request-id";
        public static string HEADER_CLIENT_ID = "client-id";
        public static string HEADER_TIMESTAMP = "timestamp";
        public static string HEADER_CLIENT_SIGNATURE = "client-signature";

        public static string TIMESTAMP_FORMAT = "MM/dd/yyyy'T'HH:mm:ss";
        public static string JSON_DATETIME_FORMAT = "MM/dd/yyyy'T'HH:mm:ss";
    }
}
