// Repository.cs
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DesignStudio.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DesignStudioContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(DesignStudioContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public T Get(int id) => _dbSet.Find(id);

        public IEnumerable<T> GetAll() => _dbSet.ToList();

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
            => _dbSet.Where(predicate);

        public void Add(T entity) => _dbSet.Add(entity);

        public void Remove(T entity) => _dbSet.Remove(entity);
    }
}
