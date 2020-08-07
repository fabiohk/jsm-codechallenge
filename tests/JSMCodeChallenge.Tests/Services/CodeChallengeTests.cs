using Xunit;
using System;
using System.IO;
using Vcr;
using JSMCodeChallenge.Services;
using System.Net.Http;
using System.Collections.Generic;
using JSMCodeChallenge.Models;

namespace JSMCodeChallenge.Tests.Services
{
    public class CodeChallengeTests
    {
        private static string baseDirectory = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}../../..";
        private readonly VCR vcr;
        private readonly CodeChallenge service;

        public CodeChallengeTests()
        {
            var cassettesPath = new DirectoryInfo($"{baseDirectory}/resources/cassettes");
            vcr = new VCR(new FileSystemCassetteStorage(cassettesPath));
            var vcrHandler = vcr.GetVcrHandler();
            vcrHandler.InnerHandler = new HttpClientHandler();
            HttpClient client = new HttpClient(vcrHandler);
            service = new CodeChallenge(client);
        }

        // [Fact(DisplayName = "Should return 999 users")]
        // public async void TestShouldReturn999Users() {
        //     using (vcr.UseCassette("load_data.yaml", RecordMode.Once)) {
        //         List<User> users = await service.LoadUsers();
        //         Assert.True(users.Count == 999);
        //     }
        // }
    }
}