using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.REST.Exceptions
{
    public class RESTServiceApplicationException : Exception
    {
        public RESTServiceApplicationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
