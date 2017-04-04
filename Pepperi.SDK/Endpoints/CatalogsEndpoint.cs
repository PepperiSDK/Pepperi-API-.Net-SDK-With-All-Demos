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
    public class CatalogsEndpoint : BaseEndpoint<Catalog, Int64>
    {
        #region Properties
        #endregion

        #region Constructor

        internal CatalogsEndpoint(string ApiBaseUri, IAuthentication Authentication, ILogger Logger) :
            base(ApiBaseUri, Authentication, Logger, "catalogs")
        {
        }


        #endregion

        #region Public methods
        #endregion
    }
}
