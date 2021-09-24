[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(GoodsStore.Infrastructure.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(GoodsStore.Infrastructure.App_Start.NinjectWebCommon), "Stop")]

namespace GoodsStore.Infrastructure.App_Start
{
    using AutoMapper;
    using AutoMapper.Extensions.ExpressionMapping;
    using GoodsStore.Business.Models.Concrete;
    using GoodsStore.Business.Services.Abstract;
    using GoodsStore.Business.Services.Concrete;
    using GoodsStore.Domain.Abstract;
    using GoodsStore.Domain.Concrete;
    using GoodsStore.Domain.Context;
    using GoodsStore.Domain.Entities;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Ninject.Web.WebApi;
    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

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
            kernel.Bind<DbContext>().To<GoodsStoreDB>().InThreadScope().WithConstructorArgument("name", "GoodsStoreDB");
            kernel.Bind<IRepository<Good>>().To<GenericRepository<Good>>();
            kernel.Bind<IRepository<Category>>().To<GenericRepository<Category>>();
            kernel.Bind<IRepository<Manufacturer>>().To<GenericRepository<Manufacturer>>();
            kernel.Bind<IRepository<Photo>>().To<GenericRepository<Photo>>();
            kernel.Bind<IRepository<Order>>().To<GenericRepository<Order>>();
            kernel.Bind<IRepository<OrderDetails>>().To<GenericRepository<OrderDetails>>();
            kernel.Bind<IRepository<User>>().To<UserRepo>();
            kernel.Bind<IRepository<Role>>().To<GenericRepository<Role>>();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();

            // Business
            kernel.Bind<IService<GoodDTO>>().To<GenericService<GoodDTO, Good>>();
            kernel.Bind<IService<CategoryDTO>>().To<GenericService<CategoryDTO, Category>>();
            kernel.Bind<IService<ManufacturerDTO>>().To<GenericService<ManufacturerDTO, Manufacturer>>();
            kernel.Bind<IService<PhotoDTO>>().To<GenericService<PhotoDTO, Photo>>();
            kernel.Bind<IService<OrderDTO>>().To<GenericService<OrderDTO, Order>>();
            kernel.Bind<IService<OrderDetailsDTO>>().To<GenericService<OrderDetailsDTO, OrderDetails>>();
            kernel.Bind<IService<UserDTO>>().To<GenericService<UserDTO, User>>();
            kernel.Bind<IService<RoleDTO>>().To<GenericService<RoleDTO, Role>>();
            kernel.Bind<IServicesUnitOfWork>().To<ServicesUnitOfWork>();
            kernel.Bind<IAuthManager>().To<JWTAuthManager>().WithConstructorArgument("secret", ConfigurationManager.AppSettings["Secret"]);
            //kernel.Bind<IUnitOfWork>().To<RawUnitOfWork>();

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

                #region Map Good
                cfg.CreateMap<Good, GoodDTO>()
                    .ForMember(dto => dto.Categories, conf => conf.MapFrom(dll => dll.Categories))
                    .ForMember(dto => dto.Manufacturer, conf => conf.MapFrom(dll => dll.Manufacturer));
                cfg.CreateMap<GoodDTO, Good>()
                    .ForMember(dll => dll.Categories, conf => conf.MapFrom(dto=>dto.Categories))
                    .ForMember(dll => dll.Manufacturer, conf => conf.MapFrom(dto => dto.Manufacturer));
                #endregion

                #region Map Category
                cfg.CreateMap<Category, CategoryDTO>()
                    .ForMember(dto => dto.Goods, conf => conf.MapFrom(dll => dll.Goods.Select(i => i.Id)));
                cfg.CreateMap<CategoryDTO, Category>()
                    .ForMember(dll => dll.Goods, conf => conf.Ignore());
                #endregion

                #region Map manufacturer
                cfg.CreateMap<Manufacturer, ManufacturerDTO>()
                    .ForMember(dto => dto.Goods, conf => conf.MapFrom(dll => dll.Goods.Select(i => i.Id)));
                cfg.CreateMap<ManufacturerDTO, Manufacturer>()
                    .ForMember(dll => dll.Goods, conf => conf.Ignore());
                #endregion

                #region Map Roles
                cfg.CreateMap<Role, RoleDTO>()
                    .ForMember(dto => dto.UserIds, conf => conf.MapFrom(dll => dll.Users.Select(i => i.Id)));
                cfg.CreateMap<RoleDTO, Role>()
                    .ForMember(dll => dll.Users, conf => conf.MapFrom(dto => dto.UserIds.Select(i => new User() { Id = i })));
                #endregion

                #region Map User
                cfg.CreateMap<User, UserDTO>()
                    .ForMember(dto => dto.RoleIds, conf => conf.MapFrom(dll => dll.Roles.Select(i => i.Id)));
                cfg.CreateMap<UserDTO, User>()
                    .ForMember(dll => dll.Roles, conf => conf.MapFrom(dto => dto.RoleIds.Select(i => new Role() { Id = i })));
                #endregion

            });

            return config;
        }


    }
}