using System;
using System.Threading.Tasks;
using Action.Models;
using IBM.WatsonDeveloperCloud.ToneAnalyzer.v3;
using IBM.WatsonDeveloperCloud.ToneAnalyzer.v3.Model;

namespace Action.Services.Watson.ToneAnalyze
{
    public class ToneService
    {
        private static ToneService _instance;

        
        public static ToneService Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new ToneService();
                return _instance;
            }
        }

        public ApplicationDbContext AppContext { get; set; }

        
        private ToneService()
        {
            
        }
        
        public static async Task StartExtractPersonality(string content, long entityId, int scrapSourceId, Guid scrapedPageId)
        {
            await Task.Run(()=> _instance.ExtractTone( content, entityId, scrapedPageId, scrapSourceId));
        }
        
        private void ExtractTone(string content, long entityId, Guid scrapedPageId, int scrapSourceId)
        {
            var service = new ToneAnalyzerService
            {
                UserName = "20551440-7c7c-423b-a641-cda03616a59f",
                Password = "ASQzybjUHEbl",
                VersionDate = "2017-08-01"
            };
            var result = service.Tone(new ToneInput {Text = content});
            var tone = ToneResult.Parse(result);
            if (tone != null)
            {
                tone.EntityId = entityId;
                tone.ScrapedPageId = scrapedPageId;
                tone.ScrapSourceId = scrapSourceId;

                AppContext.Tones.Add(tone);
                AppContext.SaveChanges();
            }
        }
    }
}