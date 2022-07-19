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

namespace Pepperi.SDK.Endpoints.Base
{
    public class Metadata_BaseEndpoint_For_UserDefinedTable
    {

        #region properties

        private string ApiBaseUri { get; set; }
        private IAuthentication Authentication { get; set; }
        private ILogger Logger { get; set; }

        /// <summary>
        /// meanninless for UserDefinedTables
        /// </summary>
        //private string ResourceName { get; set; }
        //private bool IsInternalAPI { get; set; } = false;

        #endregion

        #region constructor

        protected Metadata_BaseEndpoint_For_UserDefinedTable(string ApiBaseUri, IAuthentication Authentication, ILogger Logger)
        {
            this.ApiBaseUri = ApiBaseUri;
            this.Authentication = Authentication;
            this.Logger = Logger;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Upserts a User Defined Table
        /// </summary>
        /// <param name="UserDefinedTable"></param>
        /// <returns>The upserted User Defined Table</returns>
        public UserDefinedTable_MetaData UpsertUserDefinedTable(UserDefinedTable_MetaData UserDefinedTable)
        {
            string RequestUri = "meta_data/user_defined_tables";

            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();

            string postBody = PepperiJsonSerializer.Serialize(UserDefinedTable);               //null values are not serialized
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

            UserDefinedTable_MetaData result = PepperiJsonSerializer.DeserializeOne<UserDefinedTable_MetaData>(PepperiHttpClientResponse.Body);
            return result;
        }




        /// <summary>
        /// Returns User Degined Tables
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserDefinedTable_MetaData> GetUserDefinedTables()
        {
            string RequestUri = @"meta_data/user_defined_tables";
            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Get(
                    ApiBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    accept);

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            IEnumerable<UserDefinedTable_MetaData> result = PepperiJsonSerializer.DeserializeCollection<UserDefinedTable_MetaData>(PepperiHttpClientResponse.Body);
            return result;
        }

        /// <summary>
        /// Returns a User defined table by TableID
        /// </summary>
        /// <param name="TableID"></param>
        /// <returns></returns>
        public UserDefinedTable_MetaData GetUserDefinedTable(string TableID)
        {
            string RequestUri = string.Format(@"meta_data/user_defined_tables/{0}", TableID);
            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Get(
                    ApiBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    accept);

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            UserDefinedTable_MetaData result = PepperiJsonSerializer.DeserializeOne<UserDefinedTable_MetaData>(PepperiHttpClientResponse.Body);
            return result;
        }

        /// <summary>
        /// Deletes the given User Defined Table
        /// </summary>
        /// <param name="TableID"></param>
        /// <returns></returns>
        /// <remarks>Not Found is returned if table does not exist</remarks>
        public bool DeleteUserDefinedTable(string TableID)
        {
            string RequestUri = string.Format(@"meta_data/user_defined_tables/{0}", TableID);
            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Delete(
                    ApiBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    accept);

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            bool result = PepperiJsonSerializer.DeserializeOne<bool>(PepperiHttpClientResponse.Body);
            return result;
        }


        #endregion

        #region Private methods
        #endregion
    }


}

/*
 *  
*/
/*  


 *  
*/
