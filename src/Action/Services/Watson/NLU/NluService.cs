using System;
using System.Linq;
using System.Threading.Tasks;
using Action.Models;
using Action.Models.Scrap;
using Action.Services.Scrap.Repositories;
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

        public static Task StartExtraction()
        {
            var pages = new ScrapperContext().ScrapedPages.Where(x=>x.Status == EDataExtractionStatus.Waiting).ToList().AsParallel();
            return Task.Run(() =>
            {
                Parallel.ForEach(pages, p => { NluService.Instance.ExtractData(p.Id);});
                
            });
        }


        public void ExtractData(Guid pageId)
        {
            var service = new NaturalLanguageUnderstandingService
            {
                Password = "Of5W0mIzzGao",
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
                        Model = "10:d92acd8c-b623-4b3b-9802-a64068d95315"
                    },
                    Relations = new RelationsOptions
                    {
                        Model = "10:d92acd8c-b623-4b3b-9802-a64068d95315"
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

            foreach (var item in analysisResults.Entities.Where(e => e.Type.ToLower().Equals("marca")))
            {
                var mention = new EntityMentions {ScrapedPageId = pageId, ScrapSourceId = page.ScrapSourceId ?? 0};
                if (!AppContext.Entities.Any(x => x.Alias.Contains(item.Text)))
                {
                    var entity = new Models.Watson.Entity
                    {
                        Alias = item.Text,
                        Name = item.Text,
                        CategoryId = ECategory.Brand,
                        Date = DateTime.UtcNow
                    };

                    AppContext.Entities.Add(entity);
                    AppContext.SaveChanges();
                    mention.EntityId = entity.Id;
                }
                else
                {
                    var entity = AppContext.Entities.FirstOrDefault(x => x.Alias.Equals(item.Text));
                    entity.Date = DateTime.UtcNow;
                    AppContext.SaveChanges();
                    mention.EntityId = entity.Id;
                }
                AppContext.EntityMentions.Add(mention);
                AppContext.SaveChanges();
            }
            page.Status = EDataExtractionStatus.Finalized;
            ctx.SaveChanges();
        }
    }
}