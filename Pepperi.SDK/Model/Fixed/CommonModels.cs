using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed
{

    public class UDC_UploadFile_Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        
        public int Total { get; set; }
        public int TotalInserted { get; set; }
        public int TotalUpdated { get; set; }
        public int TotalIgnored { get; set; }
        public int TotalFailed { get; set; }
        public IEnumerable<UDC_UploadFile_Row> FailedRows { get; set; }
    }

    public class UDC_UploadFile_Row
    {
        public string Key { get; set; }
        public string Status { get; set; }
        public string Details { get; set; }
    }

    public class UDC_UploadFile_AuditLog_ResultObject
    {
        public string URI { get; set; }
    }
}
