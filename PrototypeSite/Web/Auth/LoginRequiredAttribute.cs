using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Auth
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class LoginRequiredAttribute : Attribute
    {

    }
}
