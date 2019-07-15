using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PopularCelebrities.DAL
{
    public class DbRepo<T> : IDbRepo<T>
    {
        protected const string JsonPath = @"C:\Users\zaalg\source\repos\PopularCelebrities\celebs.txt";

        public bool DataFileExist()
        {
            return File.Exists(JsonPath);
        }

        public List<T> GetDataFromJsonFile(string objectName)
        {
            string json = File.ReadAllText(JsonPath);
            return JsonConvert.DeserializeObject<List<T>>(json);
        }

        public void SaveDataToJsonFile(List<T> data)
        {
            string result = JsonConvert.SerializeObject(data, Formatting.None);
            File.WriteAllText(JsonPath, result);
        }

        public void UpdateItem(string proertyName, string propertyValue, T updatedItem)
        {
            RemoveItemFromJsonFile(proertyName, propertyValue);
            AddItemToJsonFile(updatedItem);
        }

        public void RemoveItemFromJsonFile(string proertyName, string propertyValue)
        {
            var json = File.ReadAllText(JsonPath);
            try
            {
                var jObject = JArray.Parse(json);

                var itemToDeleted =
                    jObject.FirstOrDefault(obj => obj[proertyName].Value<string>() == propertyValue);

                jObject.Remove(itemToDeleted);

                string output =
                    JsonConvert.SerializeObject(jObject, Formatting.Indented);
                File.WriteAllText(JsonPath, output);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public void AddItemToJsonFile(T item)
        {
            var json = File.ReadAllText(JsonPath);
            try
            {
                var jObject = JArray.Parse(json);
                jObject.Add(JToken.FromObject(item));

                string output =
                    JsonConvert.SerializeObject(jObject, Formatting.Indented);
                File.WriteAllText(JsonPath, output);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }
    }
}