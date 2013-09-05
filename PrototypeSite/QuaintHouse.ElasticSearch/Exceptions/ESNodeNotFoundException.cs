using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.ElasticSearch.Exceptions
{
    public class ESNodeNotFoundException : Exception
    {
        public ESNodeNotFoundException()
        {
        }

        public ESNodeNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public ESNodeNotFoundException(string message) : base(message)
        {
        }
    }
}
