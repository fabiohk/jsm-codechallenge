using System;
using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using JSMCodeChallenge.Api;
using System.Threading.Tasks;
using JSMCodeChallenge.Connectors;
using System.Linq;
using JSMCodeChallenge.Helpers;
using System.Collections.Generic;
using JSMCodeChallenge.Models;
using JSMCodeChallenge.Repositories;

namespace JSMCodeChallenge
{
    public class Program
    {
        public static void ConfigureLogger()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }


        private static async Task<IEnumerable<User>> LoadUsers()
        {
            using (var client = new HttpClient())
            {
                var connector = new CodeChallenge(client);

                var loadJsonTask = connector.LoadDataFromJSON();
                var loadCsvTask = connector.LoadDataFromCSV();

                await Task.WhenAll(loadCsvTask, loadJsonTask);

                return loadJsonTask.Result.Concat(loadCsvTask.Result)
                    .Select(userDTO => Parser.parseUserDTO(userDTO, "BR", "BR"))
                    .ToHashSet();
            }
        }
        public static async Task Main(string[] args)
        {
            ConfigureLogger();

            try
            {
                var users = await LoadUsers();
                CreateHostBuilder(args, users).Build().Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Host terminated unexpectedly!");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IEnumerable<User> users) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureServices((ctx, service) =>
                {
                    service.AddSingleton<UserRepository>(_ => new UserRepository(users));
                });
    }
}
