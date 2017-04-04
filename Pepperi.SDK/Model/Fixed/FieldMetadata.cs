using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed
{
    public class FieldMetadata
    {
        public string Name { get; set; }
        public string MapName { get; set; }
        public bool Mandatory { get; set; }
        public bool ReadOnly { get; set; }
        public string DataType { get; set; }
        public int UIType { get; set; }
        public int MaxCharacters { get; set; }
        public string Reference { get; set; }
    }
}
