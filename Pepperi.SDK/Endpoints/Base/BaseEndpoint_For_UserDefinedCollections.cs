using Pepperi.SDK.Helpers;
using Pepperi.SDK.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Pepperi.SDK.Model.Fixed;
using Pepperi.SDK.Model;
using System.IO;
using System.IO.Compression;
using Pepperi.SDK.Contracts;
using System.Threading;
using System.Reflection;
using Pepperi.SDK.Model.Fixed.MetaData;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;

namespace Pepperi.SDK.Endpoints.Base
{
    public class BaseEndpoint_For_UserDefinedCollections
    {
        #region properties

        private string ApiBaseUri { get; set; }
        private string IpaasBaseUri { get; set; }
        private AuthentificationManager AuthentificationManager { get; set; }
        private ILogger Logger { get; set; }

        private AuditLogsEndpoint AuditLogs { get; set; }
        #endregion

        #region constructor

        protected BaseEndpoint_For_UserDefinedCollections(AuthentificationManager AuthentificationManager, ILogger Logger)
        {
            this.ApiBaseUri = "https://papi.pepperi.com/v1.0/"; // Will be moved to Pepperi API
            this.IpaasBaseUri = "https://ipaas.pepperi.com/prod/api/"; // Will be moved to Pepperi API
            this.AuthentificationManager = AuthentificationManager;
            this.Logger = Logger;

            this.AuditLogs = new AuditLogsEndpoint(ApiBaseUri, AuthentificationManager.PepperiApiAuth, Logger);
        }

        #endregion

        #region Public methods

        public TModel Upsert<TModel>(string collectionName, TModel Model)
        {
            string RequestUri = $"resources/{collectionName}";

            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();

            string postBody = PepperiJsonSerializer.Serialize(Model);                                       //null values are not serialized
            string contentType = "application/json";
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.AuthentificationManager.IdpAuth, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.PostStringContent(
                    ApiBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    postBody,
                    contentType,
                    accept
                    );

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            TModel result = PepperiJsonSerializer.DeserializeOne<TModel>(PepperiHttpClientResponse.Body);
            return result;
        }

        public IEnumerable<TModel> Find<TModel>(string collectionName,
            string where = null, string order_by = null, int? page = null, int? page_size = null,
            bool? include_nested = null, bool? full_mode = null, bool? include_deleted = null, string fields = null, bool? is_distinct = null)
        {
            string RequestUri = $"resources/{collectionName}";

            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            if (where != null) { dicQueryStringParameters.Add("where", where); }
            if (order_by != null) { dicQueryStringParameters.Add("order_by", order_by); }
            if (page.HasValue) { dicQueryStringParameters.Add("page", page.Value.ToString()); }
            if (page_size.HasValue) { dicQueryStringParameters.Add("page_size", page_size.Value.ToString()); }
            if (include_nested.HasValue) { dicQueryStringParameters.Add("include_nested", include_nested.Value.ToString()); }
            if (full_mode.HasValue) { dicQueryStringParameters.Add("full_mode", full_mode.Value.ToString()); }
            if (include_deleted.HasValue) { dicQueryStringParameters.Add("include_deleted", include_deleted.Value.ToString()); }
            if (fields != null) { dicQueryStringParameters.Add("fields", fields); }
            if (is_distinct != null) { dicQueryStringParameters.Add("is_distinct", fields); }

            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.AuthentificationManager.IdpAuth, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Get(
                    ApiBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    accept
                    );

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            IEnumerable<TModel> result = PepperiJsonSerializer.DeserializeCollection<TModel>(PepperiHttpClientResponse.Body);
            return result;
        }

        public UDC_UploadFile_Result BulkUploadFile(string schemeName, string filePath)
        {
            var fileExtention = GetValidatedFileExtention(filePath);
            var contentType = MapFileExtentionToContentType(fileExtention);
            var key = Guid.NewGuid().ToString();
            var presignedUrlToPut = GetPresignedUrl(key, contentType);

            var fileData = ReadFileByteArray(filePath);
            var statusCode = UploadDataToAws(presignedUrlToPut, fileData, contentType);
            ValuesValidator.Validate(statusCode == HttpStatusCode.OK, "Can't upload data to AWS!");

            var ipaasUrlToGetFile = GetIpaasUrl($"Aws/GetAwsData?key={key}&ClientToken={this.AuthentificationManager.IdpAuth.APIToken}&contentType={contentType}&extention={fileExtention}");
            var importFileResponse = ImportFileToPepperi(schemeName, ipaasUrlToGetFile);
            var result = GetImportFileDataResult(importFileResponse);
            return result;
        }


        #endregion

        #region Private Methods

