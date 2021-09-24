using AutoMapper;
using GoodsStore.Business.Services.Abstract;
using GoodsStore.Domain.Abstract;
using GoodsStore.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace GoodsStore.Business.Services.Concrete
{
    public class GenericService<T, U> : IService<T>
        where T : class
        where U : class
    {
        private readonly IRepository<U> _repo;
        private readonly DbContext _db;
        private readonly IMapper _mapper;

        public GenericService(DbContext db, IRepository<U> repo, IMapper mapper)
        {
            _db = db;
            _repo = repo;
            _mapper = mapper;
        }

        public void CreateOrUpdate(T entity)
        {
            var dbEntity = MapToDLL(entity);
            _repo.CreateOrUpdate(dbEntity);
        }

        public T Add(T entity)
        {
            var dbEntity = MapToDLL(entity);
            var added = _repo.Add(dbEntity);
            _db.SaveChanges();
            return MapToBll(added);
        }

        public T Delete(T entity)
        {
            var dbEntity = MapToDLL(entity);
            var resp = _repo.Delete(dbEntity);
            var bllEntity = MapToBll(resp);
            return bllEntity;
        }

        public T Delete(int id)
        {
            var dbEntity = _repo.Get(id);
            var resp = _repo.Delete(dbEntity);
            var bllEntity = MapToBll(resp);
            return bllEntity;
        }

        public T Get(int id)
        {
            var resp = _repo.Get(id);
            var bllEntity = MapToBll(resp);
            return bllEntity;

        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> predicate)
        {
            var expression = _mapper.Map<Expression<Func<U, bool>>>(predicate);
            var resp = _repo.Get(expression);
            var bllArr = resp.Select(i => MapToBll(i));
            return bllArr;
        }

        public IEnumerable<T> GetAll()
        {
            var resp = _repo.GetAll();
            var bllArr = resp.Select(i => MapToBll(i));
            return bllArr;
        }

        private T MapToBll(U entity) => _mapper.Map<U, T>(entity); // fix mapping for the good
        private U MapToDLL(T entity) => _mapper.Map<T, U>(entity);
    }
}
