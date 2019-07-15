using HtmlAgilityPack;
using Newtonsoft.Json;
using Scraper.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scraper
{
    public abstract class Reader<T> : IReader<T>
    {
        IPageScraper siteReader;
        public Reader(IPageScraper siteReader)
        {
            this.siteReader = siteReader;
        }

        public async Task<List<T>> ScrapPage(string url)
        {
            List<T> data;
            try
            {
                IPageScraper siteReader = new PageScraper();
                Task<string> rawPageText = siteReader.GetHtmlAsync(url);
                Console.WriteLine("13");
                string rawPageTextResult = await rawPageText;
                Console.WriteLine("14");
                data = ParseDataFromRawPageTxt(rawPageTextResult);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
            return data;
        }

        public List<T> ParseDataFromRawPageTxt(string pageTxt)
        {
            HtmlNodeCollection contentLines = GetContentSectionFromPage(pageTxt);
            List<T> data = new List<T>();
            foreach (HtmlNode CelebrityNode in contentLines)
            {
                data.Add(ParseLine(CelebrityNode));
            }
            string result = JsonConvert.SerializeObject(data, Formatting.None);
            return data;
        }

        public abstract T ParseLine(HtmlNode node);

        public abstract HtmlNodeCollection GetContentSectionFromPage(string pageTxt);

    }
}
