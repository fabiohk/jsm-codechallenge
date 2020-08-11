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
    public class LocationTests
    {
        private static string _baseDirectory = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}../../..";

        [Fact(DisplayName = "Should deserialize a JSON into LocationDTO instance with valid values")]
        public static void TestDeserializeJSON()
        {
            using (StreamReader stream = new StreamReader($"{_baseDirectory}/resources/location-example.json"))
            {
                string json = stream.ReadToEnd();
                LocationDTO location = JsonSerializer.Deserialize<LocationDTO>(json);

                Assert.Equal("8614 avenida vinícius de morais", location.Street);
                Assert.Equal("ponta grossa", location.City);
                Assert.Equal("rondônia", location.State);
                Assert.Equal(97701, location.PostalCode);
                Assert.Equal("-76.3253", location.Coordinates.Latitude);
                Assert.Equal("137.9437", location.Coordinates.Longitude);
                Assert.Equal("-1:00", location.Timezone.Offset);
                Assert.Equal("Azores, Cape Verde Islands", location.Timezone.Description);
            }
        }

        [Fact(DisplayName = "Should deserializa a JSON into LocationDTO instance with default values when JSON is not in the expected format")]
        public static void TestDeserializeNotExpectedJSON()
        {
            using (StreamReader stream = new StreamReader($"{_baseDirectory}/resources/random-example.json"))
            {
                string json = stream.ReadToEnd();
                LocationDTO location = JsonSerializer.Deserialize<LocationDTO>(json);

                Assert.Null(location.Street);
                Assert.Null(location.City);
                Assert.Null(location.State);
                Assert.Equal(default(int), location.PostalCode);
                Assert.Null(location.Coordinates);
                Assert.Null(location.Timezone);
            }
        }

        [Fact(DisplayName = "Should deserialize a CSV into LocationDTO instance with valid values")]
        public static void TestDeserializeCSV()
        {
            using (StreamReader stream = new StreamReader($"{_baseDirectory}/resources/data-example.csv"))
            {
                CsvReader reader = new CsvReader(stream, CultureInfo.InvariantCulture);
                reader.Configuration.RegisterClassMap<LocationDTO.CSVMap>();
                List<LocationDTO> locations = reader.GetRecords<LocationDTO>().ToList();

                Assert.Single(locations);
                Assert.Equal("8614 avenida vinícius de morais", locations[0].Street);
                Assert.Equal("ponta grossa", locations[0].City);
                Assert.Equal("rondônia", locations[0].State);
                Assert.Equal(97701, locations[0].PostalCode);
                Assert.Equal("-76.3253", locations[0].Coordinates.Latitude);
                Assert.Equal("137.9437", locations[0].Coordinates.Longitude);
                Assert.Equal("-1:00", locations[0].Timezone.Offset);
                Assert.Equal("Azores, Cape Verde Islands", locations[0].Timezone.Description);
            }
        }

        [Fact(DisplayName = "Shouldnt deserialize a CSV into LocationDTO instance when CSV is not in the expected format")]
        public static void TestDeserializeNotExpectedCSV()
        {
            using (StreamReader stream = new StreamReader($"{_baseDirectory}/resources/random-example.csv"))
            {
                CsvReader reader = new CsvReader(stream, CultureInfo.InvariantCulture);
                reader.Configuration.RegisterClassMap<LocationDTO.CSVMap>();
                Assert.Throws<CsvHelper.HeaderValidationException>(() => reader.GetRecords<LocationDTO>().ToList());
            }
        }
    }
}