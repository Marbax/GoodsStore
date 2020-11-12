using GoodsStore.Domain.Abstract;
using GoodsStore.Domain.Entities;
using System;
using System.Data.Entity;

namespace GoodsStore.Domain.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool disposedValue = false;
        public DbContext db { get; }


        private GenericRepository<Good> _goodRepo;
        public IRepository<Good> Goods
        {
            get
            {
                if (_goodRepo == null)
                    _goodRepo = new GenericRepository<Good>(db);
                return _goodRepo;
            }
        }

        private GenericRepository<SalePos> _salesPosRepo;
        public IRepository<SalePos> SalesPoses
        {
            get
            {
                if (_salesPosRepo == null)
                    _salesPosRepo = new GenericRepository<SalePos>(db);
                return _salesPosRepo;
            }
        }

        private GenericRepository<Sale> _salesRepo;
        public IRepository<Sale> Sales
        {
            get
            {
                if (_salesRepo == null)
                    _salesRepo = new GenericRepository<Sale>(db);
                return _salesRepo;
            }
        }

        private GenericRepository<Photo> _photosRepo;
        public IRepository<Photo> Photos {
            get
            {
                if (_photosRepo == null)
                    _photosRepo = new GenericRepository<Photo>(db);
                return _photosRepo;
            }
        }

        private GenericRepository<Manufacturer> _mansRepo;
        public IRepository<Manufacturer> Manufacturers {
            get
            {
                if (_mansRepo == null)
                    _mansRepo = new GenericRepository<Manufacturer>(db);
                return _mansRepo;
            }
        }

        private GenericRepository<User> _usersRepo;
        public IRepository<User> Users {
            get
            {
                if (_usersRepo == null)
                    _usersRepo = new GenericRepository<User>(db);
                return _usersRepo;
            }
        }

        private GenericRepository<Category> _catsRepo;
        public IRepository<Category> Categories {
            get
            {
                if (_catsRepo == null)
                    _catsRepo = new GenericRepository<Category>(db);
                return _catsRepo;
            }
        }

        public UnitOfWork(DbContext db)
        {
            this.db = db;
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
