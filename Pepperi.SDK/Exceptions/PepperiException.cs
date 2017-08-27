using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Exceptions
{
    //TODO: do we need it ?
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
