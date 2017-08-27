using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Exceptions
{
    /// <summary>
    /// This is the excemtion returned by the SDK
    /// </summary>
    /// <usage>
    /// 1. on SDK error
    /// 2. when Api returns "Bad request" error with ApiExcprtion in the post body
    /// 3.when Api returns any other http error, eg: 503.
    /// </usage>
    public class PepperiException : Exception
    {

        #region Properties

        public HttpStatusCode? HttpStatusCode { get; set; }
    
        #endregion


        public PepperiException()
        {
        }

        public PepperiException(string message)
            : base(message)
        {
        }

        public PepperiException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public PepperiException(string message, Exception inner, HttpStatusCode HttpStatusCode)
            : base(message, inner)
        {
            this.HttpStatusCode = HttpStatusCode;
        }
    }

}
