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
    public class ItemDimensions1Endpoint : BaseEndpoint<ItemDimensions1, Int64>
    {
        #region Properties
        #endregion

        #region Constructor


        internal ItemDimensions1Endpoint(string ApiBaseUri, IAuthentication Authentication, ILogger Logger) :
            base(ApiBaseUri, Authentication, Logger, "item_dimensions1")
        {
        }


        #endregion

        #region Public methods
        #endregion
    }
}
