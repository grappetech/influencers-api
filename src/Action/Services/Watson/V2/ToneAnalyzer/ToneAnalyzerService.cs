using System;
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Model;
using WatsonToneAnalyzerService = IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.ToneAnalyzerService;
namespace Action.Services.Watson.V2.ToneAnalyzer
{
    public class ToneAnalyzerService
    {
        public Task<ToneAnalysis> ProccessToneAnalisys(string text, string language, string username, string password,
            string Version= "")
        {
            return Task.Run(() =>
            {
                var svc = new WatsonToneAnalyzerService(username, password, Version);
                return svc.Tone(new ToneInput
                    {
                        Text = text
                    },
                    "text/plain");
            });
        }
    }
}