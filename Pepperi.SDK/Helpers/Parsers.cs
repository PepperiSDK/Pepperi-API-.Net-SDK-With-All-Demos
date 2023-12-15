using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Helpers
{
    public static class TextParser
    {
        /// <summary>
        /// Parse next Format:
        /// {"CreationDateTime":"2023-04-20T13:14:30.108Z"}
        /// {"CreationDateTime":"2023-04-20T13:14:30.108Z"}
        /// </summary>
        public static IEnumerable<TData> ParseJsonRows<TData>(string jsonRows, string rowsSplitter = "\n")
        {
            var result = new List<TData>() { };
            if (string.IsNullOrEmpty(jsonRows)) return result;

            var splitted = jsonRows.Split(new string[] { rowsSplitter }, StringSplitOptions.None);
            if (splitted.Count() == 0) return result;

            var joined = string.Join(",", splitted);
            var jsonToParse = "[" + joined + "]";

            return PepperiJsonSerializer.DeserializeCollection<TData>(jsonToParse);
        }
    }
}
