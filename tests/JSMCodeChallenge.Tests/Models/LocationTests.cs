using Xunit;
using JSMCodeChallenge.Models;
using JSMCodeChallenge.Exceptions;

namespace JSMCodeChallenge.Tests.Models {
    public class LocationTests {
        [Fact(DisplayName = "Should return that a Location instace is from Brazilian North region")]
        public void TestBrazilianNorthRegion()
        {
            string[] brazilianNorthStates = new string[] { "acre", "amazonas", "amapá", "pará", "rondônia", "roraima", "tocantins" };

            foreach (string state in brazilianNorthStates) {
                BrazilianLocation location = new BrazilianLocation() { State = state };
                Assert.Equal("north", location.Region);
            }
        }

        [Fact(DisplayName = "Should return that a Location instance is from Brazilian North East region")]
        public void TestBrazilianNorthEastRegion()
        {
            string[] brazilianNorthEastStates = new string[] { "alagoas", "bahia", "ceará", "maranhão", "paraíba", "pernambuco", "piauí", "rio grande do norte", "sergipe" };

            foreach (string state in brazilianNorthEastStates) {
                BrazilianLocation location = new BrazilianLocation() { State = state };
                Assert.Equal("north east", location.Region);
            }
        }

        [Fact(DisplayName = "Should return that a Location instance is from Brazilian Center West region")]
        public void TestBrazilianCenterWestRegion()
        {
            string[] brazilianCenterWestStates = new string[] { "goiás", "mato grosso", "mato grosso do sul", "distrito federal" };

            foreach (string state in brazilianCenterWestStates) {
                BrazilianLocation location = new BrazilianLocation() { State = state };
                Assert.Equal("center west", location.Region);
            }
        }

        [Fact(DisplayName = "Should return that a Location instance is from Brazilian South region")]
        public void TestBrazilianSouthRegion()
        {
            string[] brazilianSouthStates = new string[] { "rio grande do sul", "paraná", "santa catarina" };

            foreach (string state in brazilianSouthStates) {
                BrazilianLocation location = new BrazilianLocation() { State = state };
                Assert.Equal("south", location.Region);
            }
        }

        [Fact(DisplayName = "Should return that a Location instance is from Brazilian South East region")]
        public void TestBrazilianSouthEastRegion()
        {
            string[] brazilianSouthEastStates = new string[] { "espírito santo", "minas gerais", "são paulo", "rio de janeiro" };

            foreach (string state in brazilianSouthEastStates) {
                BrazilianLocation location = new BrazilianLocation() { State = state };
                Assert.Equal("south east", location.Region);
            }
        }

        [Fact(DisplayName = "Should throw InvalidLocationStateException when state is an unknown Brazilian state")]
        public void TestBrazilianCannotDetermineRegion()
        {
            Assert.Throws<InvalidLocationStateException>(() => new BrazilianLocation() { State = "unknown" });
        }
    }
}