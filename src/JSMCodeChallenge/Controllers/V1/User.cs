using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using JSMCodeChallenge.Models;
using Serilog;

namespace JSMCodeChallenge.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get(int? pageSize, int? page, string? region, string? type)
        {
            Log.Information("Receive request to retrieve user with params: {@Params}", new { pageSize, page, region, type });
            pageSize = pageSize.HasValue ? Math.Max(Math.Min(pageSize.Value, 50), 1) : 10; // Limit to page size [1-50]
            page = page.HasValue ? Math.Max(page.Value, 1) : 1; // First page at least
            int offset = (page.Value - 1) * pageSize.Value;
            IEnumerable<User> users = Enumerable.Range(1, 100).Select(index => new User())
                .Where(user => region != null ? user.Location?.Region == region : true)
                .Where(user => type != null ? user.Type == type : true);
            return Ok(new
            {
                pageNumber = page,
                pageSize = Math.Min(pageSize.Value, users.Count()),
                totalCount = users.Count(),
                users = users.Skip(offset).Take(pageSize.Value)
            });
        }
    }
}