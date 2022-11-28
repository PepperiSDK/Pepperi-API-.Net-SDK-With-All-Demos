using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.Demo.Console.Model.TestModels
{
    public class TestUDC
    {
        public Int64? testIntegerField { get; set; }
        public Boolean? testBoolField { get; set; }
        public DateTime? testDateTimeField { get; set; }
        public Double? testDoubleField { get; set; }
        public IEnumerable<string> testStringArrayField { get; set; }
        public IEnumerable<Int64> testIntegerArrayField { get; set; }
        public string key { get; set; }
    }
}
