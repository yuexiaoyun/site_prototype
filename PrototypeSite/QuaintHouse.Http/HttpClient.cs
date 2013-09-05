using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace QuaintHouse.Http
{
    public class HttpClient
    {
        private bool allowAutoRedirect = true;
        private CookieContainer cookieContainer;

        public bool AllowAutoRedirect
        {
            get { return allowAutoRedirect; }
            set { allowAutoRedirect = value; }
        }

        public CookieContainer CookieContainer
        {
            get { return cookieContainer; }
            set { cookieContainer = value; }
        }

        static HttpClient()
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(TrustAllCertificateValidationCallback);
            ServicePointManager.Expect100Continue = false;
        }

        public virtual HttpResponse Execute(HttpMethod httpMethod)
        {
            try
            {
                HttpWebRequest httpWebRequest = httpMethod.Create();
                httpWebRequest.AllowAutoRedirect = allowAutoRedirect;
                httpWebRequest.CookieContainer = cookieContainer;

                HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                HttpResponse httpResponse = new HttpResponse(httpWebResponse);

                return httpResponse;
            }
            catch (WebException webException)
            {
                throw InterpretException(webException);
            }
            catch(Exception exception)
            {
                throw new HttpException(HttpErrorStatusCode.Others, exception);
            }
        }

        private HttpException InterpretException(WebException webException)
        {
            if(WebExceptionStatus.ProtocolError == webException.Status)
            {
                HttpResponse response = new HttpResponse((HttpWebResponse) webException.Response);
                return new HttpException((HttpErrorStatusCode) response.GetStatusCode(), response.GetStringResponse(),
                                         webException);
            }
            return new HttpException(HttpErrorStatusCode.ConnectFailure, webException);
        }

        private static bool TrustAllCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}
