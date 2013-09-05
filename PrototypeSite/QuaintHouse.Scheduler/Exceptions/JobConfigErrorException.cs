using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.Scheduler.Exceptions
{
    public class JobConfigErrorException : Exception
    {
        public JobConfigErrorException(string message)
            : base(message)
        {
        }

        public JobConfigErrorException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
