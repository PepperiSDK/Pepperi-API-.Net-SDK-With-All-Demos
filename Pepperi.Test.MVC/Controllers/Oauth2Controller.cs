using Pepperi.SDK;
using Pepperi.SDK.Contracts;
using Pepperi.SDK.Helpers;
using Pepperi.Test.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Pepperi.Test.MVC.Controllers
{
    /// <summary>
    /// NOTE:
    /// SET THE CALLBACKURL OF THE PUBLIC APPLICATION TO http://localhost:1275/Oauth2/GetAccessToken FOR DEBUGGING
    /// </summary>
    public class Oauth2Controller : Controller
    {
        [HttpGet]
        public ActionResult GetAuthorizationCode()
        {
            string callbackUrl = Url.Action("GetAccessToken", "Oauth2", null, Request.Url.Scheme, Request.Url.Host);

            ILogger Logger = new PepperiLogger();
            PublicAuthentication PublicAuthentication = new PublicAuthentication(Logger, Settings.OauthBaseUri, Settings.PublicApplication_ConsumerKey, Settings.PublicApplication_ConsumerSecret);
            string AuthorizationUrl = PublicAuthentication.GetAuthorizationUrl(callbackUrl, "READ");
            return Redirect(AuthorizationUrl);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">The authorization code we get from service when user provides valid credentials. It is sent to the service to get the access token.</param>
        /// <returns></returns>
        public ActionResult GetAccessToken(string code)
        {
            string callbackUrl = Url.Action("GetAccessToken", "Oauth2", null, Request.Url.Scheme, Request.Url.Host);

            ILogger Logger = new PepperiLogger();
            PublicAuthentication PublicAuthentication = new PublicAuthentication(Logger, Settings.OauthBaseUri, Settings.PublicApplication_ConsumerKey, Settings.PublicApplication_ConsumerSecret);
            Oauth2Token Oauth2Token = PublicAuthentication.GetAccessTokenByAuthorizationCode(code, callbackUrl, "READ", new TokenRepository(this.Session));

            return RedirectToAction("Transactions", "Transaction");
        }
    }

   

}

