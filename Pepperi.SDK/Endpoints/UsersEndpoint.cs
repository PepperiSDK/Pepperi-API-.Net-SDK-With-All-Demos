﻿using Pepperi.SDK.Contracts;
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
    public class UsersEndpoint : BaseEndpoint<User, Int64>
    {
        #region Properties
        #endregion

        #region Constructor

        internal UsersEndpoint(string ApiBaseUri, IAuthentication Authentication, ILogger Logger) :
            base(ApiBaseUri, Authentication, Logger, "users")
        {
        }
       
        #endregion

        #region Public methods
        #endregion
    }
}
