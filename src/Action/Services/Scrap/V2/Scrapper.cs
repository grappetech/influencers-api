using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Action.Services.Scrap.V2
{
    public class Scrapper
    {
        public async Task<List<LinkItem>> ProccessTask(string pageUri, int depth)
        {
            var links = await ProccessPage(pageUri);
            return links;
        }

        private async Task<List<LinkItem>> ProccessPage(string pageUri)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(pageUri);
                var response = await client.GetAsync("");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                     return LinkFinder.Find(content);
                }
                return new List<LinkItem>();
            }
        }
        
        internal class ConcurrentItem
        {
            public string Key { get; set; }
            public string Link { get; set; }
            public string Depth { get; set; }
        }
    }
}