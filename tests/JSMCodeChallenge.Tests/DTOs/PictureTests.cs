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
    public class PictureTests
    {
        private static string baseDirectory = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}../../..";

        [Fact(DisplayName = "Should deserialize a JSON into PictureDTO instance with valid values")]
        public static void TestDeserializeJSON()
        {
            using (StreamReader stream = new StreamReader($"{baseDirectory}/resources/picture-example.json"))
            {
                string json = stream.ReadToEnd();
                PictureDTO picture = JsonSerializer.Deserialize<PictureDTO>(json);

                Assert.Equal("https://randomuser.me/api/portraits/women/46.jpg", picture.Large);
                Assert.Equal("https://randomuser.me/api/portraits/med/women/46.jpg", picture.Medium);
                Assert.Equal("https://randomuser.me/api/portraits/thumb/women/46.jpg", picture.Thumbnail);
            }
        }

        [Fact(DisplayName = "Should deserializa a JSON into PictureDTO instance with default values when JSON is not in the expected format")]
        public static void TestDeserializeNotExpectedJSON()
        {
            using (StreamReader stream = new StreamReader($"{baseDirectory}/resources/random-example.json"))
            {
                string json = stream.ReadToEnd();
                PictureDTO picture = JsonSerializer.Deserialize<PictureDTO>(json);

                Assert.Null(picture.Large);
                Assert.Null(picture.Medium);
                Assert.Null(picture.Thumbnail);
            }
        }

        [Fact(DisplayName = "Should deserialize a CSV into PictureDTO instance with valid values")]
        public static void TestDeserializeCSV()
        {
            using (StreamReader stream = new StreamReader($"{baseDirectory}/resources/data-example.csv"))
            {
                CsvReader reader = new CsvReader(stream, CultureInfo.InvariantCulture);
                reader.Configuration.RegisterClassMap<PictureDTO.CSVMap>();
                List<PictureDTO> pictures = reader.GetRecords<PictureDTO>().ToList();

                Assert.Single(pictures);
                Assert.Equal("https://randomuser.me/api/portraits/women/46.jpg", pictures[0].Large);
                Assert.Equal("https://randomuser.me/api/portraits/med/women/46.jpg", pictures[0].Medium);
                Assert.Equal("https://randomuser.me/api/portraits/thumb/women/46.jpg", pictures[0].Thumbnail);
            }
        }

        [Fact(DisplayName = "Shouldnt deserialize a CSV into PictureDTO instance when CSV is not in the expected format")]
        public static void TestDeserializeNotExpectedCSV()
        {
            using (StreamReader stream = new StreamReader($"{baseDirectory}/resources/random-example.csv"))
            {
                CsvReader reader = new CsvReader(stream, CultureInfo.InvariantCulture);
                reader.Configuration.RegisterClassMap<PictureDTO.CSVMap>();
                Assert.Throws<CsvHelper.HeaderValidationException>(() => reader.GetRecords<PictureDTO>().ToList());
            }
        }
    }
}