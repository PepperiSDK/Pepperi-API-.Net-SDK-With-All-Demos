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
    public class Metadata_BaseEndpoint_For_UserDefinedCollections
    {
        #region properties

        private string ApiBaseUri { get; set; }
        private IAuthentication Authentication { get; set; }
        private ILogger Logger { get; set; }

        #endregion

        #region constructor

        protected Metadata_BaseEndpoint_For_UserDefinedCollections(IAuthentication Authentication, ILogger Logger)
        {
            this.ApiBaseUri = "https://papi.pepperi.com/v1.0/"; //Hardcoded Part
            this.Authentication = Authentication;
            this.Logger = Logger;
        }

        #endregion

        #region Public methods
        public IEnumerable<UserDefinedCollection_MetaData> GetUserDefinedCollections()
        {
            string RequestUri = @"user_defined_collections/schemes";
            Dictionary<string, string> dicQueryStringParameters = new Dictionary<string, string>();
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Get(
                    ApiBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    accept);

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            IEnumerable<UserDefinedCollection_MetaData> result = PepperiJsonSerializer.DeserializeCollection<UserDefinedCollection_MetaData>(PepperiHttpClientResponse.Body);
            return result;
        }

        #endregion
    }
}
