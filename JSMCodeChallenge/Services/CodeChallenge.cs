using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using JSMCodeChallenge.Models;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;

namespace JSMCodeChallenge.Services
{
    public class CodeChallenge
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<List<User>> LoadDataFromCSV() {
            HttpResponseMessage response = await client.GetAsync("https://storage.googleapis.com/juntossomosmais-code-challenge/input-backend.csv");
            Stream stream = await response.Content.ReadAsStreamAsync();
            using (StreamReader streamReader = new StreamReader(stream)) {
                CsvReader reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
                reader.Configuration.RegisterClassMap<UserCSVMap>();
                return reader.GetRecords<User>().ToList(); 
            }
        }

    }
}
