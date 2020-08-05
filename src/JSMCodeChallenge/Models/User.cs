using System;
using CsvHelper.Configuration;
using System.Text.Json.Serialization;
using JSMCodeChallenge.Helpers;

namespace JSMCodeChallenge.Models
{
    public class User : IEquatable<User> {
        [JsonPropertyName("gender")]
        public string Gender { get; set; }
        [JsonPropertyName("name.title")]
        public string Title { get; set; }
        [JsonPropertyName("name.first")]
        public string FirstName { get; set; }
        [JsonPropertyName("name.last")]
        public string LastName { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; } // Email is the user key!
        [JsonConverter(typeof(JSONPhoneConverter))]
        [JsonPropertyName("phone")]
        public string Phone { get; set; }
        [JsonConverter(typeof(JSONPhoneConverter))]
        [JsonPropertyName("cell")]
        public string CellPhone { get; set; }
        [JsonPropertyName("registered.date")]
        public DateTime RegisteredDate { get; set; }
        [JsonPropertyName("dob.date")]
        public DateTime BirthDate { get; set; }
        [JsonPropertyName("dob.age")]
        public int CurrentAge { get; set; }
        [JsonPropertyName("registered.age")]
        public int RegisteredAge { get; set; }
        [JsonPropertyName("location")]
        public Location Location { get; set; }
        [JsonPropertyName("picture")]
        public Picture Picture { get; set; }
        public string Nationality { get; set; } = "BR";

        public class CSVMap : ClassMap<User> {
            public CSVMap() {
                Map(member => member.Gender).Name("gender");
                Map(member => member.Title).Name("name__title");
                Map(member => member.FirstName).Name("name__first");
                Map(member => member.LastName).Name("name__last");
                Map(member => member.Email).Name("email");
                Map(member => member.Phone).Name("phone").TypeConverter<CSVPhoneConverter>();
                Map(member => member.CellPhone).Name("cell").TypeConverter<CSVPhoneConverter>();
                Map(member => member.RegisteredDate).Name("registered__date");
                Map(member => member.BirthDate).Name("dob__date");
                Map(member => member.CurrentAge).Name("dob__age");
                Map(member => member.RegisteredAge).Name("registered__age");
                References<Location.CSVMap>(member => member.Location);
                References<Picture.CSVMap>(member => member.Picture);
            }
        }

        public bool Equals(User anotherUser) {
            if (anotherUser is null)
                return false;

            return this.Email == anotherUser.Email;
        }

        public override bool Equals(object? obj) => Equals(obj as User);
        public override int GetHashCode() => Email.GetHashCode();
    }
}