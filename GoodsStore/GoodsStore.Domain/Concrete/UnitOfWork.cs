using GoodsStore.Domain.Abstract;
using GoodsStore.Entities.Domain;
using System;
using System.Data.Entity;

namespace GoodsStore.Domain.Concrete
{
    internal class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue = false;

        public DbContext db { get; }

        public IRepository<Good> goodRepo { get; }

        public IRepository<SalePos> salePosRepo { get; }

        public IRepository<Sale> saleRepo { get; }

        public IRepository<Photo> photoRepo { get; }

        public IRepository<Manufacturer> manRepo { get; }

        public IRepository<User> userRepo { get; }

        public IRepository<Category> catRepo { get; }

        public UnitOfWork(IRepository<Good> goodRepo, IRepository<SalePos> salePosRepo, IRepository<Sale> saleRepo,
            IRepository<Photo> photoRepo, IRepository<Manufacturer> manRepo, IRepository<User> userRepo, IRepository<Category> catRepo)
        {
            this.goodRepo = goodRepo;
            this.salePosRepo = salePosRepo;
            this.saleRepo = saleRepo;
            this.photoRepo = photoRepo;
            this.manRepo = manRepo;
            this.userRepo = userRepo;
            this.catRepo = catRepo;
        }

        public void Save() => db.SaveChanges();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                    db.Dispose();
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
