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
        private readonly IMapper _mapper;


        private GenericService<GoodDTO, Good> _goodServ;
        public IService<GoodDTO> Goods
        {
            get
            {
                if (_goodServ == null)
                    _goodServ = new GenericService<GoodDTO, Good>(_db, _mapper);
                return _goodServ;
            }
        }

        private GenericService<SalePosDTO, SalePos> _salePosServ;
        public IService<SalePosDTO> SalesPoses
        {
            get
            {
                if (_salePosServ == null)
                    _salePosServ = new GenericService<SalePosDTO, SalePos>(_db, _mapper);
                return _salePosServ;
            }
        }

        private GenericService<SaleDTO, Sale> _saleServ;
        public IService<SaleDTO> Sales
        {
            get
            {
                if (_saleServ == null)
                    _saleServ = new GenericService<SaleDTO, Sale>(_db, _mapper);
                return _saleServ;
            }
        }

        private GenericService<PhotoDTO, Photo> _photoServ;
        public IService<PhotoDTO> Photos
        {
            get
            {
                if (_photoServ == null)
                    _photoServ = new GenericService<PhotoDTO, Photo>(_db, _mapper);
                return _photoServ;
            }
        }

        private GenericService<ManufacturerDTO, Manufacturer> _manServ;
        public IService<ManufacturerDTO> Manufacturers
        {
            get
            {
                if (_manServ == null)
                    _manServ = new GenericService<ManufacturerDTO, Manufacturer>(_db, _mapper);
                return _manServ;
            }
        }

        private GenericService<UserDTO, User> _userServ;
        public IService<UserDTO> Users
        {
            get
            {
                if (_userServ == null)
                    _userServ = new GenericService<UserDTO, User>(_db, _mapper);
                return _userServ;
            }
        }

        private GenericService<CategoryDTO, Category> _catServ;
        public IService<CategoryDTO> Categories
        {
            get
            {
                if (_catServ == null)
                    _catServ = new GenericService<CategoryDTO, Category>(_db, _mapper);
                return _catServ;
            }
        }


        public ServicesUnitOfWork(DbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
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
