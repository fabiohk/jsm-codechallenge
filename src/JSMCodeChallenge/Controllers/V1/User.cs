using System;
using JSMCodeChallenge.Helpers;
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

        private Expression<Func<User, bool>> ParseFilterExpression(string region, string type)
        {
            Expression<Func<User, bool>> regionFilterExpression = user => region == null ? true : (user.Location != null ? user.Location.Region == region : false);
            Expression<Func<User, bool>> typeFilterExpression = user => type == null ? true : user.Type == type;
            return Expression.Lambda<Func<User, bool>>(
                Expression.AndAlso(
                    regionFilterExpression.Body,
                    Expression.Invoke(typeFilterExpression, regionFilterExpression.Parameters)
                ),
                regionFilterExpression.Parameters
            );
        }

        [HttpGet]
        public ActionResult Get(int? pageSize, int? page, string region, string type)
        {
            Log.Information("Receive request to retrieve user with params: {@Params}", new { pageSize, page, region, type });
            var filterExpression = ParseFilterExpression(region, type);
            var users = _userRepository.Filter(filterExpression);
            var usersCount = users.Count();
            var resultPage = Pagination.GetPage(users, page, pageSize);
            return Ok(new
            {
                pageNumber = resultPage.Number,
                pageSize = resultPage.Size,
                totalCount = usersCount,
                users = resultPage.Content
            });
        }
    }
}