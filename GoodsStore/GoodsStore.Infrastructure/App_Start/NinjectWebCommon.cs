[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(GoodsStore.Infrastructure.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(GoodsStore.Infrastructure.App_Start.NinjectWebCommon), "Stop")]

namespace GoodsStore.Infrastructure.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;
    using Ninject.Web.WebApi;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using GoodsStore.Domain.Context;
    using GoodsStore.Domain.Abstract;
    using GoodsStore.Domain.Entities;
    using GoodsStore.Domain.Concrete;
    using GoodsStore.Business.Services.Abstract;
    using GoodsStore.Business.Models;
    using GoodsStore.Business.Services.Concrete;
    using System.Data.Entity;
    using AutoMapper;
    using AutoMapper.Extensions.ExpressionMapping;
    using System.Linq;
    using GoodsStore.Business.Models.Concrete;

    /// <summary>
    /// source = http://www.peterprovost.org/blog/2012/06/19/adding-ninject-to-web-api/
    /// </summary>
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        public static void RegisterNinject(HttpConfiguration configuration)
        {
            // Set Web API Resolver
            configuration.DependencyResolver = new NinjectDependencyResolver(bootstrapper.Kernel);
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            // Domain
            kernel.Bind<DbContext>().To<GoodsStoreDB>();
            kernel.Bind<IRepository<Good>>().To<GenericRepository<Good>>();
            kernel.Bind<IRepository<Category>>().To<GenericRepository<Category>>();
            kernel.Bind<IRepository<Manufacturer>>().To<GenericRepository<Manufacturer>>();
            kernel.Bind<IRepository<Photo>>().To<GenericRepository<Photo>>();
            kernel.Bind<IRepository<Sale>>().To<GenericRepository<Sale>>();
            kernel.Bind<IRepository<SalePos>>().To<GenericRepository<SalePos>>();
            kernel.Bind<IRepository<User>>().To<GenericRepository<User>>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();

            // Business
            kernel.Bind<IService<GoodDTO>>().To<GenericService<GoodDTO, Good>>();
            kernel.Bind<IService<CategoryDTO>>().To<GenericService<CategoryDTO, Category>>();
            kernel.Bind<IService<ManufacturerDTO>>().To<GenericService<ManufacturerDTO, Manufacturer>>();
            kernel.Bind<IService<PhotoDTO>>().To<GenericService<PhotoDTO, Photo>>();
            kernel.Bind<IService<SaleDTO>>().To<GenericService<SaleDTO, Sale>>();
            kernel.Bind<IService<SalePosDTO>>().To<GenericService<SalePosDTO, SalePos>>();
            kernel.Bind<IService<UserDTO>>().To<GenericService<UserDTO, User>>();
            kernel.Bind<IServicesUnitOfWork>().To<ServicesUnitOfWork>();


            //Mapp

            //kernel.Bind<IValueResolver<Category, CategoryDTO, bool>>();

            var mapperConfiguration = CreateConfiguration();
            kernel.Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();

            kernel.Bind<IMapper>().ToMethod(ctx =>
                 new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));

        }

        private static MapperConfiguration CreateConfiguration()
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


    }
}