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
    public class AccountCatalogsEndpoint : BaseEndpoint<AccountCatalog, Int64>
    {
        #region Properties
        #endregion

        #region Constructor

        internal AccountCatalogsEndpoint(string ApiBaseUri, IAuthentication Authentication, ILogger Logger) :
            base(ApiBaseUri, Authentication, Logger, "account_catalogs")
        {
        }


        #endregion

        #region Public methods
        #endregion
    }
}
