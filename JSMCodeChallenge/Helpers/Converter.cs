using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using PhoneNumbers;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace JSMCodeChallenge.Helpers
{
    public class JSONPhoneConverter : JsonConverter<string> {
        public override string Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
            var phoneNumber = phoneNumberUtil.Parse(reader.GetString(), "BR");
            return phoneNumberUtil.Format(phoneNumber, PhoneNumberFormat.E164);
        }

        public override void Write(Utf8JsonWriter writer, string stringValue, JsonSerializerOptions options) {
            writer.WriteNull(stringValue);
        }
    }

    public class CSVPhoneConverter : StringConverter {
            public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData) {
                var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
                var phoneNumber = phoneNumberUtil.Parse(row.GetField("phone"), "BR");
                return phoneNumberUtil.Format(phoneNumber, PhoneNumberFormat.E164);
            }
        }
}