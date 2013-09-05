using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Cache.Exceptions
{
    public class CacheModeNotFoundException : Exception
    {
        public CacheModeNotFoundException()
        {
        }

        public CacheModeNotFoundException(string message)
            : base(message)
        {
        }

        public CacheModeNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
