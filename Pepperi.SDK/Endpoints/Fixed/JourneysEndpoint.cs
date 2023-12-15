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
    public class JourneysEndpoint
    {

        private string ApiBaseUri { get; set; }
        private string IpaasBaseUri { get; set; }
        private AuthentificationManager AuthentificationManager { get; set; }
        private ILogger Logger { get; set; }

        #region constructor

        internal JourneysEndpoint(AuthentificationManager AuthentificationManager, ILogger Logger, string papiBaseUri = null, string ipaasBaseUrl = null)
        {
            this.ApiBaseUri = papiBaseUri;
            this.IpaasBaseUri = ipaasBaseUrl;
            this.AuthentificationManager = AuthentificationManager;
            this.Logger = Logger;
        }

        #endregion

        #region Public Methods

        #region Search

        public IEnumerable<JourneyFile> SearchFiles(string where = null)
        {
            var response = SearchFilesRequest(where);
            var files = PepperiJsonSerializer.DeserializeCollection<JourneyFile>(response.Body);

            ValuesValidator.Validate(files != null, "Response Files are null!");

            return files;
        }

        public SearchJourneysResult<dynamic> SearchJourneys(string where = null)
        {
            var files = SearchFiles(where);

            var journeys = GetJourneysFromFiles<dynamic>(files);

            return journeys;
        }

        public SearchJourneysResult<TData> SearchJourneys<TData>(string where = null)
        {
            var files = SearchFiles(where);

            var journeys = GetJourneysFromFiles<TData>(files);

            return journeys;
        }

        #endregion

        #endregion

        #region Private Methods

        #region Specific Helpers

        private SearchJourneysResult<TData> GetJourneysFromFiles<TData>(IEnumerable<JourneyFile> files)
        {
            ValuesValidator.Validate(files, "Files are null!");

            var result = new SearchJourneysResult<TData>();
            if (files.Count() == 0) return result;

            Log($"Starting to retrieve journeys from {files.Count()} Files...");

            var failedFiles = new List<SearchJourneysResult_FailedFiles>();
            var currentFileNumber = 1;
            foreach (var file in files)
            {
                Log($"Starting to process {currentFileNumber} file...");
                try
                {
                    var fileJourneys = GetJourneysFromFile<TData>(file);
                    result.Journeys = result.Journeys.Concat(fileJourneys);
                    Log($"File was processed! Current Total Journeys count: {result.Journeys.Count()}");
                }
                catch (Exception ex)
                {
                    var message = ex?.Message ?? "No Message";
                    Log($"Error with getting Journeys from file. Message: {message}");
                    failedFiles.Add(new SearchJourneysResult_FailedFiles()
                    {
                        FileUrl = file?.URL,
                        ErrorMessage = message
                    });
                }

                currentFileNumber++;
            }

            result.FailedFiles = failedFiles;
            Log($"Journeys were retrieved! Total journeys count: {result.Journeys.Count()}");
            return result;
        }

        private IEnumerable<Journey<TData>> GetJourneysFromFile<TData>(JourneyFile file)
        {
            var fileUrl = file?.URL;
            ValuesValidator.Validate(fileUrl, "File Url is empty!");

            Log($"Starting to get Journeys from url: \"{fileUrl}\"");
            var journeysJson = GetJourneysJsonFromUrl(fileUrl);

            ValuesValidator.Validate(journeysJson, "Retrieved JSON is empty! (\"{fileUrl}\")");
            Log($"Retrieved JSON string with {journeysJson.Length} Length. Starting to parse...");

            var parsed = TextParser.ParseJsonRows<Journey<TData>>(journeysJson);
            Log($"JSON was parsed with {parsed.Count()} count");

            return parsed;
        }

        private string GetJourneysJsonFromUrl(string url)
        {
            var response = GetRequestBody(url);
            return response;
        }

        #endregion

        #region Requests

        private PepperiHttpClientResponse SearchFilesRequest(string where)
        {
            var url = $"files/search";
            var body = new Dictionary<string, string>() { };

            if (!string.IsNullOrEmpty(where)) body["Where"] = where;

            return PostResourseApi(url, PepperiJsonSerializer.Serialize(body));
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
            return this.ApiBaseUri + "journey/";
        }

        private void Log(string message)
        {
            this.Logger.Log(message + Environment.NewLine);
        }

        #endregion

        #endregion

    }
}
