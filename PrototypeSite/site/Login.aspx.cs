﻿using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace site
{
    public partial class Login : System.Web.UI.Page
    {
        protected string Chaos;
        protected void Page_Load(object sender, EventArgs e)
        {
            Chaos = "Hello\'s";
        }
    }
}