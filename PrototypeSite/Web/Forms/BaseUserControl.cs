using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Core;
using Core.Ioc;
using Web.Utils;

namespace Web.Forms
{
    public class BaseUserControl : UserControl
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Container container = (Container) Application[Container.CONTAINER];
            container.BuildUp(this);
        }

        protected string QueryString(string name)
        {
            if (Request.QueryString[name] == null)
            {
                return HttpUtility.HtmlEncode(Request[name]);
            }
            return HttpUtility.HtmlEncode(Request.QueryString[name]);
        }

        protected RequestContext RequestContext
        {
            get { return RequestContext.GetContext(); }
        }
    }
}
