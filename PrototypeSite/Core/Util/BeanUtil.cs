using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace Core.Util
{
    public class BeanUtil
    {
        public static void FillBeanFieldsByDataRow(object bean, DataRow dataRow)
        {
            Type type = bean.GetType();
            foreach (PropertyInfo property in type.GetProperties())
            {
                string name = property.Name;
                if (property.PropertyType.IsPrimitive || property.PropertyType == typeof(string) || property.PropertyType == typeof(int) || property.PropertyType == typeof(decimal) || property.PropertyType == typeof(DateTime)
                    || property.PropertyType == typeof(int?) || property.PropertyType == typeof(decimal?) || property.PropertyType == typeof(DateTime?))
                {
                    if (!dataRow.Table.Columns.Contains(name)) continue;
                    if (dataRow.IsNull(name)) continue;
                    property.SetValue(bean, dataRow[name], null);
                }
            } 
        }
    }
}
