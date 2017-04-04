using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Exceptions
{
    /// <summary>
    /// Exception returned by Api on Post body of 400 Bad Request
    /// </summary>
    /// <remarks>
    /// 1. To get the exception, try inserting transaction where AccountExternalID is not set.
    /// 2. Serialized Exception looks like:
    ///     {"fault":{"faultstring":"The mandatory property \"AccountInternalID\" can't be ignore.","detail":{"errorcode":"InvalidData"}}}
    /// 3. Json serializer uses the following constructor:
    ///     public ApiException(SerializationInfo info, StreamingContext context) 
    ///     http://stackoverflow.com/questions/3422703/how-to-deserialize-object-derived-from-exception-class-using-json-net-c/3423037#3423037
    /// </remarks>
    [Serializable]
    public class ApiException : Exception
    {
        #region Properties

        public Fault fault { get; set; }

        #endregion

        #region Constructor

        public ApiException()
        {
        }

        public ApiException(string message)
            : base(message)
        {
        }

        public ApiException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Construcor used by json serializer
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public ApiException(SerializationInfo info, StreamingContext context) 
        {
            if (info != null)
            {
                this.fault = info.GetValue("fault", typeof(Fault)) as Fault;
            }
        }

        #endregion

        #region Public

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
                info.AddValue("fault", this.fault);
        }

        #endregion
    }

    #region model

    public class Fault
    {
        #region Properties

        public string faultstring { get; set; }
        public Detail detail { get; set; }

        #endregion

        #region Constructor

        public Fault()
        {
        }

        #endregion
    }

    public class Detail
    {
        #region Properties

        public string errorcode { get; set; }

        #endregion

        #region Constructor

        public Detail()
        {
        }

        #endregion
    }

    #endregion
}