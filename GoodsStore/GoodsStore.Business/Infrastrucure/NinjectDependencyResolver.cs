using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using GoodsStore.Business.Models;
using GoodsStore.Business.Services.Abstract;
using GoodsStore.Business.Services.Concrete;
using GoodsStore.Domain.Abstract;
using GoodsStore.Domain.Concrete;
using GoodsStore.Domain.Context;
using GoodsStore.Domain.Entities;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace GoodsStore.Business.Infrastrucure
{
    internal class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _ninjectKernel;

        public NinjectDependencyResolver()
        {
            // Base
            _ninjectKernel = new StandardKernel();

            // Domain
            _ninjectKernel.Bind<DbContext>().To<GoodsStoreDB>();
            _ninjectKernel.Bind<IRepository<Good>>().To<GenericRepository<Good>>();
            _ninjectKernel.Bind<IRepository<Category>>().To<GenericRepository<Category>>();
            _ninjectKernel.Bind<IRepository<Manufacturer>>().To<GenericRepository<Manufacturer>>();
            _ninjectKernel.Bind<IRepository<Photo>>().To<GenericRepository<Photo>>();
            _ninjectKernel.Bind<IRepository<Sale>>().To<GenericRepository<Sale>>();
            _ninjectKernel.Bind<IRepository<SalePos>>().To<GenericRepository<SalePos>>();
            _ninjectKernel.Bind<IRepository<User>>().To<GenericRepository<User>>();
            _ninjectKernel.Bind<IUnitOfWork>().To<UnitOfWork>();

            // Business
            _ninjectKernel.Bind<IService<GoodDTO>>().To<GenericService<GoodDTO, Good>>();
            _ninjectKernel.Bind<IService<CategoryDTO>>().To<GenericService<CategoryDTO, Category>>();
            _ninjectKernel.Bind<IService<ManufacturerDTO>>().To<GenericService<ManufacturerDTO, Manufacturer>>();
            _ninjectKernel.Bind<IService<PhotoDTO>>().To<GenericService<PhotoDTO, Photo>>();
            _ninjectKernel.Bind<IService<SaleDTO>>().To<GenericService<SaleDTO, Sale>>();
            _ninjectKernel.Bind<IService<SalePosDTO>>().To<GenericService<SalePosDTO, SalePos>>();
            _ninjectKernel.Bind<IService<UserDTO>>().To<GenericService<UserDTO, User>>();
            _ninjectKernel.Bind<IServicesUnitOfWork>().To<ServicesUnitOfWork>();


            //Mapp
            InitMapping();
        }

        private void InitMapping()
        {
            //_ninjectKernel.Bind<IValueResolver<Category, CategoryDTO, bool>>();

            var mapperConfiguration = CreateConfiguration();
            _ninjectKernel.Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();

            _ninjectKernel.Bind<IMapper>().ToMethod(ctx =>
                 new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddExpressionMapping();
                //cfg.AddMaps(GetType().Assembly);

                #region Map Category
                cfg.CreateMap<Category, CategoryDTO>()
                    .ForMember(dto => dto.Id, conf => conf.MapFrom(dll => dll.CategoryId))
                    .ForMember(dto => dto.Title, conf => conf.MapFrom(dll => dll.CategoryName))
                    .ForMember(dto => dto.Goods, conf => conf.MapFrom(dll => dll.Good.Select(i => i.GoodId)));
                cfg.CreateMap<CategoryDTO, Category>()
                    .ForMember(dll => dll.CategoryId, conf => conf.MapFrom(dto => dto.Id))
                    .ForMember(dll => dll.CategoryName, conf => conf.MapFrom(dto => dto.Title))
                    .ForMember(dll => dll.Good, conf => conf.Ignore());
                #endregion

                #region Map manufacturer
                cfg.CreateMap<Manufacturer, ManufacturerDTO>()
                    .ForMember(dto => dto.Id, conf => conf.MapFrom(dll => dll.ManufacturerId))
                    .ForMember(dto => dto.Title, conf => conf.MapFrom(dll => dll.ManufacturerName))
                    .ForMember(dto => dto.Goods, conf => conf.MapFrom(dll => dll.Good.Select(i => i.GoodId)));
                cfg.CreateMap<ManufacturerDTO, Manufacturer>()
                    .ForMember(dll => dll.ManufacturerId, conf => conf.MapFrom(dto => dto.Id))
                    .ForMember(dll => dll.ManufacturerName, conf => conf.MapFrom(dto => dto.Title))
                    .ForMember(dll => dll.Good, conf => conf.Ignore());
                #endregion



            });

            return config;
        }

        public object GetService(Type serviceType)
        {
            return _ninjectKernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _ninjectKernel.GetAll(serviceType);
        }

    }


}
