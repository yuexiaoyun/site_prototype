using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.REST.Exceptions
{
    public class RESTServiceClientException : Exception
    {
        public RESTServiceClientException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
