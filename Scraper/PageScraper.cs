using Scraper.Interfaces;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Scraper
{
    public class PageScraper : IPageScraper
    {
        public async Task<string> GetHtmlAsync(string url)
        {
            string pageText = null;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "Mozilla/5.0");
                    httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
                    {
                        NoCache = true
                    };
                    Task<string> rawPageText = httpClient.GetStringAsync(url);
                    pageText = await rawPageText;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return pageText;
        }
    }
}
