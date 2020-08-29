using System.Collections.Generic;
using System.Text.Json.Serialization;
using CsvHelper.Configuration;

namespace JSMCodeChallenge.DTOs
{
    public class UserDTO
    {
        [JsonPropertyName("gender")]
        public string? Gender { get; set; }
        [JsonPropertyName("name")]
        public NameDTO? Name { get; set; }
        [JsonPropertyName("location")]
        public LocationDTO? Location { get; set; }
        [JsonPropertyName("email")]
        public string? Email { get; set; }
        [JsonPropertyName("dob")]
        public DateDTO? Birth { get; set; }
        [JsonPropertyName("registered")]
        public DateDTO? Registered { get; set; }
        [JsonPropertyName("phone")]
        public string? Phone { get; set; }
        [JsonPropertyName("cell")]
        public string? CellPhone { get; set; }
        [JsonPropertyName("picture")]
        public PictureDTO? Picture { get; set; }

        public class CSVMap : ClassMap<UserDTO>
        {
            public CSVMap()
            {
                Map(member => member.Gender).Name("gender");
                Map(member => member.Email).Name("email");
                Map(member => member.Phone).Name("phone");
                Map(member => member.CellPhone).Name("cell");
                References<NameDTO.CSVMap>(member => member.Name);
                References<DateDTO.CSVMap>(member => member.Registered, "registered");
                References<DateDTO.CSVMap>(member => member.Birth, "dob");
                References<LocationDTO.CSVMap>(member => member.Location);
                References<PictureDTO.CSVMap>(member => member.Picture);
            }
        }
    }

    public class JSONUsersDTO
    {
        [JsonPropertyName("results")]
        public List<UserDTO>? Users { get; set; }
    }
}