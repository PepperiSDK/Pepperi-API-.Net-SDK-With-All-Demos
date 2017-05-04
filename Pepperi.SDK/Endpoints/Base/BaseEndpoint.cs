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

namespace Pepperi.SDK.Endpoints.Base
{
    public class BaseEndpoint<TModel, TModelKey>
        where TModel : class
    {
        #region properties

        private string ApiBaseUri                   { get; set; }
        private IAuthentication Authentication      { get; set; }
        private ILogger Logger                      { get; set; }
        private string ResourceName                 { get; set; }      

        #endregion

        #region constructor

        protected BaseEndpoint(string ApiBaseUri, IAuthentication Authentication, ILogger Logger, string ResourceName)
        {
            this.ApiBaseUri = ApiBaseUri;
            this.Authentication = Authentication;
            this.Logger = Logger;
            this.ResourceName = ResourceName;
        }

        #endregion

        #region Public methods


        /// <summary>
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="include_nested">indicates whether nested data should be upserted.</param>
        /// <returns></returns>
        /// <remarks>
        /// Post and Get use the same model
        /// Operation may do insert or Partial update depanding on the value of ExternalID \ InternalID
        /// Insert
        ///     if the External\InternalID are not given in the request
        ///     The model properties with null value are not serialzied by APIClient. Server populates them with default value.
        ///Partial update
        ///     If the External\InternalID is given in the request
        ///     The model properties with null value are not serialzied by APIClient. server updates the values of the properties which are not null.
        ///Regarding Reference:
        ///     used in 1:1 relations when both entities may exist indepandently.
        ///     To attach relation call upsert (setting the x.Reference.Data.ExternalID or x.Reference.Data.InternalID)
        ///     To detach relation call upsert (setting the x.Rerefere.Data to null)
        ///     To read the reference call find with full_mode = true
        ///Regarding References:
        ///     used in 1:many relations when child entities may exist without parent entity.
        ///     To set relations call upsert (simply deletes all exsiting relations and set the relations we send on upsert)
        ///         (setting the x.References.Data.Add ( { InternalID=  .... ExternalID = .... })
        ///     To delete all the relations call upset *setting the x.Reference.Data to empty collection.
        ///     To read the references call find with includ_nested =  true
        ///     
        ///     used in 1:many relations when the child entity can not exist without the parent entity  
        ///             (Contact can not exist without the account)
        ///             in that case, as you create the child entity (eg, Contact) you use its reference Property  (ExternalID or InternalID) to associate the child with existing parent.   [Do not use the ParentExternalId property of the child, since it is used only in Bulk).
        /// 
        /// </remarks>
        public TModel Upsert(TModel Model, bool? include_nested = null)
        {
            string RequestUri = ResourceName;

            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            if (include_nested.HasValue) { dicQueryStringParameters.Add("include_nested", include_nested.Value.ToString()); }

            string postBody = PepperiJsonSerializer.Serialize(Model);                                       //null values are not serialized
            string contentType = "application/json";
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);
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
       /// Upsert of collection As json or csv zip
       /// </summary>
       /// <param name="data"></param>
        /// <param name="OverrideMethod"></param>
       /// <param name="BulkUploadMethod"></param>
       /// <param name="fieldsToUpload"></param>s
       /// <param name="FilePathToStoreZipFile">Optional. Stores the generated zip file for debugging purpose.</param>
       /// <returns></returns>
        public BulkUploadResponse BulkUpload(IEnumerable<TModel> data,eOverwriteMethod OverrideMethod,  eBulkUploadMethod BulkUploadMethod, IEnumerable<string> fieldsToUpload, bool SaveZipFileInLocalDirectory = false, string SubTypeID="")
        {
            //validate input
            if (fieldsToUpload == null || fieldsToUpload.Count() == 0)
            {
                throw new PepperiException("No header fields  are specified.");
            }

            
            BulkUploadResponse BulkUploadResponse = null;

            switch (BulkUploadMethod)
            {
                case eBulkUploadMethod.Json:
                    {
                        BulkUploadResponse = BulkUpload_OfJson(data, OverrideMethod, fieldsToUpload);
                        break;
                    }
                case eBulkUploadMethod.Zip:
                    {
                        string FilePathToStoreZipFile = null;
                        if (SaveZipFileInLocalDirectory)
                        {
                            string AssemblyLocation =       Assembly.GetExecutingAssembly().Location;
                            string AssemblyPath =           Path.GetDirectoryName(AssemblyLocation);
                            string zipDirectory =           AssemblyPath;
                            string zipFileName =            "BulkUpload_" + this.ResourceName + ".zip";
                            FilePathToStoreZipFile =        Path.Combine(zipDirectory, zipFileName);
                        }

                        BulkUploadResponse = BulkUploadOfZip(data, OverrideMethod, fieldsToUpload, FilePathToStoreZipFile, SubTypeID);
                        break;
                    }
                default:
                    {
                        throw new PepperiException("Invalid argument: the upload method is not supported.");
                    }
            }

            return BulkUploadResponse;
        }

        public BulkUploadResponse BulkUpload(string csvFilePath, eOverwriteMethod OverwriteMethod, Encoding fileEncoding, string SubTypeID = "", string FilePathToStoreZipFile = null)
        {
            byte[] fileAsBinary =               File.ReadAllBytes(csvFilePath);
            byte[] fileAsUtf8 =                 Encoding.Convert(fileEncoding, Encoding.UTF8, fileAsBinary);
            string fileAsUtf8String =           System.Text.Encoding.UTF8.GetString(fileAsUtf8);
            byte[] fileAsZipInUTF8 =            PepperiFlatSerializer.UTF8StringToZip(fileAsUtf8String, FilePathToStoreZipFile);

            BulkUploadResponse result = BulkUploadOfZip(fileAsZipInUTF8, OverwriteMethod, SubTypeID);
            return result;
        }

        public GetBulkJobInfoResponse GetBulkJobInfo(string JobID)
        {
            string RequestUri = string.Format("bulk/jobinfo/{0}", HttpUtility.UrlEncode(JobID));
            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Get(
                    ApiBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    accept);

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            GetBulkJobInfoResponse result = PepperiJsonSerializer.DeserializeOne<GetBulkJobInfoResponse>(PepperiHttpClientResponse.Body);   //Api returns single object
            return result;

        }


        /// <summary>
        /// Pools the status of the job until completion or timeout
        /// </summary>
        /// <param name="JobID"></param>
        /// <param name="poolingInternvalInMs"></param>
        /// <param name="numberOfPoolingAttempts"></param>
        /// <returns>the JobInfoResponse  or throws timeout exception</returns>
        public GetBulkJobInfoResponse WaitForBulkJobToComplete(string JobID, int poolingInternvalInMs = 1000, int numberOfPoolingAttempts = 60*2)
        {
            bool bulkUpsertCompleted    = false;
            int getJobInfoAttempts      = 0;
            bool poolingTimeout         = false;

            GetBulkJobInfoResponse GetBulkJobInfoResponse = null;

            while (bulkUpsertCompleted == false && poolingTimeout == false)
            {
                if (getJobInfoAttempts > 0)
                {
                    Thread.Sleep(poolingInternvalInMs);
                }

                GetBulkJobInfoResponse = GetBulkJobInfo(JobID);

                bulkUpsertCompleted = GetBulkJobInfoResponse.Status != "Not Started" && GetBulkJobInfoResponse.Status != "In Progress";

                getJobInfoAttempts++;

                poolingTimeout = (getJobInfoAttempts == numberOfPoolingAttempts);
            }


            if (poolingTimeout == true)
            {
                    throw new PepperiException("Bulk Upload did not complete within " + poolingInternvalInMs * numberOfPoolingAttempts + " ms"); 
            }

            //bulkUpsertCompleted
            return GetBulkJobInfoResponse;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="include_nested">populate the References propeties of the result</param>
        /// <param name="full_mode">populate the Reference propeties of the result</param>
        /// <returns></returns>
        public TModel FindByID(TModelKey id, bool? include_nested = null, bool? full_mode = null)
        {
            string RequestUri = ResourceName + "//" + HttpUtility.UrlEncode(id.ToString());
            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            if (include_nested.HasValue)    { dicQueryStringParameters.Add("include_nested",    include_nested.Value.ToString()); }
            if (full_mode.HasValue) { dicQueryStringParameters.Add("full_mode",                 full_mode.Value.ToString()); }
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Get(
                    ApiBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    accept);

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            TModel result = PepperiJsonSerializer.DeserializeOne<TModel>(PepperiHttpClientResponse.Body);   //Api returns single object
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="externalId"></param>
        /// <param name="include_nested">populate the References propeties of the result</param>
        /// <param name="full_mode">populate the Reference propeties of the result</param>
        /// <returns></returns>
        public TModel FindByExternalID(string externalId, bool? include_nested = null, bool? full_mode = null)
        {
            string RequestUri = ResourceName + "//externalid//" + HttpUtility.UrlEncode(externalId);
            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            if (include_nested.HasValue) { dicQueryStringParameters.Add("include_nested", include_nested.Value.ToString()); }
            if (full_mode.HasValue) { dicQueryStringParameters.Add("full_mode", full_mode.Value.ToString()); }
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Get(
                    ApiBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    accept);

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            TModel result = PepperiJsonSerializer.DeserializeOne<TModel>(PepperiHttpClientResponse.Body);   //Api returns single object
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <param name="order_by"></param>
        /// <param name="page"></param>
        /// <param name="page_size"></param>
        /// <param name="include_nested"></param>
        /// <param name="include_nested">populate the References propeties of the result</param>
        /// <param name="full_mode">populate the Reference propeties of the result</param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public IEnumerable<TModel> Find(string where = null, string order_by = null, int? page = null, int? page_size = null, bool? include_nested = null, bool? full_mode = null, bool? include_deleted = null, string fields = null)
        {
            string RequestUri = ResourceName;

            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            if (where != null) { dicQueryStringParameters.Add("where", where); }
            if (order_by != null) { dicQueryStringParameters.Add("order_by", order_by); }
            if (page.HasValue) { dicQueryStringParameters.Add("page", page.Value.ToString()); }
            if (page_size.HasValue) { dicQueryStringParameters.Add("page_size", page_size.Value.ToString()); }
            if (include_nested.HasValue) { dicQueryStringParameters.Add("include_nested", include_nested.Value.ToString()); }
            if (full_mode.HasValue) { dicQueryStringParameters.Add("full_mode", full_mode.Value.ToString()); }
            if (include_deleted.HasValue) { dicQueryStringParameters.Add("include_deleted", include_deleted.Value.ToString()); }
            if (fields != null) { dicQueryStringParameters.Add("fields", fields); }

            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);
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

        public bool DeleteByID(TModelKey id)
        {
            string RequestUri = ResourceName + "//" + HttpUtility.UrlEncode(id.ToString());
            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();

            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Delete(
                    ApiBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    accept);

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            bool result = PepperiJsonSerializer.DeserializeOne<bool>(PepperiHttpClientResponse.Body);   //Api returns bool right now. true-when resource is found and deleted. Otherwise, false.

            return result;
        }


        public bool DeleteByExternalID(string externalId)
        {
            string RequestUri = ResourceName + "//externalid//" + HttpUtility.UrlEncode(externalId);
            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();

            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Delete(
                    ApiBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    accept);

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            bool result = PepperiJsonSerializer.DeserializeOne<bool>(PepperiHttpClientResponse.Body);   //Api returns bool right now. true-when resource is found and deleted. Otherwise, false.

            return result;
        }

        public long GetCount(string where = null, bool? include_deleted = null)
        {
            #region read first Page

            string RequestUri = ResourceName;

            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            if (where != null) { dicQueryStringParameters.Add("where", where); }
            if (include_deleted.HasValue) { dicQueryStringParameters.Add("include_deleted", include_deleted.Value.ToString()); }
            dicQueryStringParameters.Add("include_count", "true");

            dicQueryStringParameters.Add("page", "1");
            dicQueryStringParameters.Add("page_size", "1");

            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Get(
                    ApiBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    accept
                    );

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            #endregion

            #region Parse result (value of X-Pepperi-Total-Pages header)

            string headerKey = "X-Pepperi-Total-Pages";
            bool header_exists = PepperiHttpClientResponse.Headers.ContainsKey(headerKey);
            if (header_exists == false)
            {
                throw new PepperiException("Failed retrieving Total pages from response.");
            }

            IEnumerable<string> headerValue = PepperiHttpClientResponse.Headers[headerKey];
            if (headerValue == null || headerValue.Count() != 1)
            {
                throw new PepperiException("Failed retrieving Total pages from response.");
            }

            string resultAsString = headerValue.First();
            long result = 0;
            bool parsedSucessfully = long.TryParse(resultAsString, out result);


            if (!parsedSucessfully)
            {
                throw new PepperiException("Failed retrieving Total pages from response.");
            }

            #endregion

            return result;
        }

        public IEnumerable<FieldMetadata> GetFieldsMetaData()
        {
            string RequestUri = string.Format(@"metadata/{0}", this.ResourceName);
            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Get(
                    ApiBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    accept);

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            IEnumerable<FieldMetadata> result = PepperiJsonSerializer.DeserializeCollection<FieldMetadata>(PepperiHttpClientResponse.Body);
            result = result.OrderBy(o => o.Name);
            return result;
        }

        /// <summary></summary>
        /// <returns>the sub types of this resource</returns>
        /// <remarks>
        /// 1. some resources (eg, transaction and activity) are "abstract types".
        ///    the concrete types "derives" from them:               eg, sales transaction, invoice  derive from transaction     (with header and lines) 
        ///                                                         eg, visit                       derive from activity        (with header)
        ///    the concrete class custom fields are modeled as TSA fields
        /// 2. All the types are returned by metadata endpoint 
        /// 3. Activities, Transactions are "abstract" type
        ///     The concrete type is identified by ActivityTypeID
        ///     For Bulk or CSV upload, the ActivityTypeID is sent on the url   
        ///     For single Upsert,      the ActivityTypeID is set on the object
        ///     The values of the ActivityTypeID are taken from the GetSubTypesMetadata method
        /// </remarks>
        public IEnumerable<TypeMetadata> GetSubTypesMetadata()
        {
            string RequestUri = string.Format("metadata");
            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Get(
                    ApiBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    accept);

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            IEnumerable<TypeMetadata> result = PepperiJsonSerializer.DeserializeCollection<TypeMetadata>(PepperiHttpClientResponse.Body);
            result = result.Where(subTypeMetadata =>  
                                    subTypeMetadata.Type == ResourceName && 
                                    subTypeMetadata.SubTypeID != null && 
                                    subTypeMetadata.SubTypeID.Length > 0
                            ).
                            ToList();

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserDefinedField"></param>
        /// <returns>The name of the newly created field</returns>
        public string CreateUserDefinedField(UserDefinedField UserDefinedField)
        {
            string RequestUri = string.Format(@"customization/user_defined_fields/{0}", this.ResourceName);
            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            string postBody = PepperiJsonSerializer.Serialize(UserDefinedField);                                       //null values are not serialized
            string contentType = "application/json";
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.PostStringContent(
                    ApiBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    postBody,
                    contentType,
                    accept
                    );

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            string result = PepperiJsonSerializer.DeserializeOne<string>(PepperiHttpClientResponse.Body);
            return result;
        }
  



        #endregion

        #region Private methods

        private BulkUploadResponse BulkUpload_OfJson(IEnumerable<TModel> data, eOverwriteMethod OverwriteMethod, IEnumerable<string> fieldsToUpload)
        {
            FlatModel FlatModel = PepperiFlatSerializer.MapDataToFlatModel(data, fieldsToUpload, "''");

            string RequestUri = string.Format("bulk/{0}/json", ResourceName);

            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            dicQueryStringParameters.Add("overwrite", OverwriteMethod.ToString());

            string postBody = PepperiJsonSerializer.Serialize(FlatModel);                         //null values are not serialzied.
            string contentType = "application/json";
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.PostStringContent(
                    ApiBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    postBody,
                    contentType,
                    accept
                    );

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            BulkUploadResponse result = PepperiJsonSerializer.DeserializeOne<BulkUploadResponse>(PepperiHttpClientResponse.Body);
            return result;
        }

        private BulkUploadResponse BulkUploadOfZip(IEnumerable<TModel> data, eOverwriteMethod OverwriteMethod, IEnumerable<string> fieldsToUpload, string FilePathToStoreZipFile, string SubTypeID)
        {
            FlatModel FlatModel = PepperiFlatSerializer.MapDataToFlatModel(data, fieldsToUpload, "''");

            string CsvFileAsString =    PepperiFlatSerializer.FlatModelToCsv(FlatModel);

            byte[] CsvFileAsZipInUTF8 = PepperiFlatSerializer.UTF8StringToZip(CsvFileAsString, FilePathToStoreZipFile);

            BulkUploadResponse result = BulkUploadOfZip(CsvFileAsZipInUTF8, OverwriteMethod, SubTypeID);
            return result;
        }

        /// <summary>
        /// reusable method
        /// </summary>
        /// <param name="fileAsZipInUTF8"></param>
        /// <param name="OverwriteMethod"></param>
        /// <param name="SubTypeID">
        ///     usually empty value. 
        ///     we use the parameter for sub types 
        ///         eg, sales order that derive from transaction
        ///         eg, invoice     that derive from transaction
        ///         eg, visit       that derive from activity 
        ///     in that case we take the SubTypeID from the metadata endpoint (see GetSubTypesMetadata method)
        ///     The custom fields are TSA fields
        ///     </param>
        /// <returns></returns>
        /// <remarks>
        /// 1. the post body is in UTF8
        /// </remarks>
        private BulkUploadResponse BulkUploadOfZip(byte[] fileAsZipInUTF8, eOverwriteMethod OverwriteMethod, string SubTypeID)
        {
            string RequestUri =
                (SubTypeID == null || SubTypeID.Length == 0) ?
                    string.Format("bulk/{0}/csv_zip", ResourceName) :
                    string.Format("bulk/{0}/{1}/csv_zip", ResourceName, SubTypeID);                                //eg, for transaction or activity
                
            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            dicQueryStringParameters.Add("overwrite", OverwriteMethod.ToString());


            byte[] postBody = fileAsZipInUTF8;
            string contentType = "application/zip";
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.PostByteArraycontent(
                    ApiBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    postBody,
                    contentType,
                    accept
                    );

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            BulkUploadResponse result = PepperiJsonSerializer.DeserializeOne<BulkUploadResponse>(PepperiHttpClientResponse.Body);
            return result;
        }

        
        #endregion
    }
}
