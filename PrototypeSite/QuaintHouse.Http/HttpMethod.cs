using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace QuaintHouse.Http
{
    /// <summary>
    /// Base HttpMethod
    /// Http request contains: Requst Line, Header Messages, Content
    /// Required in base method: 
    /// Request Line{Method, Request URL}
    /// Header{Keep Alive}
    /// </summary>
    public abstract class HttpMethod : IHttpRequestBuilder
    {
        private string url;
        private string method;
        private bool keepAlive = true;
        private int timeOut;
        private string acceptContentType;
        private WebHeaderCollection headers = new WebHeaderCollection();

        public string Url
        {
            get { return url; }
        }

        public string Method
        {
            get { return method; }
        }

        public void SetAccept(string contentType)
        {
            this.acceptContentType = contentType;
        }

        public void SetKeepAlive(bool keepAlive)
        {
            this.keepAlive = keepAlive;
        }

        public void SetRequestTimeOut(int timeout)
        {
            this.timeOut = timeout;
        }

        public void AddRequestHeader(string name, string value)
        {
            this.headers.Add(name, value);
        }

        public HttpMethod(string url, string method)
        {
            this.url = url;
            this.method = method;
        }

        public HttpWebRequest Create()
        {
            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
            request.Method = method;
            request.KeepAlive = keepAlive;
            request.Timeout = timeOut == 0 ? HttpConstans.DEFAULT_REQUEST_TIMEOUT : timeOut;
            request.Accept = acceptContentType;
            request.Headers.Add(headers);
            
            BuildHttpRequest(request);

            return request;
        }

        protected abstract void BuildHttpRequest(HttpWebRequest httpWebRequest);

        public override string ToString()
        {
            return url;
        }

        protected string GetHeaderString()
        {
            StringBuilder headerBuilder = new StringBuilder();
            for (int i = 0; i < headers.Count; i++)
            {
                headerBuilder.AppendFormat("header: {0}={1}", headers.GetKey(i), headers.Get(i)).AppendLine();
            }
            return headerBuilder.ToString();
        }
    }

    public interface IHttpRequestBuilder
    {
        HttpWebRequest Create();
    }
}
