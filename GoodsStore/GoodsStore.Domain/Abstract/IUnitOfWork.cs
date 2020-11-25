using GoodsStore.Domain.Entities;
using System;

namespace GoodsStore.Domain.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Good> Goods { get; }
        IRepository<OrderDetails> OrderDetailsRepo { get; }
        IRepository<Order> Orders { get; }
        IRepository<Photo> Photos { get; }
        IRepository<Manufacturer> Manufacturers { get; }
        IRepository<User> Users { get; }
        IRepository<Category> Categories { get; }
        IRepository<Role> Roles { get; }

        void Save();
    }
}
