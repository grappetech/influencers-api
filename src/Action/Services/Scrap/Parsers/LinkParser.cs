using System.Collections.Generic;

namespace Action.Services.Scrap.Parsers
{
    /// <summary>
    ///     Link parser class.
    /// </summary>
    public class LinkParser
    {
        #region Constants

        private const string _LINK_REGEX =
                @"(http|ftp|https)://([\w_-]+(?:(?:\.[\w_-]+)+))([\w.,@?^=%&:/~+#-]*[\w@?^=%&/~+#-])?"
            ; //"href=\"[a-zA-Z./:&\\d_-]+\"";

        #endregion

        #region Constructor

        #endregion

        /// <summary>
        ///     Parses a page looking for links.
        /// </summary>
        /// <param name="page">The page whose text is to be parsed.</param>
        /// <param name="sourceUrl">The source url of the page.</param>
        /// <param name="pLinks"></param>
        public void ParseLinks(Page page, string sourceUrl, List<string> pLinks)
        {
            //MatchCollection matches = Regex.Matches(page.Text, _LINK_REGEX);

            //for (int i = 0; i <= matches.Count - 1; i++)]
            foreach (var lLink in pLinks)
            {
                var foundHref = lLink;
                //Match anchorMatch = matches[i];

                //if (anchorMatch.Value == String.Empty)
                //{
                //    BadUrls.Add("Blank url value on page " + sourceUrl);
                //    continue;
                //}

                //string foundHref = anchorMatch.Value;
                //string foundHref = null;
                //try
                //{
                //    foundHref = anchorMatch.Value.Replace("href=\"", "");
                //    foundHref = foundHref.Substring(0, foundHref.IndexOf("\""));
                //}
                //catch (Exception exc)
                //{
                //    Exceptions.Add("Error parsing matched href: " + exc.Message);
                //}


                if (!GoodUrls.Contains(foundHref))
                    if (foundHref != "/")
                        if (IsExternalUrl(foundHref) && IsAWebPage(foundHref))
                        {
                            _externalUrls.Add(foundHref);
                        }
                        else if (!IsAWebPage(foundHref))
                        {
                            foundHref = Scraper.FixPath(sourceUrl, foundHref);
                            _otherUrls.Add(foundHref);
                        }
                        else
                        {
                            GoodUrls.Add(foundHref);
                        }
            }
        }


        /// <summary>
        ///     Is the url to an external site?
        /// </summary>
        /// <param name="url">The url whose externality of destination is in question.</param>
        /// <returns>Boolean indicating whether or not the url is to an external destination.</returns>
        private static bool IsExternalUrl(string url)
        {
            if (url.Length > 8 && (url.Substring(0, 7) == "http://" || url.Substring(0, 3) == "www" ||
                                   url.Substring(0, 7) == "https://"))
                return true;

            return false;
        }

        /// <summary>
        ///     Is the value of the href pointing to a web page?
        /// </summary>
        /// <param name="foundHref">The value of the href that needs to be interogated.</param>
        /// <returns>Boolen </returns>
        private static bool IsAWebPage(string foundHref)
        {
            if (foundHref.IndexOf("javascript:") == 0)
                return false;

            var extension = foundHref.Substring(foundHref.LastIndexOf(".") + 1,
                foundHref.Length - foundHref.LastIndexOf(".") - 1);
            switch (extension.ToLower())
            {
                case "jpg":
                case "css":
                case "png":
                case "js":
                case "pdf":
                    return false;
                default:
                    return true;
            }
        }

        #region Private Instance Fields

        /// <summary>
        ///     Good Urls
        /// </summary>
        private List<string> _goodUrls = new List<string>();

        /// <summary>
        ///     Bad Urls.
        /// </summary>
        private List<string> _badUrls = new List<string>();

        /// <summary>
        ///     Other Urls
        /// </summary>
        private List<string> _otherUrls = new List<string>();

        /// <summary>
        ///     External Urls
        /// </summary>
        private List<string> _externalUrls = new List<string>();

        /// <summary>
        ///     Exceptions
        /// </summary>
        private List<string> _exceptions = new List<string>();

        #endregion

        #region Public Properties

        /// <summary>
        ///     Good Urls.
        /// </summary>
        public List<string> GoodUrls
        {
            get => _goodUrls;
            set => _goodUrls = value;
        }

        /// <summary>
        ///     Bad Urls
        /// </summary>
        public List<string> BadUrls
        {
            get => _badUrls;
            set => _badUrls = value;
        }

        /// <summary>
        ///     Other Urls
        /// </summary>
        public List<string> OtherUrls
        {
            get => _otherUrls;
            set => _otherUrls = value;
        }

        /// <summary>
        ///     External Urls.
        /// </summary>
        public List<string> ExternalUrls
        {
            get => _externalUrls;
            set => _externalUrls = value;
        }

        /// <summary>
        ///     Exceptions
        /// </summary>
        public List<string> Exceptions
        {
            get => _exceptions;
            set => _exceptions = value;
        }

        #endregion
    }
}