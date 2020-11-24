using GoodsStore.Business.Models;
using GoodsStore.Business.Models.Concrete;
using System;

namespace GoodsStore.Business.Services.Abstract
{
    public interface IServicesUnitOfWork : IDisposable
    {
        IService<GoodDTO> Goods { get; }
        IService<SalePosDTO> SalesPoses { get; }
        IService<SaleDTO> Sales { get; }
        IService<PhotoDTO> Photos { get; }
        IService<ManufacturerDTO> Manufacturers { get; }
        IService<UserDTO> Users { get; }
        IService<CategoryDTO> Categories { get; }

        void Save();
    }
}
