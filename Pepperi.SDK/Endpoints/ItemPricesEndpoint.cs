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
    public class ItemPricesEndpoint : BaseEndpoint<ItemPrice, Int64>
    {
        #region Properties
        #endregion

        #region Constructor


        internal ItemPricesEndpoint(string ApiBaseUri, IAuthentication Authentication, ILogger Logger) :
            base(ApiBaseUri, Authentication, Logger, "item_prices")
        {
        }


        #endregion

        #region Public methods
        #endregion
    }
}
