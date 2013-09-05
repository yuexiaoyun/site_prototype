using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Util
{
    public class StringUtils
    {
        private static Regex htmlTagRegex = new Regex(@"<(.|\n)+?>", RegexOptions.Compiled);
        private static Regex suspectCharRegex = new Regex("[;]", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static string ReplaceSuspectChar(string input)
        {
            if (input == null) input = string.Empty;
            //return Regex.Replace(input, "[\"*;^>\\(\\)]", string.Empty, RegexOptions.IgnoreCase);

            //we only need to replace semicolon, it's the task parameter separated character
            return suspectCharRegex.Replace(input, string.Empty);
        }

        public static string EscapeQuote(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            return input.Replace("\"", "\"\"");
        }

        public static string ToUpperForFirstWord(string words)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(words);
        }


        public static string RemoveSQLInjection(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            return input.Replace("'", "''");
        }
        #region UrlEncode
        internal static bool IsSafe(char ch)
        {
            if ((((ch >= 'a') && (ch <= 'z')) || ((ch >= 'A') && (ch <= 'Z'))) || ((ch >= '0') && (ch <= '9')))
            {
                return true;
            }
            switch (ch)
            {
                case '\'':
                case '(':
                case ')':
                case '*':
                case '-':
                case '.':
                case '_':
                case '!':
                    return true;
            }
            return false;
        }
        internal static char IntToHex(int n)
        {
            if (n <= 9)
            {
                return (char)(n + 0x30);
            }
            return (char)((n - 10) + 0x61);
        }
        private static byte[] UrlEncodeBytesToBytesInternal(byte[] bytes, int offset, int count, bool alwaysCreateReturnValue)
        {
            int num = 0;
            int num2 = 0;
            for (int i = 0; i < count; i++)
            {
                char ch = (char)bytes[offset + i];
                if (ch == ' ')
                {
                    num++;
                }
                else if (!IsSafe(ch))
                {
                    num2++;
                }
            }
            if ((!alwaysCreateReturnValue && (num == 0)) && (num2 == 0))
            {
                return bytes;
            }
            byte[] buffer = new byte[count + (num2 * 2)];
            int num4 = 0;
            for (int j = 0; j < count; j++)
            {
                byte num6 = bytes[offset + j];
                char ch2 = (char)num6;
                if (IsSafe(ch2))
                {
                    buffer[num4++] = num6;
                }
                else if (ch2 == ' ')
                {
                    buffer[num4++] = 0x2b;
                }
                else
                {
                    buffer[num4++] = 0x25;
                    buffer[num4++] = (byte)IntToHex((num6 >> 4) & 15);
                    buffer[num4++] = (byte)IntToHex(num6 & 15);
                }
            }
            return buffer;
        }
        public static byte[] UrlEncodeToBytes(string str, Encoding e)
        {
            if (str == null)
            {
                return null;
            }
            byte[] bytes = e.GetBytes(str);
            return UrlEncodeBytesToBytesInternal(bytes, 0, bytes.Length, false);
        }
        public static string UrlEncode(string str, Encoding e)
        {
            if (str == null)
            {
                return null;
            }
            return Encoding.ASCII.GetString(UrlEncodeToBytes(str, e));
        }
        public static string UrlEncode(string str)
        {
            if (str == null)
            {
                return null;
            }
            return UrlEncode(str, Encoding.UTF8);
        }
        #endregion
        public static string UrlEncodeAmpersand(string inputPathInfo)
        {
            return inputPathInfo.Replace("&", "%26");
        }

        public static string XmlReplace(string xmlstring)
        {
            if (string.IsNullOrEmpty(xmlstring))
                return string.Empty;
            if (xmlstring.Contains("&apos;"))
            {
                xmlstring = xmlstring.Replace("&apos;", "'");
            }
            if (xmlstring.Contains("&") && !xmlstring.Contains("&amp;"))
                return xmlstring.Replace("&", "&amp;");

            return xmlstring;
        }

        public static string Left(string str, int length)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            return str.Length > length ? str.Substring(0, length) : str;
        }

        public static string Right(string str, int length)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;
            return str.Length > length ? str.Substring(str.Length - length, length) : str;
        }

        public static string RemoveHtmlTag(string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            return htmlTagRegex.Replace(value, "").Replace("\r\n", "").Replace("\"", "&quot;").Replace("'", "&#39;");
        }

        /// <summary>
        /// revert the escape characters back
        /// </summary>
        /// <param name="str"></param>
        /// <returns>reverted string</returns>
        public static string RevertChar(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            return str.Replace("&amp;", "&").Replace("&lt;", "<").Replace("&gt;", ">");
        }

        public static Dictionary<string, string> SplitStringIntoDictionary(Dictionary<string, string> dic, string keyList, string splitChar)
        {
            if (string.IsNullOrEmpty(keyList) || string.IsNullOrEmpty(splitChar))
            {
                if (dic == null)
                    dic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                return dic;
            }

            string[] keys = keyList.Split(new string[] { splitChar }, StringSplitOptions.RemoveEmptyEntries);

            if (dic == null)
                dic = new Dictionary<string, string>(keys.Length + 10, StringComparer.OrdinalIgnoreCase);
            else
                dic.Clear();

            foreach (string key in keys)
            {
                if (dic.ContainsKey(key.Trim()))
                    continue;

                dic.Add(key.Trim(), null);
            }
            return dic;
        }

        /// <summary>
        /// 将 int类型的List对象封装成以<paramref name="separator"/>分隔的字符串
        /// </summary>
        /// <param name="separator">分隔符</param>
        /// <param name="items">需要拼接成字符串的List对象</param>
        /// <returns></returns>
        public static string JoinListAsString(string separator, List<int> items)
        {
            if (items == null || items.Count < 1)
            {
                return string.Empty;
            }
            int[] arrItems = items.ToArray();
            string[] strArrary = Array.ConvertAll<int, string>(arrItems, Convert.ToString);
            return string.Join(separator, strArrary);
        }

        public static string JoinListAsString<T>(string separator, List<T> items)
        {
            if (items == null || items.Count < 1)
            {
                return string.Empty;
            }
            T[] arrItems = items.ToArray();
            string[] strArrary = Array.ConvertAll<T, string>(arrItems, ConvertAll);
            return string.Join(separator, strArrary);
        }

        public static string ConvertAll<T>(T item)
        {
            return item.ToString();
        }

        public static string ShowLimitLength(int maxLength, string separator, params string[] texts)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string s in texts)
            {
                if (string.IsNullOrEmpty(s))
                    continue;

                if (stringBuilder.Length + s.Length > maxLength)
                {
                    int len = maxLength - stringBuilder.Length;
                    int lastIndex = s.Substring(0, len).LastIndexOfAny(" ,.!'\"".ToCharArray());

                    if (lastIndex > 0)
                    {
                        if (stringBuilder.Length > 0)
                        {
                            stringBuilder.Append(separator);
                        }
                        stringBuilder.Append(s.Substring(0, lastIndex));
                    }
                    stringBuilder.Append("...");
                    return stringBuilder.ToString();
                }
                else
                {
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Append(separator);
                    }
                    stringBuilder.Append(s);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
