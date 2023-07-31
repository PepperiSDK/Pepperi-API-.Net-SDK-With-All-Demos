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

        public PepperiAuditLog AuditLogPolling(string auditLogId, int poolingInternvalInMs = 5000, int numberOfPoolingAttempts = 500)
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

                result = log;

                if (statusId == null || statusId == (int)ePepperiAuditLogStatus.Success || statusId == (int)ePepperiAuditLogStatus.Failure)
                {
                    isStillPolling = false;
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

        public void ValidateSuccessAuditLog(PepperiAuditLog auditLog, string processName = "Audit Log Pooling") {
            ValuesValidator.Validate(auditLog, $"{processName} error. Audit Log is empty!", false);

            var logStatusId = auditLog?.Status?.ID;
            ValuesValidator.Validate(logStatusId != null, $"{processName} error. Can't get Audit Log Status ID!", false);
            
            ValuesValidator.Validate(logStatusId != (int)ePepperiAuditLogStatus.Failure, 
                $"{processName} finished with \"Failure\" status! Error Message: {auditLog?.AuditInfo?.ErrorMessage ?? "No Message"}", false);

            ValuesValidator.Validate(logStatusId == (int)ePepperiAuditLogStatus.Success, 
                $"{processName} finished with \"{auditLog?.Status?.Name ?? logStatusId.ToString()}\" status! (Pooling may reached max number of attempts)", false);
        }
    }
}
