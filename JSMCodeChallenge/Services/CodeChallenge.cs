using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using JSMCodeChallenge.Models;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Text.Json;

namespace JSMCodeChallenge.Services
{
    public class CodeChallenge
    {
        private static readonly HttpClient client = new HttpClient();

        private static async Task<List<User>> LoadDataFromCSV() {
            HttpResponseMessage response = await client.GetAsync("https://storage.googleapis.com/juntossomosmais-code-challenge/input-backend.csv");
            Stream stream = await response.Content.ReadAsStreamAsync();
            using (StreamReader streamReader = new StreamReader(stream)) {
                CsvReader reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
                reader.Configuration.RegisterClassMap<User.CSVMap>();
                return reader.GetRecords<User>().ToList(); 
            }
        }

        private class JsonRootObject {
            public List<User> results { get; set; }
        }

        private static async Task<List<User>> LoadDataFromJSON() {
            HttpResponseMessage response = await client.GetAsync("https://storage.googleapis.com/juntossomosmais-code-challenge/input-backend.json");
            Stream stream = await response.Content.ReadAsStreamAsync();
            JsonRootObject jsonObject = await JsonSerializer.DeserializeAsync<JsonRootObject>(stream);
            return jsonObject.results;
        }

        public static async Task<List<User>> LoadUsers() {
            var loadCSVTask = LoadDataFromCSV();
            var loadJSONTask = LoadDataFromJSON();
            await Task.WhenAll(loadCSVTask, loadJSONTask);
            return loadCSVTask.Result.Concat(loadJSONTask.Result).ToList();
        }

    }
}
