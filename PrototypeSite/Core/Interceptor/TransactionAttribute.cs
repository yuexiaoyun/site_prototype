using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace Core.Interceptor
{
    [AttributeUsage(AttributeTargets.Method)]
    public class TransactionAttribute : HandlerAttribute
    {
        private TransactionScope scope;

        public TransactionScope GetTransactionScope()
        {
            return scope;
        }

        public TransactionAttribute()
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = IsolationLevel.ReadCommitted;

            scope = new TransactionScope(TransactionScopeOption.Required, options);
        }

        public TransactionAttribute(IsolationLevel level)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = level;

            scope = new TransactionScope(TransactionScopeOption.Required, options);
        }

        public TransactionAttribute(IsolationLevel level, TimeSpan timeOut)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = level;
            options.Timeout = timeOut;

            scope = new TransactionScope(TransactionScopeOption.Required, options);
        }

        public TransactionAttribute(IsolationLevel level, TransactionScopeOption scopeOption)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = level;

            scope = new TransactionScope(scopeOption, options);
        }

        public TransactionAttribute(IsolationLevel level, TimeSpan timeOut, TransactionScopeOption scopeOption)
        {
            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = level;
            options.Timeout = timeOut;

            scope = new TransactionScope(scopeOption, options);
        }

        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return container.Resolve<TransactionCallHandler>();
        }
    }
}
