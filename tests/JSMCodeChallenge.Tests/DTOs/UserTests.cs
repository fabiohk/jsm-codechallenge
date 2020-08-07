using Xunit;
using System.Text.Json;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using CsvHelper;
using System.Globalization;
using JSMCodeChallenge.DTOs;

namespace JSMCodeChallenge.Tests.DTOs
{
    public class UserTests
    {
        private static string baseDirectory = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}../../..";

        [Fact(DisplayName = "Should deserialize a JSON into UserDTO instance with valid values")]
        public static void TestDeserializeJSON()
        {
            using (StreamReader stream = new StreamReader($"{baseDirectory}/resources/data-example.json"))
            {
                string json = stream.ReadToEnd();
                UserDTO user = JsonSerializer.Deserialize<UserDTO>(json);
                DateTime expectedBirthDate = new DateTime(1968, 1, 24, 18, 3, 23, DateTimeKind.Utc);
                DateTime expectedRegisteredDate = new DateTime(2004, 1, 23, 23, 54, 33, DateTimeKind.Utc);

                Assert.Equal("female", user.Gender);
                Assert.Equal("mrs", user.Name.Title);
                Assert.Equal("ione", user.Name.First);
                Assert.Equal("da costa", user.Name.Last);
                Assert.Equal("8614 avenida vinícius de morais", user.Location.Street);
                Assert.Equal("ponta grossa", user.Location.City);
                Assert.Equal("rondônia", user.Location.State);
                Assert.Equal(97701, user.Location.PostalCode);
                Assert.Equal("-76.3253", user.Location.Coordinates.Latitude);
                Assert.Equal("137.9437", user.Location.Coordinates.Longitude);
                Assert.Equal("-1:00", user.Location.Timezone.Offset);
                Assert.Equal("ione.dacosta@example.com", user.Email);
                Assert.Equal(expectedBirthDate, user.Birth.Date);
                Assert.Equal(50, user.Birth.Age);
                Assert.Equal(expectedRegisteredDate, user.Registered.Date);
                Assert.Equal(14, user.Registered.Age);
                Assert.Equal("(01) 5415-5648", user.Phone);
                Assert.Equal("(10) 8264-5550", user.CellPhone);
                Assert.Equal("https://randomuser.me/api/portraits/women/46.jpg", user.Picture.Large);
                Assert.Equal("https://randomuser.me/api/portraits/med/women/46.jpg", user.Picture.Medium);
                Assert.Equal("https://randomuser.me/api/portraits/thumb/women/46.jpg", user.Picture.Thumbnail);
            }
        }

        [Fact(DisplayName = "Should deserializa a JSON into UserDTO instance with default values when JSON is not in the expected format")]
        public static void TestDeserializeNotExpectedJSON()
        {
            using (StreamReader stream = new StreamReader($"{baseDirectory}/resources/random-example.json"))
            {
                string json = stream.ReadToEnd();
                UserDTO user = JsonSerializer.Deserialize<UserDTO>(json);

                Assert.Null(user.Gender);
                Assert.Null(user.Name);
                Assert.Null(user.Location);
                Assert.Null(user.Email);
                Assert.Null(user.Birth);
                Assert.Null(user.Registered);
                Assert.Null(user.Phone);
                Assert.Null(user.CellPhone);
                Assert.Null(user.Picture);
            }
        }

        [Fact(DisplayName = "Should deserialize a CSV into UserDTO instance with valid values")]
        public static void TestDeserializeCSV()
        {
            using (StreamReader stream = new StreamReader($"{baseDirectory}/resources/data-example.csv"))
            {
                CsvReader reader = new CsvReader(stream, CultureInfo.InvariantCulture);
                reader.Configuration.RegisterClassMap<UserDTO.CSVMap>();
                List<UserDTO> users = reader.GetRecords<UserDTO>().ToList();
                DateTime expectedBirthDate = new DateTime(1968, 1, 24, 18, 3, 23, DateTimeKind.Utc);
                DateTime expectedRegisteredDate = new DateTime(2004, 1, 23, 23, 54, 33, DateTimeKind.Utc);

                Assert.Single(users);
                Assert.Equal("female", users[0].Gender);
                Assert.Equal("mrs", users[0].Name.Title);
                Assert.Equal("ione", users[0].Name.First);
                Assert.Equal("da costa", users[0].Name.Last);
                Assert.Equal("8614 avenida vinícius de morais", users[0].Location.Street);
                Assert.Equal("ponta grossa", users[0].Location.City);
                Assert.Equal("rondônia", users[0].Location.State);
                Assert.Equal(97701, users[0].Location.PostalCode);
                Assert.Equal("-76.3253", users[0].Location.Coordinates.Latitude);
                Assert.Equal("137.9437", users[0].Location.Coordinates.Longitude);
                Assert.Equal("-1:00", users[0].Location.Timezone.Offset);
                Assert.Equal("ione.dacosta@example.com", users[0].Email);
                Assert.Equal(expectedBirthDate, users[0].Birth.Date);
                Assert.Equal(50, users[0].Birth.Age);
                Assert.Equal(expectedRegisteredDate, users[0].Registered.Date);
                Assert.Equal(14, users[0].Registered.Age);
                Assert.Equal("(01) 5415-5648", users[0].Phone);
                Assert.Equal("(10) 8264-5550", users[0].CellPhone);
                Assert.Equal("https://randomuser.me/api/portraits/women/46.jpg", users[0].Picture.Large);
                Assert.Equal("https://randomuser.me/api/portraits/med/women/46.jpg", users[0].Picture.Medium);
                Assert.Equal("https://randomuser.me/api/portraits/thumb/women/46.jpg", users[0].Picture.Thumbnail);
            }
        }

        [Fact(DisplayName = "Shouldn't deserialize a CSV into UserDTO instance when CSV is not in the expected format")]
        public static void TestDeserializeNotExpectedCSV()
        {
            using (StreamReader stream = new StreamReader($"{baseDirectory}/resources/random-example.csv"))
            {
                CsvReader reader = new CsvReader(stream, CultureInfo.InvariantCulture);
                reader.Configuration.RegisterClassMap<UserDTO.CSVMap>();
                Assert.Throws<CsvHelper.HeaderValidationException>(() => reader.GetRecords<UserDTO>().ToList());
            }
        }

        [Fact(DisplayName = "Should deserialize a JSON results into list of UserDTO")]
        public static void TestDeserializeJSONUsersDTO()
        {
            using (StreamReader stream = new StreamReader($"{baseDirectory}/resources/user-results-example.json"))
            {
                string json = stream.ReadToEnd();
                JSONUsersDTO deserializedJSON = JsonSerializer.Deserialize<JSONUsersDTO>(json);

                Assert.Single(deserializedJSON.Users);
            }
        }
    }
}