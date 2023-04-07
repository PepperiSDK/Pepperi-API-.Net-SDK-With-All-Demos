using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Exceptions
{
    public class IpaasException : Exception
    {
        #region Properties
        public HttpStatusCode? HttpStatusCode { get; set; }
        #endregion

        public IpaasException()
        {
        }

        public IpaasException(string message)
            : base(message)
        {
        }
        public IpaasException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public IpaasException(string message, HttpStatusCode HttpStatusCode)
            : base(message)
        {
            this.HttpStatusCode = HttpStatusCode;
        }

        public IpaasException(string message, Exception inner, HttpStatusCode HttpStatusCode)
            : base(message, inner)
        {
            this.HttpStatusCode = HttpStatusCode;
        }
    }
}
