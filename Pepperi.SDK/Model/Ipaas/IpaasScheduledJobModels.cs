using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Ipaas
{

    public class IpaasStartScheduledJobResponse
    {
        public string Message { get; set; }
        public int ResultStatus { get; set; }
        public int ScheduledJobLogId { get; set; }
    }

    public class IpaasScheduledJobLogStatusApi
    {
        public int ScheduledJobLogId { get; set; }
        public int RunStatusId { get; set; }
        public int TasksLogsSequence { get; set; }
    }

    public class IpaasDataUrls
    {
        public string CsvUrl { get; set; }
        public string JsonUrl { get; set; }
    }
}
