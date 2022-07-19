using Pepperi.SDK.Contracts;
using Pepperi.SDK.Model.Fixed;
using Pepperi.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;

namespace Pepperi.SDK.Endpoints
{
    public class AuditLogsEndpoint
    {
        private string ApiBaseUri { get; set; }
        private IAuthentication Authentication { get; set; }
        private ILogger Logger { get; set; }

        internal AuditLogsEndpoint(string ApiBaseUri, IAuthentication Authentication, ILogger Logger)
        {
            this.ApiBaseUri = ApiBaseUri;
            this.Authentication = Authentication;
            this.Logger = Logger;
        }

        public PepperiAuditLog AuditLogPolling(string auditLogId, int poolingInternvalInMs = 1000, int numberOfPoolingAttempts = 60 * 5)
        {
            PepperiAuditLog result = null;
            var current = 0;
            var isStillPolling = true;
            while (current < numberOfPoolingAttempts && isStillPolling)
            {
                Thread.Sleep(poolingInternvalInMs);
                current += 1;

                var log = GetAuditLog(auditLogId);

                var statusId = log?.Status?.ID;

                if (statusId != (int)ePepperiAuditLogStatus.InProgress)
                {
                    isStillPolling = false;
                    result = log;
                }
            }

            return result;
        }

        public PepperiAuditLog GetAuditLog(string auditLogId)
        {

            var RequestUri = $"audit_logs/{auditLogId}";
            var dicQueryStringParameters = new Dictionary<string, string>();
            PepperiHttpClient PepperiHttpClient = new PepperiHttpClient(this.Authentication, this.Logger);
            PepperiHttpClientResponse PepperiHttpClientResponse = PepperiHttpClient.Get(
                    ApiBaseUri,
                    RequestUri,
                    dicQueryStringParameters,
                    "application/json"
                    );

            PepperiHttpClient.HandleError(PepperiHttpClientResponse);

            return PepperiJsonSerializer.DeserializeOne<PepperiAuditLog>(PepperiHttpClientResponse.Body);
        }
    }
}
