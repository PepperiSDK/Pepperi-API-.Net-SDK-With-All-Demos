using Pepperi.SDK;
using Pepperi.SDK.Contracts;
using Pepperi.SDK.Exceptions;
using Pepperi.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.ModelGenerator
{
    internal class Factory
    {
        internal static ILogger GetLogger(string LogFilePath)
        {
            return new PepperiLogger(LogFilePath);
        }

        internal static ILogger GetLogger()
        {
            return new PepperiLogger();
        }

        internal static ApiClient GetApiClient_ForPrivateApplication(string Username, string Password, ILogger Logger)
        {
            string PrivateApplication_ConsumerKey = Settings.PrivateApplication_ConsumerKey;
            APITokenData APITokenData = Factory.GetAPITokenData_AndSave(Username, Password, Logger);
            var IAuthentication = new PrivateAuthentication(PrivateApplication_ConsumerKey, APITokenData.APIToken);

            string ApiBaseUri = Settings.ApiBaseUri;
            ApiClient ApiClient = new ApiClient(ApiBaseUri, IAuthentication, Logger);

            return ApiClient;
        }

        internal static ApiClient GetApiClient_ForPrivateApplication(string APIToken, ILogger Logger)
        {
            #region validate input

            if (APIToken == null || APIToken.Trim().Length == 0)
            {
                throw new PepperiException("Invalid argument. Api token can not be empty.");
            }

            #endregion

            string ApiBaseUri = Settings.ApiBaseUri;
            string PrivateApplication_ConsumerKey = Settings.PrivateApplication_ConsumerKey;

            var IAuthentication = new PrivateAuthentication(PrivateApplication_ConsumerKey, APIToken);

            ApiClient ApiClient = new ApiClient(ApiBaseUri, IAuthentication, Logger);

            return ApiClient;
        }

        internal static APITokenData GetAPITokenData_AndSave(string Username, string Password, ILogger Logger)
        {
            string ApiTokenBaseUri = Settings.ApiTokenBaseUri;
            string PrivateApplication_ConsumerKey = Settings.PrivateApplication_ConsumerKey;

            APITokenData APITokenData = PrivateAuthentication.GetAPITokenData(ApiTokenBaseUri, PrivateApplication_ConsumerKey, Username, Password, Logger);

            //save APITokenData.BaseURI in Settings (eg, file)
            Settings.ApiBaseUriFromTokenOrOauth = APITokenData.BaseURI;

            //save APITokenData.APIToken in Settings (eg, file)
            Settings.PrivateApplication_ApiToken = APITokenData.APIToken;

            return APITokenData;
        }

        /// <summary>
        /// used for console appliction
        /// </summary>
        /// <returns></returns>
        internal static ApiClient CreateApiClientForPrivateApplication(ILogger Logger)
        {

            //step1: read Api Token 
            string configuration_ApiToken = Settings.PrivateApplication_ApiToken;
            bool configuration_ApiToken_is_empty = configuration_ApiToken == null || configuration_ApiToken.Trim().Length == 0;


            //step2: ask user whether to change the api token
            bool? continueWithLastToken = null;
            if (configuration_ApiToken_is_empty)
            {
                continueWithLastToken = false;
            }
            else
            {
                while (continueWithLastToken == null)
                {
                    System.Console.WriteLine("The last used token is :");
                    System.Console.WriteLine(configuration_ApiToken == null ? "null" : configuration_ApiToken);
                    System.Console.WriteLine("continue with that ? (type 'yes' to continue or 'no'- to enter user and password)");

                    string resultAsString = System.Console.ReadLine();
                    if (resultAsString == "yes")
                    {
                        continueWithLastToken = true;
                    }
                    if (resultAsString == "no")
                    {
                        continueWithLastToken = false;
                    }
                }
            }


            //step3: exit if user selected: yes
            if (continueWithLastToken.Value)
            {
                ApiClient ApiClient = Factory.GetApiClient_ForPrivateApplication(Settings.PrivateApplication_ApiToken, Logger);
                return ApiClient;
            }

            //step4: read username and password
            System.Console.WriteLine("Enter username:");
            string Username = System.Console.ReadLine();

            System.Console.WriteLine("Enter Password:");
            string Password = System.Console.ReadLine();


            APITokenData APITokenData = Factory.GetAPITokenData_AndSave(Username, Password, Logger);

            ApiClient result = Factory.GetApiClient_ForPrivateApplication(Settings.PrivateApplication_ApiToken, Logger);
            return result;

        }

    }
}
