using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.Scheduler.Action.Interceptor
{
    public interface IInterceptor
    {
        void Intercepte(ActionProxy actionProxy);

        int Order { get; }
    }

    public class InterceptorComparer : IComparer<IInterceptor>
    {
        public int Compare(IInterceptor x, IInterceptor y)
        {
            if (x.Order > y.Order)
                return 1;
            if (x.Order < y.Order)
                return -1;
            return 0;
        }
    }
}
