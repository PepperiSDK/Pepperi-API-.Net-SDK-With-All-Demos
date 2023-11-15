using Pepperi.SDK.Contracts;
using Pepperi.SDK.Helpers;
using Pepperi.SDK.Model.Fixed;
using Pepperi.SDK.Model.Fixed.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Pepperi.SDK.Endpoints.Fixed
{
    public class NotificationsEndpoint
    {

        private string ApiBaseUri { get; set; }
        private string IpaasBaseUri { get; set; }
        private AuthentificationManager AuthentificationManager { get; set; }
        private ILogger Logger { get; set; }

        #region constructor

        internal NotificationsEndpoint(AuthentificationManager AuthentificationManager, ILogger Logger, string papiBaseUri = null, string ipaasBaseUrl = null)
        {
            this.ApiBaseUri = papiBaseUri;
            this.IpaasBaseUri = ipaasBaseUrl;
            this.AuthentificationManager = AuthentificationManager;
            this.Logger = Logger;
        }

        #endregion

        #region Public Methods

        #region Search

        public IEnumerable<Notification> Find(string where = null, string order_by = null, string fields = null)
        {
            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            if (where != null) { dicQueryStringParameters.Add("where", where); }
            if (order_by != null) { dicQueryStringParameters.Add("order_by", order_by); }
            if (fields != null) { dicQueryStringParameters.Add("fields", fields); }

            var response = GetResourseApi("", dicQueryStringParameters);
            var result = PepperiJsonSerializer.DeserializeCollection<Notification>(response.Body);

            return result;
        }

        #endregion

        public Notification Post(Notification notification)
        {
            ValuesValidator.Validate(notification, "Notification is empty!");
            var body = PepperiJsonSerializer.Serialize(notification);

            var response = PostResourseApi("", body);
            var result = PepperiJsonSerializer.DeserializeOne<Notification>(response.Body);

            return result;
        }

        #endregion

        #region Private Methods

        #region Requests

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

        private PepperiHttpClientResponse PostResourseApi(string requestUri, string body, Dictionary<string, string> dicQueryStringParameters = null)
        {
            string contentType = "application/json";
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.AuthentificationManager.IdpAuth, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.PostStringContent(
                    this.GetResourceBaseUrl(),
                    requestUri,
                    dicQueryStringParameters ?? new Dictionary<string, string>() { },
                    body,
                    contentType,
                    accept
                    );

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            return PepperiHttpClientResponse;
        }

        private string GetRequestBody(string url, int timeoutMinutes = 5)
        {
            var HttpClient = new HttpClient(new LoggingHandler(this.Logger)) { };

            //send request
            HttpClient.Timeout = new TimeSpan(0, timeoutMinutes, 0);// by default wait 5 minutes
            using (var HttpResponseMessage = HttpClient.GetAsync(url).Result)
            {
                string body = HttpResponseMessage.Content.ReadAsStringAsync().Result;
                ValuesValidator.Validate(HttpResponseMessage.IsSuccessStatusCode, $"Got Response '{HttpResponseMessage.StatusCode.ToString()}' with Status Code! Response Body: \"{body ?? "No Body"}\"!");
                return body;
            };
        }

        private string GetResourceBaseUrl()
        {
            return this.ApiBaseUri + "push_notifications";
        }

        private void Log(string message)
        {
            this.Logger.Log(message + Environment.NewLine);
        }

        #endregion

        #endregion

    }
}
