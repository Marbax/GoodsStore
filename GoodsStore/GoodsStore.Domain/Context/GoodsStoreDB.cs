namespace GoodsStore.Context.Domain
{
    using GoodsStore.Entities.Domain;
    using GoodsStore.Migrations.Domain;
    using System.Data.Entity;

    public partial class GoodsStoreDB : DbContext
    {
        public GoodsStoreDB()
            : base("name=GoodsStoreDB")
        {
        }

        public virtual DbSet<C__EFMigrationsHistory> C__EFMigrationsHistory { get; set; }
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Good> Good { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<Photo> Photo { get; set; }
        public virtual DbSet<Sale> Sale { get; set; }
        public virtual DbSet<SalePos> SalePos { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Good>()
                .Property(e => e.GoodName)
                .IsUnicode(false);

            modelBuilder.Entity<Good>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Good>()
                .Property(e => e.GoodCount)
                .HasPrecision(18, 3);

            modelBuilder.Entity<Good>()
                .HasMany(e => e.SalePos)
                .WithRequired(e => e.Good)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Sale>()
                .Property(e => e.UserPhone)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Sale>()
                .Property(e => e.Summa)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Sale>()
                .HasMany(e => e.SalePos)
                .WithRequired(e => e.Sale)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SalePos>()
                .Property(e => e.Summa)
                .HasPrecision(19, 4);
        }
    }
}
