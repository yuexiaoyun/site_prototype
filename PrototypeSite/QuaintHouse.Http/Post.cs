using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;

namespace QuaintHouse.Http
{
    /// <summary>
    /// Http Post Method
    /// 1. Append header message
    /// 2. Append request content
    /// </summary>
    public abstract class Post:HttpMethod
    {
        protected Encoding encoding = Encoding.ASCII;

        public Post(string url)
            : base(url, HttpConstans.METHOD_POST)
        {
        }

        public Encoding Encoding
        {
            set { encoding = value; }
        }

        protected override void BuildHttpRequest(HttpWebRequest httpWebRequest)
        {
            SetRequestContentType(httpWebRequest);
            string body = ConstructBody();
            byte[] postDataBytes = encoding.GetBytes(body);
            httpWebRequest.ContentLength = postDataBytes.Length;

            using (Stream writer = httpWebRequest.GetRequestStream())
            {
                writer.Write(postDataBytes, 0, postDataBytes.Length);
                writer.Close();
            }
        }

        protected abstract void SetRequestContentType(HttpWebRequest httpWebRequest);

        protected abstract string ConstructBody();
    }
}
