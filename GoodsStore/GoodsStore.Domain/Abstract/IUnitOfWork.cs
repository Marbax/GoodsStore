using GoodsStore.Domain.Entities;
using System;
using System.Data.Entity;

namespace GoodsStore.Domain.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext db { get; }

        IRepository<Good> goodRepo { get; }
        IRepository<SalePos> salePosRepo { get; }
        IRepository<Sale> saleRepo { get; }
        IRepository<Photo> photoRepo { get; }
        IRepository<Manufacturer> manRepo { get; }
        IRepository<User> userRepo { get; }
        IRepository<Category> catRepo { get; }

        void Save();
    }
}
