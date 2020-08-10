using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace JSMCodeChallenge.Tests.Controllers.V1
{
    public class UserTests : IClassFixture<WebApplicationFactory<Api.Startup>>
    {
        private readonly HttpClient Client;
        public UserTests(WebApplicationFactory<Api.Startup> fixture)
        {
            Client = fixture.CreateClient();
        }

        [Fact(DisplayName = "Should retrieve 10 users when no pageSize parameter is given")]
        public async Task TestPageSizeDefault()
        {
            var response = await Client.GetAsync("/api/v1/user");
            var jsonString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(jsonString);
            var users = responseJson["users"].ToObject<List<dynamic>>();
            var pageSize = responseJson["pageSize"].ToObject<dynamic>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(users.Count <= 10);
            Assert.True(pageSize == 10);
        }

        [Theory(DisplayName = "Should retrieve at most the given pageSize parameter of users")]
        [InlineData("/api/v1/user?pageSize=2", 2)]
        [InlineData("/api/v1/user?pageSize=10", 10)]
        public async Task TestPageSize(string uri, int expectedCount)
        {
            var response = await Client.GetAsync(uri);
            var jsonString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(jsonString);
            var users = responseJson["users"].ToObject<List<dynamic>>();
            var pageSize = responseJson["pageSize"].ToObject<dynamic>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(users.Count <= expectedCount);
            Assert.True(pageSize <= expectedCount);
        }

        [Theory(DisplayName = "Should retrieve at most 50 users, even if the pageSize parameter is greater than 50")]
        [InlineData("/api/v1/user?pageSize=50")]
        [InlineData("/api/v1/user?pageSize=100")]
        public async Task TestPageSizeAtMost50(string uri)
        {
            var response = await Client.GetAsync(uri);
            var jsonString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(jsonString);
            var users = responseJson["users"].ToObject<List<dynamic>>();
            var pageSize = responseJson["pageSize"].ToObject<dynamic>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(users.Count <= 50);
            Assert.True(pageSize <= 50);
        }

        [Theory(DisplayName = "Should retrieve at least 1 user, even if the pageSize parameter is less than 1")]
        [InlineData("/api/v1/user?pageSize=1")]
        [InlineData("/api/v1/user?pageSize=0")]
        [InlineData("/api/v1/user?pageSize=-100")]
        public async Task TestPageSizeAtLeast1(string uri)
        {
            var response = await Client.GetAsync(uri);
            var jsonString = await response.Content.ReadAsStringAsync();
            var responseJson = JObject.Parse(jsonString);
            var users = responseJson["users"].ToObject<List<dynamic>>();
            var pageSize = responseJson["pageSize"].ToObject<dynamic>();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Single(users);
            Assert.Equal(1, pageSize);
        }
    }
}