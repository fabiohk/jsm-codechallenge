using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using JSMCodeChallenge.DTOs;
using Serilog;

namespace JSMCodeChallenge.Connectors
{
    public class CodeChallenge
    {
        private readonly HttpClient _client;

        public CodeChallenge(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<UserDTO>> LoadDataFromCSV()
        {
            Log.Information("Retrieving data from CSV...");
            HttpResponseMessage response = await _client.GetAsync("https://storage.googleapis.com/juntossomosmais-code-challenge/input-backend.csv");
            Stream stream = await response.Content.ReadAsStreamAsync();
            using (StreamReader streamReader = new StreamReader(stream))
            {
                CsvReader reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
                reader.Configuration.RegisterClassMap<UserDTO.CSVMap>();
                IEnumerable<UserDTO> users = reader.GetRecords<UserDTO>();
                Log.Information("Successfully retrieved {Count} users from CSV.", users.Count());
                return users.ToList();
            }
        }

        public async Task<IEnumerable<UserDTO>> LoadDataFromJSON()
        {
            Log.Information("Retrieving data from JSON...");
            HttpResponseMessage response = await _client.GetAsync("https://storage.googleapis.com/juntossomosmais-code-challenge/input-backend.json");
            Stream stream = await response.Content.ReadAsStreamAsync();
            JSONUsersDTO results = await JsonSerializer.DeserializeAsync<JSONUsersDTO>(stream);
            Log.Information("Successfully retrieved {Count} users from JSON.", results.Users.Count());
            return results.Users;
        }

    }
}
