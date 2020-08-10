using Xunit;
using JSMCodeChallenge.Models;
using JSMCodeChallenge.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace JSMCodeChallenge.Tests.Models {
    public class UserTests {
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

        [Fact(DisplayName = "Should retrieve User Phones/CellPhones property in E.164 format")]
        public void TestValidPhones()
        {
            List<string> validPhones = new List<string>() { "(54) 3648-2276", "(98) 3543-7813" }, expectedPhones = new List<string>() { "+555436482276", "+559835437813" };
            List<string> validCellphones = new List<string>() { "(98) 98346-5273", "(21) 98386-0771" }, expectedCellphones = new List<string>() { "+5598983465273", "+5521983860771" };
            User user = new User() { Phones = validPhones, CellPhones = validCellphones };
            
            Assert.Equal(expectedPhones.OrderBy(phone => phone), user.Phones.OrderBy(phone => phone));
            Assert.Equal(expectedCellphones.OrderBy(phone => phone), user.CellPhones.OrderBy(phone => phone));
        }
    }

}