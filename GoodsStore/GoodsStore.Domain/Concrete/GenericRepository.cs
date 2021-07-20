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
        protected readonly DbContext _db;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public virtual void CreateOrUpdate(T entity)
        {
            _dbSet.AddOrUpdate(entity);
        }

        public virtual T Add(T entity)
        {
            return _dbSet.Add(entity);
        }

        public virtual T Delete(T entity)
        {
            return _dbSet.Remove(entity);
        }

        public virtual T Get(long id)
        {
            return _dbSet.Find(id);
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.AsNoTracking<T>().Where(predicate);
        }

        // some kind of fucking black magick, after item edititing
        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet;
        }
    }
}
