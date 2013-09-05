using System;
using System.Collections.Generic;
using System.Text;
using Core.Cache;
using Core.Interceptor;
using DataAccess;
using Microsoft.Practices.Unity;

namespace Business
{
    public class UserManager : BaseManager
    {
        private UserData userData;

        [Dependency]
        public UserData UserData
        {
            set { userData = value; }
        }

        [Cache("User", "GetUserCount", CacheMode.LOCAL)]
        public virtual int GetUserCount()
        {
            return userData.GetUserCount();
        }
    }
}
