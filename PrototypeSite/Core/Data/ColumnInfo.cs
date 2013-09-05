using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Data
{
    public class ColumnInfo
    {
        private string columnName;
        private Type columnType;

        public ColumnInfo(string columnName, Type columnType)
        {
            this.columnName = columnName;
            this.columnType = columnType;
        }

        public string ColumnName
        {
            get { return columnName; }
            set { columnName = value; }
        }

        public Type ColumnType
        {
            get { return columnType; }
            set { columnType = value; }
        }
    }
}
