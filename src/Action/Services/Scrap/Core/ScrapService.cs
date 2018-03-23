using System;
using System.Linq;
using System.Threading.Tasks;
using Action.Models;
using Action.Models.Scrap;
using Microsoft.EntityFrameworkCore;

namespace Action.Services.Scrap.Core
{
    public class ScrapService
    {
        public void StartScraperV2(ApplicationDbContext ctx)
        {
            var date = DateTime.UtcNow.Date;
            var queue = ctx.ScrapQueue.Where(x => !x.Completed).AsNoTracking().ToList();
            if (!queue.Any())
            {
                foreach (var page in ctx.ScrapSources.Where(p => p.Url.ToLower().Contains("{{entity}}"))
                    .AsNoTracking().ToList())
                {
                    foreach (var entity in ctx.Entities)
                    {
                        ctx.ScrapQueue.Add(new ScrapQueue
                        {
                            Url = page.Url.ToLower().Replace("{{entity}}", entity.Name),
                            EnqueueDateTime = date
                        });
                    }
                }

                foreach (var page in ctx.ScrapSources
                    .Where(p => !p.Url.ToLower().Contains("{{entity}}")).AsNoTracking().ToList())
                {
                    ctx.ScrapQueue.Add(new ScrapQueue
                    {
                        Url = page.Url.ToLower(),
                        EnqueueDateTime = date
                    });
                }
            }

            ctx.SaveChanges();
        }
    }
}

}