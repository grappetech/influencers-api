using System.Collections.Generic;
using System.Linq;
using Action.Models.Scrap;
using Action.Services.Scrap.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Action.Services.Scrap
{
    public class ScrapService
    {
   
        public void StartScraper()
        {
            using (var ctx = new ScrapperContext())
            {
                List<ScrapedPage> lLista = new List<ScrapedPage>();
                foreach (ScrapSource lItem in ctx.ScrapSources.ToList())
                {
                    Scraper.ScrapedPageObject(lItem.Url, lItem.StarTag, lItem.EndTag, lItem.Dept, lItem.Limit)
                        .Where(x => x.Text.Length > 100).ToList()
                        .ForEach(x =>{
                        x.ScrapSourceId = lItem.Id;
                        ctx.Entry(x).State = EntityState.Added;
                        ctx.SaveChanges();
                    });
                }
            }
        }
    }
}