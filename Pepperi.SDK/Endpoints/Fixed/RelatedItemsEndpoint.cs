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
using Pepperi.SDK.Endpoints.Base;

namespace Pepperi.SDK.Endpoints.Fixed
{
    public class RelatedItemsEndpoint
    {
        private string ApiBaseUri { get; set; }
        private string IpaasBaseUri { get; set; }
        private AuthentificationManager AuthentificationManager { get; set; }
        private ILogger Logger { get; set; }

        private BaseEndpoint_For_GenericResources<RelatedItem> BaseEndpoint { get; set; }

        #region constructor

        internal RelatedItemsEndpoint(AuthentificationManager AuthentificationManager, ILogger Logger, 
            string papiBaseUri = null, string ipaasBaseUrl = null,
            AuditLogsEndpoint auditLogs = null)
        {
            this.ApiBaseUri = papiBaseUri;
            this.IpaasBaseUri = ipaasBaseUrl;
            this.AuthentificationManager = AuthentificationManager;
            this.Logger = Logger;

            this.BaseEndpoint = new BaseEndpoint_For_GenericResources<RelatedItem>("related_items",
                AuthentificationManager, Logger, papiBaseUri, ipaasBaseUrl, auditLogs);
        }

        #endregion

        #region Public Methods

        #region Basic GRUD

        public IEnumerable<RelatedItem> Find(string where = null, string fields = null, string order_by = null, int? page = null, int? page_size = null)
        {
            return this.BaseEndpoint.Find(
                where: where, fields: fields, order_by: order_by,
                page: page, page_size: page_size
                );
        }

        public RelatedItem FindByKey(string key)
        {
            return this.BaseEndpoint.FindByKey(key);
        }

        public RelatedItem Upsert(RelatedItem relatedItem)
        {
            return this.BaseEndpoint.Upsert(relatedItem);
        }

        public void ExportAsync(string filePath, string format = null, string where = null, string fields = null,
            string delimiter = null, IEnumerable<string> excludedKeys = null, 
            int poolingInternvalInMs = 1000, int numberOfPoolingAttempts = 60 * 5)
        {
            this.BaseEndpoint.ExportAsync(
                filePath: filePath,
                format: format,
                where: where,
                fields: fields,
                delimiter: delimiter,
                excludedKeys: excludedKeys,
                poolingInternvalInMs: poolingInternvalInMs,
                numberOfPoolingAttempts: numberOfPoolingAttempts
                );

            return;
        }

        public GenericResource_UploadFile_Result BulkUploadFile(string filePath, bool? overwriteObject = null, bool? overwriteTable = null,
            int poolingInternvalInMs = 1000, int numberOfPoolingAttempts = 60 * 5)
        {
            return this.BaseEndpoint.BulkUploadFile(
                filePath: filePath,
                overwriteObject: overwriteObject,
                overwriteTable: overwriteTable,
                poolingInternvalInMs: poolingInternvalInMs,
                numberOfPoolingAttempts: numberOfPoolingAttempts
                );
        }

        public GenericResource_UploadFile_Result BulkUploadViaFileApi(IEnumerable<RelatedItem> data, bool? overwriteObject = null, bool? overwriteTable = null, int poolingInternvalInMs = 1000, int numberOfPoolingAttempts = 60 * 5)
        {
            return this.BaseEndpoint.BulkUploadViaFileApi(
                data: data,
                overwriteObject: overwriteObject,
                overwriteTable: overwriteTable,
                poolingInternvalInMs: poolingInternvalInMs,
                numberOfPoolingAttempts: numberOfPoolingAttempts
                );
        }

        public GenericResource_UploadFile_Result BulkUploadViaFileApi(string jsonData, bool? overwriteObject = null, bool? overwriteTable = null, int poolingInternvalInMs = 1000, int numberOfPoolingAttempts = 60 * 5)
        {
            return this.BaseEndpoint.BulkUploadViaFileApi(
                jsonData: jsonData,
                overwriteObject: overwriteObject,
                overwriteTable: overwriteTable,
                poolingInternvalInMs: poolingInternvalInMs,
                numberOfPoolingAttempts: numberOfPoolingAttempts
                );
        }

        #endregion

        #endregion

        #region Private Methods

        #region Helpers

        private PepperiHttpClientResponse GetResourseApi(string requestUri, Dictionary<string, string> dicQueryStringParameters = null)
        {
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.AuthentificationManager.IdpAuth, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Get(
                    this.ApiBaseUri,
                    GetResourceUrl(requestUri),
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
                    this.ApiBaseUri,
                    GetResourceUrl(requestUri),
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

        private string GetResourceUrl(string url)
        {
            return "resources/related_items" + (string.IsNullOrEmpty(url) ? "" : $"/{url}");
        }

        private void Log(string message)
        {
            this.Logger.Log(message + Environment.NewLine);
        }

        #endregion

        #endregion

    }
}
