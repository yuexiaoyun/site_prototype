using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.Unity;

namespace Core.Data
{
    public abstract class DistributedBaseData : BaseData
    {
        [Dependency("CustomedDb")]
        public new DatabaseDelegator DatabaseDelegator
        {
            set { database = value.CreateDatabase(); }
        }
    }
}
