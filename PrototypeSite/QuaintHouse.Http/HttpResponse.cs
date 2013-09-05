using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace QuaintHouse.Http
{
    public class HttpResponse
    {
        private HttpWebResponse httpWebResponse;

        public HttpResponse(HttpWebResponse httpWebResponse)
        {
            this.httpWebResponse = httpWebResponse;
        }

        public HttpStatusCode GetStatusCode()
        {
            return httpWebResponse.StatusCode;
        }

        public CookieCollection GetCookies()
        {
            return httpWebResponse.Cookies;
        }

        public WebHeaderCollection GetHeaders()
        {
            return httpWebResponse.Headers;
        }

        public string GetStringResponse()
        {
            using (StreamReader reader = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8))
            {
                string result = reader.ReadToEnd();

                httpWebResponse.Close();
                
                return result;
            }
        }

        public byte[] GetByteResponse()
        {
            MemoryStream ms = new MemoryStream();

            using (Stream response = httpWebResponse.GetResponseStream())
            {
                byte[] buffer = new byte[2048];
                int readCount;
                while ((readCount = response.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, readCount);
                }
                
                response.Close();
            }

            return ms.ToArray();
        }
    }
}
