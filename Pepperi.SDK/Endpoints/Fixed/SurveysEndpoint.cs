using Pepperi.SDK.Contracts;
using Pepperi.SDK.Helpers;
using Pepperi.SDK.Model.Fixed;
using Pepperi.SDK.Model.Fixed.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Endpoints.Fixed
{
    public class SurveysEndpoint
    {
        private string ResourceName { get; set; } = "MySurveys";

        private string ApiBaseUri { get; set; }
        private string IpaasBaseUri { get; set; }
        private AuthentificationManager AuthentificationManager { get; set; }
        private ILogger Logger { get; set; }

        #region constructor

        internal SurveysEndpoint(AuthentificationManager AuthentificationManager, ILogger Logger)
        {
            this.ApiBaseUri = "https://papi.pepperi.com/v1.0/";
            this.IpaasBaseUri = "https://integration.pepperi.com/prod/api/";
            this.AuthentificationManager = AuthentificationManager;
            this.Logger = Logger;
        }

        #endregion

        #region Public Methods

        #region Read / Get

        public IEnumerable<Survey> Find()
        {

            var response = GetSurveyApi();
            var result = PepperiJsonSerializer.DeserializeCollection<Survey>(response.Body);
            return result;
        }

        public Survey Upsert(Survey survey)
        {
            string postBody = PepperiJsonSerializer.Serialize(survey);

            var response = PostSurveyApi(postBody);

            var result = PepperiJsonSerializer.DeserializeOne<Survey>(response.Body);
            return result;
        }

        #endregion

        #endregion

        #region Private Methods

        #region Helpers

        private PepperiHttpClientResponse GetSurveyApi(Dictionary<string, string> dicQueryStringParameters = null)
        {
            string accept = "application/json";
            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.AuthentificationManager.IdpAuth, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Get(
                    this.GetResourceBaseUrl(),
                    "",
                    dicQueryStringParameters ?? new Dictionary<string, string>() { },
                    accept
                    );

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            return PepperiHttpClientResponse;
        }

        private PepperiHttpClientResponse PostSurveyApi(string body)
        {
            string contentType = "application/json";
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.AuthentificationManager.IdpAuth, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.PostStringContent(
                    this.GetResourceBaseUrl(),
                    "",
                    new Dictionary<string, string>() { },
                    body,
                    contentType,
                    accept
                    );

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            return PepperiHttpClientResponse;
        }

        private string GetResourceBaseUrl()
        {
            return this.ApiBaseUri + "resources/" + this.ResourceName;
        }

        #endregion

        #endregion

    }
}
