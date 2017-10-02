using System;
using System.Collections.Generic;
using System.Linq;
using Action.Models;
using Action.Models.Scrap;

namespace Action.Services.Scrap
{
    public class ScrapService
    {
        public static void ExecutarCrawler(Dictionary<string, KeyValuePair<string, string>> pFontes, ApplicationDbContext context)
        {
            List<ScrapedPage> lLista = new List<ScrapedPage>();
            foreach (var lItem in pFontes)
                lLista.AddRange(ScrapPages(lItem.Key, lItem.Value.Key, lItem.Value.Value, 1, 2));

            lLista.Where(x => x.Text.Length > 100).ToList().ForEach(x =>
            {
                context.ScrapedPages.Add(x);
            });
            context.SaveChanges();
        }

        private static IEnumerable<ScrapedPage> ScrapPages(string lItemKey, string valueKey, string valueValue, int i, int i1)
        {
            throw new NotImplementedException();
        }
    }
}