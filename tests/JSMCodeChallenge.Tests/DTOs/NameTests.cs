using Xunit;
using System.Text.Json;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using CsvHelper;
using System.Globalization;
using JSMCodeChallenge.DTOs;

namespace JSMCodeChallenge.Tests.Models
{
    public class NameTests
    {
        private static string baseDirectory = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}../../..";

        [Fact(DisplayName = "Should deserialize a JSON into NameDTO instance with valid values")]
        public static void TestDeserializeJSON()
        {
            using (StreamReader stream = new StreamReader($"{baseDirectory}/resources/name-example.json"))
            {
                string json = stream.ReadToEnd();
                NameDTO name = JsonSerializer.Deserialize<NameDTO>(json);

                Assert.Equal("mrs", name.Title);
                Assert.Equal("ione", name.First);
                Assert.Equal("da costa", name.Last);
            }
        }

        [Fact(DisplayName = "Should deserializa a JSON into NameDTO instance with default values when JSON is not in the expected format")]
        public static void TestDeserializeNotExpectedJSON()
        {
            using (StreamReader stream = new StreamReader($"{baseDirectory}/resources/random-example.json"))
            {
                string json = stream.ReadToEnd();
                NameDTO name = JsonSerializer.Deserialize<NameDTO>(json);

                Assert.Null(name.Title);
                Assert.Null(name.First);
                Assert.Null(name.Last);
            }
        }

        [Fact(DisplayName = "Should deserialize a CSV into NameDTO instance with valid values")]
        public static void TestDeserializeCSV()
        {
            using (StreamReader stream = new StreamReader($"{baseDirectory}/resources/data-example.csv"))
            {
                CsvReader reader = new CsvReader(stream, CultureInfo.InvariantCulture);
                reader.Configuration.RegisterClassMap<NameDTO.CSVMap>();
                List<NameDTO> names = reader.GetRecords<NameDTO>().ToList();

                Assert.Single(names);
                Assert.Equal("mrs", names[0].Title);
                Assert.Equal("ione", names[0].First);
                Assert.Equal("da costa", names[0].Last);
            }
        }

        [Fact(DisplayName = "Shouldnt deserialize a CSV into NameDTO instance when CSV is not in the expected format")]
        public static void TestDeserializeNotExpectedCSV()
        {
            using (StreamReader stream = new StreamReader($"{baseDirectory}/resources/random-example.csv"))
            {
                CsvReader reader = new CsvReader(stream, CultureInfo.InvariantCulture);
                reader.Configuration.RegisterClassMap<NameDTO.CSVMap>();
                Assert.Throws<CsvHelper.HeaderValidationException>(() => reader.GetRecords<NameDTO>().ToList());
            }
        }
    }
}