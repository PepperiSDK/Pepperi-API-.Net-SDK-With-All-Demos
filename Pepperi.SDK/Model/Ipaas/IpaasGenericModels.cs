using Pepperi.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Ipaas
{
    /// <summary>
    /// Wrapper over Http Response
    /// </summary>
    internal class IpaasHttpClientResponse
    {
        internal HttpStatusCode HttpStatusCode { get; set; }
        internal string Body { get; set; }
        internal Dictionary<string, IEnumerable<string>> Headers { get; set; }

        internal IpaasHttpClientResponse(PepperiHttpClientResponse pepperiHttpClientResponse)
        {
            this.HttpStatusCode = pepperiHttpClientResponse.HttpStatusCode;
            this.Body = pepperiHttpClientResponse.Body;
            this.Headers = pepperiHttpClientResponse.Headers;
        }

        internal IpaasHttpClientResponse()
        {
            this.HttpStatusCode = HttpStatusCode;
            this.Body = Body;
            this.Headers = Headers;
        }


    }

    public class IpaasGenericResponse<TData>
    {
        public TData Data { get; set; }
        public IpaasGenericBaseException BaseException { get; set; }
    }

    public class IpaasGenericBaseException
    {
        public string Message { get; set; }
    }
}
