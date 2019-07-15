using PopularCelebrities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PopularCelebrities.BL
{
    public interface ICelebsBl
    {
        IEnumerable<Celebrity> AddCelebrityToJsonFile(Celebrity item);
        IEnumerable<Celebrity> UpdateCelebrity(string name, Celebrity updatedItem);
        Task<IEnumerable<Celebrity>> GetData();
        Task<IEnumerable<Celebrity>> InitData();
        IEnumerable<Celebrity> RemoveCelebrity(string celebName);
    }
}