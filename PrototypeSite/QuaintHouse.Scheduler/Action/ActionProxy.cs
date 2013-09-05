using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Ioc;
using Microsoft.Practices.Unity;
using QuaintHouse.Scheduler.Action.Interceptor;

namespace QuaintHouse.Scheduler.Action
{
    public class ActionProxy
    {
        private IAction action;
        private ActionContext actionContext;
        private IEnumerator enumerator;

        [Dependency]
        public IInterceptor[] Interceptors
        {
            set
            {
                Array.Sort(value, new InterceptorComparer());
                enumerator = value.GetEnumerator();
            }
        }

        [Dependency]
        public IAction Action
        {
            set { action = value; }
            get { return action; }
        }

        public ActionContext ActionContext
        {
            get { return actionContext; }
            set { actionContext = value; }
        }

        public void Execute()
        {
            if (enumerator.MoveNext())
            {
                ((IInterceptor) enumerator.Current).Intercepte(this);
            }
            else
            {
                enumerator.Reset();
                action.Execute(actionContext);
            }
        }
    }
}
