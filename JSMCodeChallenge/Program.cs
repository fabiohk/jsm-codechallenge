using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using JSMCodeChallenge.Services;
using JSMCodeChallenge.Models;

namespace JSMCodeChallenge
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            List<User> users = await CodeChallenge.LoadUsers();
            Console.WriteLine(users[0].Phone);
        }
    }
}
