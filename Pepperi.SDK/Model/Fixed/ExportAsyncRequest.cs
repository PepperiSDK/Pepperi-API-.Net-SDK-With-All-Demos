using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed
{
    public class ExportAsyncRequest
    {
        public string where { get; set; }
        public string order_by { get; set; }
        public bool? include_deleted { get; set; }
        public string fields { get; set; }
        public bool? is_distinct { get; set; }
    }
}
