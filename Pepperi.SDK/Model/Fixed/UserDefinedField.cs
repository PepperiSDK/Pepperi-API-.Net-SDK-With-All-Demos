using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Model.Fixed
{
    public class UserDefinedField
    {
        public string Type { get; set; }        //value from eUserDefinedFieldType
        public string ID { get; set; }
        public string Label { get; set; }
        public List<string> MultipleValues { get; set; }
        public eUiControlType UIControlType { get; set; }
    }

}

/*
 * {
							"Type":				"TypeSafeAttributeConfigDouble",
							"ReadOnly":			"false",
							"Name":				"TSADouble5",
							"Label":			"TSADouble5",
							"NumberOfDigits":	"2",
							"MaxValue":			"10000",
							"MinValue":			"-10000",
							"MultipleValues":	["abcd","efgh","ijklmn"],
							"UIControlType" :	1
						}
*/