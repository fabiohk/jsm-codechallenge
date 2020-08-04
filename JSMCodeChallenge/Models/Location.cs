using CsvHelper.Configuration;

namespace JSMCodeChallenge.Models
{
    public class Location {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string TimezoneOffset { get; set; }
    }

    public class LocationCSVMap : ClassMap<Location> {
        public LocationCSVMap() {
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