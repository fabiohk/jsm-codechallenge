using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JSMCodeChallenge.Models;

namespace JSMCodeChallenge.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Dictionary<string, object> Get()
        {
            _logger.LogInformation("Receive request to retrieve users with params");
            int pageSize = 10, pageNumber = 0;
            IEnumerable<User> users = Enumerable.Range(1, 20).Select(index => new User());
            return new Dictionary<string, object>(){
                {"pageNumber", pageNumber},
                {"pageSize" , pageSize},
                {"totalCount" , users.Count()},
                {"users" , users}
            };
        }
    }
}