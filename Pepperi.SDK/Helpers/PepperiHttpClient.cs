using Pepperi.SDK.Contracts;
using Pepperi.SDK.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Pepperi.SDK.Helpers
{

    /// <summary>
    /// wrapper over HttpClient Adding Authentication functionality
    /// </summary>
    /// <remarks>
    /// 1. Works with any authentication, relaying on IAuthentication to implement the authentication speciffic details.
    /// 2. Works with any logger, relaying on ILogger to implement the Logger specific details.
    /// </remarks>
    internal class PepperiHttpClient
    {
        #region Properties

        private IAuthentication Authentication { get; set; }
        private ILogger Logger { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Authentication">manges special request headers. value may be null.</param>
        /// <param name="Logger"></param>
        internal PepperiHttpClient(IAuthentication Authentication, ILogger Logger)
        {
            this.Authentication = Authentication;
            this.Logger = Logger;
        }

        #endregion

        #region Internals


        /// <summary>
        /// Throws relevant exception, according to the status code
        /// </summary>
        /// <param name="PepperiHttpClientResponse"></param>
        internal void HandleError(PepperiHttpClientResponse PepperiHttpClientResponse)
        {
            if (PepperiHttpClientResponse.HttpStatusCode == System.Net.HttpStatusCode.OK || PepperiHttpClientResponse.HttpStatusCode == System.Net.HttpStatusCode.Created)
            {
                //on upsert (post): 200 status code is returned for Update    201 status code is returned for Insert
                return;
            }


            ApiException ApiException = null;

            if (PepperiHttpClientResponse.HttpStatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                try
                {
                    ApiException = PepperiJsonSerializer.DeserializeOne<ApiException>(PepperiHttpClientResponse.Body);
                }
                catch (Exception e)
                {
                    ApiException = new ApiException("Api returned Bad Request. Failed to Parse body as exception.\r\nBody:" +
                                                    PepperiHttpClientResponse.Body == null ? "" : PepperiHttpClientResponse.Body +
                                                    "\r\nParsing error: " +
                                                    e.Message);
                }

            }
            else
            {
                ApiException =  new ApiException(
                    string.Format("Status Code: {0} \r\n Body: {1}",
                        PepperiHttpClientResponse.HttpStatusCode.ToString(),
                        PepperiHttpClientResponse.Body == null ? "" : PepperiHttpClientResponse.Body)
                        );
            }

            //this.Logger.Log("Error:\r\n");
            //this.Logger.Log("------\r\n");
            //this.Logger.Log(ApiException.ToString() + "\r\n");

            throw ApiException;
        }


        internal PepperiHttpClientResponse Get(
            string BaseUri,
            string RequestUri,
            Dictionary<string, string> dicQueryStringParameters,        //where, order by, any other parameters (page, page_size etc)
            string accept
        )
        {
            var HttpClient = new HttpClient(new LoggingHandler(this.Logger))
            {
                BaseAddress = new Uri(BaseUri)
            };

            //add authorization header (for basic authentiction, Oauth2 etc)
            AddAuthorizationHeader(HttpClient);

            //add: accept header
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(accept));

            //add: custom request headers
            AddAuthenticationCustomRequestHeaders(HttpClient);

            //create query string (required by endpoint and authentication)
            string QueryString = BuildQueryString(dicQueryStringParameters, null);


            //send request
            HttpResponseMessage HttpResponseMessage = HttpClient.GetAsync(RequestUri + QueryString).Result;
            string body = HttpResponseMessage.Content.ReadAsStringAsync().Result;
            var ResponseHeaders = this.GetResponseHeaders(HttpResponseMessage);
                   
            //Create result
            var PepperiHttpClientResponse = new PepperiHttpClientResponse(HttpResponseMessage.StatusCode, body, ResponseHeaders);
            return PepperiHttpClientResponse;
        }



        /// <summary>
        /// post string (eg, json)
        /// </summary>
        /// <param name="BaseUri"></param>
        /// <param name="RequestUri"></param>
        /// <param name="dicQueryStringParameters"></param>
        /// <param name="postBody"></param>
        /// <param name="contentType"></param>
        /// <param name="accept"></param>
        /// <returns></returns>
        internal PepperiHttpClientResponse PostStringContent(
            string BaseUri,
            string RequestUri,
            Dictionary<string, string> dicQueryStringParameters,//where, order by, any other parameters (page, page_size etc)
            string postBody,
            string contentType,     //eg, application/json
            string accept
        )
        {
            var HttpClient = new HttpClient(new LoggingHandler(this.Logger))
            {
                BaseAddress = new Uri(BaseUri)
            };

            //add authentication header (for basic authentiction, Oauth2 etc)
            AddAuthorizationHeader(HttpClient);


            //add: accept header
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(accept));

            //add: custom request headers
            AddAuthenticationCustomRequestHeaders(HttpClient);


            //create query string (required by endpoint and authentication)
            string QueryString = BuildQueryString(dicQueryStringParameters, null);

            //send request
            StringContent StringContent = new StringContent(postBody, Encoding.UTF8);       //new StringContent(postBody, Encoding.UTF8, "application/json")
            StringContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);      //add content type header
            HttpResponseMessage HttpResponseMessage = HttpClient.PostAsync(RequestUri + QueryString, StringContent).Result;
            string body = HttpResponseMessage.Content.ReadAsStringAsync().Result;
            var ResponseHeaders = this.GetResponseHeaders(HttpResponseMessage);

            //Create result
            var PepperiHttpClientResponse = new PepperiHttpClientResponse(HttpResponseMessage.StatusCode, body, ResponseHeaders);
            return PepperiHttpClientResponse;
        }



        /// <summary>
        /// post byte[] (eg, zip file)
        /// </summary>
        /// <param name="BaseUri"></param>
        /// <param name="RequestUri"></param>
        /// <param name="dicQueryStringParameters"></param>
        /// <param name="postBody"></param>
        /// <param name="contentType"></param>
        /// <param name="accept"></param>
        /// <returns></returns>
        internal PepperiHttpClientResponse PostByteArraycontent(
           string BaseUri,
           string RequestUri,
           Dictionary<string, string> dicQueryStringParameters,//where, order by, any other parameters (page, page_size etc)
           byte[] postBody,
           string contentType,     //eg, application/zip
           string accept
       )
        {
            var HttpClient = new HttpClient(new LoggingHandler(this.Logger))
            {
                BaseAddress = new Uri(BaseUri)
            };

            //add authentication header (for basic authentiction, Oauth2 etc)
            AddAuthorizationHeader(HttpClient);


            //add: accept header
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(accept));

            //add: custom request headers
            AddAuthenticationCustomRequestHeaders(HttpClient);


            //create query string (required by endpoint and authentication)
            string QueryString = BuildQueryString(dicQueryStringParameters, null);


            //send request
            ByteArrayContent ByteArrayContent = new ByteArrayContent(postBody);
            ByteArrayContent.Headers.ContentType = new MediaTypeHeaderValue(contentType); //add content type header
            HttpResponseMessage HttpResponseMessage = HttpClient.PostAsync(RequestUri + QueryString, ByteArrayContent).Result;
            string body = HttpResponseMessage.Content.ReadAsStringAsync().Result;
            var ResponseHeaders = this.GetResponseHeaders(HttpResponseMessage);

            //Create result
            var PepperiHttpClientResponse = new PepperiHttpClientResponse(HttpResponseMessage.StatusCode, body, ResponseHeaders);
            return PepperiHttpClientResponse;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="BaseUri"></param>
        /// <param name="RequestUri"></param>
        /// <param name="dicQueryStringParameters"></param>
        /// <param name="postBody"></param>
        /// <param name="contentType"></param>
        /// <param name="accept"></param>
        /// <returns></returns>
        /// <remarks>
        /// 1./https://msdn.microsoft.com/en-us/library/system.net.http.formurlencodedcontent(v=vs.118).aspx
        /// </remarks>
        internal PepperiHttpClientResponse PostFormUrlEncodedContent(
            string BaseUri,
            string RequestUri,
            Dictionary<string, string> dicQueryStringParameters,//where, order by, any other parameters (page, page_size etc)
            List<KeyValuePair<string, string>> postBody,
            string contentType,                                 //eg, application/x-www-form-urlencoded
            string accept
        )
        {
            var HttpClient = new HttpClient(new LoggingHandler(this.Logger))
            {
                BaseAddress = new Uri(BaseUri)
            };

            //add authentication header (for basic authentiction, Oauth2 etc)
            AddAuthorizationHeader(HttpClient);


            //add: accept header
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(accept));

            //add: custom request headers
            AddAuthenticationCustomRequestHeaders(HttpClient);


            //create query string (required by endpoint and authentication)
            string QueryString = BuildQueryString(dicQueryStringParameters, null);

            //send request
            FormUrlEncodedContent FormUrlEncodedContent = new FormUrlEncodedContent(postBody);
            FormUrlEncodedContent.Headers.ContentType = new MediaTypeHeaderValue(contentType); //add content type header
            HttpResponseMessage HttpResponseMessage = HttpClient.PostAsync(RequestUri + QueryString, FormUrlEncodedContent).Result;
            string body = HttpResponseMessage.Content.ReadAsStringAsync().Result;
            var ResponseHeaders = this.GetResponseHeaders(HttpResponseMessage);

            //Create result
            var PepperiHttpClientResponse = new PepperiHttpClientResponse(HttpResponseMessage.StatusCode, body, ResponseHeaders);
            return PepperiHttpClientResponse;
        }


        internal PepperiHttpClientResponse Delete(
            string BaseUri,
            string RequestUri,
            Dictionary<string, string> dicQueryStringParameters,        //where, order by, any other parameters (page, page_size etc)
            string accept
            )
        {

            var HttpClient = new HttpClient(new LoggingHandler(this.Logger))
            {
                BaseAddress = new Uri(BaseUri)
            };

            //add authorization header (for basic authentiction, Oauth2 etc)
            AddAuthorizationHeader(HttpClient);

            //add: accept header
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(accept));

            //add: custom request headers
            AddAuthenticationCustomRequestHeaders(HttpClient);


            //create query string (required by endpoint and authentication)
            string QueryString = BuildQueryString(dicQueryStringParameters, null);


            //send request
            HttpResponseMessage HttpResponseMessage = HttpClient.DeleteAsync(RequestUri + QueryString).Result;
            string body = HttpResponseMessage.Content.ReadAsStringAsync().Result;
            var ResponseHeaders = this.GetResponseHeaders(HttpResponseMessage);

            //Create result
            var PepperiHttpClientResponse = new PepperiHttpClientResponse(HttpResponseMessage.StatusCode, body, ResponseHeaders);
            return PepperiHttpClientResponse;

        }


        #endregion

        #region private

        private void AddAuthorizationHeader(HttpClient HttpClient)
        {
            if (this.Authentication != null)
            {
                var AuthorizationHeaderValue = this.Authentication.GetAuthorizationHeaderValue();                   //different authentication method adds different authentication header value 
                HttpClient.DefaultRequestHeaders.Authorization = AuthorizationHeaderValue;
            }
        }

        private void AddAuthenticationCustomRequestHeaders(HttpClient HttpClient)
        {
            if (this.Authentication != null)
            {
                Dictionary<string, string> CustomRequestHeaders = this.Authentication.GetCustomRequestHeaders();    //different authentication methods adds different custom headers
                if (CustomRequestHeaders == null)
                {
                    CustomRequestHeaders = new Dictionary<string, string>();
                }
                foreach (var key in CustomRequestHeaders.Keys)
                {
                    string value = CustomRequestHeaders[key];
                    HttpClient.DefaultRequestHeaders.Add(key, value);
                }
            }
           
        }

        private string BuildQueryString(Dictionary<string, string> dicEndpointQueryStringParameters, Dictionary<string, string> dicAuthenticationQeryStringParameters)
        {
            string QueryString = "?";

            //add endpoint related parameters to the generated query string
            foreach (string key in dicEndpointQueryStringParameters.Keys)
            {
                string Parameter = key;
                string Value = dicEndpointQueryStringParameters[Parameter];

                QueryString = string.Format("{0}{1}={2}&",
                        QueryString, HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(Value));
            }

            if (dicAuthenticationQeryStringParameters == null)
            {
                dicAuthenticationQeryStringParameters = new Dictionary<string, string>();
            }
            foreach (string key in dicAuthenticationQeryStringParameters.Keys)
            {
                string Parameter = key;
                string Value = dicAuthenticationQeryStringParameters[Parameter];

                QueryString = string.Format("{0}{1}={2}&",
                        QueryString, HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(Value));
            }

            //refine result
            string result = (QueryString == "?") ?
                                    string.Empty :                                      //no parameters
                                    QueryString.Substring(0, QueryString.Length - 1);   //remove the last &

            return result;
        }

        private Dictionary<string, IEnumerable<string>> GetResponseHeaders(HttpResponseMessage HttpResponseMessage)
        {
            Dictionary<string, IEnumerable<string>> result = new Dictionary<string, IEnumerable<string>>();

            foreach (var Header in HttpResponseMessage.Headers)
            {
                string Key = Header.Key;
                IEnumerable<string> Value = Header.Value;

                result[Key] = Value;
            }

            return result;
        }

        
        #endregion
    }


    #region Model

    /// <summary>
    /// Wrapper over Http Response
    /// </summary>
    internal class PepperiHttpClientResponse
    {
        internal PepperiHttpClientResponse(HttpStatusCode HttpStatusCode, string Body, Dictionary<string, IEnumerable<string>> Headers)
        {
            this.HttpStatusCode = HttpStatusCode;
            this.Body = Body;
            this.Headers = Headers;
        }

        internal HttpStatusCode HttpStatusCode { get; set; }
        internal string Body { get; set; }
        internal Dictionary<string, IEnumerable<string>> Headers { get; set; }

    }

    #endregion

    
    /// <summary>
    ///responsible for logging HttpClient traffic to the ILogger
    /// </summary>
    /// <remarks>
    /// http://stackoverflow.com/questions/29126354/logging-requests-responses-and-exceptions-of-webclient
    /// </remarks>
    public class LoggingHandler : DelegatingHandler
    {
        #region Properties

        private ILogger Logger { get; set; }

        #endregion

        #region Constructor

        public LoggingHandler(ILogger Logger)
            : this(new HttpClientHandler(),Logger)
        {
            this.Logger = Logger;
        }

        public LoggingHandler(HttpMessageHandler innerHandler, ILogger Logger)
            : base(innerHandler)
        {
            this.Logger = Logger;
        }

        #endregion

        #region override

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Request:");
            sb.AppendLine("--------");
            sb.Append(request.ToString());
            sb.AppendLine();

            if (request.Content != null)
            {
                string requestContent = await request.Content.ReadAsStringAsync().ConfigureAwait(false);
                sb.Append(requestContent);
                sb.AppendLine();
            }

            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            sb.AppendLine("Response:");
            sb.AppendLine("--------");
            sb.Append(response.ToString());
            sb.AppendLine();

            if (response.Content != null)
            {
                string responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                sb.Append(responseContent);
                sb.AppendLine();
            }



            string LogMessage = sb.ToString();
            this.Logger.Log(LogMessage);

            return response;
        }

        #endregion
    }
}
