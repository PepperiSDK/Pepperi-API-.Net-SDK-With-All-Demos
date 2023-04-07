using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pepperi.SDK.Contracts;
using Pepperi.SDK.Endpoints.Base;

namespace Pepperi.SDK.Endpoints
{
    public class UserDefinedCollectionsMetaData_Endpoint : Metadata_BaseEndpoint_For_UserDefinedCollections
    {

        #region Properties
        #endregion

        #region Constructor

        internal UserDefinedCollectionsMetaData_Endpoint(IAuthentication Authentication, ILogger Logger, string papiBaseUri = null) :
            base(Authentication, Logger, papiBaseUri: papiBaseUri)
        {
            //Note: RespourceName is empty
        }

        #endregion
    }
}
