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
        private string SurveysResourseName { get; set; } = "MySurveys";
        private string SurveysTemplatesResourseName { get; set; } = "MySurveyTemplates";

        private string ApiBaseUri { get; set; }
        private string IpaasBaseUri { get; set; }
        private AuthentificationManager AuthentificationManager { get; set; }
        private ILogger Logger { get; set; }

        #region constructor

        internal SurveysEndpoint(AuthentificationManager AuthentificationManager, ILogger Logger, string papiBaseUri = null, string ipaasBaseUrl = null)
        {
            this.ApiBaseUri = papiBaseUri;
            this.IpaasBaseUri = ipaasBaseUrl;
            this.AuthentificationManager = AuthentificationManager;
            this.Logger = Logger;
        }

        #endregion

        #region Public Methods

        #region Read / Get

        public IEnumerable<Survey> Find(string where = null, string fields = null)
        {
            return Find<Survey>(where, fields);
        }

        public IEnumerable<TObject> Find<TObject>(string where = null, string fields = null)
        {

            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            if (where != null) { dicQueryStringParameters.Add("where", where); }
            if (fields != null) { dicQueryStringParameters.Add("fields", fields); }

            var response = GetSurveyApi(dicQueryStringParameters);
            var result = PepperiJsonSerializer.DeserializeCollection<TObject>(response.Body);
            return result;
        }

        public Survey Upsert(Survey survey)
        {
            return Upsert<Survey>(survey);
        }

        public TObject Upsert<TObject>(TObject survey)
        {
            string postBody = PepperiJsonSerializer.Serialize(survey);

            var response = PostSurveyApi(postBody);

            var result = PepperiJsonSerializer.DeserializeOne<TObject>(response.Body);
            return result;
        }

        #endregion

        #region Survey Templates

        public IEnumerable<SurveyTemplate> FindTemplates(string where = null, string fields = null)
        {
            return FindTemplates<SurveyTemplate>(where, fields);
        }

        public IEnumerable<TObject> FindTemplates<TObject>(string where = null, string fields = null)
        {

            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            if (where != null) { dicQueryStringParameters.Add("where", where); }
            if (fields != null) { dicQueryStringParameters.Add("fields", fields); }

            var response = GetSurveyTemplatesApi(dicQueryStringParameters);
            var result = PepperiJsonSerializer.DeserializeCollection<TObject>(response.Body);
            return result;
        }

        #endregion

        #endregion

        #region Private Methods

        #region Helpers

        private PepperiHttpClientResponse GetSurveyApi(Dictionary<string, string> dicQueryStringParameters = null)
        {
            return GetResourseApi(this.SurveysResourseName, dicQueryStringParameters);
        }

        private PepperiHttpClientResponse PostSurveyApi(string body)
        {
            return PostResourseApi(this.SurveysResourseName, body);
        }

        private PepperiHttpClientResponse GetSurveyTemplatesApi(Dictionary<string, string> dicQueryStringParameters = null)
        {
            return GetResourseApi(this.SurveysTemplatesResourseName, dicQueryStringParameters);
        }

        private PepperiHttpClientResponse GetResourseApi(string requestUri, Dictionary<string, string> dicQueryStringParameters = null)
        {
            string accept = "application/json";
            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.AuthentificationManager.IdpAuth, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Get(
                    this.GetResourceBaseUrl(),
                    requestUri,
                    dicQueryStringParameters ?? new Dictionary<string, string>() { },
                    accept
                    );

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            return PepperiHttpClientResponse;
        }

        private PepperiHttpClientResponse PostResourseApi(string requestUri, string body)
        {
            string contentType = "application/json";
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.AuthentificationManager.IdpAuth, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.PostStringContent(
                    this.GetResourceBaseUrl(),
                    requestUri,
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
            return this.ApiBaseUri + "resources/";
        }

        #endregion

        #endregion

    }
}