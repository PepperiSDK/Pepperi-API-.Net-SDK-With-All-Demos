using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Exceptions
{
    public class PepperiException : Exception
    {
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
    }

}
