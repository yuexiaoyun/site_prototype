using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Auth.Entity
{
    public class Identity
    {
        private string name;
        private bool isAuthenticated;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public bool IsAuthenticated
        {
            get { return isAuthenticated; }
            set { isAuthenticated = value; }
        }

        public Identity(string name, bool isAuthenticated)
        {
            this.name = name;
            this.isAuthenticated = isAuthenticated;
        }
    }
}
