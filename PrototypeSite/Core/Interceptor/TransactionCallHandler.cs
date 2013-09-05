using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Microsoft.Practices.Unity.InterceptionExtension;
using log4net;

namespace Core.Interceptor
{
    public class TransactionCallHandler : ICallHandler
    {
        private ILog logger = LogManager.GetLogger("Global");

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            TransactionAttribute transactionAttribute =
                input.MethodBase.GetCustomAttributes(typeof (TransactionAttribute), false)[0] as TransactionAttribute;

            string methodName = string.Format("{0}.{1}", input.MethodBase.ReflectedType, input.MethodBase.Name);

            logger.Debug("Transaction call handler, method: " + methodName);

            using (TransactionScope scope = transactionAttribute.GetTransactionScope())
            {
                IMethodReturn result;
                try
                {
                    result = getNext()(input, getNext);
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    logger.Error("Transaction call handler, method: " + methodName + ", execption: " + ex.Message);
                    throw ex;
                }
                finally
                {
                    scope.Dispose();
                }

                return result;
            }
        }

        public int Order { get; set; }
    }
}
