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
    public class PublicAuthentication : IAuthentication
    {
        #region Properties

        private ILogger Logger { get; set; }
        private string OauthBaseUri { get; set; }
        private string AppConsumerKey { get; set; }                         //client id.        sent as X-Pepperi-ConsumerKey header on each request of the private application.     value taken from apigee. used by apigee to identify the application, the authentication type of the application and monitor the application activity.
        private string AppConsumerSecret { get; set; }                      //client secret.    sent to refresh token.
        private Oauth2Token Oauth2Token { get; set; }
        private IOauth2TokenRepository Oauth2TokenRepository { get; set; }  //used to allow the sdk consumer to store the token in persistent memory (after renew, for example)

        #endregion

        #region Constructor

        /// <summary>
        /// creates fully initialized instance that can be used for consuming the API (and refreshing access token automatically)
        /// </summary>
        /// <param name="OauthBaseUri"></param>
        /// <param name="AppConsumerKey"></param>
        /// <param name="AppConsumerSecret"></param>
        /// <param name="Oauth2Token"></param>
        /// <param name="Oauth2TokenRepository"></param>
        public PublicAuthentication(ILogger Logger, string OauthBaseUri, string AppConsumerKey, string AppConsumerSecret, Oauth2Token Oauth2Token, IOauth2TokenRepository Oauth2TokenRepository)
        {
            this.Logger = Logger;
            this.OauthBaseUri = OauthBaseUri;
            this.AppConsumerKey = AppConsumerKey;
            this.AppConsumerSecret = AppConsumerSecret;
            this.Oauth2Token = Oauth2Token;
            this.Oauth2TokenRepository = Oauth2TokenRepository;
        }

        /// <summary>
        /// creates fully initialized instance that can be used for getting access token from ui
        /// </summary>
        /// <param name="OauthBaseUri"></param>
        /// <param name="AppConsumerKey"></param>
        /// <param name="AppConsumerSecret"></param>
        public PublicAuthentication(ILogger Logger, string OauthBaseUri, string AppConsumerKey, string AppConsumerSecret)
        {
            this.Logger = Logger;
            this.OauthBaseUri = OauthBaseUri;
            this.AppConsumerKey = AppConsumerKey;
            this.AppConsumerSecret = AppConsumerSecret;
            this.Oauth2Token = null;
            this.Oauth2TokenRepository = null;
        }

        #endregion

        #region public methods

        public string GetAuthorizationUrl(string redirect_uri, string scope)
        {
            string RequestUri = string.Format("{0}/oauth/authorize?client_id={1}&scope={2}&response_type={3}&redirect_uri={4}",
                this.OauthBaseUri,
                this.AppConsumerKey,
                scope,              //the level of access that the application request from service. Eg, READ
                "code",             //requesting for authorization code 
                redirect_uri        //the uri to get the authorization coe
                );

            return RequestUri;
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authorization_code">The authorization code we get from service on callback, indicating that user has given valid credentinals.</param>
        /// <param name="redirect_uri"></param>
        /// <param name="scope"></param>
        /// <param name="Oauth2TokenRepository"></param>
        /// <returns></returns>
        public Oauth2Token GetAccessTokenByAuthorizationCode(string authorization_code, string redirect_uri, string scope, IOauth2TokenRepository Oauth2TokenRepository)
        {
            string RequestUri = "oauth//access_token";

            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();

            List<KeyValuePair<string, string>> postBody = new List<KeyValuePair<string, string>>();
            postBody.Add(new KeyValuePair<string, string>("client_id", this.AppConsumerKey));
            postBody.Add(new KeyValuePair<string, string>("client_secret", this.AppConsumerSecret));
            postBody.Add(new KeyValuePair<string, string>("scope", scope));
            postBody.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
            postBody.Add(new KeyValuePair<string, string>("code", authorization_code));
            postBody.Add(new KeyValuePair<string, string>("redirect_uri", redirect_uri));

            string contentType = "application/x-www-form-urlencoded";

            string accept = "application/json";

            IAuthentication Authentication = null; //no special headers
            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(Authentication, Logger);
            ///
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.PostFormUrlEncodedContent(
                    this.OauthBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    postBody,
                    contentType,
                    accept);

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            Oauth2Token result = PepperiJsonSerializer.DeserializeOne<Oauth2Token>(PepperiHttpClientResponse.Body);   //Api returns single object

            //save result
            if (Oauth2TokenRepository != null)
            {
                Oauth2TokenRepository.SaveToken(result);
            }

            return result;
        }
            

        #endregion

        #region IAuthentication

        public AuthenticationHeaderValue GetAuthorizationHeaderValue()
        {
            //the method to get authorization header is called for every request. 
            //before handling the requst we will renew the access token if it is expired and call the repository to store the token in persistent memeory.

            if (TokenExpired())
            {
                this.Oauth2Token = RefreshToken();
                Oauth2TokenRepository.SaveToken(this.Oauth2Token);
            }

            var result = new AuthenticationHeaderValue(
                "Bearer",
                this.Oauth2Token.access_token
               );

            return result;
        }

        public Dictionary<string, string> GetCustomRequestHeaders()
        {
            var result = new Dictionary<string, string>();
            result.Add("X-Pepperi-ConsumerKey", this.AppConsumerKey);
            return result;
        }


        #endregion

        #region Private methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 1. The method is called on every Http operation used with this Authentication (from GetAuthorizationHeaderValue)
        /// </remarks>
        private bool TokenExpired()
        {

            string issued_at_as_string = this.Oauth2Token.issued_at;
            string expires_in_as_string = this.Oauth2Token.expires_in;

            double issued_at_as_decimal = 0;   //number of ms since 1.1.1970 utc           
            double expires_in_as_decimal = 0;   //number of seconds that the token is valid

            bool issued_at_parsed_suceesfully = double.TryParse(issued_at_as_string, out issued_at_as_decimal);
            bool expired_in_parsed_sucessfully = double.TryParse(this.Oauth2Token.expires_in, out expires_in_as_decimal);
            if (issued_at_parsed_suceesfully == false || expired_in_parsed_sucessfully == false)
            {
                throw new PepperiException("Failed to check if token expired. Please check that token contains valid numeric valued for issued_at and expired_in.");
            }


            DateTime dtOriginInUTC = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            double expiration_time_in_unix_timestamp = issued_at_as_decimal / 1000 + expires_in_as_decimal;    //unix time stamp - number of senconds since 1.1.1970
            DateTime dtExpirationTimeInUTC = dtOriginInUTC.AddSeconds(expiration_time_in_unix_timestamp);

            return (DateTime.UtcNow > dtExpirationTimeInUTC);

        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 1. The method is called on every Http operation used with this Authentication (from GetAuthorizationHeaderValue)
        /// </remarks>
        private Oauth2Token RefreshToken()
        {
            string RequestUri = "oauth//refresh_token";

            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();

            List<KeyValuePair<string, string>> postBody = new List<KeyValuePair<string, string>>();
            postBody.Add(new KeyValuePair<string, string>("client_id", this.AppConsumerKey));
            postBody.Add(new KeyValuePair<string, string>("client_secret", this.AppConsumerSecret));
            postBody.Add(new KeyValuePair<string, string>("grant_type", "refresh_token"));
            postBody.Add(new KeyValuePair<string, string>("refresh_token", this.Oauth2Token.refresh_token));
        
            string contentType = "application/x-www-form-urlencoded";

            string accept = "application/json";

            IAuthentication Authentication = null; //no special headers
            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(Authentication, Logger);
                        
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.PostFormUrlEncodedContent(
                    this.OauthBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    postBody,
                    contentType,
                    accept);

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            Oauth2Token result = PepperiJsonSerializer.DeserializeOne<Oauth2Token>(PepperiHttpClientResponse.Body);   //Api returns single object

            return result;
            
        }

        #endregion
    }

    #region Interfaces

    public interface IOauth2TokenRepository
    {
        void SaveToken(Oauth2Token Oauth2Token);
    }

    #endregion

    #region helpers



    #endregion

    #region Model

    /// <summary>
    /// The class returned by ApiGee (eg, on refresh or get token)
    /// </summary>
    public class Oauth2Token
    {
        public string refresh_token_expires_in { get; set; }
        public string refresh_token_status { get; set; }
        public string api_product_list { get; set; }
        public string old_access_token_life_time { get; set; }
        public string organization_name { get; set; }
        //public string developer.email               { get; set; }    
        public string token_type { get; set; }          //*     "token_type": "BearerToken",
        public string issued_at { get; set; }           //      "issued_at": "1485788895129",
        public string user_id { get; set; }
        public string client_id { get; set; }
        public string access_token { get; set; }        //*     "access_token": "F2hyJtEltXhyTxDIJ1VGjf3lpQfv",
        public string refresh_token { get; set; }       //*     "refresh_token": "Irdm5xtQ02d5bZG7JbaN825P7rAPP2RT",
        public string application_name { get; set; }
        public string scope { get; set; }
        public string refresh_token_issued_at { get; set; }
        public string expires_in { get; set; }          //*     "expires_in": "1799",
        public string refresh_count { get; set; }
        public string status { get; set; }
        public string base_uri { get; set; }            //the sdk base uri
    }

    #endregion

}



