using Pepperi.SDK.Contracts;
using Pepperi.SDK.Exceptions;
using Pepperi.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK
{
    public class PrivateAuthentication : IAuthentication
    {
        #region Properties

        private string AppConsumerKey { get; set; }     //Identifies the application on apigee.          Sent as X-Pepperi-ConsumerKey header on each request of the private application.     value taken from apigee. used by apigee to identify the application, the authentication type of the application and monitor the application activity.
        private string APIToken { get; set; }           //Identifies the user whose data is manipulated. Sent as the "password" on each request of the private application                    (Basic authnetication username=TokenAuth  password=APIToken)

        #endregion

        #region Constructor

        public PrivateAuthentication(string AppConsumerKey, string APIToken)
        {
            this.AppConsumerKey = AppConsumerKey;
            this.APIToken = APIToken;
        }

        #endregion

        #region IAuthentication

        public AuthenticationHeaderValue GetAuthorizationHeaderValue()
        {
            var result = new AuthenticationHeaderValue(
                        "Basic",
                //Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", this.Username, this.Password)))
                        Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("TokenAuth:{0}", this.APIToken))));

            return result;
        }

        public Dictionary<string, string> GetCustomRequestHeaders()
        {
            var result = new Dictionary<string, string>();
            result.Add("X-Pepperi-ConsumerKey", this.AppConsumerKey);
            return result;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ApiTokenBaseUri"></param>
        /// <param name="AppConsumerKey"></param>
        /// <param name="Username"></param>
        /// <param name="Password"></param>
        /// <returns>the api token will later on be sent as "password" of basic authentication</returns>
        /// <remarks>
        /// 1. Using PepperiHttpClient for consistency and maintainability (logging etc)
        /// </remarks>
        public static APITokenData GetAPITokenData(string ApiTokenBaseUri, string AppConsumerKey, string Username, string Password, ILogger Logger)
        {
            string RequestUri = "company//ApiToken"; 
            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();

            string accept = "application/json";

            IAuthentication Authentication = new BasicAuthentication(Username, Password, AppConsumerKey, true);

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(Authentication, Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Get(
                    ApiTokenBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    accept);

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            APITokenData APITokenData = PepperiJsonSerializer.DeserializeOne<APITokenData>(PepperiHttpClientResponse.Body);   //Api returns single object
            //string result = APITokenData.APIToken;

            //return result;
            return APITokenData;
        }

        #endregion

        #region Private methods
        #endregion
    }



    #region Models

    public class APITokenData
    {
        public string CompanyID { get; set; }
        public string APIToken { get; set; }
        public string BaseURI { get; set; }
    }

    #endregion
}


