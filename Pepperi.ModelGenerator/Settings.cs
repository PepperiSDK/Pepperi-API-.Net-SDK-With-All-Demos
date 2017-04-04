using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.ModelGenerator
{
    /// <summary>
    /// token and BaseURI returned by the server are stored in Persistent memeory (file)
    /// </summary>
    internal class Settings
    {
        public static string ApiTokenBaseUri { get { return ConfigurationManager.AppSettings["ApiTokenBaseUri"]; } }

        public static string OauthBaseUri { get { return ConfigurationManager.AppSettings["OauthBaseUri"]; } }

        public static string ApiBaseUri
        {
            get
            {
                //if the value exists in setting file, take it from there (that is the value stored as we get public\private token from server.)
                //otherwise. take value from configuration file

                if (ApiBaseUriFromTokenOrOauth == null || ApiBaseUriFromTokenOrOauth.Trim().Length == 0)
                {
                    return ConfigurationManager.AppSettings["ApiBaseUri"];
                }
                else
                {
                    return ApiBaseUriFromTokenOrOauth + "/v1.0/";
                }
            }
        }



        /// <summary>
        /// stores the ApiBaseUri returned with private\public token
        /// </summary>
        /// <remarks>
        /// 1. The setter is called once we get private\public api token
        /// 2. The getter is called to get APIBaseURI for the ApiClientConstructor
        /// </remarks>
        public static string ApiBaseUriFromTokenOrOauth
        {
            get
            {
                string AssemblyLocation = Assembly.GetExecutingAssembly().Location;
                string AssemblyPath = Path.GetDirectoryName(AssemblyLocation);
                string fileDirectory = AssemblyPath;
                string FileName = "Settings_ApiBaseUriFromTokenOrOauth.txt";
                string filePath = Path.Combine(fileDirectory, FileName);

                string result = null;
                if (System.IO.File.Exists(filePath) == true)
                {
                    result = System.IO.File.ReadAllText(filePath);
                }

                return result;
            }
            set
            {
                string AssemblyLocation = Assembly.GetExecutingAssembly().Location;
                string AssemblyPath = Path.GetDirectoryName(AssemblyLocation);
                string fileDirectory = AssemblyPath;
                string FileName = "Settings_ApiBaseUriFromTokenOrOauth.txt";
                string filePath = Path.Combine(fileDirectory, FileName);

                System.IO.File.WriteAllText(filePath, value);
            }
        }


        /// <summary>
        /// get\set value against setting file (instead db)
        /// </summary>
        /// <remakrks>
        /// 1. the value is returned from server given username and password (see GetAPITokenData)
        /// 2. the value is used to manupulate user data through the API on behalf of the user 
        /// 3. for demo, the value is stored in file (can come from db)
        /// </remakrks>
        public static string PrivateApplication_ApiToken
        {
            get
            {
                //get is called to initialzie API Client for private application
                string AssemblyLocation = Assembly.GetExecutingAssembly().Location;
                string AssemblyPath = Path.GetDirectoryName(AssemblyLocation);
                string fileDirectory = AssemblyPath;
                string FileName = "Settings_PrivateApplication_ApiToken.txt";
                string filePath = Path.Combine(fileDirectory, FileName);

                string result = null;
                if (System.IO.File.Exists(filePath) == true)
                {
                    result = System.IO.File.ReadAllText(filePath);
                }

                return result;
            }
            set
            {
                //set is called after GetAPITokenData is called (user provides username and password, server returns API Token)
                //Save in repositry (file).
                string AssemblyLocation = Assembly.GetExecutingAssembly().Location;
                string AssemblyPath = Path.GetDirectoryName(AssemblyLocation);
                string fileDirectory = AssemblyPath;
                string FileName = "Settings_PrivateApplication_ApiToken.txt";
                string filePath = Path.Combine(fileDirectory, FileName);

                System.IO.File.WriteAllText(filePath, value);
            }
        }





        public static string PublicApplication_ConsumerKey { get { return ConfigurationManager.AppSettings["PublicApplication_ConsumerKey"]; } }

        public static string PublicApplication_ConsumerSecret { get { return ConfigurationManager.AppSettings["PublicApplication_ConsumerSecret"]; } }


        public static string PrivateApplication_ConsumerKey { get { return ConfigurationManager.AppSettings["PrivateApplication_ConsumerKey"]; } }

    }
}
