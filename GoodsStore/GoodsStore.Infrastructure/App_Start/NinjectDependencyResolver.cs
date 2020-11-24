using GoodsStore.Infrastructure.App_Start;
using Ninject;
using System.Web.Http.Dependencies;

namespace GoodsStore.App_Start.Infrastructure
{
    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
    {
        private readonly IKernel _ninjectKernel;

        public NinjectDependencyResolver(IKernel kernel) : base(kernel)
        {
            _ninjectKernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(_ninjectKernel.BeginBlock());
        }
    }
}
