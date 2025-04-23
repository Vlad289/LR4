// IRepository.cs
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DesignStudio.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Remove(T entity);
    }
}
