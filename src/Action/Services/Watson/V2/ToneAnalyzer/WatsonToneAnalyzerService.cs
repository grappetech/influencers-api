using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Action.Models.Watson.ToneAnalyze;
using WatsonServices.Services.ApiClient.Core;
using WatsonServices.Services.ApiClient.Core.Models;

namespace WatsonServices.Services.ApiClient.Watson
{
    public class WatsonToneAnalyzerService: AApiClientService
    {
        private static WatsonToneAnalyzerService _instance;
        private const string VERSION = "2016-05-19";
        public static WatsonToneAnalyzerService Instance => _instance ?? (_instance = new WatsonToneAnalyzerService());
        private static Uri Url = new Uri("https://gateway.watsonplatform.net/tone-analyzer/api/v3/");

        protected static ApiAuthorization Credendials { get; set; } =
            new ApiAuthorization().SetBasicAuthorization("13774f9f-c586-4323-82e4-b6e012eb869b", "DUB3O5alF7Jb");

        public void SetCredentials(string userId, string password)
        {
            Credendials = new ApiAuthorization().SetBasicAuthorization(userId, password);
        }
        
        public async Task<ToneResult> PostTone(string request, ApiAuthorization credentials = null)
        {
            var parameters = new Dictionary<string, string> {{"version", VERSION},{"sentences","true"}};
            var result = await Post<ToneResult>(Url, $"tone", new { text=request}, true,
               Credendials, parameters);
            return result;
        }
    }
}