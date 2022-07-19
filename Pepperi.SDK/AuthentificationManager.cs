using Pepperi.SDK.Contracts;
using Pepperi.SDK.Exceptions;
using Pepperi.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK
{
    public class AuthentificationManager
    {
        #region Properties
        private ILogger Logger { get; set; }
        public IpaasAddonAuthentification IpaasAuth { get; private set; }
        public IpaasAddonAuthentification IdpAuth { get; private set; }
        public PrivateAuthentication PepperiApiAuth { get; private set; }
        #endregion

        #region Constructor

        public AuthentificationManager(ILogger Logger, string APIToken, string ConsumerKey)
        {
            this.Logger = Logger;
            this.IpaasAuth = new IpaasAddonAuthentification(Logger, APIToken, false);
            this.IdpAuth = new IpaasAddonAuthentification(Logger, APIToken, true);
            this.PepperiApiAuth = new PrivateAuthentication(ConsumerKey, APIToken);
        }

        #endregion


    }

}


