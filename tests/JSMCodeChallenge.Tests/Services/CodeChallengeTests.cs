using Xunit;
using System;
using System.IO;
using Vcr;
using JSMCodeChallenge.Services;
using System.Net.Http;
using System.Collections.Generic;
using JSMCodeChallenge.DTOs;

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

        [Fact(DisplayName = "Should return list of UserDTO instances from CSV")]
        public async void TestLoadCSV()
        {
            using (vcr.UseCassette("load_data.yaml", RecordMode.Once))
            {
                List<UserDTO> users = await service.LoadDataFromCSV();
                Assert.NotEmpty(users);
            }
        }

        [Fact(DisplayName = "Should return list of UserDTO instances from JSON")]
        public async void TestLoadJSON()
        {
            using (vcr.UseCassette("load_data.yaml", RecordMode.Once))
            {
                List<UserDTO> users = await service.LoadDataFromJSON();
                Assert.NotEmpty(users);
            }
        }


    }
}