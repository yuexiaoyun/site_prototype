using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace QuaintHouse.REST.Exceptions
{
    public class RESTServiceClientJsonException : Exception
    {
        public RESTServiceClientJsonException(string message, JsonException innerException)
            : base(message, innerException)
        {

        }
    }
}
