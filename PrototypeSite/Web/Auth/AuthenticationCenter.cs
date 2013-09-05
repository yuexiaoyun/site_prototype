using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.Unity;
using Web.Auth.Entity;
using Web.Session;

namespace Web.Auth
{
    public class AuthenticationCenter
    {
        private SessionManager sessionManager;
        
        [Dependency]
        public SessionManager SessionManager
        {
            set { sessionManager = value; }
        }

        public void SignIn(string userName)
        {
            Identity curIdentity = new Identity(userName, true);

            sessionManager.SetIdentity(curIdentity);
        }

        public void SignOut()
        {
            sessionManager.RemoveIdentity();
        }
    }
}
