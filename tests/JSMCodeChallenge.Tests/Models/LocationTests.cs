using Xunit;
using JSMCodeChallenge.Models;
using JSMCodeChallenge.Exceptions;

namespace JSMCodeChallenge.Tests.Models
{
    public class LocationTests
    {
        [Theory(DisplayName = "Should return that a Location instace is from Brazilian North region")]
        [InlineData("acre")]
        [InlineData("amazonas")]
        [InlineData("amapá")]
        [InlineData("pará")]
        [InlineData("rondônia")]
        [InlineData("roraima")]
        [InlineData("tocantins")]
        public void TestBrazilianNorthRegion(string state)
        {
            BrazilianLocation location = new BrazilianLocation() { State = state };
            Assert.Equal("north", location.Region);
        }

        [Theory(DisplayName = "Should return that a Location instance is from Brazilian North East region")]
        [InlineData("alagoas")]
        [InlineData("bahia")]
        [InlineData("ceará")]
        [InlineData("maranhão")]
        [InlineData("paraíba")]
        [InlineData("pernambuco")]
        [InlineData("piauí")]
        [InlineData("rio grande do norte")]
        [InlineData("sergipe")]
        public void TestBrazilianNorthEastRegion(string state)
        {
            BrazilianLocation location = new BrazilianLocation() { State = state };
            Assert.Equal("north east", location.Region);
        }

        [Theory(DisplayName = "Should return that a Location instance is from Brazilian Center West region")]
        [InlineData("goiás")]
        [InlineData("mato grosso")]
        [InlineData("mato grosso do sul")]
        [InlineData("distrito federal")]
        public void TestBrazilianCenterWestRegion(string state)
        {
            BrazilianLocation location = new BrazilianLocation() { State = state };
            Assert.Equal("center west", location.Region);
        }

        [Theory(DisplayName = "Should return that a Location instance is from Brazilian South region")]
        [InlineData("rio grande do sul")]
        [InlineData("paraná")]
        [InlineData("santa catarina")]
        public void TestBrazilianSouthRegion(string state)
        {
            BrazilianLocation location = new BrazilianLocation() { State = state };
            Assert.Equal("south", location.Region);
        }

        [Theory(DisplayName = "Should return that a Location instance is from Brazilian South East region")]
        [InlineData("espírito santo")]
        [InlineData("minas gerais")]
        [InlineData("são paulo")]
        [InlineData("rio de janeiro")]
        public void TestBrazilianSouthEastRegion(string state)
        {
            BrazilianLocation location = new BrazilianLocation() { State = state };
            Assert.Equal("south east", location.Region);
        }

        [Fact(DisplayName = "Should throw InvalidLocationStateException when state is an unknown Brazilian state")]
        public void TestBrazilianCannotDetermineRegion()
        {
            Assert.Throws<InvalidLocationStateException>(() => new BrazilianLocation() { State = "unknown" });
        }
    }
}