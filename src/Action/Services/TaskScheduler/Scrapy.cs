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
        internal static InstagramData GetInstagramMetrics(string page)
        {

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(page);
            var strm = hc.GetStringAsync("").Result;
            
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(strm);
            var insta = new InstagramData();
            insta.Posts = Convert.ToInt32(document.DocumentNode?
                                               .SelectNodes("//header/section/ul/li/span/span")?[0]?.InnerText?.Replace(".","") ?? "0");

            insta.Followers = Convert.ToInt32(document.DocumentNode?
                                                  .SelectNodes("//header/section/ul/li/a/span")?[0]?.Attributes["title"]?.Value?.Replace(".","") ?? "0");
            
            insta.Following = Convert.ToInt32(document.DocumentNode?
                                                  .SelectNodes("//header/section/ul/li/a/span")?[1]?.InnerText?.Replace(".","") ?? "0");
            return insta;
        }
        
        internal static TwitterData GetTwitterMetrics(string page)
        {

            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(page);
            var strm = hc.GetStringAsync("").Result;
            
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(strm);
            var tweet = new TwitterData();
            tweet.Tweets = Convert.ToInt32(document.DocumentNode?
                                           .SelectNodes("//*[@class=\"ProfileNav-value\"]")?[0]?.Attributes["data-count"]?.Value ?? "0");

            tweet.Followers = Convert.ToInt32(document.DocumentNode?
                                                .SelectNodes("//*[@class=\"ProfileNav-value\"]")?[2]?.Attributes["data-count"]?.Value ?? "0");
            
            tweet.Following = Convert.ToInt32(document.DocumentNode?
                                                  .SelectNodes("//*[@class=\"ProfileNav-value\"]")?[1]?.Attributes["data-count"]?.Value ?? "0");
            
            
            tweet.Favorites = Convert.ToInt32(document.DocumentNode?
                                                  .SelectNodes("//*[@class=\"ProfileNav-value\"]")?[3]?.Attributes["data-count"]?.Value ?? "0");
            return tweet;
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
        
        internal struct TwitterData
        {
            internal int Tweets;
            internal int Followers;
            internal int Following;
            internal int Favorites;

        }
        
        internal struct InstagramData
        {
            internal int Posts;
            internal int Followers;
            internal int Following;

        }
    }
}