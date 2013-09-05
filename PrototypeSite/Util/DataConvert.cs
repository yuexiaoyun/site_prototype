using System;
using System.Globalization;

namespace Util
{
    /// <summary>
    /// Provides a set of APIs to convert datas.
    /// </summary>
    public static class DataConvert
    {
        /// <summary>
        /// Formats text with culture-specific formatting information.
        /// </summary>
        /// <param name="value">Text need to convert.</param>
        /// <returns>Text with culture-specific formatting information.</returns>
        public static string ToString(string value)
        {
            return string.Format(CultureInfo.CurrentCulture, "{0}", value);
        }
        /// <summary>
        /// Formats integer with culture-specific formatting informationn.
        /// </summary>
        /// <param name="value">Integer need to format.</param>
        /// <returns>Integer with culture-specific formatting information.</returns>
        public static string ToString(int value)
        {
            return Convert.ToString(value, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Formats value of this instance with culture-specific formatting information.
        /// </summary>
        /// <param name="value">Object instance need to format.</param>
        /// <returns>Text value of this instance with culture-specific formatting information.</returns>
        public static string ToString(object value)
        {
            return Convert.ToString(value, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Converts text into nullable datetime instance with culture-specific formatting information.
        /// If text parameter is empty or null, it will return null.
        /// </summary>
        /// <param name="value">Text of datetime.</param>
        /// <returns>Nullable datetime instance.</returns>
        public static DateTime? ToNullableDateTime(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            if (string.IsNullOrEmpty(value.Trim()))
            {
                return null;
            }
            DateTime result;
            if (DateTime.TryParse(value, out result))
            {
                return result;
            }
            return null;
        }
        /// <summary>
        /// Converts this instance into datetime instance.
        /// </summary>
        /// <param name="value">Object instance need to convert.</param>
        /// <returns>Datetime instance.</returns>
        public static DateTime ToDateTime(object value)
        {
            return Convert.ToDateTime(value, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Converts text into nullable integer with culture-specific formatting information.
        /// If text parameter is empty or null, it will return null.
        /// </summary>
        /// <param name="value">Text need to convert.</param>
        /// <returns>Nullable integer with culture-specific formatting information.</returns>
        public static int? ToNullableInt32(object value)
        {
            if (value == null)
            {
                return null;
            }
            if (string.IsNullOrEmpty(value.ToString().Trim()))
            {
                return null;
            }
            return Convert.ToInt32(value, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Converts this instance into integer with culture-specific formatting information.
        /// </summary>
        /// <param name="value">Object instance need to convert.</param>
        /// <returns>Integer with culture-specific formatting information.</returns>
        public static int ToInt32(object value)
        {
            if (value == null)
            {
                return -1;
            }
            if (string.IsNullOrEmpty(value.ToString()))
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(value, CultureInfo.CurrentCulture);
            }
        }
        /// <summary>
        /// Converts nullable object into integer with culture-specific formatting information.
        /// If the nullable integer parameter is null, it will return 0.
        /// </summary>
        /// <param name="value">Nullable object instance need to convert.</param>
        /// <returns>Integer with culture-specific formatting information.</returns>
        public static int ToInt32(int? value)
        {
            if (string.IsNullOrEmpty(value.ToString()))
            {
                return 0;
            }
            return Convert.ToInt32(value, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Converts text into nullable decimal with culture-specific formatting information.
        /// If the text parameter is null or empty, it will return null either.
        /// If the text parameter starts with "$", "$" will be removed.
        /// </summary>
        /// <param name="value">Text need to convert.</param>
        /// <returns>Nullable decimal with culture-specific formatting information.</returns>
        public static decimal? ToNullableDecimal(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            if (string.IsNullOrEmpty(value.Trim()))
            {
                return null;
            }
            if (value.StartsWith("$"))
            {
                value = value.Replace("$", "");
            }
            if (value.EndsWith("%"))
            {
                value = value.Replace("%", "");
                return Convert.ToDecimal(value, CultureInfo.CurrentCulture) / 100;
            }
            return Convert.ToDecimal(value, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Converts text into decimal with culture-specific formatting information.
        /// If the text parameter is empty or null, it will return 0.
        /// It the text parameter starts with "$", "$" will be removed.
        /// </summary>
        /// <param name="value">Text need to convert.</param>
        /// <returns>Decimal with culture-specific formatting information.</returns>
        public static decimal ToDecimal(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }
            if (value.StartsWith("$"))
            {
                value = value.Replace("$", "");
            }
            if (value.EndsWith("%"))
            {
                value = value.Replace("%", "");
                return Convert.ToDecimal(value, CultureInfo.CurrentCulture) / 100;
            }
            return Convert.ToDecimal(value, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts datatime instance into short datetime text.
        /// If the datetime parameter is minimum, it will return empty text.
        /// </summary>
        /// <param name="value">Datetime instance need to convert.</param>
        /// <returns></returns>
        public static string ToString(DateTime value)
        {
            if (value == DateTime.MinValue)
            {
                return "";
            }
            else
            {
                return value.ToShortDateString();
            }
        }
        /// <summary>
        /// Converts text into short datetime text with culture-specific formatting information.
        /// </summary>
        /// <param name="value">Text need to convert.</param>
        /// <returns>Short datetime text with culture-specific formatting information.</returns>
        public static string ToDateString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }
            else
            {
                return Convert.ToDateTime(value, CultureInfo.CurrentCulture).ToShortDateString();
            }
        }
        /// <summary>
        /// Converts text into boolean value.
        /// If the parameter value equals "Y" or "y", it will return true.
        /// Otherwise if the parameter value equals "N" or "n", it will return false.
        /// </summary>
        /// <param name="value">Text need to convert.</param>
        /// <returns>Boolean value that represnets the parameter value.</returns>
        public static bool ToBoolean(object value)
        {
            if (value == null) return false;
            if (!string.IsNullOrEmpty(value.ToString()))
            {
                if (value.ToString().Equals("Y", StringComparison.OrdinalIgnoreCase)) return true;
                else if (value.ToString().Equals("TRUE", StringComparison.OrdinalIgnoreCase)) return true;
            }
            return false;
        }
        /// <summary>
        /// Converts the nullable boolean value to an equivalent boolean value.
        /// </summary>
        /// <param name="value">Nullable value need to convert.</param>
        /// <returns>Boolean value that is equivalent the parameter.</returns>
        public static bool ToNullableBoolean(bool? value)
        {
            return Convert.ToBoolean(value, CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Converts boolean value into an equivalent text.
        /// If the parameter is true, it will return "Y". Else it will return "N".
        /// </summary>
        /// <param name="value">Boolean value need to convert.</param>
        /// <returns>Text that is equivalent the parameter.</returns>
        public static string ToBooleanString(bool value)
        {
            if (value)
            {
                return "Y";
            }
            else
            {
                return "N";
            }
        }
        /// <summary>
        /// Converts the parameter text into boolean text.
        /// If the parameter equals "Y" or "true" ignored case, it will return "Y".
        /// Otherwise it will return "N", even if the parameter text is empty or null.
        /// </summary>
        /// <param name="value">Text need to convert.</param>
        /// <returns>Text that is equivalent the parameter text.</returns>
        public static string ToBooleanString(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "N";
            }
            if (value.Equals("Y", StringComparison.OrdinalIgnoreCase) || value.Equals("true", StringComparison.OrdinalIgnoreCase) || value.Equals("yes", StringComparison.OrdinalIgnoreCase))
            {
                return "Y";
            }
            return "N";
        }
        /// <summary>
        /// Converts nullable integer into text.
        /// If the integer parameter is null, it will return empty text.
        /// </summary>
        /// <param name="value">Nullable integer need to convert.</param>
        /// <returns>Text that is equivalent the parameter integer.</returns>
        public static string ToString(int? value)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                return value.ToString();
            }
        }
        /// <summary>
        /// Converts decimal into text with culture-specific formatting information.
        /// If the the decimal parameter is 0, it will return empty text.
        /// </summary>
        /// <param name="value">Decimal need to convert.</param>
        /// <returns>Text that is equivalent the decimal parameter, with culture-specific formatting information.</returns>
        public static string ToString(decimal value)
        {
            if (value == 0)
            {
                return "";
            }
            else
            {
                return value.ToString(CultureInfo.CurrentCulture);
            }
        }
        /// <summary>
        /// Converts char into text.
        /// If the char parameter is space or "\0", it will return empty text.
        /// </summary>
        /// <param name="value">Char need to convert.</param>
        /// <returns>Text that is equivalent the char parameter.</returns>
        public static string ToString(char value)
        {
            if (value == ' ' || value == '\0')
            {
                return "";
            }
            else
            {
                return value.ToString();
            }
        }
        /// <summary>
        /// Converts nullable decimal into text.
        /// </summary>
        /// <param name="value">Nullable decimal need to convert.</param>
        /// <returns>Text that is equivalent the decimal parameter.</returns>
        public static string ToString(decimal? value)
        {
            if (value == null)
            {
                return "";
            }
            else
            {
                return value.ToString();
            }
        }
        /// <summary>
        /// Converts text with filter.
        /// </summary>
        /// <param name="value">Text need to convert.</param>
        /// <param name="tip">Tip text of this field.</param>
        /// <returns>If value equals tip text, then empty will be returned. Otherwrise the whole text will be returned.</returns>
        public static string ToStringWithFilter(string value, string tip)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            if (value.Equals(tip))
            {
                return string.Empty;
            }
            return value;
        }

        /// <summary>
        /// Converts text into nullable char.
        /// If the text parameter is null or empty, it will return null.
        /// If length of text is more 1, it will return first char of text.
        /// </summary>
        /// <param name="value">Text need to convert.</param>
        /// <returns>First char of the text parameter.</returns>
        public static char? ToChar(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            else
            {
                return value[0];
            }
        }
        /// <summary>
        /// Converts nullable datetime instance into date text of USA, with culture-specific formatting information.
        /// If the datetime instance is null, it will return empty text.
        /// </summary>
        /// <param name="dateTime">Nullable datetime instance need to convert.</param>
        /// <returns>Text that is equivalent the datetime instance, with culture-specific formatting information.</returns>
        public static string ToUsaDateString(DateTime? dateTime)
        {
            if (!dateTime.HasValue) return string.Empty;
            return dateTime.Value.ToString("MM/dd/yyyy", CultureInfo.CurrentCulture);
        }
        /// <summary>
        /// Converts text into null.
        /// If the text parameter is null or empty, it will return null too.
        /// Otherwise the intact text parameter will be returned.
        /// </summary>
        /// <param name="value">Text need to convert.</param>
        /// <returns>Nullable text.</returns>
        public static string ConvertToNull(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return value;
        }
        /// <summary>
        /// Verifies whether the text is datetime format.
        /// </summary>
        /// <param name="value">Text need to verify.</param>
        /// <returns>Value that indicates whether the text parameter is datetime format.</returns>
        public static bool IsDateTimeFormat(string value)
        {
            DateTime dateTime;
            return DateTime.TryParse(value, out dateTime);
        }

        /// <summary>
        /// Convert integer into five numeric chars.
        /// If length of integer value is less than 5, 0 will be padded on the left.
        /// </summary>
        /// <param name="order">Integer need to convert.</param>
        /// <returns>Five numeric chars with culture-specific formatting information.</returns>
        public static string FillOrderField(int order)
        {
            if (order.ToString(CultureInfo.CurrentCulture).Length < 5)
            {
                return order.ToString(CultureInfo.CurrentCulture).PadLeft(5, '0');
            }
            return order.ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Verifies whether the text parameter is with decimal format.
        /// </summary>
        /// <param name="value">Text need to verify.</param>
        /// <returns>Boolean value that represents whether the text parameter is with decimal format.</returns>
        public static bool IsDecimal(string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            if (value.StartsWith("$"))
            {
                value = value.Replace("$", "");
            }
            value = value.Replace("%", "");
            decimal temp;
            return Decimal.TryParse(value, out temp);
        }
        /// <summary>
        /// Verifies whether the text parameter is with integer format.
        /// </summary>
        /// <param name="value">Text need to verify.</param>
        /// <returns>Boolean value that represents whether the text parameter is with integer format.</returns>
        public static bool IsInteger(string value)
        {
            if (string.IsNullOrEmpty(value)) return true;
            int temp;
            return int.TryParse(value, out temp);
        }
        /// <summary>
        /// Switch between two texts.
        /// If the first text is empty or null, the second text will be returned. 
        /// </summary>
        /// <param name="first">First string.</param>
        /// <param name="second">Second string.</param>
        /// <returns>One of two text parameters.</returns>
        public static string SwitchString(string first, string second)
        {
            if (!string.IsNullOrEmpty(first)) return first;
            return second;
        }
        /// <summary>
        /// Converts this instance into datetime instance.
        /// </summary>
        /// <param name="value">Object instance need to convert.</param>
        /// <returns>float instance.</returns>
        public static float ToSingle(object value)
        {
            return Convert.ToSingle(value, CultureInfo.CurrentCulture);
        }

        public static int TryToInt(object input, int defaultValue)
        {
            if (input == null) return defaultValue;
            int value;
            if (!int.TryParse(input.ToString(), out value)) value = defaultValue;
            return value;
        }

        public static int TryToInt(object input)
        {
            if (input == null) return 0;
            int value;
            if (!int.TryParse(input.ToString(), out value)) value = 0;
            return value;
        }

        public static decimal TryToDecimal(object input, decimal defaultValue)
        {
            if (input == null) return defaultValue;

            if (input.ToString().StartsWith("$")) input = input.ToString().Replace("$", "");

            decimal value;
            if (!decimal.TryParse(input.ToString(), out value)) value = defaultValue;
            return value;
        }
    }
}