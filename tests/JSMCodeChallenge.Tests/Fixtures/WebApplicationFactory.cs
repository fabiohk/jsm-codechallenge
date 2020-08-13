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
        private User CreateUser(Location? location) => new User()
        {
            Name = new Name(),
            Email = $"{location?.State ?? "region"}-{random.Next()}@email.com",
            Location = location ?? new BrazilianLocation() { Coordinates = new Coordinates(), Timezone = new Timezone() },
            Picture = new Picture(),

        };

        private User CreateBrazilLocatedUser(Coordinates coordinates, string state)
        {
            Location location = new BrazilianLocation() { State = state, Coordinates = coordinates, Timezone = new Timezone() };
            return CreateUser(location);
        }

        private User CreateLaboriousUser(string state)
        {
            var laboriousCoordinates = new Coordinates() { Latitude = 0, Longitude = 0 };
            return CreateBrazilLocatedUser(laboriousCoordinates, state);
        }

        private User CreateSpecialUser(string state)
        {
            var specialCoordinates = new Coordinates() { Latitude = -40, Longitude = -10 };
            return CreateBrazilLocatedUser(specialCoordinates, state);
        }

        private User CreateNormalUser(string state)
        {
            var normalCoordinates = new Coordinates() { Latitude = -50, Longitude = -30 };
            return CreateBrazilLocatedUser(normalCoordinates, state);
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
                    User laboriousNorthUser = CreateLaboriousUser("amazonas"),
                         laboriousNorthEastUser = CreateLaboriousUser("bahia"),
                         laboriousCenterWestUser = CreateLaboriousUser("goiás"),
                         laboriousSouthEastUser = CreateLaboriousUser("são paulo"),
                         laboriousSouthUser = CreateLaboriousUser("paraná"),
                         specialNorthUser = CreateSpecialUser("acre"),
                         specialNorthEastUser = CreateSpecialUser("sergipe"),
                         specialCenterWestUser = CreateSpecialUser("mato grosso"),
                         specialSouthEastUser = CreateSpecialUser("rio de janeiro"),
                         specialSouthUser = CreateSpecialUser("santa catarina"),
                         normalNorthUser = CreateNormalUser("roraima"),
                         normalNorthEastUser = CreateNormalUser("piauí"),
                         normalCenterWestUser = CreateNormalUser("distrito federal"),
                         normalSouthEastUser = CreateNormalUser("espírito santo"),
                         normalSouthUser = CreateNormalUser("rio grande do sul");
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
                    IEnumerable<User> unknownRegionUsers = Enumerable.Range(0, 50).Select(_ => CreateUser(null));

                    service.AddSingleton<UserRepository>(_ => new UserRepository(unknownRegionUsers.Concat(knownRegionUsers)));
                });
    }
}