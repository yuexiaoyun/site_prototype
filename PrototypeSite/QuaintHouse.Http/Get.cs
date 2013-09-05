using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace QuaintHouse.Http
{
    /// <summary>
    /// Http Get Method
    /// </summary>
    public class Get : HttpMethod
    {
        public Get(string url)
            : base(url, HttpConstans.METHOD_GET)
        {
        }

        protected override void BuildHttpRequest(HttpWebRequest httpWebRequest)
        {
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("method:").Append(HttpConstans.METHOD_GET).AppendLine();
            builder.Append("url:").Append(Url).AppendLine();
            builder.Append(GetHeaderString());
            return builder.ToString();
        }
    }
}
