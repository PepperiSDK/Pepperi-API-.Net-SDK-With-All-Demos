using Pepperi.SDK.Helpers;
using Pepperi.SDK.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.IO.Compression;
using Pepperi.SDK.Contracts;
using System.Threading;
using System.Reflection;
using Pepperi.SDK.Model.Fixed.MetaData;

namespace Pepperi.SDK.Endpoints.Base
{
    /// <summary>
    /// Base endpoint for Metadata of "Standard resource"
    /// </summary>
    /// <remarks>
    /// 1. "Standard resournce" is any resource other than: Account,Activity,Transaction
    /// </remarks>
    public class Metadata_BaseEndpoint_For_StandardResource
    {
        #region properties

        private string ApiBaseUri { get; set; }
        private IAuthentication Authentication { get; set; }
        private ILogger Logger { get; set; }
        private string ResourceName { get; set; }
        private bool IsInternalAPI { get; set; } = false;

        #endregion

        #region constructor

        protected Metadata_BaseEndpoint_For_StandardResource(string ApiBaseUri, IAuthentication Authentication, ILogger Logger, string ResourceName, bool IsInternalAPI)
        {
            this.ApiBaseUri = ApiBaseUri;
            this.Authentication = Authentication;
            this.Logger = Logger;
            this.ResourceName = ResourceName;
            this.IsInternalAPI = IsInternalAPI;
        }

        #endregion



        #region Public methods


        /// <summary>
        /// returns list of ist of (Standart and User Defined) fields that belong to the resource
        /// </summary>
        /// <returns></returns>
        /// <Note>
        /// 1.The value of IsUserDefinedField property defines whether a given field is a "User Defined Field"
        /// </Note>
        public IEnumerable<Field_MetaData> GetFields()
        {
            string RequestUri = string.Format(@"meta_data/{0}/fields", this.ResourceName);
            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);

            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Get(
                    ApiBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    accept);

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            IEnumerable<Field_MetaData> result = PepperiJsonSerializer.DeserializeCollection<Field_MetaData>(PepperiHttpClientResponse.Body);
            result = result.OrderBy(o => o.FieldID);
            return result;
        }



        /// <summary>
        /// Upserts a UserDefinedField
        /// </summary>
        /// <param name="Field"></param>
        /// <returns></returns>
        /// <remarks>
        /// 1.Attempt to Upsert a field that is not a "User Defined Field" retruns Bad Request
        /// </remarks>
        public Field_MetaData UpsertUserDefinedField(Field_MetaData Field)
        {
            string RequestUri = string.Format(@"meta_data/{0}/fields", this.ResourceName);
            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            string postBody = PepperiJsonSerializer.Serialize(Field);                                       //null values are not serialized
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

            Field_MetaData result = PepperiJsonSerializer.DeserializeOne<Field_MetaData>(PepperiHttpClientResponse.Body);
            return result;
        }



        /// <summary>
        /// Deletes a UserDefinedField
        /// </summary>
        /// <param name="FieldID">Identifies the field to delete</param>
        /// <returns>true on Success</returns>
        /// <remarks>
        /// 1. Attempting to delete a field that is not a "User Defined Field" will fail
        /// </remarks>
        public bool DeleteUserDefinedField(string FieldID)
        {
            string RequestUri = string.Format(@"meta_data/{0}/fields/{1}", this.ResourceName, FieldID);
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



        /// <summary>
        /// Renames a UserDefinedField
        /// </summary>
        /// <param name="FieldID">The original field name</param>
        /// <param name="newFieldID">The new field Name</param>
        /// <returns>The updated Feild</returns>
        /// <remarks>
        /// 1.Attempt to Rename a field that is not a "User Defined Field" retruns Bad Request
        /// </remarks>
        public Field_MetaData RenameUserDefinedField(string FieldID, string newFieldID)
        {
            string RequestUri = string.Format(@"meta_data/{0}/fields/{1}/rename", this.ResourceName, FieldID);
            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            string putBody = PepperiJsonSerializer.Serialize(new { FieldID = newFieldID });        //null values are not serialized
            string contentType = "application/json";
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);

            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.PutStringContent(
                    ApiBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    putBody,
                    contentType,
                    accept
                    );

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            Field_MetaData result = PepperiJsonSerializer.DeserializeOne<Field_MetaData>(PepperiHttpClientResponse.Body);
            return result;
        }
        #endregion


    }
}