using Pepperi.SDK.Contracts;
using Pepperi.SDK.Endpoints.ipaasApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK
{
    public class IpaasApiClient
    {
        #region Properties

        public IpaasScheduledJobsEndpoint ScheduledJobs { get; private set; }

        #endregion
        public IpaasApiClient(string ipaasBaseUrl, ILogger Logger, AuthentificationManager AuthentificationManager)
        {
            Initialize(ipaasBaseUrl, Logger, AuthentificationManager);
        }

        private void Initialize(string ipaasBaseUrl, ILogger Logger, AuthentificationManager AuthentificationManager)
        {
            var ipaasAuth = AuthentificationManager?.IpaasAuth;
            this.ScheduledJobs = new IpaasScheduledJobsEndpoint(ipaasBaseUrl, Logger, ipaasAuth);
        }
    }
}
