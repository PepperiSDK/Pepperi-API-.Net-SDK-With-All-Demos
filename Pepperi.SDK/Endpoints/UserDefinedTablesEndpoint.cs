using Pepperi.SDK.Helpers;
using Pepperi.SDK.Endpoints.Base;
using Pepperi.SDK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pepperi.SDK.Contracts;
using Pepperi.SDK.Model.Fixed;

namespace Pepperi.SDK.Endpoints
{
    public class UserDefinedTablesEndpoint : BaseEndpoint<UserDefinedTable, Int64>
    {
        #region Properties
        #endregion

        #region Constructor

        internal UserDefinedTablesEndpoint(string ApiBaseUri, IAuthentication Authentication, ILogger Logger) :
            base(ApiBaseUri, Authentication, Logger, "user_defined_tables")
        {
        }

        #endregion

        #region Public methods
        #endregion
    }
   
}
