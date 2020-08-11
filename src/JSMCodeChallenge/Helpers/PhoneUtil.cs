using PhoneNumbers;
using System.Collections.Generic;

namespace JSMCodeChallenge.Helpers
{
    public static class PhoneUtil
    {
        private static readonly PhoneNumberUtil _phoneNumberUtil = PhoneNumberUtil.GetInstance();

        public static string ConvertToE164Format(PhoneNumber phone)
        {
            return _phoneNumberUtil.Format(phone, PhoneNumberFormat.E164);
        }

        public static IEnumerable<PhoneNumber> RetrieveValidPhoneNumbers(IEnumerable<string> phones, string region)
        {
            HashSet<PhoneNumber> phoneNumbers = new HashSet<PhoneNumber>();

            foreach (string phone in phones)
            {
                try
                {
                    PhoneNumber phoneNumber = _phoneNumberUtil.Parse(phone, region);
                    if (_phoneNumberUtil.IsValidNumber(phoneNumber))
                        phoneNumbers.Add(phoneNumber);
                }
                catch (NumberParseException) { }
            }

            return phoneNumbers;
        }
    }
}