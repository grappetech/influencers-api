using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Action.Services.TaskScheduler
{
    public class Scrapy
    {
        

        private static  string GetResponse(string url)
        {
            using (var _HttpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
                {
                    request.Headers.TryAddWithoutValidation("Accept",
                        "text/html,application/xhtml+xml,application/xml");
                    request.Headers.TryAddWithoutValidation("User-Agent",
                        "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                    using (var response = _HttpClient.SendAsync(request).GetAwaiter().GetResult())
                    {
                        response.EnsureSuccessStatusCode();
                        return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    }
                }
            }
        }
        
        internal static void GetTwitterMetrics(string page, out int tweets, out int followers, out int following, out int favorites)
        {

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(page);
            var strm = hc.GetStringAsync("").Result;
            
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(strm);
             tweets = Convert.ToInt32(document.DocumentNode?
                                           .SelectNodes("//*[@class=\"ProfileNav-item ProfileNav-item--tweets is-active\"]")?.FirstOrDefault()?
                                           .SelectNodes("//*[@class=\"ProfileNav-value\"]")?.FirstOrDefault()?.Attributes["data-count"]?.Value ?? "0");

             followers = Convert.ToInt32(document.DocumentNode?
                                                .SelectNodes("//*[@class=\"ProfileNav-item ProfileNav-item--followers\"]")?.FirstOrDefault()?
                                                .SelectNodes("//*[@class=\"ProfileNav-value\"]")?.FirstOrDefault()?.Attributes["data-count"]?.Value ?? "0");
            
             following = Convert.ToInt32(document.DocumentNode?
                                                .SelectNodes("//*[@class=\"ProfileNav-item ProfileNav-item--following\"]")?.FirstOrDefault()?
                                               .SelectNodes("//*[@class=\"ProfileNav-value\"]")?.FirstOrDefault()?.Attributes["data-count"]?.Value ?? "0");
            
            
             favorites = Convert.ToInt32(document.DocumentNode?
                                                .SelectNodes("//*[@class=\"ProfileNav-item ProfileNav-item--favorites\"]")?.FirstOrDefault()?
                                                .SelectNodes("//*[@class=\"ProfileNav-value\"]")?.FirstOrDefault()?.Attributes["data-count"]?.Value ?? "0");
            
        }


        internal static void GetFacebookContent(string page)
        {
            HtmlWeb webpage = new HtmlWeb();
            var document = webpage.Load(page);
            var comments = document.DocumentNode.SelectNodes("//*[class=\"text_exposed_root\"]/p");
            foreach (var comment in comments)
            {
                Console.WriteLine(comment.InnerText);
            }
        }

        internal static void GetFacebookMetrics(string page)
        {
            HtmlWeb webpage = new HtmlWeb();
            var document = webpage.Load(page);
            var metrics = document.DocumentNode.SelectNodes("//div[@class=\"_4bl7 _3xoj\"]");
            var totalSeguidores = metrics.FirstOrDefault()?.Descendants("div")?.FirstOrDefault()?.InnerText;
            Console.WriteLine("Total de Seguidores: " + totalSeguidores);
        }
    }
}