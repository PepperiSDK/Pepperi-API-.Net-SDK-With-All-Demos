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
    public class ItemDimensions2Endpoint : BaseEndpoint<ItemDimensions2, Int64>
    {
        #region Properties
        #endregion

        #region Constructor


        internal ItemDimensions2Endpoint(string ApiBaseUri, IAuthentication Authentication, ILogger Logger) :
            base(ApiBaseUri, Authentication, Logger, "item_dimensions2")
        {
        }


        #endregion

        #region Public methods
        #endregion
    }
}
