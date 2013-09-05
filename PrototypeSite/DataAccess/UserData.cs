using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Core.Data;

namespace DataAccess
{
    public class UserData : BaseData
    {
        public virtual int GetUserCount()
        {
            DbCommand dbCommand = database.GetSqlStringCommand("select count(1) from dbo.[user] with(NOLOCK)");
            int result = ExecuteScalar<int>(dbCommand);

            return result;
        }
    }
}
