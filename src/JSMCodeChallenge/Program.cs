using System;
using System.Net.Http;
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
            foreach (var user in users) {
                if (user.Phone != null) {
                    Console.WriteLine(user.Phone);
                }
            }
        }
    }
}
