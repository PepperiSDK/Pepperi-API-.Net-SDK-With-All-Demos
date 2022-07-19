using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed
{
    public class PepperiAuditLog
    {
        public string DistributorUUID { get; set; }
        public string UUID { get; set; }
        public DateTime? CreationDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }
        public string AuditType { get; set; }
        public PepperiAuditLogEvent Event { get; set; }
        public string SourceAuditLog { get; set; }
        public PepperiAuditLogStatus Status { get; set; }
        public PepperiAuditLogAuditInfo AuditInfo { get; set; }
    }

    public class PepperiAuditLogEvent
    {
        public string Type { get; set; }
        public PepperiAuditLogEventUser User { get; set; }
    }

    public class PepperiAuditLogEventUser
    {
        public int InternalID { get; set; }
        public string UUID { get; set; }
        public string Email { get; set; }
    }

    public class PepperiAuditLogStatus
    {
        public int ID { get; set; } //Succeeded = 1, InProgress = 2
        public string Name { get; set; }
    }

    public class PepperiAuditLogAuditInfo
    {
        public string ResultObject { get; set; }
        public PepperiAuditLogAuditInfoAddon Addon { get; set; }
        public string AddonJobExecutionUUID { get; set; }
        public string Type { get; set; }
        public string FromVersion { get; set; }
        public string ToVersion { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class PepperiAuditLogAuditInfoAddon
    {
        public string Name { get; set; }
        public string UUID { get; set; }
    }

    public class PepperiResponseForAuditLog
    {
        public string ExecutionUUID { get; set; }
        public string URI { get; set; }
    }
}
