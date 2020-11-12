using GoodsStore.Domain.Concrete;
using System.Data.Entity;

namespace GoodsStore.Business.Services.Concrete
{
    public class RawUnitOfWork : UnitOfWork
    {
        public RawUnitOfWork(DbContext db) : base(db)
        {
        }
    }

}
