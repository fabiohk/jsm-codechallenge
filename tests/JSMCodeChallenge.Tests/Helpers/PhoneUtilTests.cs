using Xunit;
using JSMCodeChallenge.Helpers;
using System.Collections.Generic;
using PhoneNumbers;
using System.Linq;

namespace JSMCodeChallenge.Tests.Helpers
{
    public class PhoneUtilTests
    {
        private static readonly PhoneNumberUtil _phoneNumberUtil = PhoneNumberUtil.GetInstance();

        [Fact(DisplayName = "Should retrieve only valid phone numbers")]
        public void TestValidPhoneNumbersOnly()
        {
            List<string> possiblePhones = new List<string>() { "clearly-not-phone", "(21) 2724-5438", "(21) 98386-0771", "(02) 3764-0902" };
            IEnumerable<PhoneNumber> phones = PhoneUtil.RetrieveValidPhoneNumbers(possiblePhones, "BR");
            HashSet<PhoneNumber> expectedPhones = new HashSet<PhoneNumber>();

            expectedPhones.Add(_phoneNumberUtil.Parse("(21) 2724-5438", "BR"));
            expectedPhones.Add(_phoneNumberUtil.Parse("(21) 98386-0771", "BR"));

            Assert.Equal(expectedPhones.OrderBy(phone => phone.CountryCode), phones.OrderBy(phone => phone.CountryCode));
        }
    }
}