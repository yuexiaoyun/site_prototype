using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace QuaintHouse.Http
{
    public class TextPost : Post
    {
        private string body;
        private string contentType = HttpConstans.CONTENT_TYPE_JSON;

        public TextPost(string url)
            : base(url)
        {
        }

        public string Body
        {
            set { body = value; }
        }

        protected override void SetRequestContentType(HttpWebRequest httpWebRequest)
        {
            httpWebRequest.ContentType = contentType;
        }

        public void SetContentType(string contentType)
        {
            this.contentType = contentType; 
        }

        protected override string ConstructBody()
        {
            return body;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("method:").Append(HttpConstans.METHOD_POST).AppendLine();
            builder.Append("url:").Append(Url).AppendLine();
            builder.Append(GetHeaderString()).AppendLine();
            builder.Append("body:").Append(body);
            return builder.ToString();
        }
    }
}
