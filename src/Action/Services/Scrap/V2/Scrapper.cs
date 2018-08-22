using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using HtmlAgilityPack;

namespace Action.Services.Scrap.V2
{
    public class Scrapper
    {
        public async Task<List<LinkItem>> ProccessLinkTask(string pageUri, int depth)
        {
            var links = new Dictionary<int, List<LinkItem>>();


            HtmlWeb web = new HtmlWeb();
            var doc = await web.LoadFromWebAsync(pageUri, Encoding.UTF8);
            var nodes = doc.DocumentNode.SelectNodes("//a");
            var zlinks = new List<LinkItem>();
            foreach (var node in nodes)
            {
                zlinks.Add(new LinkItem
                {
                    Href = node.Attributes["href"]?.Value,
                    Text = node.InnerText
                });
            }

            var zindex = 1;
            links.Add(1, zlinks);

            while (zindex < depth)
            {
                zindex++;
                foreach (var xlinks in links[zindex -1])
                {
                    doc = await web.LoadFromWebAsync(xlinks.Href, Encoding.UTF8);
                    nodes = doc.DocumentNode.SelectNodes("a");
                    var ylinks = new List<LinkItem>();
                    foreach (var node in nodes)
                    {
                        ylinks.Add(new LinkItem
                        {
                            Href = node.Attributes["href"]?.Value,
                            Text = node.InnerText
                        });
                    }
                    links.Add(zindex, ylinks);
                }

            }
            return links.SelectMany(x => x.Value).ToList();
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
    }
}