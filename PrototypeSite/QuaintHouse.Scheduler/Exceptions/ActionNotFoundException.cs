using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.Scheduler.Exceptions
{
    public class ActionNotFoundException : Exception
    {
        public ActionNotFoundException(string message)
            : base(message)
        {
        }

        public ActionNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
