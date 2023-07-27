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

        protected BaseEndpoint_For_UserDefinedCollections(AuthentificationManager AuthentificationManager, ILogger Logger,
            string papiBaseUri = null, string ipaasBaseUrl = null)
        {
            this.ApiBaseUri = papiBaseUri;
            this.IpaasBaseUri = ipaasBaseUrl;
            this.AuthentificationManager = AuthentificationManager;
            this.Logger = Logger;

            this.AuditLogs = new AuditLogsEndpoint(ApiBaseUri, AuthentificationManager?.PepperiApiAuth, Logger);
        }

        #endregion

        #region Public methods


        #region CRUD Operations
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

        /// <summary>
        /// Limitations:
        /// 1) Maximum input is 6MB
        /// 2) A single object cannot exceed 400KB
        /// 3) Maximum of 500 objects in input
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <returns></returns>
        public IEnumerable<UDC_ImportData_Response> ImportData<TData>(string schemeName, IEnumerable<TData> data, bool? overwriteObject = null)
        {

            ValuesValidator.Validate(schemeName, "Scheme Name is empty!");
            ValuesValidator.Validate(data, "Scheme Name is empty!");

            var subUrl = $"resources/{schemeName}/import/data";

            var requestData = new UDC_ImportData_Request<TData>()
            {
                Objects = data,
                OverwriteObject = overwriteObject
            };
            var body = PepperiJsonSerializer.Serialize(requestData);
            var response = PostPepperiApi(subUrl, body);

            return PepperiJsonSerializer.DeserializeCollection<UDC_ImportData_Response>(response.Body);
        }

        public IEnumerable<TModel> Find<TModel>(string collectionName,
            string where = null, string fields = null, string order_by = null, int? page = null, int? page_size = null)
        {
            PepperiHttpClientResponse PepperiHttpClientResponse = Find(collectionName, where, fields, order_by, page, page_size);

            IEnumerable<TModel> result = PepperiJsonSerializer.DeserializeCollection<TModel>(PepperiHttpClientResponse.Body);
            return result;
        }

        public TModel FindByKey<TModel>(string schemeName, string key)
        {
            ValuesValidator.Validate(schemeName, "Scheme Name is empty!");
            ValuesValidator.Validate(key, "Key is empty!");

            var subUrl = $"resources/{schemeName}/key/{key}";
            var response = GetPepperiApi(subUrl);

            var result = PepperiJsonSerializer.DeserializeOne<TModel>(response.Body);
            return result;
        }

        public JArray FindGeneric(string collectionName,
            string where = null, string fields = null, string order_by = null, int? page = null, int? page_size = null)
        {
            PepperiHttpClientResponse PepperiHttpClientResponse = Find(collectionName, where, fields, order_by, page, page_size);
            JsonReader reader = new JsonTextReader(new StringReader(PepperiHttpClientResponse.Body));
            reader.DateParseHandling = DateParseHandling.None;
            JArray obj = JArray.Load(reader);
            return obj;
        }

        public bool Delete(string schemeName, string key)
        {
            ValuesValidator.Validate(schemeName, "Scheme Name is empty!");
            ValuesValidator.Validate(key, "Key is empty!");

            var subUrl = $"resources/{schemeName}";

            var body = PepperiJsonSerializer.Serialize(new UDC_Base_Row()
            {
                Hidden = true,
                Key = key
            });
            var response = PostPepperiApi(subUrl, body);

            return PepperiJsonSerializer.DeserializeOne<UDC_Base_Row>(response.Body)?.Hidden == true;
        }

        #endregion

        public UDC_UploadFile_Result BulkUploadFile(string schemeName, string filePath, bool? overwriteObject = null, bool? overwriteTable = null, int poolingInternvalInMs = 5000, int numberOfPoolingAttempts = 500)
        {
            var fileExtention = GetValidatedFileExtention(filePath);
            var fileData = ReadFileByteArray(filePath);

            return BulkUploadFile(schemeName, fileData, fileExtention, overwriteObject, overwriteTable, poolingInternvalInMs, numberOfPoolingAttempts);
        }

        public UDC_UploadFile_Result BulkUploadViaFileApi<TData>(string schemeName, IEnumerable<TData> data, bool? overwriteObject = null, bool? overwriteTable = null, int poolingInternvalInMs = 5000, int numberOfPoolingAttempts = 500)
        {
            var jsonData = PepperiJsonSerializer.Serialize(data);
            return BulkUploadViaFileApi(schemeName, jsonData, overwriteObject, overwriteTable, poolingInternvalInMs, numberOfPoolingAttempts);
        }

        public UDC_UploadFile_Result BulkUploadViaFileApi(string schemeName, string jsonData, bool? overwriteObject = null, bool? overwriteTable = null, int poolingInternvalInMs = 5000, int numberOfPoolingAttempts = 500)
        {
            var byteData = Encoding.UTF8.GetBytes(jsonData);
            return BulkUploadFile(schemeName, byteData, ".json", overwriteObject, overwriteTable, poolingInternvalInMs, numberOfPoolingAttempts);
        }

        public void ExportAsync(string schemeName, string filePath, string format = null, string where = null, string fields = null, string delimiter = null, IEnumerable<string> excludedKeys = null)
        {
            ValuesValidator.Validate(filePath, "File Path is empty!");
            var parent = Directory.GetParent(filePath);
            ValuesValidator.Validate(parent, "Can't get parent folder!");
            var fileExtension = Path.GetExtension(filePath);
            ValuesValidator.Validate(fileExtension == ".json" || fileExtension == ".csv", "Incorrect file extension! (Should be .json or .csv)");

            var fileUrl = ExportFile(schemeName, format, where, fields, delimiter, excludedKeys);
            var response = GetRequestByteArrayBody(fileUrl);

            File.WriteAllBytes(filePath, response);
            return;
        }

        #endregion

        #region Private Methods

        private UDC_UploadFile_Result BulkUploadFile(string schemeName, byte[] data, string fileExtention, bool? overwriteObject = null, bool? overwriteTable = null, int poolingInternvalInMs = 5000, int numberOfPoolingAttempts = 500)
        {
            ValuesValidator.Validate(schemeName, "Scheme Name is empty!");

            var contentType = MapFileExtentionToContentType(fileExtention);
            var key = Guid.NewGuid().ToString();
            var presignedUrlToPut = GetPresignedUrl(key, contentType);

            var statusCode = UploadDataToAws(presignedUrlToPut, data, contentType);
            ValuesValidator.Validate(statusCode == HttpStatusCode.OK, "Can't upload data to AWS!");

            var ipaasUrlToGetFile = GetIpaasUrl($"Aws/GetAwsData?key={key}&ClientToken={this.AuthentificationManager.IdpAuth.APIToken}&contentType={contentType}&extention={fileExtention}");
            var importFileResponse = ImportFileToPepperi(schemeName, ipaasUrlToGetFile, overwriteObject, overwriteTable, poolingInternvalInMs, numberOfPoolingAttempts);
            var result = GetImportFileDataResult(importFileResponse);
            return result;
        }

        private bool DeleteSchemeData(string schemeName, byte[] data = null, string fileExtention = null, string filePath = null)
        {
            var schemeMeta = GetUserDefinedCollection(schemeName);

            var schemeMandatoryFields = schemeMeta?.Fields.Where(field => field.Value?.Mandatory == true).Select(field => field.Key).ToList();
            schemeMandatoryFields.Insert(0, "Key"); // Key is required
            var schemeMandatoryFieldsString = string.Join(",", schemeMandatoryFields);

            var schemeData = GetAllSchemeDataForOverwrite<IDictionary<string, object>>(schemeName, schemeMandatoryFieldsString);

            if (schemeData.Count() == 0)
            {
                return true;
            }

            schemeData = ExcludeKeys(schemeData, data, fileExtention, schemeMeta, filePath);

            if (schemeData.Count() == 0)
            {
                return true;
            }
            foreach (var item in schemeData)
            {
                if (item.ContainsKey("Hidden"))
                {
                    item["Hidden"] = true;
                }
                else
                {
                    item.Add("Hidden", true);
                }

            }

            var uploadedResponse = BulkUploadViaFileApi(schemeName, schemeData, true, false);

            return uploadedResponse.Total == schemeData.Count() && uploadedResponse.TotalFailed == 0;
        }

        private IEnumerable<IDictionary<string, object>> ExcludeKeys(IEnumerable<IDictionary<string, object>> schemeData, byte[] data, string fileExtention, UDC_MetaData schemeMeta, string filePath = null)
        {
            var result = schemeData;
            if (fileExtention == ".json")
            {
                if (schemeMeta?.DocumentKey?.Type == "Composite")
                {
                    var fields = schemeMeta?.DocumentKey?.Fields;
                    var parsedKeys = PepperiJsonSerializer.DeserializeCollection<IDictionary<string, object>>(
                        System.Text.Encoding.UTF8.GetString(data, 0, data.Length)
                        )
                        .Select(row => GenerateKey(row, fields))
                        .ToList();
                    result = schemeData.Where(row => !parsedKeys.Contains((string)row["Key"])).ToList();
                }
            }
            else if (fileExtention == ".csv")
            {
                //using (var parser = new GenericParser(filePath))
                //{

                //}
            }
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

        private string ExportFile(string schemeName, string format = null, string where = null,
            string fields = null, string delimiter = null, IEnumerable<string> excludedKeys = null)
        {

            var exportFileResponse = SendExportFileRequest(schemeName, format, where, fields, delimiter, excludedKeys);
            var auditLogId = exportFileResponse.ExecutionUUID;
            var finalAuditLog = this.AuditLogs.AuditLogPolling(auditLogId);

            ValuesValidator.Validate(
                finalAuditLog?.Status?.ID == (int)ePepperiAuditLogStatus.Succeeded,
                $"Export File Finished with '{finalAuditLog?.Status?.Name ?? "Null"}' Status! Error Message - {finalAuditLog?.AuditInfo?.ErrorMessage ?? "No Message"}");

            var resultObject = finalAuditLog?.AuditInfo?.ResultObject;
            ValuesValidator.Validate(resultObject, "Can't get result Object from final audit log!");

            var parsed = PepperiJsonSerializer.DeserializeOne<IDictionary<string, string>>(resultObject);

            ValuesValidator.Validate(parsed.ContainsKey("URI"), "Parsed result Object does not contatin URI!");
            var resultUri = parsed["URI"];

            return resultUri;
        }

        private IEnumerable<TData> GetAllSchemeDataForOverwrite<TData>(string schemeName, string fields = null)
        {
            var responseData = ExportAsync<TData>(schemeName, fields: fields);
            return responseData;
        }

        /// <summary>
        /// Only for json format
        /// </summary>
        private IEnumerable<TData> ExportAsync<TData>(string schemeName, string where = null, string fields = null, string delimiter = null, IEnumerable<string> excludedKeys = null)
        {
            var fileUrl = ExportFile(schemeName, "json", where, fields, delimiter, excludedKeys);
            var response = GetRequestBody(fileUrl);
            return PepperiJsonSerializer.DeserializeCollection<TData>(response);
        }

        private PepperiHttpClientResponse Find(string schemeName, string where = null, string fields = null, string order_by = null, int? page = null, int? page_size = null)

        {
            string RequestUri = $"resources/{schemeName}";

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

        private PepperiAuditLog ImportFileToPepperi(string schemeName, string fileUrl, bool? overwriteObject = null, bool? overwriteTable = null, int poolingInternvalInMs = 5000, int numberOfPoolingAttempts = 500)
        {
            var importFileResponse = SendImportFileToPepperiRequest(schemeName, fileUrl, overwriteObject, overwriteTable);
            var auditLogId = importFileResponse.ExecutionUUID;
            var finalAuditLog = this.AuditLogs.AuditLogPolling(auditLogId, poolingInternvalInMs, numberOfPoolingAttempts);
            return finalAuditLog;
        }

        private PepperiResponseForAuditLog SendImportFileToPepperiRequest(string schemeName, string fileUrl, bool? overwriteObject = null, bool? overwriteTable = null)
        {
            var RequestUri = $"resources/{HttpUtility.UrlEncode(schemeName)}/import/file";
            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.AuthentificationManager.IdpAuth, this.Logger);

            string postBody = PepperiJsonSerializer.Serialize(new UDC_UploadFile_Request()
            {
                URI = fileUrl,
                OverwriteObject = overwriteObject,
                OverwriteTable = overwriteTable
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

        /// <summary>
        /// 
        /// </summary>
        /// User Defined Collection Name / Scheme Name
        /// <param name="schemeName"></param>
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
        private PepperiResponseForAuditLog SendExportFileRequest(string schemeName,
            string format = null, string where = null,
            string fields = null, string delimiter = null, IEnumerable<string> excludedKeys = null)
        {

            var requestUri = $"resources/{HttpUtility.UrlEncode(schemeName)}/export/file";
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

        private UDC_MetaData GetUserDefinedCollection(string schemeName)
        {
            ValuesValidator.Validate(schemeName, "Scheme Name is empty!");

            var requestUri = $"user_defined_collections/schemes/{schemeName}";
            var response = GetPepperiApi(requestUri);

            return PepperiJsonSerializer.DeserializeOne<UDC_MetaData>(response.Body);
        }

        private UDC_UploadFile_Result GetImportFileDataResult(PepperiAuditLog auditLog)
        {
            var logResultObject = auditLog?.AuditInfo?.ResultObject;
            var parsedResultObject = PepperiJsonSerializer.DeserializeOne<UDC_UploadFile_AuditLog_ResultObject>(logResultObject);
            var url = parsedResultObject?.URI;
            ValuesValidator.Validate(url, "Can't get URI Response!");

            var HttpClient = new HttpClient(new LoggingHandler(this.Logger)) { };

            //send request
            HttpClient.Timeout = new TimeSpan(0, 5, 0);// by default wait 5 minutes
            HttpResponseMessage HttpResponseMessage = HttpClient.GetAsync(url).Result;
            string body = HttpResponseMessage.Content.ReadAsStringAsync().Result;

            ValuesValidator.Validate(HttpResponseMessage.StatusCode == HttpStatusCode.OK, $"Can't get Required File! (Status Code - {HttpResponseMessage.StatusCode})");

            var table = PepperiJsonSerializer.DeserializeCollection<UDC_UploadFile_Row>(body);

            var result = new UDC_UploadFile_Result()
            {
                Total = table.Count(),
                TotalUpdated = 0,
                TotalIgnored = 0,
                TotalFailed = 0,
                TotalMergedBeforeUpload = 0,
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

    public class GenericIpaasResponse<TData>
    {
        public TData Data { get; set; }
        public object BaseException { get; set; }
    }
}
