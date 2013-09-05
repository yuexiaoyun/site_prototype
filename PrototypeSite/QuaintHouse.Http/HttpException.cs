using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace QuaintHouse.Http
{
    public class HttpException : Exception
    {
        private readonly HttpErrorStatusCode errorStatusCode;
        private readonly string responseContent;

        public HttpException(HttpErrorStatusCode errorStatusCode, Exception exception)
            : base(string.Empty, exception)
        {
            this.errorStatusCode = errorStatusCode;
        }

        public HttpException(HttpErrorStatusCode errorStatusCode, string responseContent, WebException webException)
            : base(string.Empty, webException)
        {
            this.errorStatusCode = errorStatusCode;
            this.responseContent = responseContent;
        }

        public HttpErrorStatusCode ErrorStatusCode
        {
            get { return errorStatusCode; }
        }

        public string ResponseContent
        {
            get { return responseContent; }
        }
    }

    public enum HttpErrorStatusCode
    {
        BadRequest = 400,
        Unauthorized = 401,
        ResourceNotFound = 404,
        MethodNotAllowed = 405,
        ServerInternalError = 500,
        BadGateway = 502,
        ServiceUnavailable = 503,
        GatewayTimeout = 504,
        ConnectFailure = 1000,
        Others = 1001
    }
}
