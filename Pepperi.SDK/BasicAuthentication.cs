using Pepperi.SDK.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Pepperi.SDK
{
    public class BasicAuthentication : IAuthentication
    {
        #region Properties

        private string Username { get; set; }
        private string Password { get; set; }
        private string AppConsumerKey { get; set; }             
        private bool AddConsumerKeyHeader { get; set; }
        #endregion

        #region Constructor

        public BasicAuthentication(string Username, string Password, string AppConsumerKey, bool AddConsumerKeyHeader)
        {
            this.Username = Username;
            this.Password = Password;
            this.AppConsumerKey = AppConsumerKey;
            this.AddConsumerKeyHeader = AddConsumerKeyHeader;
        }

        #endregion

        #region IAuthentication
        
        public System.Net.Http.Headers.AuthenticationHeaderValue GetAuthorizationHeaderValue()
        {
            var result = new AuthenticationHeaderValue(
                        "Basic",
                        Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", this.Username, this.Password)))
                        );

            return result;
        }


        public Dictionary<string, string> GetCustomRequestHeaders()
        {
            var result = new Dictionary<string, string>();

            if (AddConsumerKeyHeader)
            {
                result.Add("X-Pepperi-ConsumerKey", this.AppConsumerKey);
            }

            return result;
        }

        #endregion
    }
}
