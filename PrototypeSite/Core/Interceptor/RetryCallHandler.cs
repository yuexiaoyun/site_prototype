using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Core.Interceptor
{
    public class RetryCallHandler : ICallHandler
    {
        private int retryTimes;

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            RetryAttribute retryAttribute =
                (RetryAttribute) input.MethodBase.GetCustomAttributes(typeof (RetryAttribute), false)[0];

            IMethodReturn returnValue = DoInvoke(input, getNext);
            if(returnValue.Exception != null && IsExceptionTypeMatch(returnValue.Exception.GetType(), retryAttribute))
            {
                if (retryTimes >= retryAttribute.RetryTimes)
                    return returnValue;

                retryTimes++;

                Invoke(input, getNext);
            }

            return returnValue;
        }

        private bool IsExceptionTypeMatch(Type exceptionType, RetryAttribute retryAttribute)
        {
            return exceptionType == retryAttribute.Type || exceptionType.IsSubclassOf(retryAttribute.Type);
        }

        private IMethodReturn DoInvoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            return getNext()(input, getNext);
        }

        public int Order { get; set; }
    }
}
