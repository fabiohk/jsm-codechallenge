using System.Collections.Generic;
using JSMCodeChallenge.Models;
using JSMCodeChallenge.DTOs;
using System.Linq;
using System.Linq.Expressions;
using JSMCodeChallenge.Helpers;
using JSMCodeChallenge.Repositories.Interfaces;
using System;

namespace JSMCodeChallenge.Repositories
{
    public class UserRepository : IRepository<User>
    {
        public IEnumerable<User> Users { get; }

        public UserRepository(IEnumerable<User> users)
        {
            Users = users;
        }

        public UserRepository(IEnumerable<UserDTO> usersDTO, string defaultPhoneRegion, string defaultNationality)
        {
            Users = usersDTO
                .Select(userDTO => Parser.parseUserDTO(userDTO, defaultPhoneRegion, defaultNationality));
        }

        public IEnumerable<User> All() => Users;

        public IEnumerable<User> Filter(Expression<Func<User, bool>> filterPredicate = null) => Users.Where(filterPredicate.Compile());

        public int Count(Expression<Func<User, bool>> predicate) => Users.Where(predicate.Compile()).Count();
    }
}