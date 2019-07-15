using PopularCelebrities.DAL;
using PopularCelebrities.Models;
using Scraper;
using Scraper.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopularCelebrities.BL
{
    public class CelebsBl : ICelebsBl
    {
        private IDbRepo<Celebrity> dbRepo;

        private IReader<Celebrity> reader;

        private const string URL = "https://www.imdb.com/list/ls052283250/";

        public CelebsBl(IDbRepo<Celebrity> dbRepo, IReader<Celebrity> reader)
        {
            this.dbRepo = dbRepo;
            this.reader = reader;
        }

        public async Task<IEnumerable<Celebrity>> GetData()
        {
            if (dbRepo.DataFileExist())
            {
                return dbRepo.GetDataFromJsonFile(typeof(Celebrity).Name);
            }
            var initData = InitData();
            return await initData;
        }

        public IEnumerable<Celebrity> AddCelebrityToJsonFile(Celebrity item)
        {
            dbRepo.AddItemToJsonFile(item);
            return GetData().Result;
        }

        public IEnumerable<Celebrity> UpdateCelebrity(string name, Celebrity updatedItem)
        {
            dbRepo.UpdateItem(nameof(Celebrity.Name), name, updatedItem);
            return GetData().Result;
        }
        public IEnumerable<Celebrity> RemoveCelebrity(string celebName)
        {
            dbRepo.RemoveItemFromJsonFile(nameof(Celebrity.Name), celebName);
            return GetData().Result;
        }

        public async Task<IEnumerable<Celebrity>> InitData()
        {
            Task<List<Celebrity>> newData =
                reader.ScrapPage(URL);
            List<Celebrity> newDataResult = await newData;
            await Task.Factory.StartNew(() =>
             {
                 SaveDataToJsonFile(newDataResult);
             });
            return newDataResult;
        }

        private void SaveDataToJsonFile(List<Celebrity> data)
        {
            dbRepo.SaveDataToJsonFile(data);
        }
    }
}