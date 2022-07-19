using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed
{
    public class GetExportJobInfoResponse
    {
        public string ID { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }


        public string Status { get; set; }              //Succeeded = 1, InProgress = 2, Failed = 3
        public int StatusCode { get; set; }             //Succeeded = 1, InProgress = 2, Failed = 3
        public Int64? RecordsCount { get; set; }            //value is null when Status is Failed
        public string TotalProcessingTimeMs { get; set; }
        public string ErrorMessage { get; set; }
        public string GetDataURL { get; set; }
    }
}

