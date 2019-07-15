using System.Threading.Tasks;

namespace Scraper.Interfaces
{
    public interface IPageScraper
    {
        Task<string> GetHtmlAsync(string url);
    }
}
