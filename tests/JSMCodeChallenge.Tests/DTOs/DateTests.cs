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
    public class DateTests
    {
        private static string _baseDirectory = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}../../..";

        [Fact(DisplayName = "Should deserialize a JSON into DateDTO instance with valid values")]
        public static void TestDeserializeJSON()
        {
            using (StreamReader stream = new StreamReader($"{_baseDirectory}/resources/date-example.json"))
            {
                string json = stream.ReadToEnd();
                DateDTO dto = JsonSerializer.Deserialize<DateDTO>(json);
                DateTime expectedDate = new DateTime(1968, 1, 24, 18, 3, 23, DateTimeKind.Utc);

                Assert.Equal(expectedDate, dto.Date);
                Assert.Equal(50, dto.Age);
            }
        }

        [Fact(DisplayName = "Should deserializa a JSON into DateDTO instance with default values when JSON is not in the expected format")]
        public static void TestDeserializeNotExpectedJSON()
        {
            using (StreamReader stream = new StreamReader($"{_baseDirectory}/resources/random-example.json"))
            {
                string json = stream.ReadToEnd();
                DateDTO dto = JsonSerializer.Deserialize<DateDTO>(json);

                Assert.Equal(default(DateTime), dto.Date);
                Assert.Equal(default(int), dto.Age);
            }
        }

        [Fact(DisplayName = "Should deserialize a CSV into DateDTO instance with valid values")]
        public static void TestDeserializeCSV()
        {
            using (StreamReader stream = new StreamReader($"{_baseDirectory}/resources/data-example.csv"))
            {
                CsvReader reader = new CsvReader(stream, CultureInfo.InvariantCulture);
                DateDTO.CSVMap map = new DateDTO.CSVMap("dob");
                reader.Configuration.RegisterClassMap(map);
                List<DateDTO> dtoList = reader.GetRecords<DateDTO>().ToList();
                DateTime expectedDate = new DateTime(1968, 1, 24, 18, 3, 23, DateTimeKind.Utc);

                Assert.Single(dtoList);
                Assert.Equal(expectedDate, dtoList[0].Date);
                Assert.Equal(50, dtoList[0].Age);
            }
        }

        [Fact(DisplayName = "Shouldnt deserialize a CSV into DateDTO instance when CSV is not in the expected format")]
        public static void TestDeserializeNotExpectedCSV()
        {
            using (StreamReader stream = new StreamReader($"{_baseDirectory}/resources/random-example.csv"))
            {
                CsvReader reader = new CsvReader(stream, CultureInfo.InvariantCulture);
                DateDTO.CSVMap map = new DateDTO.CSVMap("dob");
                reader.Configuration.RegisterClassMap(map);
                Assert.Throws<CsvHelper.HeaderValidationException>(() => reader.GetRecords<DateDTO>().ToList());
            }
        }
    }
}