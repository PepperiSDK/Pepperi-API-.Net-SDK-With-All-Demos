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
    public class IpaasAddonAuthentification : IAuthentication
    {
        #region Properties
        private ILogger Logger { get; set; }

        public string APIToken { get; private set; }           //Identifies the user whose data is manipulated. Sent as the "password" on each request of the private application                    (Basic authnetication username=TokenAuth  password=APIToken)
        private string IdpToken { get; set; }
        private string IpaasBaseurl { get; set; }

        public bool IsIdpToken { get; set; }


        #endregion

        #region Constructor

        public IpaasAddonAuthentification(ILogger Logger, string APIToken, bool IsIdpToken = true)
        {
            this.Logger = Logger;
            this.APIToken = APIToken;
            this.IsIdpToken = IsIdpToken;
            this.IpaasBaseurl = "https://integration.pepperi.com/prod/api/";

            //if (IsIdpToken) {
            //    this.IdpToken = GetIdpTokenData();
            //}
        }

        #endregion

        #region IAuthentication

        public AuthenticationHeaderValue GetAuthorizationHeaderValue()
        {
            var result = new AuthenticationHeaderValue(
                        "Bearer",
                        IsIdpToken ? GetIdpTokenData() : APIToken);

            return result;
        }

        public Dictionary<string, string> GetCustomRequestHeaders()
        {
            var result = new Dictionary<string, string>();
            return result;
        }

        #endregion

        #region Private methods

        private string GetIdpTokenData()
        {
            var baseUrl = this.IpaasBaseurl;
            string RequestUri = "PepperiApi/GetIdpToken";
            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();

            string accept = "application/json";

            IAuthentication Authentication = new IpaasAddonAuthentification(this.Logger, this.APIToken, false);

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(Authentication, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Get(
                    baseUrl,
                    RequestUri,
                    dicQueryStringParameters,
                    accept);

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            var response = PepperiJsonSerializer.DeserializeOne<GetIpaasTokenResponse>(PepperiHttpClientResponse.Body);   //Api returns single object
            //string result = APITokenData.APIToken;

            //return result;
            return response?.Data;
        }

        #endregion
    }

    #region Additional Models

    internal class GetIpaasTokenResponse
    {
        public string Data { get; set; }
    }

    #endregion
}


