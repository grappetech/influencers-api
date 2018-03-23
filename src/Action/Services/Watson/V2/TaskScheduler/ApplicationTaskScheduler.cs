using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Action.Models;
using Action.Models.Core;
using Action.Models.Scrap;
using Action.Models.Watson;
using Action.Models.Watson.NLU;
using Action.Models.Watson.PersonalityInsights;
using Action.Services.Scrap.V2;
using Action.Services.Watson.V2.LanguageTanslator;
using Action.Services.Watson.V2.NaturalLanguageUnderstanding;
using Action.Services.Watson.V2.PersonalityInsights;
using IBM.WatsonDeveloperCloud.NaturalLanguageUnderstanding.v1.Model;
using Microsoft.EntityFrameworkCore;

namespace Action.Services.Watson.V2.TaskScheduler
{
    public class ApplicationTaskScheduler
    {
        public static async Task ProccessDataExtraction(ApplicationDbContext dbContext)
        {
            var credentials = dbContext.WatsonCredentials.AsNoTracking().ToList();
            var analysis = new List<AnalysisResults>();
            var scrappedPages = new List<ScrapedPage>();

            await EnhanceSources(dbContext, credentials, analysis, scrappedPages);
            await ExtractPersonality(dbContext, credentials, scrappedPages);
        }

        private static async Task ExtractPersonality(ApplicationDbContext dbContext,
            List<WatsonCredentials> credentials,
            List<ScrapedPage> scrapedPages)
        {
            var wltCredential = credentials.FirstOrDefault(x => x.Service == EWatsonServices.WatsonLanguageTranslator);
            var psiCredential = credentials.FirstOrDefault(x => x.Service == EWatsonServices.WatsonPersonalityInsights);
            scrapedPages.ForEach(async page =>
            {
                page.Translated = await new LanguageTranslatorService().ProccessTranslation(page.Text, "pt", "en",
                    wltCredential.UserName, wltCredential.Password);
                dbContext.Entry(page).State = EntityState.Modified;
            });

            await dbContext.SaveChangesAsync();

            var personalities = new List<PersonalityResult>();
            
            scrapedPages.ForEach(async page =>
            {
                var analysis = await new PersonalityInsightsService().ProccessText(page.Translated,
                    psiCredential.UserName, psiCredential.Password, psiCredential.Version);
                var result = PersonalityResult.Parse(analysis);
                result.ScrapedPageId = page.Id;
                personalities.Add(result);
            });

            var idx = 0;
            foreach (var personality in personalities)
            {
                dbContext.Entry(personality).State = EntityState.Added;
                if (idx == 30)
                {
                    await dbContext.SaveChangesAsync();
                    idx = 0;
                }
            }

            await dbContext.SaveChangesAsync();

        }

        private static async Task EnhanceSources(ApplicationDbContext dbContext, List<WatsonCredentials> credentials,
            List<AnalysisResults> nluResults, List<ScrapedPage> scrapedPages)
        {
            await Task.Run(() =>
            {
                var nluUser =
                    credentials.FirstOrDefault(x => x.Service == EWatsonServices.WatsonNaturalLanguageUnderstanding);

                List<LinkItem> items = new List<LinkItem>();
                var queue = dbContext.ScrapQueue.AsNoTracking().ToList();
                foreach (var scrapQueue in queue)
                {
                    items.AddRange(new Scrapper().ProccessTask(scrapQueue.Url, 1).GetAwaiter().GetResult());
                }

                var idx = 0;
                items.ForEach(async item =>
                {
                    try
                    {
                        var nluSvc = new NLUService();
                        var analisys = await nluSvc.ProccessUrl(item.Href, nluUser.UserName, nluUser.Password,
                            nluUser.Model, nluUser.Version);
                        var result = NLUResult.Parse(analisys);
                        var scrappedPage = new ScrapedPage
                        {
                            Date = DateTime.Today,
                            Id = Guid.NewGuid(),
                            Status = EDataExtractionStatus.Finalized,
                            Text = analisys.AnalyzedText
                        };

                        result.ScrapedPageId = scrappedPage.Id;
                        scrapedPages.Add(scrappedPage);
                        nluResults.Add(analisys);
                        dbContext.Entry(scrappedPage).State = EntityState.Added;
                        dbContext.Entry(result).State = EntityState.Added;

                        if (idx == 30)
                        {
                            await dbContext.SaveChangesAsync();
                            idx = 0;
                        }

                        idx++;
                    }
                    catch
                    {
                    }

                    await dbContext.SaveChangesAsync();
                });
            });
        }
    }
}