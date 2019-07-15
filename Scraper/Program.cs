using System.Resources;

namespace Scraper
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ResourceWriter rw = new ResourceWriter(@"..\..\Resources.resources"))
            {
                rw.AddResource("JsonPath" , @"C:\Users\zaalg\source\repos\PopularCelebrities\celebs.txt");
                rw.AddResource("Url", "https://www.imdb.com/list/ls052283250/");
            }
            //new CelebritiesReader()
            //    .ScrapPage("https://www.imdb.com/list/ls052283250/");
            while (true)
            {

            }
        }
    }
}
