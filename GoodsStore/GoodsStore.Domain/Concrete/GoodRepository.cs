using GoodsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsStore.Domain.Concrete
{
    public class GoodRepository : GenericRepository<Good>
    {
        private readonly DbSet<Manufacturer> _dbSetManufacturer;
        private readonly DbSet<Category> _dbSetCategories;
        private readonly DbSet<OrderDetails> _dbSetOrderDetails;
        public GoodRepository(DbContext db) : base(db)
        {
            _dbSetManufacturer = db.Set<Manufacturer>();
            _dbSetCategories = db.Set<Category>();
            _dbSetOrderDetails = db.Set<OrderDetails>();
        }

        /// <summary>
        /// Correctly bind manufacturer and categories
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override Good Add(Good entity)
        {
            var manufacturer = _dbSetManufacturer.Find(entity?.Manufacturer?.Id);
            var catIds = entity.Categories.Select(c => c.Id);
            var categories = _dbSetCategories.Where(i => catIds.Contains(i.Id)).ToList();

            var ordersIds = entity.OrderDetails.Select(c => c.Id);
            var orderDetails = _dbSetOrderDetails.Where(i => ordersIds.Contains(i.Id)).ToList();
            entity.OrderDetails = orderDetails;

            entity.Manufacturer = manufacturer;
            entity.Categories = categories;

            return base.Add(entity);
        }
    }
}
