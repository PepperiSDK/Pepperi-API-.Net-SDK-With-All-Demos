using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormApiDemo.Helpers
{
    static internal class JsonParser
    {
        internal static string Serialize<TData>(TData data)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(
                                    data,
                                    Newtonsoft.Json.Formatting.Indented,
                                    new Newtonsoft.Json.JsonSerializerSettings
                                    {
                                        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                                        DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc //will serialize date time as utc
                                    });
        }

        internal static TData Deserialize<TData>(string data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<TData>(data);
        }
    }
}
