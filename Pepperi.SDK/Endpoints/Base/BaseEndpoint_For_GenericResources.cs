using Pepperi.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Pepperi.SDK.Model.Fixed;
using System.IO;
using Pepperi.SDK.Contracts;
using Pepperi.SDK.Model.Fixed.MetaData;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Pepperi.SDK.Endpoints.Base
{
    internal class BaseEndpoint_For_GenericResources<TModel>
    {
        #region Properties

        private string ResourceName { get; set; }
        private string ApiBaseUri { get; set; }
        private string IpaasBaseUri { get; set; }
        private AuthentificationManager AuthentificationManager { get; set; }
        private ILogger Logger { get; set; }

        private AuditLogsEndpoint AuditLogs { get; set; }

        #endregion

        #region constructor

        internal BaseEndpoint_For_GenericResources(string resourceName, AuthentificationManager AuthentificationManager, ILogger Logger, 
            string papiBaseUri = null, string ipaasBaseUrl = null, AuditLogsEndpoint auditLogs = null)
        {
            this.ResourceName = resourceName;
            this.ApiBaseUri = papiBaseUri;
            this.IpaasBaseUri = ipaasBaseUrl;
            this.AuthentificationManager = AuthentificationManager;
            this.Logger = Logger;

            this.AuditLogs = auditLogs ?? new AuditLogsEndpoint(ApiBaseUri, AuthentificationManager?.PepperiApiAuth, Logger);
        }

        #endregion

        #region Public Methods

        internal TModel Upsert(TModel Model)
        {
            ValuesValidator.Validate(this.ResourceName, "Resource Name is empty!");
            string RequestUri = $"resources/{this.ResourceName}";

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

        /// <summary>
        /// Limitations:
        /// 1) Maximum input is 6MB
        /// 2) A single object cannot exceed 400KB
        /// 3) Maximum of 500 objects in input
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        internal IEnumerable<GenericResource_ImportData_Response> ImportData(IEnumerable<TModel> data, bool? overwriteObject = null)
        {
            ValuesValidator.Validate(this.ResourceName, "Resource Name is empty!");
            ValuesValidator.Validate(data, "Scheme Name is empty!");

            var subUrl = $"resources/{this.ResourceName}/import/data";

            var requestData = new GenericResource_ImportData_Request<TModel>()
            {
                Objects = data,
                OverwriteObject = overwriteObject
            };
            var body = PepperiJsonSerializer.Serialize(requestData);
            var response = PostPepperiApi(subUrl, body);

            return PepperiJsonSerializer.DeserializeCollection<GenericResource_ImportData_Response>(response.Body);
        }

        internal IEnumerable<TModel> Find(string where = null, string fields = null, string order_by = null, int? page = null, int? page_size = null)
        {
            PepperiHttpClientResponse PepperiHttpClientResponse = FindWithResponse(where, fields, order_by, page, page_size);

            IEnumerable<TModel> result = PepperiJsonSerializer.DeserializeCollection<TModel>(PepperiHttpClientResponse.Body);
            return result;
        }

        internal TModel FindByKey(string key)
        {
            ValuesValidator.Validate(this.ResourceName, "Resource Name is empty!");
            ValuesValidator.Validate(key, "Key is empty!");

            var subUrl = $"resources/{this.ResourceName}/key/{key}";
            var response = GetPepperiApi(subUrl);

            var result = PepperiJsonSerializer.DeserializeOne<TModel>(response.Body);
            return result;
        }

        internal JArray FindGeneric(string where = null, string fields = null, string order_by = null, int? page = null, int? page_size = null)
        {
            PepperiHttpClientResponse PepperiHttpClientResponse = FindWithResponse(where, fields, order_by, page, page_size);
            JsonReader reader = new JsonTextReader(new StringReader(PepperiHttpClientResponse.Body));
            reader.DateParseHandling = DateParseHandling.None;
            JArray obj = JArray.Load(reader);
            return obj;
        }

        internal GenericResource_UploadFile_Result BulkUploadFile(string filePath, bool? overwriteObject = null, bool? overwriteTable = null,
            int poolingInternvalInMs = 1000, int numberOfPoolingAttempts = 60 * 5)
        {
            var fileExtention = GetValidatedFileExtention(filePath);
            var fileData = ReadFileByteArray(filePath);

            return BulkUploadFile(fileData, fileExtention, overwriteObject, overwriteTable, poolingInternvalInMs, numberOfPoolingAttempts);
        }

        internal GenericResource_UploadFile_Result BulkUploadViaFileApi<TData>(IEnumerable<TData> data, bool? overwriteObject = null, bool? overwriteTable = null, int poolingInternvalInMs = 1000, int numberOfPoolingAttempts = 60 * 5)
        {
            var jsonData = PepperiJsonSerializer.Serialize(data);
            return BulkUploadViaFileApi(jsonData, overwriteObject, overwriteTable, poolingInternvalInMs, numberOfPoolingAttempts);
        }

        internal GenericResource_UploadFile_Result BulkUploadViaFileApi(string jsonData, bool? overwriteObject = null, bool? overwriteTable = null, int poolingInternvalInMs = 1000, int numberOfPoolingAttempts = 60 * 5)
        {
            var byteData = Encoding.UTF8.GetBytes(jsonData);
            return BulkUploadFile(byteData, ".json", overwriteObject, overwriteTable, poolingInternvalInMs, numberOfPoolingAttempts);
        }

        internal void ExportAsync(string filePath, string format = null, string where = null, string fields = null,
            string delimiter = null, IEnumerable<string> excludedKeys = null,
            int poolingInternvalInMs = 1000, int numberOfPoolingAttempts = 60 * 5)
        {
            ValuesValidator.Validate(filePath, "File Path is empty!");
            var parent = Directory.GetParent(filePath);
            ValuesValidator.Validate(parent, "Can't get parent folder!");
            var fileExtension = Path.GetExtension(filePath);
            ValuesValidator.Validate(fileExtension == ".json" || fileExtension == ".csv", "Incorrect file extension! (Should be .json or .csv)");

            var fileUrl = ExportFile(format, where, fields, delimiter, excludedKeys, poolingInternvalInMs: poolingInternvalInMs, numberOfPoolingAttempts: numberOfPoolingAttempts);

            this.Logger.Log($"Result File Url: {fileUrl ?? "No URL"}");

            this.Logger.Log($"Retrieving file...");
            var response = GetRequestByteArrayBody(fileUrl);

            this.Logger.Log($"Saving file...");
            File.WriteAllBytes(filePath, response);

            this.Logger.Log($"File was saved!" + Environment.NewLine + Environment.NewLine);
            return;
        }

        #endregion

        #region Private Methods


        internal GenericResource_UploadFile_Result BulkUploadFile(byte[] data, string fileExtention, bool? overwriteObject = null, bool? overwriteTable = null,
             int poolingInternvalInMs = 1000, int numberOfPoolingAttempts = 60 * 5)
        {
            ValuesValidator.Validate(this.ResourceName, "Resource Name is empty!");

            this.Logger.Log("Starting Data Import..." + Environment.NewLine + Environment.NewLine);
            var contentType = MapFileExtentionToContentType(fileExtention);
            var key = Guid.NewGuid().ToString();

            this.Logger.Log("Generated AWS Key - " + key);
            var presignedUrlToPut = GetPresignedUrl(key, contentType);
            this.Logger.Log("Starting to upload data to AWS with generated url - " + presignedUrlToPut);

            var statusCode = UploadDataToAws(presignedUrlToPut, data, contentType);
            this.Logger.Log("Data was uploaded to AWS with Status Code - " + statusCode);
            ValuesValidator.Validate(statusCode == HttpStatusCode.OK, "Can't upload data to AWS!");

            var ipaasUrlToGetFile = GetIpaasUrl($"Aws/GetAwsData?key={key}&ClientToken={this.AuthentificationManager.IdpAuth.APIToken}&contentType={contentType}&extention={fileExtention}");
            this.Logger.Log("Generated IPAAS URL - " + ipaasUrlToGetFile + Environment.NewLine + Environment.NewLine);

            this.Logger.Log("Starting importing process...");
            var importFileResponse = ImportFileToPepperi(ipaasUrlToGetFile, overwriteObject, overwriteTable, poolingInternvalInMs, numberOfPoolingAttempts);

            this.Logger.Log("Retrieving importing result Data...");
            var result = GetImportFileDataResult(importFileResponse);
            return result;
        }

        private string GenerateKey(IDictionary<string, object> row, IEnumerable<string> fields)
        {
            var key = "";
            foreach (var field in fields)
            {
                key += row.ContainsKey(field) ? row[field] : "";
            }
            return key;
        }

        private string ExportFile(string format = null, string where = null,
            string fields = null, string delimiter = null, IEnumerable<string> excludedKeys = null,
            int poolingInternvalInMs = 1000, int numberOfPoolingAttempts = 60 * 5)
        {

            this.Logger.Log("Starting to Export File..." + Environment.NewLine + Environment.NewLine);
            var exportFileResponse = SendExportFileRequest(format, where, fields, delimiter, excludedKeys);
            this.Logger.Log("Retrieved Audit Log ID: " + (exportFileResponse?.ExecutionUUID ?? "No Log ID") + Environment.NewLine + Environment.NewLine);
            var auditLogId = exportFileResponse.ExecutionUUID;
            var finalAuditLog = this.AuditLogs.AuditLogPolling(auditLogId,
                poolingInternvalInMs: poolingInternvalInMs,
                numberOfPoolingAttempts: numberOfPoolingAttempts);

            this.AuditLogs.ValidateSuccessAuditLog(finalAuditLog, "Export File");

            var resultObject = finalAuditLog?.AuditInfo?.ResultObject;
            ValuesValidator.Validate(resultObject, "Can't get result Object from final audit log!");

            var parsed = PepperiJsonSerializer.DeserializeOne<IDictionary<string, string>>(resultObject);

            ValuesValidator.Validate(parsed.ContainsKey("URI"), "Parsed result Object does not contatin URI!");
            var resultUri = parsed["URI"];

            return resultUri;
        }

        /// <summary>
        /// Only for json format
        /// </summary>
        private IEnumerable<TData> ExportAsync<TData>(string where = null, string fields = null, string delimiter = null, IEnumerable<string> excludedKeys = null)
        {
            var fileUrl = ExportFile("json", where, fields, delimiter, excludedKeys);
            var response = GetRequestBody(fileUrl);
            return PepperiJsonSerializer.DeserializeCollection<TData>(response);
        }

        private PepperiHttpClientResponse FindWithResponse(string where = null, string fields = null, string order_by = null, int? page = null, int? page_size = null)

        {
            string RequestUri = $"resources/{this.ResourceName}";

            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            if (where != null) { dicQueryStringParameters.Add("where", where); }
            if (fields != null) { dicQueryStringParameters.Add("fields", fields); }
            if (order_by != null) { dicQueryStringParameters.Add("order_by", order_by); }
            if (page.HasValue) { dicQueryStringParameters.Add("page", page.Value.ToString()); }
            if (page_size.HasValue) { dicQueryStringParameters.Add("page_size", page_size.Value.ToString()); }

            return GetPepperiApi(RequestUri, dicQueryStringParameters);
        }

        private PepperiHttpClientResponse GetPepperiApi(string requestUrl, Dictionary<string, string> dicQueryStringParameters = null)
        {
            string accept = "application/json";
            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.AuthentificationManager.IdpAuth, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Get(
                    ApiBaseUri,
                    requestUrl,
                    dicQueryStringParameters ?? new Dictionary<string, string>() { },
                    accept
                    );

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            return PepperiHttpClientResponse;
        }

        private PepperiHttpClientResponse PostPepperiApi(string requestUrl, string body)
        {
            string contentType = "application/json";
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.AuthentificationManager.IdpAuth, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.PostStringContent(
                    ApiBaseUri,
                    requestUrl,
                    new Dictionary<string, string>() { },
                    body,
                    contentType,
                    accept
                    );

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            return PepperiHttpClientResponse;
        }

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
            var HttpClient = new HttpClient(new LoggingHandler(this.Logger));

            //send request
            HttpClient.Timeout = new TimeSpan(0, 5, 0);// by default wait 5 minutes
            var content = new ByteArrayContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            HttpResponseMessage HttpResponseMessage = HttpClient.PutAsync(url, content).Result;
            string body = HttpResponseMessage.Content.ReadAsStringAsync().Result;
            return HttpResponseMessage.StatusCode;
        }

        private PepperiAuditLog ImportFileToPepperi(string fileUrl, bool? overwriteObject = null, bool? overwriteTable = null,
             int poolingInternvalInMs = 1000, int numberOfPoolingAttempts = 60 * 5)
        {
            var importFileResponse = SendImportFileToPepperiRequest(fileUrl, overwriteObject, overwriteTable);
            var auditLogId = importFileResponse.ExecutionUUID;
            this.Logger.Log("Retrieved Audit Log ID: " + (auditLogId ?? "No Log ID") + Environment.NewLine + Environment.NewLine);
            var finalAuditLog = this.AuditLogs.AuditLogPolling(auditLogId, poolingInternvalInMs, numberOfPoolingAttempts);

            this.AuditLogs.ValidateSuccessAuditLog(finalAuditLog, "Import file");

            return finalAuditLog;
        }

        private PepperiResponseForAuditLog SendImportFileToPepperiRequest(string fileUrl, bool? overwriteObject = null, bool? overwriteTable = null)
        {
            var RequestUri = $"resources/{HttpUtility.UrlEncode(this.ResourceName)}/import/file";
            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.AuthentificationManager.IdpAuth, this.Logger);

            string postBody = PepperiJsonSerializer.Serialize(new GenericResource_UploadFile_Request()
            {
                URI = fileUrl,
                OverwriteObject = overwriteObject,
                OverwriteTable = overwriteTable
            });
            string contentType = "application/json";
            string accept = "application/json";

            Logger.Log($"Sending Import File Request...{Environment.NewLine}URL - {ApiBaseUri + RequestUri}{Environment.NewLine}Body - {postBody}");
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

        /// <summary>
        /// 
        /// output format. either json or csv. defaults to json.
        /// <param name="format"></param>
        /// 
        /// include deleted records. Default is false. 
        /// <param name="includeDeleted"></param>
        /// 
        /// Where clause. Empty where clause will export the entire resource. 'Key LIKE "I%"'
        /// <param name="where"></param>
        /// 
        /// The fields to be exported from the object. If omitted, exports the entire object. "Key,ExtraField,ImportantField"
        /// <param name="fields"></param>
        /// 
        /// in case of CSV. the default is the distributor's default delimiter.
        /// Must not be '.' dot. Must be exactly 1 char. ";"
        /// <param name="delimiter"></param>
        /// 
        /// Array of keys that should be excluded from the export.
        /// Records that match the keys will not be included in the final export file.
        /// <param name="excludedKeys"></param>
        /// 
        /// <returns></returns>
        private PepperiResponseForAuditLog SendExportFileRequest(string format = null, string where = null,
            string fields = null, string delimiter = null, IEnumerable<string> excludedKeys = null)
        {

            var requestUri = $"resources/{HttpUtility.UrlEncode(this.ResourceName)}/export/file";
            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.AuthentificationManager.IdpAuth, this.Logger);

            string postBody = PepperiJsonSerializer.Serialize(new UDC_ExportFile_Request()
            {
                Format = format,
                Where = where,
                Fields = fields,
                Delimiter = delimiter,
                ExcludedKeys = excludedKeys
            });
            string contentType = "application/json";
            string accept = "application/json";

            Logger.Log($"Sending Export File Request...\nURL - {ApiBaseUri + requestUri}\nBody - {postBody}");
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.PostStringContent(
                    ApiBaseUri,
                    requestUri,
                    new Dictionary<string, string>(),
                    postBody,
                    contentType,
                    accept
                    );

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);
            return PepperiJsonSerializer.DeserializeOne<PepperiResponseForAuditLog>(PepperiHttpClientResponse.Body);
        }

        private GenericResource_UploadFile_Result GetImportFileDataResult(PepperiAuditLog auditLog)
        {
            var logResultObject = auditLog?.AuditInfo?.ResultObject;
            ValuesValidator.Validate(logResultObject, $"Import File: Can't get Audit Log Result Object!", false);

            var parsedResultObject = PepperiJsonSerializer.DeserializeOne<GenericResource_UploadFile_AuditLog_ResultObject>(logResultObject);
            var url = parsedResultObject?.URI;

            ValuesValidator.Validate(url, $"Can't get URI Response! Response Error Message: {parsedResultObject?.ErrorMessage ?? "No Message"}");

            Logger.Log($"URL with upload result data - {url}");

            var HttpClient = new HttpClient(new LoggingHandler(this.Logger)) { };

            //send request
            HttpClient.Timeout = new TimeSpan(0, 5, 0);// by default wait 5 minutes
            HttpResponseMessage HttpResponseMessage = HttpClient.GetAsync(url).Result;
            string body = HttpResponseMessage.Content.ReadAsStringAsync().Result;

            ValuesValidator.Validate(HttpResponseMessage.StatusCode == HttpStatusCode.OK, $"Can't get Required File! (Status Code - {HttpResponseMessage.StatusCode})");

            var table = PepperiJsonSerializer.DeserializeCollection<GenericResource_UploadFile_Row>(body);

            var result = new GenericResource_UploadFile_Result()
            {
                Total = table.Count(),
                TotalUpdated = 0,
                TotalIgnored = 0,
                TotalFailed = 0,
                TotalMergedBeforeUpload = 0,
                FailedRows = new List<GenericResource_UploadFile_Row>() { }
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
                    case "Merge":
                        result.TotalMergedBeforeUpload = gropedRow.Count();
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


        private string GetRequestBody(string url)
        {
            var HttpClient = new HttpClient(new LoggingHandler(this.Logger)) { };

            //send request
            HttpClient.Timeout = new TimeSpan(0, 5, 0);// by default wait 5 minutes
            HttpResponseMessage HttpResponseMessage = HttpClient.GetAsync(url).Result;
            string body = HttpResponseMessage.Content.ReadAsStringAsync().Result;
            return body;
        }

        private byte[] GetRequestByteArrayBody(string url)
        {
            var HttpClient = new HttpClient(new LoggingHandler(this.Logger)) { };

            //send request
            HttpClient.Timeout = new TimeSpan(0, 5, 0);// by default wait 5 minutes
            HttpResponseMessage HttpResponseMessage = HttpClient.GetAsync(url).Result;
            var body = HttpResponseMessage.Content.ReadAsByteArrayAsync().Result;
            return body;
        }

        private string GetIpaasUrl(string path)
        {
            return this.IpaasBaseUri + path;
        }
        #endregion
    }
}
