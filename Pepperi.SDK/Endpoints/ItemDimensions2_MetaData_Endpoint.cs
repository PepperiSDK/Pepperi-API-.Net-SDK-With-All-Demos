using Pepperi.SDK.Contracts;
using Pepperi.SDK.Endpoints.Base;
using Pepperi.SDK.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Pepperi.SDK.Endpoints
{
    public class ItemDimensions2_MetaData_Endpoint : Metadata_BaseEndpoint_For_StandardResource
    {
        #region Properties
        #endregion

        #region Constructor

        internal ItemDimensions2_MetaData_Endpoint(string ApiBaseUri, IAuthentication Authentication, ILogger Logger, bool IsInternalAPI = false) :
            base(ApiBaseUri, Authentication, Logger, "item_dimensions2", IsInternalAPI)
        {
        }


        #endregion

        #region Public methods
        #endregion
    }
}
