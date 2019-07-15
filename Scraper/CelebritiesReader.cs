using HtmlAgilityPack;
using PopularCelebrities.Models;
using Scraper.Interfaces;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Scraper
{
    public class CelebritiesReader
        : Reader<Celebrity>
    {
        private const string CELEB_TYPE_CLASS = "text-muted text-small";
        private const string CONTENT_AREA_REGEX =
            "//div[contains(@class, 'lister list detail sub-list')]";
        private const string CONTENT_LINES_REGEX =
            "//div[contains(@class, 'lister-item mode-detail')]";
        private const string DATE_REGEX =
            @"((January)|(February)|(March)|(April)|(May)|(June)|(July)|(August)|(September)|(October)|(November)|(December))\s([1-9]|([12][0-9])|(3[01])),\s\d\d\d\d";

        public CelebritiesReader(IPageScraper siteReader) : base(siteReader) { }

        public override HtmlNodeCollection GetContentSectionFromPage(string pageTxt)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(pageTxt);

            try
            {
                HtmlNodeCollection temp = htmlDocument.DocumentNode.SelectNodes(CONTENT_AREA_REGEX).First()
                      .SelectNodes(CONTENT_LINES_REGEX);
            }
            catch (Exception ex)
            {

                var s = ex.Message;
            }
            return htmlDocument.DocumentNode.SelectNodes(CONTENT_AREA_REGEX).First()
              .SelectNodes(CONTENT_LINES_REGEX);

        }

        public override Celebrity ParseLine(HtmlNode celebrityNode)
        {
            Celebrity celebrity = new Celebrity();

            celebrity.PictureUrl = celebrityNode.Descendants("img").
                Select(e => e.GetAttributeValue("src", null))
                .Where(s => !String.IsNullOrEmpty(s))
                .First();

            celebrity.Name = celebrityNode.Descendants("img").
               Select(e => e.GetAttributeValue("alt", null))
               .Where(s => !String.IsNullOrEmpty(s))
               .First();

            celebrity.CelebrityType = celebrityNode.ChildNodes.Descendants()
                .Where(n => n.Attributes["class"] != null
                    && n.Attributes["class"].Value == CELEB_TYPE_CLASS)
                .First()
                .FirstChild
                .InnerHtml
                .Trim();

            SetGender(celebrityNode, celebrity);
            SetBirthDate(celebrityNode, celebrity);
            return celebrity;
        }

        private void SetGender(HtmlNode celebrityNode, Celebrity celebrity)
        {
            if (celebrity.CelebrityType == "Actor")
                celebrity.IsMale = true;
            else if (celebrity.CelebrityType != "Actress")
            {
                string description = celebrityNode.ChildNodes[3].ChildNodes[5].InnerText;
                var values = new[] { " her ", " she " };
                if (!values.Any(description.ToLower().Contains))
                    celebrity.IsMale = true;
            }
        }

        private void SetBirthDate(HtmlNode celebrityNode, Celebrity celebrity)
        {
            string description = celebrityNode.ChildNodes[3].ChildNodes[5].InnerText;
            if (description.Contains("born"))
            {
                MatchCollection matchCollection = Regex.Matches(description,
                DATE_REGEX);
                foreach (Match date in matchCollection)
                {
                    celebrity.BirthDate = date.Value;
                    return;
                }
            }
            celebrity.BirthDate = "Unknown";
        }
    }
}