        private string GetPresignedUrl(string key, string contentType = "application/json")
        {
            var requestUri = "Aws/GetAwsPreSignedUrlForPut";
            var queryParams = new Dictionary<string, string>() {
                { "key", key},
                { "contentType", contentType}
            };
            string accept = "application/json";
            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.AuthentificationManager.IpaasAuth, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Get(
                    IpaasBaseUri,
                    requestUri,
                    queryParams,
                    accept
                    );

            var deserialized = PepperiJsonSerializer.DeserializeOne<GenericIpaasResponse<IEnumerable<string>>>(PepperiHttpClientResponse.Body);
            return deserialized.Data.FirstOrDefault();
        }
        private HttpStatusCode UploadDataToAws(string url, byte[] data, string contentType = "application/json")
        {
            var HttpClient = new HttpClient(new LoggingHandler(this.Logger))
            {

            };

            //send request
            HttpClient.Timeout = new TimeSpan(0, 5, 0);// by default wait 5 minutes
            var content = new ByteArrayContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            HttpResponseMessage HttpResponseMessage = HttpClient.PutAsync(url, content).Result;
            string body = HttpResponseMessage.Content.ReadAsStringAsync().Result;
            return HttpResponseMessage.StatusCode;
        }

        private PepperiAuditLog ImportFileToPepperi(string schemeName, string fileUrl)
        {
            var importFileResponse = SendImportFileToPepperiRequest(schemeName, fileUrl);
            var auditLogId = importFileResponse.ExecutionUUID;
            var finalAuditLog = this.AuditLogs.AuditLogPolling(auditLogId);
            return finalAuditLog;
        }

        private PepperiResponseForAuditLog SendImportFileToPepperiRequest(string schemeName, string fileUrl)
        {
            var RequestUri = $"resources/{HttpUtility.UrlEncode(schemeName)}/import/file";
            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.AuthentificationManager.IdpAuth, this.Logger);

            string postBody = PepperiJsonSerializer.Serialize(new Dictionary<string, string>() {
                { "URI", fileUrl}
            });
            string contentType = "application/json";
            string accept = "application/json";

            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.PostStringContent(
                    ApiBaseUri,
                    RequestUri,
                    new Dictionary<string, string>(),
                    postBody,
                    contentType,
                    accept
                    );

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);
            return PepperiJsonSerializer.DeserializeOne<PepperiResponseForAuditLog>(PepperiHttpClientResponse.Body);
        }

        private UDC_UploadFile_Result GetImportFileDataResult(PepperiAuditLog auditLog) {
            var logResultObject = auditLog?.AuditInfo?.ResultObject;
            var parsedResultObject = PepperiJsonSerializer.DeserializeOne<UDC_UploadFile_AuditLog_ResultObject>(logResultObject);
            var url = parsedResultObject?.URI;
            ValuesValidator.Validate(url, "Can't get URI Response!");

            var HttpClient = new HttpClient(new LoggingHandler(this.Logger)){};

            //send request
            HttpClient.Timeout = new TimeSpan(0, 5, 0);// by default wait 5 minutes
            HttpResponseMessage HttpResponseMessage = HttpClient.GetAsync(url).Result;
            string body = HttpResponseMessage.Content.ReadAsStringAsync().Result;

            ValuesValidator.Validate(HttpResponseMessage.StatusCode == HttpStatusCode.OK, $"Can't get Required File! (Status Code - {HttpResponseMessage.StatusCode})");

            var table = PepperiJsonSerializer.DeserializeCollection<UDC_UploadFile_Row>(body);

            var result = new UDC_UploadFile_Result()
            {
                Success = true,
                Total = table.Count(),
                TotalUpdated = 0,
                TotalIgnored = 0,
                TotalFailed = 0,
                FailedRows = new List<UDC_UploadFile_Row>() { }
            };

            var grouped = table.GroupBy(row => row.Status);
            foreach (var gropedRow in grouped)
            {
                switch (gropedRow.Key)
                {
                    case "Error":
                        result.TotalFailed = gropedRow.Count();
                        result.FailedRows = gropedRow.ToList();
                        break;
                    case "Update":
                        result.TotalUpdated = gropedRow.Count();
                        break;
                    case "Insert":
                        result.TotalInserted = gropedRow.Count();
                        break;
                    case "Ignore":
                        result.TotalIgnored = gropedRow.Count();
                        break;
                    default:
                        break;
                }
            }

            
            return result;
        }
        private string GetValidatedFileExtention(string inputFilename)
        {
            var extention = Path.GetExtension(inputFilename);

            var validExtentions = new List<string>() { ".json", ".csv" };
            ValuesValidator.Validate(validExtentions.Contains(extention), $"Incorrect File Extension. Got {extention ?? "null"}, should be {String.Join(", ", validExtentions)}");

            return extention;
        }

        private string MapFileExtentionToContentType(string extention)
        {
            switch (extention)
            {
                case ".json":
                    return "application/json";
                case ".csv":
                    return "text/csv";
                default:
                    ValuesValidator.Validate(false, "Can't Map File Extention to Content Type!");
                    return null;
            }
        }

        private byte[] ReadFileByteArray(string inputFilename)
        {
            return File.ReadAllBytes(inputFilename);
        }

        private string GetIpaasUrl(string path)
        {
            return this.IpaasBaseUri + path;
        }

        #endregion
    }

    public class GenericIpaasResponse<TData>
    {
        public TData Data { get; set; }
        public object BaseException { get; set; }
    }
}
