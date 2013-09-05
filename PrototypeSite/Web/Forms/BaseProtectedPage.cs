using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Microsoft.Practices.Unity;
using Web.Auth;
using Web.Session;
using Web.Utils;
using HttpUtility = Web.Utils.HttpUtility;

namespace Web.Forms
{
    [LoginRequired]
    public class BaseProtectedPage : BasePage
    {
        private SessionManager sessionManager;
        private string defaultOpenPage;

        [Dependency]
        public SessionManager SessionManager
        {
            set { sessionManager = value; }
            get { return sessionManager; }
        }

        [Dependency("DefaultOpenPage")]
        public string DefaultOpenPage
        {
            set { defaultOpenPage = value; }
        }

        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            CheckLogin();
        }

        protected virtual void CheckLogin()
        {
            LoginRequiredAttribute[] attributes =
                (LoginRequiredAttribute[]) GetType().GetCustomAttributes(typeof (LoginRequiredAttribute), true);

            if(attributes.Length > 0)
            {
                if (sessionManager.Identity == null)
                {
                    string returnUrl = HttpUtility.HtmlEncode(HttpContext.Current.Request.RawUrl);
                    string redirectUrl = string.Format("{0}?returnUrl={1}", defaultOpenPage, returnUrl);
                    RedirectHelper.Redirect(redirectUrl, "Not Login");
                }
            }
        }


    }
}
