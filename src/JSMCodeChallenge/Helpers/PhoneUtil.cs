using PhoneNumbers;
using System.Collections.Generic;

namespace JSMCodeChallenge.Helpers
{
    public class PhoneUtil
    {
        private static readonly PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();

        public static string ConvertToE164Format(PhoneNumber phone)
        {
            return phoneNumberUtil.Format(phone, PhoneNumberFormat.E164);
        }

        public static List<PhoneNumber> RetrieveValidPhoneNumbers(IEnumerable<string> phones, string region)
        {
            List<PhoneNumber> phoneNumbers = new List<PhoneNumber>();

            foreach (string phone in phones)
            {
                try
                {
                    PhoneNumber phoneNumber = phoneNumberUtil.Parse(phone, region);
                    if (phoneNumberUtil.IsValidNumber(phoneNumber))
                        phoneNumbers.Add(phoneNumber);
                }
                catch (NumberParseException) { }
            }

            return phoneNumbers;
        }
    }
}