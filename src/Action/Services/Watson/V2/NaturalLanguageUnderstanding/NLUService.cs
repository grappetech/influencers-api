using System.Threading.Tasks;
using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1;
using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model;

namespace Action.Services.Watson.V2.NaturalLanguageUnderstanding
{
    public class NLUService
    {
        public Task ProccessUrl(string url, string username, string password, string modelId, string version = "2017-02-27")
        {
            return Task.Run( () =>
            {
                NaturalLanguageUnderstandingService svc =
                    new NaturalLanguageUnderstandingService(username, password, version);

                return svc.Analyze(new Parameters
                {
                    Url = url,
                    Language = "pt-BR",
                    ReturnAnalyzedText = true,
                    Features = new Features
                    {
                        Entities = new EntitiesOptions
                        {
                            Mentions = true,
                            Model = modelId
                        },
                        Keywords = new KeywordsOptions(),
                        Relations = new RelationsOptions
                        {
                            Model = modelId
                        }
                    }
                });

            });
        }
    }
}