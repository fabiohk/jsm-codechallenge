using Xunit;
using System.IO;
using Vcr;
using JSMCodeChallenge.Services;
using System.Net.Http;
using System.Collections.Generic;
using JSMCodeChallenge.Models;

namespace JSMCodeChallenge.Tests.Services
{
    public class CodeChallengeTests {
        [Fact(DisplayName = "Should return 999 users")]
        public async void TestShouldReturn999Users() {
            List<User> users = await CodeChallenge.LoadUsers();
            Assert.True(users.Count == 999);
        }
    }
}