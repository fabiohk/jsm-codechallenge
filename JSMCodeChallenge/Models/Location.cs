using CsvHelper.Configuration;
using System.Text.Json.Serialization;

namespace JSMCodeChallenge.Models
{
    public class Location {
        [JsonPropertyName("street")]
        public string Street { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("state")]
        public string State { get; set; }
        [JsonPropertyName("postcode")]
        public int PostalCode { get; set; }
        [JsonPropertyName("coordinates.latitude")]
        public string Latitude { get; set; }
        [JsonPropertyName("coordinates.longitude")]
        public string Longitude { get; set; }
        [JsonPropertyName("timezone.offset")]
        public string TimezoneOffset { get; set; }

        public class CSVMap : ClassMap<Location> {
            public CSVMap() {
                Map(member => member.Street).Name("location__street");
                Map(member => member.City).Name("location__city");
                Map(member => member.State).Name("location__state");
                Map(member => member.PostalCode).Name("location__postcode");
                Map(member => member.Latitude).Name("location__coordinates__latitude");
                Map(member => member.Longitude).Name("location__coordinates__longitude");
                Map(member => member.TimezoneOffset).Name("location__timezone__offset");
            }
        }
    }
}