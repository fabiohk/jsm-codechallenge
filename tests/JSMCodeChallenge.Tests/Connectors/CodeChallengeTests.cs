using Xunit;
using System;
using System.IO;
using Vcr;
using JSMCodeChallenge.Connectors;
using System.Net.Http;
using System.Collections.Generic;
using JSMCodeChallenge.DTOs;

namespace JSMCodeChallenge.Tests.Connectors
{
    public class CodeChallengeTests
    {
        private static string _baseDirectory = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}../../..";
        private readonly VCR _vcr;
        private readonly CodeChallenge _repository;


        public CodeChallengeTests()
        {
            var cassettesPath = new DirectoryInfo($"{_baseDirectory}/resources/cassettes");
            _vcr = new VCR(new FileSystemCassetteStorage(cassettesPath));
            var vcrHandler = _vcr.GetVcrHandler();
            vcrHandler.InnerHandler = new HttpClientHandler();
            HttpClient client = new HttpClient(vcrHandler);
            _repository = new CodeChallenge(client);
        }

        [Fact(DisplayName = "Should return list of UserDTO instances from CSV")]
        public async void TestLoadCSV()
        {
            using (_vcr.UseCassette("load_data.yaml", RecordMode.Once))
            {
                IEnumerable<UserDTO> users = await _repository.LoadDataFromCSV();
                Assert.NotEmpty(users);
            }
        }

        [Fact(DisplayName = "Should return list of UserDTO instances from JSON")]
        public async void TestLoadJSON()
        {
            using (_vcr.UseCassette("load_data.yaml", RecordMode.Once))
            {
                IEnumerable<UserDTO> users = await _repository.LoadDataFromJSON();
                Assert.NotEmpty(users);
            }
        }


    }
}