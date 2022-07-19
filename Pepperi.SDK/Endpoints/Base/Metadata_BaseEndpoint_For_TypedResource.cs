using Pepperi.SDK.Helpers;
using Pepperi.SDK.Exceptions;
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

namespace Pepperi.SDK.Endpoints.Base
{

    /// <summary>
    /// Base endpoint for Metadata of "Typed resource"  (Account, Activity, Transaction)
    /// </summary>
    /// <remarks>
    /// Customer        is usually a type of Account.
    /// Visit and Pohto are usually types of Activity.
    /// Sales Order     is usually a type of Transaction.
    /// </remarks>
    public class Metadata_BaseEndpoint_For_TypedResource
    {
        #region properties

        private string ApiBaseUri { get; set; }
        private IAuthentication Authentication { get; set; }
        private ILogger Logger { get; set; }
        private string ResourceName { get; set; }
        private bool IsInternalAPI { get; set; } = false;

        #endregion

        #region constructor

        protected Metadata_BaseEndpoint_For_TypedResource(string ApiBaseUri, IAuthentication Authentication, ILogger Logger, string ResourceName, bool IsInternalAPI)
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
        /// Returns the Types of the Resource
        /// </summary>
        /// <returns>The Types of the given resuorce (Accounts, Activities, Transactions)</returns>
        /// <remarks>
        /// eg, ApiClient.AccountsMetaData.GetTypes()       may return Customer.
        ///     ApiClient.ActivitiesMetaData.GetTypes()     may return Photo and  Visit.
        ///     ApiClient.TransactionsMetaData.GetTyoes()   may returns SalesOrder.
        /// </remarks>
        public IEnumerable<Type_MetaData> GetTypes()
        {
            string RequestUri = string.Format(@"meta_data/{0}/types", this.ResourceName);
            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);

            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Get(
                    ApiBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    accept);

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            IEnumerable<Type_MetaData> result = PepperiJsonSerializer.DeserializeCollection<Type_MetaData>(PepperiHttpClientResponse.Body);
            return result;
        }

        /// <summary>
        /// Returns a specific Type by its TypeID
        /// </summary>
        /// <param name="TypeID"></param>
        /// <returns>The Type with the given TypeID</returns>
        public Type_MetaData GetTypeByID(long TypeID)
        {
            string RequestUri = string.Format(@"meta_data/{0}/types/{1}", this.ResourceName, TypeID);
            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);

            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Get(
                    ApiBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    accept);

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            Type_MetaData result = PepperiJsonSerializer.DeserializeOne<Type_MetaData>(PepperiHttpClientResponse.Body);
            return result;
        }

        /// <summary>
        /// returns list of (Standart and User Defined) fields that belong to ANY Type
        /// </summary>
        /// <returns></returns>
        /// <example>
        /// Eg, if activities has two Types: Visit and Photo,
        ///     Then a call to Apiclient.ActivitiesMetaData.GetFields() 
        ///     will return a list with the fields of "Visit" and "Photo" wilthout duplicates.
        /// </example>
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
        ///returns list of ist of (Standart and User Defined) fields of that belong to the given Type
        /// </summary>
        /// <param name="TypeID"></param>
        /// <returns>The fields of the given type</returns>
        /// <Note>
        /// 1.The value of IsUserDefinedField property defines whether a given field is a "User Defined Field"
        /// </Note>
        public IEnumerable<Field_MetaData> GetFields_By_TypeID(long TypeID)
        {
            string RequestUri = string.Format(@"meta_data/{0}/types/{1}/fields", this.ResourceName, TypeID);
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
            return result;
        }


        /// <summary>
        ///returns list of ist of (Standart and User Defined) fields of that belong to the given Type
        /// </summary>
        /// <param name="ExternalID">The value of Type.ExternalID</param>
        /// <returns>The fields of the given type</returns>
        /// <Note>
        /// 1.The value of IsUserDefinedField property defines whether a given field is a "User Defined Field"
        /// </Note>
        public IEnumerable<Field_MetaData> GetFields_By_TypeExternalID(string TypeExternalID)
        {
            string RequestUri = string.Format(@"meta_data/{0}/types/externalid/{1}/fields", this.ResourceName, System.Web.HttpUtility.UrlPathEncode(TypeExternalID));
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
            return result;
        }



        /// <summary>
        /// Upserts a UserDefinedField to a given Type
        /// </summary>
        /// <param name="TypeID">The type to store the field</param>
        /// <param name="Field">The field being upserted</param>
        /// <returns></returns>
        /// <remarks>
        /// 1.Attempt to Upsert a field that is not a "User Defined Field" retruns Bad Request
        /// </remarks>
        public Field_MetaData UpsertUserDefinedField(long TypeID, Field_MetaData Field)
        {
            string RequestUri = string.Format(@"meta_data/{0}/types/{1}/fields", this.ResourceName, TypeID.ToString());
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
        /// Deletes a UserDefinedField from a given Type
        /// </summary>
        /// <param name="FieldID">Identifies the field to delete</param>
        /// <param name="TypeID">Identifies the  type that holds the field to delete</param>
        /// <returns>true on Success</returns>
        /// <remarks>
        /// 1. Attempting to delete a field that is not a "User Defined Field" will fail
        /// </remarks>
        public bool DeleteUserDefinedField(long TypeID, string FieldID)
        {
            string RequestUri = string.Format(@"meta_data/{0}/types/{1}/fields/{2}", this.ResourceName, TypeID, FieldID);
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
        /// <param name="TypeID">The type to which the field belongs</param>
        /// <param name="FieldID">The original field name</param>
        /// <param name="newFieldID">The new field Name</param>
        /// <returns>The updated Feild</returns>
        /// <remarks>
        /// 1.Attempt to Rename a field that is not a "User Defined Field" retruns Bad Request
        /// </remarks>
        public Field_MetaData RenameUserDefinedField(long TypeID, string FieldID, string newFieldID)
        {
            string RequestUri = string.Format(@"meta_data/{0}/types/{1}/fields/{2}/rename", this.ResourceName, TypeID.ToString(), FieldID);
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

        #region Private methods
        #endregion

    }
}



