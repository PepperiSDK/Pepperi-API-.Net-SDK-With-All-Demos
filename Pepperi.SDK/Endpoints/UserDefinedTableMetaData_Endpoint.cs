using Pepperi.SDK.Contracts;
using Pepperi.SDK.Endpoints.Base;
using Pepperi.SDK.Helpers;
using Pepperi.SDK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK.Endpoints
{
    /// <summary>
    /// Note: this endpoint does not use ResourceName
    /// </summary>
    public class UserDefinedTableMetaData_Endpoint : Metadata_BaseEndpoint_For_UserDefinedTable
    {

        #region Properties
        #endregion

        #region Constructor

        internal UserDefinedTableMetaData_Endpoint(string ApiBaseUri, IAuthentication Authentication, ILogger Logger) :
            base(ApiBaseUri, Authentication, Logger)
        {
            //Note: RespourceName is empty
        }

        #endregion
    }
}
