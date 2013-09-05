using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace QuaintHouse.Http
{
    public class Delete : HttpMethod
    {
        public Delete(string url)
            : base(url, HttpConstans.METHOD_DELETE)
        {
        }

        protected override void BuildHttpRequest(HttpWebRequest httpWebRequest)
        {
            
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("method:").Append(HttpConstans.METHOD_DELETE).AppendLine();
            builder.Append("url:").Append(Url).AppendLine();
            builder.Append(GetHeaderString()).AppendLine();
            return builder.ToString();
        }
    }
}
