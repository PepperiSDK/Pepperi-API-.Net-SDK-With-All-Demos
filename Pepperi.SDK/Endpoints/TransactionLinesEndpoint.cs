using Pepperi.SDK.Helpers;
using Pepperi.SDK.Endpoints.Base;
using Pepperi.SDK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pepperi.SDK.Contracts;

namespace Pepperi.SDK.Endpoints
{
    public class TransactionLinesEndpoint : BaseEndpoint<TransactionLine, Int64>
    {
        #region Properties
        #endregion

        #region Constructor

        internal TransactionLinesEndpoint(string ApiBaseUri, IAuthentication Authentication, ILogger Logger) :
            base(ApiBaseUri, Authentication, Logger, "transaction_lines")
        {
        }


        #endregion

        #region Public methods
        #endregion
    }
}
