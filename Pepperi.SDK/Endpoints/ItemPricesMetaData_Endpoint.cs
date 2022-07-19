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
    public class ItemPricesMetaData_Endpoint : Metadata_BaseEndpoint_For_StandardResource
    {
        #region Properties
        #endregion

        #region Constructor


        internal ItemPricesMetaData_Endpoint(string ApiBaseUri, IAuthentication Authentication, ILogger Logger, bool IsInternalAPI = false) :
            base(ApiBaseUri, Authentication, Logger, "item_prices", IsInternalAPI)
        {
        }


        #endregion

        #region Public methods
        #endregion
    }
}
