using Xunit;
using JSMCodeChallenge.Models;
using JSMCodeChallenge.Exceptions;

namespace JSMCodeChallenge.Tests.Models
{
    public class UserTests
    {
        [Fact(DisplayName = "Should set User Gender property to 'f' when 'female' is given")]
        public void TestGenderFemale()
        {
            User user = new User() { Gender = "female" };

            Assert.Equal("f", user.Gender);
        }

        [Fact(DisplayName = "Should set User Gender property to 'm' when 'male' is given")]
        public void TestGenderMale()
        {
            User user = new User() { Gender = "male" };

            Assert.Equal("m", user.Gender);
        }

        [Fact(DisplayName = "Should raise InvalidGenderException when unkown gender is given")]
        public void TestInvalidGender()
        {
            Assert.Throws<InvalidGenderException>(() => new User() { Gender = "unknown" });
        }
    }

}