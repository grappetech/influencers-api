using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using Action.Models.Scrap;
using Action.Services.Scrap.Interfaces;
using Action.Services.Scrap.Logging;
using Action.Services.Scrap.Parsers;
using Action.Services.Scrap.Repositories;

namespace Action.Services.Scrap
{
    /// <summary>
    ///     Main Scraper class.
    /// </summary>
    public class Scraper
    {
        /// <summary>
        ///     Initializing the reporting process.
        /// </summary>
        public void InitilizeCreateReport()
        {
            var stringBuilder = Reporting.CreateReport(_externalUrlRepository, _otherUrlRepository,
                _failedUrlRepository, _currentPageUrlRepository, _pages, _exceptions);

            Logging.Logging.WriteReportToDisk(stringBuilder.ToString());

            // System.Diagnostics.Process.Start(ConfigurationManager.AppSettings["logTextFileName"].ToString());

            Environment.Exit(0);
        }

        /// <summary>
        ///     Crawls a page.
        /// </summary>
        /// <param name="url">The url to crawl.</param>
        private static void ScrapPage(string url, string pTagInicial, string pTagFinal, int pNivel, int pLimite,
            ref List<ScrapedPage> pPaginas)
        {
            if (!PageHasBeenCrawled(url))
            {
                var lLinks = new List<string>();
                var htmlText = GetWebText(url, pTagInicial, pTagFinal, pNivel, lLinks);

                var linkParser = new LinkParser();

                var page = new Page();
                page.Text = htmlText;
                page.Url = url;

                _pages.Add(page);

                linkParser.ParseLinks(page, url, lLinks);

                pPaginas.Add(new ScrapedPage {Date = DateTime.Today, Text = htmlText, Url = url});

                //Crawl all the links found on the page.
                foreach (var link in lLinks)
                {
                    var formattedLink = link;
                    try
                    {
                        formattedLink = FixPath(url, formattedLink);

                        if (formattedLink != string.Empty)
                        {
                            var lLimite = pLimite - pNivel;
                            if (lLimite <= 0)
                                return;
                            ScrapPage(formattedLink, pTagInicial, pTagFinal, pNivel + 1, pLimite, ref pPaginas);
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        /// <summary>
        ///     Fixes a path. Makes sure it is a fully functional absolute url.
        /// </summary>
        /// <param name="originatingUrl">The url that the link was found in.</param>
        /// <param name="link">The link to be fixed up.</param>
        /// <returns>A fixed url that is fit to be fetched.</returns>
        public static string FixPath(string originatingUrl, string link)
        {
            var formattedLink = string.Empty;

            if (link.IndexOf("../") > -1)
                formattedLink = ResolveRelativePaths(link, originatingUrl);
            else if (originatingUrl.IndexOf(originatingUrl) > -1
                     && link.IndexOf(originatingUrl) == -1 && !link.Contains("http:"))
                formattedLink = originatingUrl.Substring(0, originatingUrl.LastIndexOf("/") + 1) + link;
            else if (link.IndexOf(originatingUrl) == -1)
                formattedLink = link; //ConfigurationManager.AppSettings["url"].ToString() + 

            return formattedLink;
        }

        /// <summary>
        ///     Needed a method to turn a relative path into an absolute path. And this seems to work.
        /// </summary>
        /// <param name="relativeUrl">The relative url.</param>
        /// <param name="originatingUrl">The url that contained the relative url.</param>
        /// <returns>A url that was relative but is now absolute.</returns>
        public static string ResolveRelativePaths(string relativeUrl, string originatingUrl)
        {
            var resolvedUrl = string.Empty;

            var relativeUrlArray = relativeUrl.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
            var originatingUrlElements = originatingUrl.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
            var indexOfFirstNonRelativePathElement = 0;
            for (var i = 0; i <= relativeUrlArray.Length - 1; i++)
                if (relativeUrlArray[i] != "..")
                {
                    indexOfFirstNonRelativePathElement = i;
                    break;
                }

            var countOfOriginatingUrlElementsToUse =
                originatingUrlElements.Length - indexOfFirstNonRelativePathElement - 1;
            for (var i = 0; i <= countOfOriginatingUrlElementsToUse - 1; i++)
                if (originatingUrlElements[i] == "http:" || originatingUrlElements[i] == "https:")
                    resolvedUrl += originatingUrlElements[i] + "//";
                else
                    resolvedUrl += originatingUrlElements[i] + "/";

            for (var i = 0; i <= relativeUrlArray.Length - 1; i++)
                if (i >= indexOfFirstNonRelativePathElement)
                {
                    resolvedUrl += relativeUrlArray[i];

                    if (i < relativeUrlArray.Length - 1)
                        resolvedUrl += "/";
                }

            return resolvedUrl;
        }

        /// <summary>
        ///     Checks to see if the page has been crawled.
        /// </summary>
        /// <param name="url">The url that has potentially been crawled.</param>
        /// <returns>Boolean indicating whether or not the page has been crawled.</returns>
        public static bool PageHasBeenCrawled(string url)
        {
            foreach (var page in _pages)
                if (page.Url == url)
                    return true;

            return false;
        }

        /// <summary>
        ///     Merges a two lists of strings.
        /// </summary>
        /// <param name="targetList">The list into which to merge.</param>
        /// <param name="sourceList">The list whose values need to be merged.</param>
        private static void AddRangeButNoDuplicates(Dictionary<string, string> targetList, List<string> sourceList,
            string pText)
        {
            foreach (var str in sourceList)
                if (!targetList.Keys.Contains(str))
                    targetList.Add(str, pText);
        }

        /// <summary>
        ///     Gets the response text for a given url.
        /// </summary>
        /// <param name="url">The url whose text needs to be fetched.</param>
        /// <returns>The text of the response.</returns>
        public static string GetWebText(string url, string pTagInicial, string pTagFinal, int pNivel,
            List<string> pLinks)
        {
            //WebRequest request = WebRequest.Create(url);

            var htmlText = "";
            try
            {
                using (var requestClient = new HttpClient())
                {
                    requestClient.Timeout = TimeSpan.FromMilliseconds(100000);
                    var teste = requestClient.GetAsync(new Uri(url)).Result;
                    var stream = teste.Content.ReadAsStreamAsync().Result;
                    var reader = new StreamReader(stream);

                    htmlText = reader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                return "";
            }

            var matches = Regex.Matches(htmlText,
                @"(http|ftp|https)://([\w_-]+(?:(?:\.[\w_-]+)+))([\w.,@?^=%&:/~+#-]*[\w@?^=%&/~+#-])?");

            if (pNivel == 1)
                for (var i = 0; i <= matches.Count - 1; i++)
                {
                    var anchorMatch = matches[i];
                    var lUrl = FixPath(anchorMatch.Value, "");

                    if (!lUrl.Equals(" ") && lUrl.Length > 0)
                        pLinks.Add(anchorMatch.Value);
                }

            var regularExpressionPattern1 =
                string.Format(@"<{0}(.*?)<\/{1}>", pTagInicial, pTagFinal); //@"<body(.*?)<\/body>";
            var texto = GetInnerText(htmlText, regularExpressionPattern1);

            if (texto.Length > 0)
                texto = texto.Substring(0, texto.Length - 1);

            if (pNivel > 1)
                for (var i = 0; i <= matches.Count - 1; i++)
                {
                    var anchorMatch = matches[i];
                    var lUrl = FixPath(anchorMatch.Value, "");

                    if (!lUrl.Equals(" ") && lUrl.Length > 0)
                        pLinks.Add(anchorMatch.Value);
                }
            pLinks = pLinks.Distinct().ToList();

            GetInnerText(texto, @"<!--(.*?)-->").Split('§').ToList().ForEach(x =>
            {
                if (x != string.Empty) texto = texto.Replace(x, string.Empty);
            });

            GetInnerText(texto, @"<script(.*?)<\/script>").Split('§').ToList().ForEach(x =>
            {
                if (x != string.Empty) texto = texto.Replace(x, string.Empty);
            });

            GetInnerText(texto, @"<style(.*?)<\/style>").Split('§').ToList().ForEach(x =>
            {
                if (x != string.Empty) texto = texto.Replace(x, string.Empty);
            });

            GetInnerText(texto, @"<div(.*?)\>").Split('§').ToList().ForEach(x =>
            {
                if (x != string.Empty) texto = texto.Replace(x, string.Empty);
            });

            GetInnerText(texto, @"<img(.*?)\>").Split('§').ToList().ForEach(x =>
            {
                if (x != string.Empty) texto = texto.Replace(x, string.Empty);
            });

            GetInnerText(texto, @"<iframe(.*?)\>").Split('§').ToList().ForEach(x =>
            {
                if (x != string.Empty) texto = texto.Replace(x, string.Empty);
            });

            texto = texto.Replace("§", string.Empty);

            htmlText = Regex.Replace(texto, "<.*?>", string.Empty);
            htmlText = htmlText.Replace("\n", " ");
            htmlText = htmlText.Trim();

            return WebUtility.HtmlDecode(htmlText);
        }

        private static string GetInnerText(string htmlText, string regularExpressionPattern1)
        {
            var regex = new Regex(regularExpressionPattern1, RegexOptions.Singleline);
            var collection = regex.Matches(htmlText);

            var i = 1;
            var texto = "";
            while (i <= collection.Count)
            {
                texto += collection[i - 1].Value + "§";
                i++;
            }

            return texto;
        }

        public static string ScraperPageString(string pUrl, string pTagInicial, string pTagFinal, int pNivel,
            int pLimite)
        {
            var lListScraperObject = new List<ScrapedPage>();

            ScrapPage(pUrl, pTagInicial, pTagFinal, pNivel, pLimite, ref lListScraperObject);


            var lTexto = "";


            foreach (var lItem in lListScraperObject)
                lTexto += lItem.Text;

            return lTexto;
        }

        public static List<ScrapedPage> ScrapedPageObject(string pUrl, string pTagInicial, string pTagFinal, int pNivel,
            int pLimite)
        {
            var lListScraperObject = new List<ScrapedPage>();

            ScrapPage(pUrl, pTagInicial, pTagFinal, pNivel, pLimite, ref lListScraperObject);

            return lListScraperObject;
        }


        /// <summary>
        ///     external URL repository
        /// </summary>
        private readonly IRepository _externalUrlRepository = new ExternalUrlRepository();

        /// <summary>
        ///     Other URL repository
        /// </summary>
        private readonly IRepository _otherUrlRepository = new OtherUrlRepository();

        /// <summary>
        ///     Failed URL repository
        /// </summary>
        private readonly IRepository _failedUrlRepository = new FailedUrlRepository();

        /// <summary>
        ///     Current page URL repository
        /// </summary>
        private readonly IRepository _currentPageUrlRepository = new CurrentPageUrlRepository();

        /// <summary>
        ///     List of Pages.
        /// </summary>
        private static readonly List<Page> _pages = new List<Page>();

        /// <summary>
        ///     List of exceptions.
        /// </summary>
        private static readonly List<string> _exceptions = new List<string>();

        /// <summary>
        ///     Is current page or not
        /// </summary>
        /// private bool isCurrentPage = true;
        /// <summary>
        ///     Constructor of the class.
        /// </summary>
        /// <summary>
        ///     Initializing the crawling process.
        /// </summary>
        public void InitializeCrawl()
        {
            //int lLimite = 3;
            //CrawlPage(ConfigurationManager.AppSettings["url"], 1, ref lLimite);
        }
    }
}