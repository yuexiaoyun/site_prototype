using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using Core.Ioc;

namespace Web.Ioc
{
    public class UnityHttpHandlerFactory : IHttpHandlerFactory
    {
        public IHttpHandler GetHandler(HttpContext context, string requestType, string url, string pathTranslated)
        {
            IHttpHandlerFactory pageHandlerFactory = CreateFactory();

            IHttpHandler pageHandler = pageHandlerFactory.GetHandler(context, requestType, url, pathTranslated);
            BuildUpPage(pageHandler);

            return pageHandler;
        }

        public void ReleaseHandler(IHttpHandler handler)
        {
            IHttpHandlerFactory pageHandlerFactory = CreateFactory();
            pageHandlerFactory.ReleaseHandler(handler);
        }

        private IHttpHandlerFactory CreateFactory()
        {
            IHttpHandlerFactory pageHandlerFactory = Activator.CreateInstance(typeof(PageHandlerFactory), true) as IHttpHandlerFactory;
            if (pageHandlerFactory == null)
            {
                throw new ApplicationException("Create PageHandlerFactory failed");
            }
            return pageHandlerFactory;
        }

        private void BuildUpPage(IHttpHandler page)
        {
            Container container = HttpContext.Current.Application[Container.CONTAINER] as Container;
            if (container == null)
            {
                throw new ApplicationException("Container in application context is null.");
            }

            container.BuildUp(page, page.GetType().BaseType);
        }
    }
}
