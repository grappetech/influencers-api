using System;
using System.Linq;
using System.Threading.Tasks;
using Action.Models;
using Action.Models.Scrap;
using Action.Services.Scrap;
using Action.Services.Scrap.Repositories;
using Action.Services.Watson.PersonalityInsights;
using Action.Services.Watson.ToneAnalyze;
using Hangfire;
using Hangfire.Common;
using Hangfire.MemoryStorage;
using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1;
using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model;
using Microsoft.EntityFrameworkCore;

namespace Action.Services.Watson.NLU
{
    public class NluService
    {
        private static NluService _instance;

        private NluService()
        {
        }

        public ApplicationDbContext AppContext { get; set; }

        public static NluService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new NluService();
                return _instance;
            }
        }

        public static async Task StartExtraction()
        {
            var pages = new ScrapperContext().ScrapedPages.Where(x=>x.Status == EDataExtractionStatus.Waiting).ToList();
            await Task.Run(() =>
            {
                foreach (var page in pages)
                {
                    NluService.Instance.ExtractData(page.Id);
                }
                
            });
        }


        public void ExtractData(Guid pageId)
        {
            var service = new NaturalLanguageUnderstandingService
            {
                Password = "Of5W0mIzzGao",
                VersionDate = "2017-02-27",
                UserName = "be93137e-6d1d-4136-b240-87804f7f80d7"
            };

            var ctx = new ScrapperContext();
            var page = ctx.ScrapedPages.Find(pageId);
            page.Status = EDataExtractionStatus.InProcces;
            ctx.SaveChanges();

            var analysisResults = service.Analyze(new Parameters
            {
                FallbackToRaw = true,
                ReturnAnalyzedText = true,
                Features = new Features
                {
                    Entities = new EntitiesOptions
                    {
                        Model = "10:eb36f28a-7a16-46d0-92c8-65bdf0b2bffb"
                    },
                    Relations = new RelationsOptions
                    {
                        Model = "10:eb36f28a-7a16-46d0-92c8-65bdf0b2bffb"
                    },
                    Keywords = new KeywordsOptions()
                },
                Language = "pt-BR",
                Text = page.Text
            });

            var nluResult = NLUResult.Parse(analysisResults);
            nluResult.ScrapedPageId = pageId;
            AppContext.NluResults.Add(nluResult);
            AppContext.SaveChanges();

            foreach (var item in analysisResults.Entities.Where(e => e.Type.ToLower().Equals("marcas") || e.Type.ToLower().Equals("pessoa")))
            {
                Models.Watson.Entity entity;
                var mention = new EntityMentions {ScrapedPageId = pageId, ScrapSourceId = page.ScrapSourceId ?? 0};
                if (!AppContext.Entities.Any(x => x.Alias.Contains(item.Text)))
                {
                    entity = new Models.Watson.Entity
                    {
                        Alias = item.Text,
                        Name = item.Text,
                        CategoryId = item.Type.ToLower().Equals("marcas") ? ECategory.Brand : ECategory.Person ,
                        Date = DateTime.UtcNow
                    };

                    AppContext.Entities.Add(entity);
                    AppContext.SaveChanges();
                    mention.EntityId = entity.Id;
                }
                else
                {
                    entity = AppContext.Entities.FirstOrDefault(x => x.Alias.Equals(item.Text));
                    entity.Date = DateTime.UtcNow;
                    AppContext.SaveChanges();
                    mention.EntityId = entity.Id;
                }
                AppContext.EntityMentions.Add(mention);
                AppContext.SaveChanges();
                
                StartTasks(page.Translated, entity.Id, page.ScrapSourceId ?? 0, page.Id);
            }
            page.Status = EDataExtractionStatus.Finalized;
            ctx.SaveChanges();
        }

        private static void StartTasks(string content, long entityId, int scrapSourceId, Guid scrapedPageId)
        {
            var job = BackgroundJob.Enqueue(() => PersonalityService.StartExtractPersonality(content,entityId,scrapSourceId, scrapedPageId));
            BackgroundJob.ContinueWith(job,
                () => ToneService.StartExtractPersonality(content, entityId, scrapSourceId, scrapedPageId));
        }
    }
}