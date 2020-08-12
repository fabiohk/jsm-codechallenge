using Xunit;
using JSMCodeChallenge.Models;
using JSMCodeChallenge.Business;

namespace JSMCodeChallenge.Tests.Business
{
    public class UserTypeTests
    {

        [Theory(DisplayName = "Should return that the user type is special")]
        [InlineData(-40, -10)]
        [InlineData(-50, -20)]
        public void TestSpecialUserType(decimal latitude, decimal longitude)
        {
            /* Special Type bounding boxes:
                minlon: -2.196998
                minlat -46.361899
                maxlon: -15.411580
                maxlat: -34.276938

                and

                minlon: -19.766959
                minlat -52.997614
                maxlon: -23.966413
                maxlat: -44.428305
             */
            Coordinates coordinates = new Coordinates() { Latitude = latitude, Longitude = longitude };
            Location specialLocation = new BrazilianLocation() { Coordinates = coordinates };
            User user = new User() { Location = specialLocation };

            Assert.Equal("special", UserType.GetUserType(user));
        }

        [Fact(DisplayName = "Should return that the user type is normal")]
        public void TestNormalUserType()
        {
            /* Normal Type bounding boxes:
                minlon: -26.155681
                minlat -54.777426
                maxlon: -34.016466
                maxlat: -46.603598
             */
            Coordinates coordinates = new Coordinates() { Latitude = -50, Longitude = -30 };
            Location normalLocation = new BrazilianLocation() { Coordinates = coordinates };
            User user = new User() { Location = normalLocation };

            Assert.Equal("normal", UserType.GetUserType(user));
        }

        [Fact(DisplayName = "Should return that the user type is laborious")]
        public void TestLaboriousUserType()
        {
            Coordinates coordinates = new Coordinates() { Latitude = 0, Longitude = 0 };
            Location laboriousLocation = new BrazilianLocation() { Coordinates = coordinates };
            User user = new User() { Location = laboriousLocation };

            Assert.Equal("laborious", UserType.GetUserType(user));
        }
    }
}