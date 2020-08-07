using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using JSMCodeChallenge.Services;
using JSMCodeChallenge.Models;
using JSMCodeChallenge.DTOs;

namespace JSMCodeChallenge
{
    public class Program
    {
        private readonly static CodeChallenge service = new CodeChallenge(new HttpClient());
        public static async Task Main(string[] args)
        {
            List<UserDTO> users = await service.LoadDataFromCSV();
            foreach (var user in users)
            {
                Console.WriteLine(user.Name.Title);
            }
        }
    }
}
