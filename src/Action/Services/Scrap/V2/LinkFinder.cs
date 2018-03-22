using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Action.Services.Scrap.V2
{
    static class LinkFinder
    {
        public static  List<LinkItem> Find(string file)
        {
            List<LinkItem> list = new List<LinkItem>();

            var m1 = FindAllMatches(file);
            LoadMatches(m1, list);
            return list;
        }
        

        private static MatchCollection FindAllMatches(string file)
        {
            MatchCollection m1 = Regex.Matches(file, @"(<a.*?>.*?</a>)",
                RegexOptions.Singleline);
            return m1;
        }

        private static void LoadMatches(MatchCollection m1, List<LinkItem> list)
        {
            foreach (Match m in m1)
            {
                var i = new LinkItem();
                var value = m.Groups[1].Value;
                ExtractHrefFromLinks(value, ref i);
                ExtractPagesUrl(list, value, ref i);
            }
        }

        private static void ExtractHrefFromLinks(string value, ref LinkItem lnk)
        {
            Match m2 = Regex.Match(value, @"href=\""(.*?)\""",
                RegexOptions.Singleline);
            if (m2.Success)
            {
                lnk.Href = m2.Groups[1].Value;
            }
        }
        

        private static void ExtractPagesUrl(List<LinkItem> list, string value, ref LinkItem lnk)
        {
            var t = Regex.Replace(value, @"\s*<.*?>\s*", String.Empty, RegexOptions.Singleline);
            lnk.Text = t;
            list.Add(lnk);
        }
    }
}