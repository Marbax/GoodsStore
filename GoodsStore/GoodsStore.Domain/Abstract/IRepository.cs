using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GoodsStore.Domain.Abstract
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        void CreateOrUpdate(T entity);
        T Delete(T entity);
        T Add(T entity);
    }
}
