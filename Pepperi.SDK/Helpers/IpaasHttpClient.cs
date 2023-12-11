using Pepperi.SDK.Contracts;
using Pepperi.SDK.Exceptions;
using Pepperi.SDK.Model.Ipaas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Helpers
{
    internal class IpaasHttpClient
    {
        #region Properties
        private string IpaasBaseUri { get; set; }
        private IpaasAddonAuthentification IpaasAuth { get; set; }
        private ILogger Logger { get; set; }

        #endregion

        internal IpaasHttpClient(string ipaasBaseUrl, ILogger logger, IpaasAddonAuthentification IpaasAuth)
        {
            this.IpaasAuth = IpaasAuth;
            this.Logger = logger;
            this.IpaasBaseUri = ipaasBaseUrl;
        }

        #region Internal Methods

        internal TResult Get<TResult>(string requestUri, Dictionary<string, string> dicQueryStringParameters = null,
            bool isMinifiedLog = false)
        {
            var response = Get(requestUri, dicQueryStringParameters, isMinifiedLog);
            var parsed = PepperiJsonSerializer.DeserializeOne<IpaasGenericResponse<TResult>>(response.Body);
            ValuesValidator.Validate(parsed, "Can't correctly parse response!");
            return parsed.Data;
        }

        internal IpaasHttpClientResponse Get(string requestUri, Dictionary<string, string> dicQueryStringParameters = null,
            bool isMinifiedLog = false)
        {
            string accept = "application/json";
            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.IpaasAuth, this.Logger);
            var response = PepperiHttpClient.Get(
                    this.IpaasBaseUri,
                    requestUri,
                    dicQueryStringParameters ?? new Dictionary<string, string>() { },
                    accept,
                    isMinifiedLog
                    );

            HandleError(response);

            return new IpaasHttpClientResponse(response);
        }

        internal TResult PostJson<TData, TResult>(string requestUri, TData data, bool isMinifiedLog = false)
        {
            return PostJson<TResult>(requestUri, PepperiJsonSerializer.Serialize(data), isMinifiedLog);
        }

        internal TResult PostJson<TResult>(string requestUri, string data, bool isMinifiedLog = false)
        {
            var response = PostJson(requestUri, data, isMinifiedLog);
            var parsed = PepperiJsonSerializer.DeserializeOne<IpaasGenericResponse<TResult>>(response.Body);
            ValuesValidator.Validate(parsed, "Can't correctly parse response!");
            return parsed.Data;
        }

        internal IpaasHttpClientResponse PostJson<TData>(string requestUri, TData data, bool isMinifiedLog = false)
        {
            return PostJson(requestUri, PepperiJsonSerializer.Serialize(data), isMinifiedLog);
        }

        internal IpaasHttpClientResponse PostJson(string requestUri, string data, bool isMinifiedLog = false)
        {
            string contentType = "application/json";
            string accept = "application/json";

            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.IpaasAuth, this.Logger);
            var response = PepperiHttpClient.PostStringContent(
                    this.IpaasBaseUri,
                    requestUri,
                    new Dictionary<string, string>() { },
                    data,
                    contentType,
                    accept,
                    isMinifiedLog
                    );

            HandleError(response);

            return new IpaasHttpClientResponse(response);
        }

        #endregion

        #region Private Methods

        internal void HandleError(PepperiHttpClientResponse response)
        {
            // Use Only OK Status
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return;
            }

            var message = "";
            var prefixMessage = "Ipaas Api Error. ";
            if (response.HttpStatusCode != System.Net.HttpStatusCode.BadRequest)
            {
                message = $"{prefixMessage}Response Body - {response.Body}";
                throw new IpaasException(message, response.HttpStatusCode);
            }


            try
            {
                var parsed = PepperiJsonSerializer.DeserializeOne<IpaasGenericResponse<object>>(response.Body);
                message = $"{prefixMessage}Error message - {parsed?.BaseException?.Message ?? $"No Parsed Message, body - {response.Body}"}";
            }
            catch (Exception e)
            {
                message = $"{prefixMessage}Can't parse response. Body - ${response.Body ?? "No Body"}. Parsing error: ${e.Message ?? "No Message"}";
                throw new IpaasException(message, e, response.HttpStatusCode);
            }
            throw new IpaasException(message, response.HttpStatusCode);
        }

        #endregion
    }
}
