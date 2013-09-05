using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using QuaintHouse.Http;
using QuaintHouse.REST.Cryptographer;
using QuaintHouse.REST.Exceptions;
using QuaintHouse.REST.Serializer;
using log4net;
using JsonSerializer = QuaintHouse.REST.Serializer.JsonSerializer;

namespace QuaintHouse.REST
{
    public class RESTServiceClient
    {
        private ILog logger = LogManager.GetLogger(typeof (RESTServiceClient));

        private string contentType = HttpConstans.CONTENT_TYPE_JSON;
        private HttpClient httpClient = new HttpClient();
        private ISerializer serializer = new JsonSerializer();
        private int maxRequestTimeout = HttpConstans.DEFAULT_REQUEST_TIMEOUT;
        private Encoding encoding = Encoding.UTF8;
        private bool requireClientSignature = true;
        private string clientId;
        private string clientKey;

        public T Get<T>(string url)
        {
            Get get = new Get(url);
            logger.Info("Get request url" + url);
            get.SetAccept(contentType);
            ConstructRequestHeader(get, HttpConstans.METHOD_GET, url, string.Empty);
            return Execute<T>(get);
        }

        public T Post<U, T>(string url, U request)
        {
            TextPost post = new TextPost(url);
            logger.Info("Post request url" + url);
            post.SetAccept(contentType);
            post.SetContentType(contentType);
            string body = serializer.Serialize(request);
            post.Body = body;
            logger.Info("Post request body: " + body);
            ConstructRequestHeader(post, HttpConstans.METHOD_POST, url, body);
            return Execute<T>(post);
        }

        public T Put<U, T>(string url, U request)
        {
            Put put = new Put(url);
            logger.Info("Put request url" + url);
            put.SetAccept(contentType);
            put.SetContentType(contentType);
            string body = serializer.Serialize(request);
            put.Body = body;
            logger.Info("Put request body: " + body);
            ConstructRequestHeader(put, HttpConstans.METHOD_PUT, url, body);
            return Execute<T>(put);
        }

        public T Delete<T>(string url)
        {
            Delete delete = new Delete(url);
            logger.Info("Delete request url" + url);
            delete.SetAccept(contentType);
            ConstructRequestHeader(delete, HttpConstans.METHOD_DELETE, url, string.Empty);
            return Execute<T>(delete);
        }

        private void ConstructRequestHeader(HttpMethod httpMethod, string method, string url, string body)
        {
            string timestamp = DateTime.Now.ToString(RESTConstants.TIMESTAMP_FORMAT, CultureInfo.InvariantCulture);
            httpMethod.AddRequestHeader(RESTConstants.HEADER_REQUEST_ID, (new Guid()).ToString());
            if (requireClientSignature)
            {
                httpMethod.AddRequestHeader(RESTConstants.HEADER_CLIENT_ID, clientId);
                httpMethod.AddRequestHeader(RESTConstants.HEADER_TIMESTAMP, timestamp);
                httpMethod.AddRequestHeader(RESTConstants.HEADER_CLIENT_SIGNATURE,
                                            HashUtil.SHA1Sign(BuildSignMessage(method, url, body), clientKey, encoding));    
            }
        }

        private string BuildSignMessage(string method, string url, string body)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("method:{0};", method);
            builder.AppendFormat("uri:{0};", url);
            builder.AppendFormat("body:{0};", body);
            return builder.ToString();
        }

        public T Execute<T>(HttpMethod httpMethod)
        {
            HttpResponse response = null;
            try
            {
                httpMethod.SetRequestTimeOut(maxRequestTimeout);
                response = httpClient.Execute(httpMethod);
                return serializer.Deserialize<T>(response.GetStringResponse());
            }
            catch (HttpException httpException)
            {
                string errorText = BuildErrorText(httpMethod.ToString(), httpException.ResponseContent);
                if(IsServerLevelErrorCode(httpException.ErrorStatusCode))
                {
                    RESTServiceServerException serverException = new RESTServiceServerException(errorText, httpException);
                    logger.Error("REST service server exception", serverException);
                    throw serverException;
                }
                else
                {
                    RESTServiceApplicationException applicationException = new RESTServiceApplicationException(errorText, httpException);
                    logger.Error("REST service application exception", applicationException);
                    throw applicationException;
                }
            }
            catch(JsonException jsonException)
            {
                string errorText = BuildErrorText(httpMethod.ToString(), response.GetStringResponse());
                RESTServiceClientJsonException exception = new RESTServiceClientJsonException(errorText, jsonException);
                logger.Error("REST service client json serailize exception", exception);
                throw exception;
            }
            catch(Exception exception)
            {
                string errorText = BuildErrorText(httpMethod.ToString(), exception.Message);
                RESTServiceClientException clientException = new RESTServiceClientException(errorText, exception);
                logger.Error("REST service client exception", clientException);
                throw clientException;
            }
        }

        private string BuildErrorText(string requestContent, string responseContent)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Request").AppendLine().Append(requestContent);
            builder.AppendLine();
            builder.Append("Response").AppendLine().Append(responseContent == string.Empty ? "empty" : requestContent);
            return builder.ToString();
        }

        private bool IsServerLevelErrorCode(HttpErrorStatusCode statusCode)
        {
            if (statusCode == HttpErrorStatusCode.ConnectFailure)
                return true;
            int status = (int)statusCode;
            return status >= 500 && status < 600;
        }

        public int MaxRequestTimeout
        {
            set { maxRequestTimeout = value; }
        }

        public string ContentType
        {
            set { contentType = value; }
        }

        public ISerializer Serializer
        {
            set { serializer = value; }
        }

        /// <summary>
        /// Sign request encoding
        /// </summary>
        public Encoding Encoding
        {
            set { encoding = value; }
        }

        public bool RequireClientSignature
        {
            set { requireClientSignature = value; }
        }
    }
}
