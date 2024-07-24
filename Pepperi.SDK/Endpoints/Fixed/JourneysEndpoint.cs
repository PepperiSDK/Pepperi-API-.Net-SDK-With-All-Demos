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

        #region Files

        /// <summary>
        /// Method supports paging with pageKey. First request should be with null/empty "pageKey" param. Result can contain "NextPageKey" property value. 
        /// If it doesn't - there are no other pages. If it does - you can access next page by passing "NextPageKey" value to "pageKey" param and calling method again.
        /// </summary>
        /// <param name="where"></param>
        /// <param name="pageKey"></param>
        /// <returns></returns>
        public SearchJourneyFilesResult SearchFiles(string where = null, string pageKey = null)
        {
            var response = SearchFilesRequest(where, pageKey);
            var result = PepperiJsonSerializer.DeserializeOne<SearchJourneyFilesResult>(response.Body);

            ValuesValidator.Validate(result, "Response is null!");
            ValuesValidator.Validate(result.Objects, "Response files are null!");

            return result;
        }


        public IEnumerable<JourneyFile> SearchAllFiles(string where = null, int timeoutMinutes = 300)
        {
            Logger.Log("Search All Files started!");

            var timeoutDateTime = DateTime.UtcNow.AddMinutes(timeoutMinutes);
            //var maxPageSize = 100;
            var finalResult = new List<JourneyFile>();

            Logger.Log($"Where clause: \"{where}\", timeoutMinutes: {timeoutMinutes}");

            var pageKey = string.Empty;
            var shouldStop = false;
            while (!shouldStop) {
                var searchResult = SearchFiles(where, pageKey);

                pageKey = searchResult.NextPageKey;

                finalResult.AddRange(searchResult.Objects);

                shouldStop = string.IsNullOrEmpty(pageKey);

                // If we already have done paging
                if (!shouldStop) {
                    var timeoutWasExceeded = DateTime.UtcNow > timeoutDateTime;
                    ValuesValidator.Validate(!timeoutWasExceeded, $"Timeout of {timeoutMinutes} minutes was exceeded!", false);
                }
            }

            Logger.Log($"Search all files finished. Total Journey files count: {finalResult.Count()}");
            return finalResult;
        }

        #endregion

        public SearchJourneysResult<dynamic> SearchJourneys(string where = null, string pageKey = null)
        {
            var searchFilesResult = SearchFiles(where, pageKey);

            var journeys = GetJourneysFromFiles<dynamic>(searchFilesResult.Objects);
            journeys.NextPageKey = searchFilesResult.NextPageKey;

            return journeys;
        }

        public SearchJourneysResult<TData> SearchJourneys<TData>(string where = null, string pageKey = null)
        {
            var searchFilesResult = SearchFiles(where, pageKey);

            var journeys = GetJourneysFromFiles<TData>(searchFilesResult.Objects);
            journeys.NextPageKey = searchFilesResult.NextPageKey;

            return journeys;
        }

        public SearchJourneysResult<dynamic> SearchAllJourneys(string where = null, int timeoutMinutes = 300)
        {
            var searchFilesResult = SearchAllFiles(where, timeoutMinutes);

            var journeys = GetJourneysFromFiles<dynamic>(searchFilesResult);

            return journeys;
        }

        public SearchJourneysResult<TData> SearchAllJourneys<TData>(string where = null, int timeoutMinutes = 300)
        {
            var searchFilesResult = SearchAllFiles(where, timeoutMinutes);

            var journeys = GetJourneysFromFiles<TData>(searchFilesResult);

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

        private PepperiHttpClientResponse SearchFilesRequest(string where, string pageKey = null, int? pageSize = null)
        {
            var url = $"files/search";
            var body = new Dictionary<string, object>() { };

            if (!string.IsNullOrEmpty(where)) body["Where"] = where;
            if (!string.IsNullOrEmpty(pageKey)) body["PageKey"] = pageKey;

            if (pageSize.HasValue) {
                ValuesValidator.Validate(pageSize.Value > 0, $"Page size should be > 0, but got {pageSize.Value}!");
                body["PageSize"] = pageSize.Value;
            }

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
