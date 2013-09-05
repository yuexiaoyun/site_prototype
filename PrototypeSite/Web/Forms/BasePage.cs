using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using Core;
using Core.Ioc;
using Microsoft.Practices.Unity;
using Web.Session;

namespace Web.Forms
{
    public class BasePage : Page
    {
        protected RequestContext RequestContext
        {
            get { return RequestContext.GetContext(); }
        }

        private SessionManager sessionManager;
        [Dependency]
        public SessionManager SessionManager
        {
            set { sessionManager = value; }
        }

        private CookieManager cookieManager;
        [Dependency]
        public CookieManager CookieManager
        {
            set { cookieManager = value; }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            Container container = (Container)Application[Container.CONTAINER];
            container.BuildUp(this);

            EnableViewState = ViewStateEnabled();

            Response.AddHeader("P3P", "CP=CAO PSA OUR");
        }

        protected string QueryString(string name)
        {
            if(Request.QueryString[name] == null)
            {
                return HttpUtility.HtmlEncode(Request[name]);
            }
            return HttpUtility.HtmlEncode(Request.QueryString[name]);
        }

        private bool ViewStateEnabled()
        {
            return GetType().GetCustomAttributes(typeof(ViewStateEnableAttribute), true).Length > 0;
        }

        protected override object SaveViewState()
        {
            if (!ViewStateEnabled())
                return null;
            else
                return base.SaveViewState();
        }

        protected override void LoadViewState(object savedState)
        {
            if (!ViewStateEnabled())
                return;
            else
                base.LoadViewState(savedState);
        }

        protected override object LoadPageStateFromPersistenceMedium()
        {
            if (!ViewStateEnabled())
                return null;
            else
                return base.LoadPageStateFromPersistenceMedium();
        }

        protected override void SavePageStateToPersistenceMedium(object state)
        {
            if (!ViewStateEnabled())
                return;
            base.SavePageStateToPersistenceMedium(state);
        }
    }
}
