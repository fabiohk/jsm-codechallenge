using System.Text.Json.Serialization;
using CsvHelper.Configuration;

namespace JSMCodeChallenge.DTOs
{
    public class NameDTO
    {
        [JsonPropertyName("title")]
        public string? Title { get; set; }
        [JsonPropertyName("first")]
        public string? First { get; set; }
        [JsonPropertyName("last")]
        public string? Last { get; set; }

        public class CSVMap : ClassMap<NameDTO>
        {
            public CSVMap()
            {
                Map(member => member.Title).Name("name__title");
                Map(member => member.First).Name("name__first");
                Map(member => member.Last).Name("name__last");
            }
        }
    }
}