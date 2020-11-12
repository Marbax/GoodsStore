using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace GoodsStore.Business.Services.Abstract
{
    public interface IService<T>
        where T : class
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        void CreateOrUpdate(T entity);
        T Delete(T entity);
    }
}
