using GoodsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace GoodsStore.Domain.Concrete
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _db;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public void CreateOrUpdate(T entity)
        {
            _dbSet.AddOrUpdate(entity);
        }

        public T Add(T entity)
        {
            return _dbSet.Add(entity);
        }

        public T Delete(T entity)
        {
            return _dbSet.Remove(entity);
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AsNoTracking<T>().Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet;
        }
    }
}
