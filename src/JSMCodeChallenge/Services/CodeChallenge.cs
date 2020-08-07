using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using JSMCodeChallenge.DTOs;

namespace JSMCodeChallenge.Services
{
    public class CodeChallenge
    {
        private readonly HttpClient client;

        public CodeChallenge(HttpClient client)
        {
            this.client = client;
        }

        public async Task<List<UserDTO>> LoadDataFromCSV()
        {
            HttpResponseMessage response = await client.GetAsync("https://storage.googleapis.com/juntossomosmais-code-challenge/input-backend.csv");
            Stream stream = await response.Content.ReadAsStreamAsync();
            using (StreamReader streamReader = new StreamReader(stream))
            {
                CsvReader reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
                reader.Configuration.RegisterClassMap<UserDTO.CSVMap>();
                return reader.GetRecords<UserDTO>().ToList();
            }
        }

        public async Task<List<UserDTO>> LoadDataFromJSON()
        {
            HttpResponseMessage response = await client.GetAsync("https://storage.googleapis.com/juntossomosmais-code-challenge/input-backend.json");
            Stream stream = await response.Content.ReadAsStreamAsync();
            JSONUsersDTO results = await JsonSerializer.DeserializeAsync<JSONUsersDTO>(stream);
            return results.Users;
        }

    }
}
