using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuaintHouse.ElasticSearch.Utils
{
    public static class StringUtil
    {
        /// <summary>
        /// String Extendes Statis Method, 
        /// example: "s1".Append("s2") == "s1s2"
        /// </summary>
        /// <param name="originalString"></param>
        /// <param name="additive"></param>
        /// <returns></returns>
        public static string Append(this string originalString, object additive)
        {
            return string.Format("{0}{1}", originalString, additive);
        }

        /// <summary>
        /// String Extendes Statis Method, 
        /// example: "s1".Append("s2", "s3") == "s1s2s3";
        /// </summary>
        /// <param name="originalString"></param>
        /// <param name="additives"></param>
        /// <returns></returns>
        public static string Append(this string originalString, params object[] additives)
        {
            string adds = string.Join("", additives);
            return string.Format("{0}{1}", originalString, adds);
        }

        /// <summary>
        /// String Extendes Statis Method, 
        /// example: "s1".Append("s2") == "s2s1";
        /// </summary>
        /// <param name="originalString"></param>
        /// <param name="additive"></param>
        /// <returns></returns>
        public static string Prefix(this string originalString, object additive)
        {
            return string.Format("{0}{1}", additive, originalString);
        }

        /// <summary>
        /// String Extendes Statis Method, 
        /// </summary>
        /// <param name="formatString"></param>
        /// <param name="additives"></param>
        /// <returns></returns>
        public static string FillFormat(this string formatString, params object[] additives)
        {
            return string.Format(formatString, additives);
        }
    }
}
