using GoodsStore.Domain.Entities;
using System;
using System.Data.Entity;

namespace GoodsStore.Domain.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext db { get; }

        IRepository<Good> Goods{ get; }
        IRepository<SalePos> SalesPoses { get; }
        IRepository<Sale> Sales { get; }
        IRepository<Photo> Photos { get; }
        IRepository<Manufacturer> Manufacturers { get; }
        IRepository<User> Users { get; }
        IRepository<Category> Categories { get; }

        void Save();
    }
}
