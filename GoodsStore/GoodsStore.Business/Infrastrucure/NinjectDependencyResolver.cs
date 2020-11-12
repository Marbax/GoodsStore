using GoodsStore.Domain.Abstract;
using GoodsStore.Domain.Concrete;
using GoodsStore.Domain.Context;
using GoodsStore.Domain.Entities;
using Ninject;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            _ninjectKernel.Bind<DbContext>().To<GoodsStoreDB>().InThreadScope();// InSingletonScope();
            _ninjectKernel.Bind<IRepository<Good>>().To<GenericRepository<Good>>();
            _ninjectKernel.Bind<IRepository<Category>>().To<GenericRepository<Category>>();
            _ninjectKernel.Bind<IRepository<Manufacturer>>().To<GenericRepository<Manufacturer>>();
            _ninjectKernel.Bind<IRepository<Photo>>().To<GenericRepository<Photo>>();
            _ninjectKernel.Bind<IRepository<Sale>>().To<GenericRepository<Sale>>();
            _ninjectKernel.Bind<IRepository<SalePos>>().To<GenericRepository<SalePos>>();
            _ninjectKernel.Bind<IUnitOfWork>().To<UnitOfWork>();

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
