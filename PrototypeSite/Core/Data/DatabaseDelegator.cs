using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.Unity;

namespace Core.Data
{
    public class DatabaseDelegator
    {
        private string dbName;

        [InjectionConstructor]
        public DatabaseDelegator()
            : this(string.Empty)
        {

        }

        public DatabaseDelegator(string name)
        {
            this.dbName = name;
        }

        public Database CreateDatabase()
        {
            if(string.IsNullOrEmpty(dbName))
            {
                return DatabaseFactory.CreateDatabase();
            }
            return DatabaseFactory.CreateDatabase(dbName);
        }
    }
}
