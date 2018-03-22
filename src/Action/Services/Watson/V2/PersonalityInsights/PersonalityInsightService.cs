using System.Collections.Generic;
using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.PersonalityInsights.v3.Model;
using Microsoft.IdentityModel.Tokens;
using WatsonPersonalityInsights = IBM.WatsonDeveloperCloud.PersonalityInsights.v3.PersonalityInsightsService;

namespace Action.Services.Watson.V2.PersonalityInsights
{
    public class PersonalityInsightsService
    {
        public Task<Profile> ProccessText(string text,  string username, string password, string modelId, string version = "2017-02-27")
        {
            return Task.Run(() =>
            {
                var svc = new WatsonPersonalityInsights(username, password, version);
                return svc.Profile(new Content{
                    ContentItems = new List<ContentItem>
                    {
                        new ContentItem
                        {
                            Content = text,
                            Contenttype = ContentItem.ContenttypeEnum.TEXT_PLAIN,
                            Language = ContentItem.LanguageEnum.EN
                        }
                    }
                }, "text/plain");
            });
        }
    }
}