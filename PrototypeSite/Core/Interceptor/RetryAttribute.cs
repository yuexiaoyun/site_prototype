using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Core.Interceptor
{
    [AttributeUsage(AttributeTargets.Method)]
    public class RetryAttribute : HandlerAttribute
    {
        private int retryTimes;
        private Type type;

        public RetryAttribute(int retryTimes, Type type)
        {
            this.retryTimes = retryTimes;
            this.type = type;
        }

        public int RetryTimes
        {
            get { return retryTimes; }
        }

        public Type Type
        {
            get { return type; }
        }

        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return container.Resolve<RetryCallHandler>();
        }
    }
}
