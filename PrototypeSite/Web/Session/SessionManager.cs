using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Web.Auth.Entity;

namespace Web.Session
{
    public class SessionManager
    {
        public Identity Identity
        {
            get { return Contain(SessionKeyConsts.Identity) ? GetSession<Identity>(SessionKeyConsts.Identity) : null; }
        }

        public void SetIdentity(Identity identity)
        {
            SetSession(SessionKeyConsts.Identity, identity);
        }

        public void RemoveIdentity()
        {
            DeleteSession(SessionKeyConsts.Identity);
        }

        public string GetSessionAsString(string name)
        {
            return GetSession<string>(name);
        }

        public void AddSession(string name, object value)
        {
            SetSession(name, value);
        }

        public void RemoveSession(string name)
        {
            DeleteSession(name);
        }

        private static T GetSession<T>(string name)
        {
            if (Contain(name))
                return (T) HttpContext.Current.Session[name];
            return default(T);
        }

        private static bool Contain(string name)
        {
            return HttpContext.Current.Session[name] != null;
        }

        private static void SetSession(string name, object value)
        {
            HttpContext.Current.Session.Add(name, value);
        }

        private static void DeleteSession(string name)
        {
            if (!Contain(name))
                return;
            HttpContext.Current.Session.Remove(name);
        }
    }
}
