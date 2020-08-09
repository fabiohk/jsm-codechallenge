using System.Text.Json.Serialization;
using CsvHelper.Configuration;

namespace JSMCodeChallenge.DTOs
{
    public class LocationDTO
    {
        [JsonPropertyName("street")]
        public string Street { get; set; }
        [JsonPropertyName("city")]
        public string City { get; set; }
        [JsonPropertyName("state")]
        public string State { get; set; }
        [JsonPropertyName("postcode")]
        public int PostalCode { get; set; }
        [JsonPropertyName("coordinates")]
        public CoordinatesDTO Coordinates { get; set; }
        [JsonPropertyName("timezone")]
        public TimezoneDTO Timezone { get; set; }

        public class CSVMap : ClassMap<LocationDTO>
        {
            public CSVMap()
            {
                Map(member => member.Street).Name("location__street");
                Map(member => member.City).Name("location__city");
                Map(member => member.State).Name("location__state");
                Map(member => member.PostalCode).Name("location__postcode");
                References<CoordinatesDTO.CSVMap>(member => member.Coordinates);
                References<TimezoneDTO.CSVMap>(member => member.Timezone);
            }
        }
    }

    public class CoordinatesDTO
    {
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }
        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }

        public class CSVMap : ClassMap<CoordinatesDTO>
        {
            public CSVMap()
            {
                Map(member => member.Latitude).Name("location__coordinates__latitude");
                Map(member => member.Longitude).Name("location__coordinates__longitude");
            }
        }
    }

    public class TimezoneDTO
    {
        [JsonPropertyName("offset")]
        public string Offset { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }

        public class CSVMap : ClassMap<TimezoneDTO>
        {
            public CSVMap()
            {
                Map(member => member.Offset).Name("location__timezone__offset");
                Map(member => member.Description).Name("location__timezone__description");
            }
        }
    }
}