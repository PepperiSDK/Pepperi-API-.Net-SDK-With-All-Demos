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
        public IEnumerable<UDC_MetaData> GetUserDefinedCollections()
        {
            var requestUri = $"user_defined_collections/schemes";
            var response = GetPepperiApi(requestUri);

            return PepperiJsonSerializer.DeserializeCollection<UDC_MetaData>(response.Body);
        }

        public UDC_MetaData GetUserDefinedCollection(string schemeName)
        {
            ValuesValidator.Validate(schemeName, "Scheme Name is empty!");

            var requestUri = $"user_defined_collections/schemes/{schemeName}";
            var response = GetPepperiApi(requestUri);

            return PepperiJsonSerializer.DeserializeOne<UDC_MetaData>(response.Body);
        }

        public UDC_MetaData UpsertUserDefinedCollection(UDC_MetaData collection)
        {
            ValuesValidator.Validate(collection, "Collection is not empty!");

            var requestUri = $"user_defined_collections/schemes";
            var body = PepperiJsonSerializer.Serialize(collection);
            var response = PostPepperiApi(requestUri, body);

            var result = PepperiJsonSerializer.DeserializeOne<UDC_MetaData>(response.Body);
            return result;
        }

        public bool DeleteUserDefinedCollection(string schemeName)
        {
            ValuesValidator.Validate(schemeName, "Scheme Name is empty!");

            var isSchemeExist = GetUserDefinedCollection(schemeName)?.Name != null;
            ValuesValidator.Validate(isSchemeExist, "Scheme is not exist!");
            var requestUri = $"user_defined_collections/schemes";
            var body = PepperiJsonSerializer.Serialize(new UDC_MetaData()
            {
                Name = schemeName,
                Hidden = true
            });
            var response = PostPepperiApi(requestUri, body);

            var result = PepperiJsonSerializer.DeserializeOne<UDC_MetaData>(response.Body);
            return result?.Hidden == true;
        }

        #endregion

        #region Private Methods

        private PepperiHttpClientResponse GetPepperiApi(string requestUrl, Dictionary<string, string> dicQueryStringParameters = null)
        {
            string accept = "application/json";
            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);
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

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);
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

        #endregion
    }
}
