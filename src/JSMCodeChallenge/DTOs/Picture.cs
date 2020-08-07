using CsvHelper.Configuration;
using System.Text.Json.Serialization;

namespace JSMCodeChallenge.DTOs
{
    public class PictureDTO
    {
        [JsonPropertyName("large")]
        public string Large { get; set; }
        [JsonPropertyName("medium")]
        public string Medium { get; set; }
        [JsonPropertyName("thumbnail")]
        public string Thumbnail { get; set; }

        public class CSVMap : ClassMap<PictureDTO>
        {
            public CSVMap()
            {
                Map(member => member.Large).Name("picture__large");
                Map(member => member.Medium).Name("picture__medium");
                Map(member => member.Thumbnail).Name("picture__thumbnail");
            }
        }
    }
}