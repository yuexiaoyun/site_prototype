using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.Scheduler.Action.Log
{
    public interface ILog
    {
        void Log(string message);
        void Log(string message, Exception exception);
        void Flush();
    }
}
