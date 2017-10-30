using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Action.Models;
using Action.Models.Scrap;
using Action.Models.Watson;
using Action.Services.Scrap.Repositories;
using Action.Services.Watson.LanguageTranslator;
using Microsoft.EntityFrameworkCore;

namespace Action.Services.Scrap
{
    public class ScrapService
    {
        public void StartScraper()
        {
            var lLista = new List<ScrapedPage>();
            
            var items = new ScrapperContext().ScrapSources.Where(x => x.PageStatus == EPageStatus.Enabled).ToList().AsParallel();
            Parallel.ForEach(items, item =>
            {
                using (var ctx = new ScrapperContext())
                {
                    try
                    {
                        Scraper.ScrapedPageObject(item.Url, item.StarTag, item.EndTag, item.Dept, item.Limit)
                            .Where(x => x.Text.Length > 100).ToList()
                            .ForEach(x =>
                            {
                                if (!ctx.ScrapedPages.Any(p =>
                                    p.ScrapSourceId == x.ScrapSourceId && p.Hash.Equals(x.Hash)))
                                {
                                    x.ScrapSourceId = item.Id;
                                    x.Translated = WatsonTranslateService.Instance.Translate(x.Text);
                                    ctx.Entry(x).State = EntityState.Added;
                                    ctx.SaveChanges();
                                }
                            });
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        ctx.ScrapSources.Find(item.Id).PageStatus = EPageStatus.Error;
                        ctx.SaveChanges();
                    }
                }
            });
        }
    }
}