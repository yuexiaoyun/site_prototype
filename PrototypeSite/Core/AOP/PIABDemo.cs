using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Text;

namespace Core.AOP
{
    public class PIABDemo
    {

    }

    public class PIABRealProxy<T> : RealProxy
    {
        private T target;

        public PIABRealProxy(T target)
        {
            this.target = target;
        }

        public override IMessage Invoke(IMessage msg)
        {
            Console.WriteLine("The injected pre-operation is invoked.");

            IMethodCallMessage callMessage = (IMethodCallMessage) msg;
            object returnValue = callMessage.MethodBase.Invoke(this.target, callMessage.Args);

            Console.WriteLine("The injected post-operation is invoked.");

            return new ReturnMessage(returnValue, new object[0], 0, null, callMessage);
        }
    }

    public static class PIABFactory
    {
        public static T Create<T>()
        {
            T instance = Activator.CreateInstance<T>();
            PIABRealProxy<T> realProxy = new PIABRealProxy<T>(instance);
            T transparentProxy = (T) realProxy.GetTransparentProxy();
            return transparentProxy;
        }
    }


}
