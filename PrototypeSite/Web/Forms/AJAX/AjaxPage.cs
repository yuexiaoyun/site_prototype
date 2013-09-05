using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;
using Core.Util;
using Microsoft.Practices.Unity;
using Web.Forms.AJAX.Result;
using log4net;

namespace Web.Forms.AJAX
{
    public class AjaxPage : BasePage
    {
        private static ILog logger = LogManager.GetLogger(typeof (AjaxPage));
        private static readonly JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        private IResult result;
        private const string METHOD = "method";

        private ActionSelector actionSelector;
        [Dependency]
        public ActionSelector ActionSelector
        {
            set { actionSelector = value; }
        }

        protected override void OnError(EventArgs e)
        {
            Exception lastException = Server.GetLastError();
            while (lastException is TargetInvocationException)
            {
                lastException = lastException.InnerException;
            }

            if(lastException != null)
            {
                logger.Error("Ajax Error", lastException);
            }

            Server.ClearError();
        }

        protected override void OnLoad(EventArgs e)
        {
            string actionName = QueryString(METHOD);

            logger.Debug("ActionName is " + actionName);

            MethodInfo action = actionSelector.SelectAction(GetType(), actionName, Request.HttpMethod);

            logger.Debug("Execute action");

            result = ExecuteAction(action, Request.QueryString);

            logger.Debug("Execute action end");
        }

        internal IResult ExecuteAction(MethodInfo action, NameValueCollection parameters)
        {
            ParameterInfo[] parameterInfos = action.GetParameters();

            List<object> paramValue = new List<object>();
            foreach (ParameterInfo parameterInfo in parameterInfos)
            {
                foreach (string key in parameters.Keys)
                {
                    if(parameterInfo.Name.Equals(key))
                        paramValue.Add(Convert.ChangeType(parameters[key], parameterInfo.ParameterType));
                }
            }

            object result = action.Invoke(this, paramValue.ToArray());

            return result as IResult;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Cache-Control", "no-cache");

            if(result is AjaxResult)
            {
                base.Render(writer);
            }
            else if (result is StringResult)
            {
                Response.Write(((StringResult) result).Result);
            }
            else if (result is JsonResult)
            {
                Response.ContentType = "application/json";
                Response.Write(jsSerializer.Serialize(((JsonResult) result).Result));
            }
            else if(result is XmlResult)
            {
                Response.ContentType = "text/xml";
                Response.Write(XMLUtility.Serialize(((XmlResult) result).Result));
            }
        }

    }
}
