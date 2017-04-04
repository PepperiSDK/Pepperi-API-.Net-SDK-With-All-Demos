using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Helpers
{
    internal class PepperiJsonSerializer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        /// <remarks>
        /// Properties with null values are not serialzied.
        /// This way, on post we do not sent on Post body properties that user did not set (app properties in model object are nullable)
        /// Server will  perpform parial post: on insert it will set default values for the missing properties. on update it will not change the value of missing properties.
        /// </remarks>
        internal static string Serialize<T>(T obj)
        {
            string result = Newtonsoft.Json.JsonConvert.SerializeObject(
                                    obj,
                                    Newtonsoft.Json.Formatting.None,
                                    new Newtonsoft.Json.JsonSerializerSettings
                                    {
                                        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                                        DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc //will serialize date time as utc
                                    });

            return result;
        }

        

        internal static TModel DeserializeOne<TModel>(string json) //where TModel : class
        {
            TModel Model = Newtonsoft.Json.JsonConvert.DeserializeObject<TModel>(json);
            return Model;
        }

        internal static IEnumerable<TModel> DeserializeCollection<TModel>(string json) //where TModel : class
        {
            IEnumerable<TModel> Model = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<TModel>>(json);
            return Model;
        }

    }
}
