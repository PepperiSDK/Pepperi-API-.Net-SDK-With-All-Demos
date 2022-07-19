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
    public class ActivitiesMetaData_Endpoint : Metadata_BaseEndpoint_For_TypedResource
    {
        #region Properties
        #endregion

        #region Constructor

        internal ActivitiesMetaData_Endpoint(string ApiBaseUri, IAuthentication Authentication, ILogger Logger, bool IsInternalAPI = false) :
            base(ApiBaseUri, Authentication, Logger, "activities", IsInternalAPI)
        {
        }


        #endregion

        #region Public methods
        #endregion
    }
}
