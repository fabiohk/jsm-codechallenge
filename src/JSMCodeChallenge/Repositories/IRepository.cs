using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace JSMCodeChallenge.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        public IEnumerable<T> All();
        public IEnumerable<T> Filter(Expression<Func<T, bool>> filterPredicate);
        public int Count(Expression<Func<T, bool>> predicate);
    }
}