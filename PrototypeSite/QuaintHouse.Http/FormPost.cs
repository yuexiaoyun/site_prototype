using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web;

namespace QuaintHouse.Http
{
    public class FormPost : Post
    {
        private NameValueCollection postData = new NameValueCollection();

        public FormPost(string url)
            : base(url)
        {
        }

        public void AddPostData(string key, string value)
        {
            postData.Add(key, value);
        }

        protected override void SetRequestContentType(HttpWebRequest httpWebRequest)
        {
            httpWebRequest.ContentType = HttpConstans.CONTENT_TYPE_FORM;
        }

        protected override string ConstructBody()
        {
            if (postData.Count <= 0) return string.Empty;

            StringBuilder postDataBuilder = new StringBuilder();
            foreach (string paramName in postData)
            {
                postDataBuilder.AppendFormat("{0}={1}&", paramName, HttpUtility.UrlEncode(postData[paramName]));
            }

            return postDataBuilder.ToString().Remove(postDataBuilder.Length - 1, 1);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("method:").Append(HttpConstans.METHOD_POST).AppendLine();
            builder.Append("url:").Append(Url).AppendLine();
            builder.Append(GetHeaderString()).AppendLine();
            builder.Append("body:").Append(ConstructBody());
            return builder.ToString();
        }
    }
}
