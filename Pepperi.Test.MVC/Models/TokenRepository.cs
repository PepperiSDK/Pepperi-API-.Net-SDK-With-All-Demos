using Pepperi.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pepperi.Test.MVC.Models
{
    public class TokenRepository : IOauth2TokenRepository
    {
        #region Properties 

        private HttpSessionStateBase Session { get; set; }

        #endregion

        #region Constructor

        public TokenRepository(HttpSessionStateBase Session)
        {
            this.Session = Session;
        }
        
        #endregion

        #region IOauth2TokenRepository

        public void SaveToken(Oauth2Token Oauth2Token)
        {
            this.Session["Oauth2Token"] = Oauth2Token;
        }

        #endregion

        #region public methods

        public Oauth2Token GetOauth2Token()
        {
            return this.Session["Oauth2Token"] as Oauth2Token;
        }

        #endregion

    }
}