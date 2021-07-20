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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Note:One-to-one relationships are technically not possible in MS SQL Server.
            // These will always be one-to-zero-or-one relationships. EF forms One-to-One relationships on entities not in the DB.
            modelBuilder.Entity<Payment>()
                .HasRequired(i => i.Order)
                .WithRequiredPrincipal(ad => ad.Payment);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Good> Goods { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Payment> Payments { get; set; }
    }
}
