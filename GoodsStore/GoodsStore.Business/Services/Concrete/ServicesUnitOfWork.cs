using AutoMapper;
using GoodsStore.Business.Models;
using GoodsStore.Business.Models.Concrete;
using GoodsStore.Business.Services.Abstract;
using GoodsStore.Domain.Entities;
using System;
using System.Data.Entity;

namespace GoodsStore.Business.Services.Concrete
{
    public class ServicesUnitOfWork : IServicesUnitOfWork
    {
        private bool _disposedValue = false;
        private readonly DbContext _db;


        public IService<GoodDTO> Goods { get; }

        public IService<OrderDetailsDTO> SalesPoses { get; }

        public IService<OrderDTO> Sales { get; }

        public IService<PhotoDTO> Photos { get; }

        public IService<ManufacturerDTO> Manufacturers { get; }

        public IService<UserDTO> Users { get; }

        public IService<CategoryDTO> Categories { get; }

        public IService<RoleDTO> Roles { get; }

        public ServicesUnitOfWork(DbContext db, IService<GoodDTO> goods, IService<OrderDetailsDTO> salesPoses,
            IService<OrderDTO> sales, IService<PhotoDTO> photos, IService<ManufacturerDTO> manufacturers,
            IService<UserDTO> users, IService<CategoryDTO> categories, IService<RoleDTO> roles)
        {
            _db = db;
            Goods = goods;
            SalesPoses = salesPoses;
            Sales = sales;
            Photos = photos;
            Manufacturers = manufacturers;
            Users = users;
            Categories = categories;
            Roles = roles;
        }

        public void Save() => _db.SaveChanges();

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                    _db.Dispose();
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }

}
