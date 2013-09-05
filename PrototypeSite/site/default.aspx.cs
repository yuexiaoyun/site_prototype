using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Core.Ioc;
using Web.Forms;
using log4net;
using Microsoft.Practices.Unity;

namespace site
{
    public partial class _default : BasePage
    {
        private ILog logger = LogManager.GetLogger(typeof (_default));

        private UserManager userManager;

        [Dependency]
        public UserManager UserManager
        {
            set { userManager = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Container container = Application[Container.CONTAINER] as Container;
            //if (container != null)
            //    userManager = container.GetInstance<UserManager>();
            logger.Debug("This is a debug log");
            logger.Info("This is a information log");
            logger.Error("This is a error log");
            logger.Fatal("This is a fatal log");

            int result = userManager.GetUserCount();

            Response.Write("Result:" + result);
        }
    }
}