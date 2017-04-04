using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Contracts
{
    public interface IAuthentication
    {
        AuthenticationHeaderValue GetAuthorizationHeaderValue();
        Dictionary<string, string> GetCustomRequestHeaders();

    }
}
