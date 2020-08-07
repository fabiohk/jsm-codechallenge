using System;
using System.Text.Json.Serialization;
using CsvHelper.Configuration;
using System.Globalization;

namespace JSMCodeChallenge.DTOs
{
    public class DateDTO
    {
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("age")]
        public int Age { get; set; }

        public class CSVMap : ClassMap<DateDTO>
        {
            public CSVMap(string rootKey)
            {
                Map(member => member.Date).Name($"{rootKey}__date").TypeConverterOption.DateTimeStyles(DateTimeStyles.AdjustToUniversal);
                Map(member => member.Age).Name($"{rootKey}__age");
            }
        }
    }
}