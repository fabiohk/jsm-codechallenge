using Xunit;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using JSMCodeChallenge.Tests.Fixtures;

namespace JSMCodeChallenge.Tests.Controllers.V1
{
    public class UserTests : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        public UserTests(CustomWebApplicationFactory fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact(DisplayName = "Should retrieve up to 10 users when no pageSize parameter is given")]
        public async Task TestPageSizeDefault()
        {
            var response = await _client.GetAsync("/api/v1/users");
            var jsonString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(jsonString);
            var users = responseJson["users"].ToObject<List<dynamic>>();
            var pageSize = responseJson["pageSize"].ToObject<dynamic>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(users.Count <= 10);
            Assert.True(pageSize == 10);
        }

        [Theory(DisplayName = "Should retrieve at most the given pageSize parameter of users")]
        [InlineData("/api/v1/users?pageSize=2", 2)]
        [InlineData("/api/v1/users?pageSize=10", 10)]
        public async Task TestPageSize(string uri, int expectedCount)
        {
            var response = await _client.GetAsync(uri);
            var jsonString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(jsonString);
            var users = responseJson["users"].ToObject<List<dynamic>>();
            var pageSize = responseJson["pageSize"].ToObject<dynamic>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(users.Count <= expectedCount);
            Assert.True(pageSize <= expectedCount);
        }

        [Theory(DisplayName = "Should retrieve at most 50 users, even if the pageSize parameter is greater than 50")]
        [InlineData("/api/v1/users?pageSize=50")]
        [InlineData("/api/v1/users?pageSize=100")]
        public async Task TestPageSizeAtMost50(string uri)
        {
            var response = await _client.GetAsync(uri);
            var jsonString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(jsonString);
            var users = responseJson["users"].ToObject<List<dynamic>>();
            var pageSize = responseJson["pageSize"].ToObject<dynamic>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(users.Count <= 50);
            Assert.True(pageSize <= 50);
        }

        [Theory(DisplayName = "Should retrieve at least 1 user, even if the pageSize parameter is less than 1")]
        [InlineData("/api/v1/users?pageSize=1")]
        [InlineData("/api/v1/users?pageSize=0")]
        [InlineData("/api/v1/users?pageSize=-100")]
        public async Task TestPageSizeAtLeast1(string uri)
        {
            var response = await _client.GetAsync(uri);
            var jsonString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(jsonString);
            var users = responseJson["users"].ToObject<List<dynamic>>();
            var pageSize = responseJson["pageSize"].ToObject<dynamic>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Single(users);
            Assert.Equal(1, pageSize);
        }

        [Fact(DisplayName = "Should retrieve the first page when no page parameter is given")]
        public async Task TestFirstPageDefault()
        {
            var response = await _client.GetAsync("/api/v1/users");
            var jsonString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(jsonString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(1, responseJson["pageNumber"]);
        }

        [Theory(DisplayName = "Should filter by region")]
        [InlineData("/api/v1/users?region=north", "north")]
        [InlineData("/api/v1/users?region=north east", "north east")]
        [InlineData("/api/v1/users?region=center west", "center west")]
        [InlineData("/api/v1/users?region=south east", "south east")]
        [InlineData("/api/v1/users?region=south", "south")]
        public async Task TestFilterRegion(string uri, string region)
        {
            var response = await _client.GetAsync(uri);
            var jsonString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(jsonString);
            var users = responseJson["users"].ToObject<List<dynamic>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.All(users, user => Assert.Equal(region, user["location"]?["region"].Value));
        }

        [Theory(DisplayName = "Should filter by type")]
        [InlineData("/api/v1/users?type=laborious", "laborious")]
        [InlineData("/api/v1/users?type=normal", "normal")]
        [InlineData("/api/v1/users?type=special", "special")]
        public async Task TestFilterType(string uri, string type)
        {
            var response = await _client.GetAsync(uri);
            var jsonString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(jsonString);
            var users = responseJson["users"].ToObject<List<dynamic>>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.All(users, user => Assert.Equal(type, user["type"].Value));
        }

        [Fact(DisplayName = "Should have the expected contract as defined by the challenge")]
        public async Task TestExpectedContract()
        {
            var response = await _client.GetAsync("/api/v1/users");
            var jsonString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(jsonString);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(responseJson["pageNumber"]);
            Assert.NotNull(responseJson["pageSize"]);
            Assert.NotNull(responseJson["totalCount"]);
            Assert.NotNull(responseJson["users"]);

            // Root keys from one user
            var user = responseJson["users"][0];
            Assert.NotNull(user["type"]);
            Assert.NotNull(user["gender"]);
            Assert.NotNull(user["email"]);
            Assert.NotNull(user["birthday"]);
            Assert.NotNull(user["registered"]);
            Assert.NotNull(user["telephoneNumbers"]);
            Assert.NotNull(user["mobileNumbers"]);
            Assert.NotNull(user["nationality"]);
            Assert.NotNull(user["name"]);
            Assert.NotNull(user["location"]);
            Assert.NotNull(user["picture"]);

            // Name keys
            var name = user["name"];
            Assert.NotNull(name["title"]);
            Assert.NotNull(name["first"]);
            Assert.NotNull(name["last"]);

            // Picture keys
            var picture = user["picture"];
            Assert.NotNull(picture["large"]);
            Assert.NotNull(picture["medium"]);
            Assert.NotNull(picture["thumbnail"]);

            // Location keys
            var location = user["location"];
            Assert.NotNull(location["region"]);
            Assert.NotNull(location["street"]);
            Assert.NotNull(location["city"]);
            Assert.NotNull(location["state"]);
            Assert.NotNull(location["postcode"]);
            Assert.NotNull(location["coordinates"]);
            Assert.NotNull(location["timezone"]);

            // Coordinates keys
            var coordinates = location["coordinates"];
            Assert.NotNull(coordinates["latitude"]);
            Assert.NotNull(coordinates["longitude"]);

            // Timezone keys
            var timezone = location["timezone"];
            Assert.NotNull(timezone["offset"]);
            Assert.NotNull(timezone["description"]);
        }
    }
}