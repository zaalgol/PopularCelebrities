using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Interfaces
{
    public interface IReader<T>
    {
        Task<List<T>> ScrapPage(string url);
        List<T> ParseDataFromRawPageTxt(string pageTxt);
        T ParseLine(HtmlNode node);
        HtmlNodeCollection GetContentSectionFromPage(string pageTxt);

    }
}
