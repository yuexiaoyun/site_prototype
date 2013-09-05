using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using Core.Ioc;
using Microsoft.Practices.Unity;
using Web.Utils;

namespace Web.Module
{
    public class CustomErrorPageModule : IHttpModule
    {
        private string fatalErrorPage;
        [Dependency]
        public string FatalErrorPage
        {
            set { fatalErrorPage = value; }
        }

        public void Init(HttpApplication context)
        {
            Container container = context.Application[Container.CONTAINER] as Container;
            container.BuildUp(this);

            context.Error += Context_Error;
        }

        private void Context_Error(object sender, EventArgs e)
        {
            HttpApplication httpApplication = sender as HttpApplication;

            HttpException lastError = httpApplication.Server.GetLastError() as HttpException;
            if (lastError == null)
            {
                return;
            }

            if (500 != lastError.GetHttpCode())
            {
                return;
            }

            CustomErrorsSection customErrorsSection = WebConfigurationManager.OpenWebConfiguration("/").GetSection("system.web/customErrors") as CustomErrorsSection;
            if (customErrorsSection == null)
            {
                return;
            }

            CustomError customError = customErrorsSection.Errors[lastError.GetHttpCode()];
            string customErroPage;
            if (customError == null)
            {
                customErroPage = customErrorsSection.DefaultRedirect;
            }
            else
            {
                customErroPage = customError.Redirect;
            }

            if (IsCurrentPageErrorPage(httpApplication.Request.RawUrl, customErroPage))
            {
                RedirectHelper.Redirect(fatalErrorPage, "Fatal Error");
            }
        }

        private bool IsCurrentPageErrorPage(string requestUrl, string customErroPage)
        {
            if (string.IsNullOrEmpty(customErroPage))
            {
                return false;
            }
            
            customErroPage = customErroPage.TrimStart('~');
            return requestUrl.StartsWith(customErroPage, StringComparison.OrdinalIgnoreCase);
        }

        public void Dispose()
        {
            //do nothing
        }
    }
}
