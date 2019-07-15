using System.Collections.Generic;

namespace PopularCelebrities.DAL
{
    public interface IDbRepo<T>
    {
        bool DataFileExist();
        List<T> GetDataFromJsonFile(string objectName);
        void AddItemToJsonFile(T item);
        void UpdateItem(string proertyName, string propertyValue, T updatedItem);
        void RemoveItemFromJsonFile(string proertyName, string propertyValue);
        void SaveDataToJsonFile(List<T> data);
    }
}