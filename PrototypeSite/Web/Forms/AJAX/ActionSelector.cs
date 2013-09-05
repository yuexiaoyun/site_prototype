using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Core.Cache;
using Core.Interceptor;

namespace Web.Forms.AJAX
{
    public class ActionSelector
    {
        [Cache("System", "AjaxMethodInfo", CacheMode.LOCAL)]
        public virtual MethodInfo SelectAction([CacheKey]Type type, [CacheKey]string actionName, [CacheKey]string httpMethod)
        {
            MethodInfo[] allMethods =
                type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod);


            MethodInfo[] actionMethods = Array.FindAll(allMethods,
                                                 m => m.Name == actionName && m.GetCustomAttributes(typeof (ActionMethodAttribute), false).Length > 0);

            if(actionMethods.Length == 0)
            {
                throw new ArgumentException("Action not found, action name = " + actionName);
            }

            return actionMethods[0];
        }
    }
}
