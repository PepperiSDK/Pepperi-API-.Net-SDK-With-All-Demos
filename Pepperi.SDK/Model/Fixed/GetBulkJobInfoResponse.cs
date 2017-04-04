using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed
{
    public class GetBulkJobInfoResponse
    {
        public string ID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string Status { get; set; }                  //"Not Started","In Progress","Ok","Failed"
        public int StatusCode { get; set; }
        public Int64 Records { get; set; }
        public Int64 RecordsInserted { get; set; }
        public Int64 RecordsIgnored { get; set; }
        public Int64 RecordsUpdated { get; set; }
        public Int64 RecordsFailed { get; set; }
        public string TotalProcessingTime { get; set; }
        public string OverwriteType { get; set; }
        public string Error { get; set; }
    }
}
