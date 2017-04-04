using Pepperi.SDK;
using Pepperi.SDK.Contracts;
using Pepperi.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Pepperi.Test.MVC.Models
{
    internal class Factory
    {
        internal static ILogger GetLogger()
        {
            return new PepperiLogger();
        }

        internal static ApiClient GetAapiClient_ForPublicApplication(HttpSessionStateBase Session, ILogger Logger)
        {

            string ApiBaseUri =                         Settings.ApiBaseUri;
            string OauthBaseUri =                       Settings.OauthBaseUri;
            string PublicApplication_ConsumerKey =      Settings.PublicApplication_ConsumerKey;
            string PublicApplication_ConsumerSecret =   Settings.PublicApplication_ConsumerSecret;


            TokenRepository Oauth2TokenRepository =     new TokenRepository(Session);
            Oauth2Token Oauth2Token =                   Oauth2TokenRepository.GetOauth2Token();

            var IAuthentication =                       new PublicAuthentication(Logger, OauthBaseUri, PublicApplication_ConsumerKey, PublicApplication_ConsumerSecret, Oauth2Token, Oauth2TokenRepository);

            ApiClient ApiClient =                       new ApiClient(ApiBaseUri, IAuthentication, Logger);

            return ApiClient;
        }
    }
}