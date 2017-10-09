using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Action.Models;
using IBM.WatsonDeveloperCloud.PersonalityInsights.v3;
using IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Model;

namespace Action.Services.Watson.PersonalityInsights
{
    public class PersonalityService
    {
        private static PersonalityService _instance;

        public static PersonalityService Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new PersonalityService();
                return _instance;
            }
        }

        public ApplicationDbContext AppContext { get; set; }

        
        private PersonalityService()
        {
            
        }


        public static async Task StartExtractPersonality(string content, long entityId, int scrapSourceId, Guid scrapedPageId)
        {
            await Task.Run(() =>
            {
                _instance.ExtractPersonality(content, entityId, scrapedPageId, scrapSourceId);
            });
        }

        private void ExtractPersonality(string content, long entityId, Guid scrapedPageId, int scrapSourceId)
        {
             var personality = GetPersonalityResult(content);

            personality.EntityId = entityId;
                            personality.ScrapedPageId = scrapedPageId;
                            personality.ScrapSourceId = scrapSourceId;
            
                            AppContext.Personalities.Add(personality);
                            AppContext.SaveChanges();
        }

        public static PersonalityResult GetPersonalityResult(string content)
        {
            var service = new PersonalityInsightsService
            {
                UserName = "48f4249d-5d39-4921-83bd-1149590de5fe",
                Password = "2Mz1lyPKLPQk",
                VersionDate = "2016-10-20"
            };

            ContentListContainer contentListContainer = new ContentListContainer()
            {
                ContentItems = new List<ContentItem>()
                {
                    new ContentItem()
                    {
                        Contenttype = ContentItem.ContenttypeEnum.TEXT_PLAIN,
                        Language = ContentItem.LanguageEnum.EN,
                        Content = content
                    }
                }
            };

            var result = service.Profile("text/plain", "application/json",
                contentListContainer,
                rawScores: true, consumptionPreferences: true, csvHeaders: true);

            var personality = PersonalityResult.Parse(result);
            return personality;
        }
    }
}