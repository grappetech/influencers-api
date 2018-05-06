using System;
using System.Net.Http.Headers;
using System.Text;

namespace WatsonServices.Services.ApiClient.Core.Models
{
    public class ApiAuthorization
    {
        internal EAuthorizationType Type { get; private set; }
        internal string Token { get; private set; }

        public ApiAuthorization SetBasicAuthorization(string UserName, string Password)
        {
            Type = EAuthorizationType.Basic;
            Token = Convert.ToBase64String(UTF8Encoding.UTF8.GetBytes($"{UserName}:{Password}"));
            return this;
        }
        
        public ApiAuthorization SetBearerToken(string token)
        {
            Type = EAuthorizationType.Bearer;
            Token = token;
            return this;
        }

        public AuthenticationHeaderValue GetAuthorizationValue()
        {
            string type = Type == EAuthorizationType.Basic ? "Basic" : "Bearer";
            return AuthenticationHeaderValue.Parse($"{type} {Token}");
        }
    }
}