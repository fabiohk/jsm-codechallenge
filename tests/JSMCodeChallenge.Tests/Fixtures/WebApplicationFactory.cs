using Microsoft.AspNetCore.Mvc.Testing;
using System.Collections.Generic;
using JSMCodeChallenge.Models;
using System.Linq;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using JSMCodeChallenge.Repositories;
using Microsoft.AspNetCore.TestHost;
using Serilog;
using System;

namespace JSMCodeChallenge.Tests.Fixtures
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Api.Startup>
    {
        private Random random = new Random();
        private User createUser(Coordinates coordinates, string state) => new User() { Email = $"{state}-{random.Next()}@email.com", Location = new BrazilianLocation() { State = state, Coordinates = coordinates } };
        private User createLaboriousUser(string state)
        {
            var laboriousCoordinates = new Coordinates() { Latitude = 0, Longitude = 0 };
            return createUser(laboriousCoordinates, state);
        }

        private User createSpecialUser(string state)
        {
            var specialCoordinates = new Coordinates() { Latitude = -40, Longitude = -10 };
            return createUser(specialCoordinates, state);
        }

        private User createNormalUser(string state)
        {
            var normalCoordinates = new Coordinates() { Latitude = -50, Longitude = -30 };
            return createUser(normalCoordinates, state);
        }

        protected override IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder()
                .UseSerilog()
                .ConfigureWebHostDefaults(builder =>
                {
                    builder.UseStartup<Api.Startup>()
                        .UseTestServer();
                })
                .ConfigureServices((ctx, service) =>
                {
                    User laboriousNorthUser = createLaboriousUser("amazonas"),
                         laboriousNorthEastUser = createLaboriousUser("bahia"),
                         laboriousCenterWestUser = createLaboriousUser("goiás"),
                         laboriousSouthEastUser = createLaboriousUser("são paulo"),
                         laboriousSouthUser = createLaboriousUser("paraná"),
                         specialNorthUser = createSpecialUser("acre"),
                         specialNorthEastUser = createSpecialUser("sergipe"),
                         specialCenterWestUser = createSpecialUser("mato grosso"),
                         specialSouthEastUser = createSpecialUser("rio de janeiro"),
                         specialSouthUser = createSpecialUser("santa catarina"),
                         normalNorthUser = createNormalUser("roraima"),
                         normalNorthEastUser = createNormalUser("piauí"),
                         normalCenterWestUser = createNormalUser("distrito federal"),
                         normalSouthEastUser = createNormalUser("espírito santo"),
                         normalSouthUser = createNormalUser("rio grande do sul");
                    IEnumerable<User> knownRegionUsers = new List<User>() {
                        laboriousNorthUser,
                        laboriousNorthEastUser,
                        laboriousCenterWestUser,
                        laboriousSouthEastUser,
                        laboriousSouthUser,
                        specialNorthUser,
                        specialNorthEastUser,
                        specialCenterWestUser,
                        specialSouthEastUser,
                        specialSouthUser,
                        normalNorthUser,
                        normalNorthEastUser,
                        normalCenterWestUser,
                        normalSouthEastUser,
                        normalSouthUser,
                    };
                    IEnumerable<User> unknownRegionUsers = Enumerable.Range(0, 50).Select(_ => new User() { Email = $"random-{random.Next()}@email.com" });

                    service.AddSingleton<UserRepository>(_ => new UserRepository(unknownRegionUsers.Concat(knownRegionUsers)));
                });
    }
}