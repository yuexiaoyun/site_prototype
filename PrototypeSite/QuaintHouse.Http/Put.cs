using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace QuaintHouse.Http
{
    public class Put : HttpMethod
    {
        private string body;
        private string contentType = HttpConstans.CONTENT_TYPE_JSON;
        private Encoding encoding = Encoding.UTF8;

        public Put(string url)
            : base(url, HttpConstans.METHOD_PUT)
        {
        }

        protected override void BuildHttpRequest(HttpWebRequest httpWebRequest)
        {
            httpWebRequest.ContentType = contentType;
            byte[] contentBytes = encoding.GetBytes(body);
            httpWebRequest.ContentLength = contentBytes.Count();
            using (Stream writer = httpWebRequest.GetRequestStream())
            {
                writer.Write(contentBytes, 0, contentBytes.Length);
                writer.Close();
            }
        }

        public string Body
        {
            set { body = value; }
        }

        public void SetContentType(string contentType)
        {
            this.contentType = contentType;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("method:").Append(HttpConstans.METHOD_PUT).AppendLine();
            builder.Append("url:").Append(Url).AppendLine();
            builder.Append(GetHeaderString()).AppendLine();
            builder.Append("body:").Append(body);
            return builder.ToString();
        }
    }
}
