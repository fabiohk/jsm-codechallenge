using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using JSMCodeChallenge.Models;
using System.Linq.Expressions;
using JSMCodeChallenge.Repositories;
using Serilog;

namespace JSMCodeChallenge.Controllers.V1
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserRepository _userRepository;

        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult Get(int? pageSize, int? page, string? region, string? type)
        {
            Log.Information("Receive request to retrieve user with params: {@Params}", new { pageSize, page, region, type });
            pageSize = pageSize.HasValue ? Math.Max(Math.Min(pageSize.Value, 50), 1) : 10; // Limit to page size [1-50]
            page = page.HasValue ? Math.Max(page.Value, 1) : 1; // First page at least
            int offset = (page.Value - 1) * pageSize.Value;
            Expression<Func<User, bool>> regionFilterExpression = user => region == null ? true : (user.Location != null ? user.Location.Region == region : false);
            Expression<Func<User, bool>> typeFilterExpression = user => type == null ? true : user.Type == type;
            var expression = Expression.Lambda<Func<User, bool>>(
                Expression.AndAlso(
                    regionFilterExpression.Body,
                    Expression.Invoke(typeFilterExpression, regionFilterExpression.Parameters)
                ),
                regionFilterExpression.Parameters
            );
            var users = _userRepository.Filter(expression);
            var usersCount = users.Count();
            return Ok(new
            {
                pageNumber = page,
                pageSize = Math.Min(pageSize.Value, usersCount),
                totalCount = usersCount,
                users = users.Skip(offset).Take(pageSize.Value)
            });
        }
    }
}