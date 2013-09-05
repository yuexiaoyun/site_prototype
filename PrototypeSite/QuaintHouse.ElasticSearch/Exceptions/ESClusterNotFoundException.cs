using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.ElasticSearch.Exceptions
{
    public class ESClusterNotFoundException : Exception
    {
        public ESClusterNotFoundException()
        {
        }

        public ESClusterNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ESClusterNotFoundException(string message) : base(message)
        {
        }
    }
}
