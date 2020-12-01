using GoodsStore.Domain.Entities;
using System.Data.Entity;

namespace GoodsStore.Domain.Context
{
    public class GoodsStoreDB : DbContext
    {
        public GoodsStoreDB(string name)
            : base(name)
        {
            Database.SetInitializer(new GoodsStoreDBInitializer());
        }

        public DbSet<Good> Goods { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
