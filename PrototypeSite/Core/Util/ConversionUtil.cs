using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Util
{
    public class ConversionUtil
    {
        public static T To<T>(object value)
        {
            if (Convert.IsDBNull(value))
            {
                return default(T);
            }

            if (typeof(T) == typeof(char) || typeof(T) == typeof(char?))
            {
                value = ((string)value)[0];
            }
            else if (typeof(T) == typeof(bool))
            {
                if (value != null && ("Y".Equals(value.ToString(), StringComparison.OrdinalIgnoreCase) || "TRUE".Equals(value.ToString(), StringComparison.OrdinalIgnoreCase)))
                    value = true;
                else
                    value = false;
            }
            else if (typeof(T) == typeof(string))
            {
                if (value == null)
                {
                    value = string.Empty;
                }
            }
            try
            {
                return (T)value;
            }
            catch
            {
                return (T)Convert.ChangeType(value, typeof(T));
            }
        }
    }
}
