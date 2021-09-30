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
        private readonly DbSet<Photo> _dbSetPhotos;
        public GoodRepository(DbContext db) : base(db)
        {
            _dbSetManufacturer = db.Set<Manufacturer>();
            _dbSetCategories = db.Set<Category>();
            _dbSetOrderDetails = db.Set<OrderDetails>();
            _dbSetPhotos = db.Set<Photo>();
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

            #region Photos logic
            var oldSavedPhotoIds = entity.Photos.Select(c => c.Id);
            var oldSavedPhotos = _dbSetPhotos.Where(i => oldSavedPhotoIds.Contains(i.Id)).ToList();

            var newPhotos = entity.Photos.Where(i => i.Id == 0);

            List<Photo> currentPhotos = newPhotos.ToList();
            currentPhotos.AddRange(oldSavedPhotos);
            #endregion

            entity.Manufacturer = manufacturer;
            entity.Categories = categories;
            entity.Photos = currentPhotos;

            return base.Add(entity);
        }

        public override void CreateOrUpdate(Good entity)
        {
            //TODO: made photos proccessing
            var manufacturer = _dbSetManufacturer.Find(entity?.Manufacturer?.Id);
            var catIds = entity.Categories.Select(c => c.Id);
            var categories = _dbSetCategories.Where(i => catIds.Contains(i.Id)).ToList();

            var ordersIds = entity.OrderDetails.Select(c => c.Id);
            var orderDetails = _dbSetOrderDetails.Where(i => ordersIds.Contains(i.Id)).ToList();
            entity.OrderDetails = orderDetails;

            #region Photos logic
            var currentGood = _dbSet.Find(entity.Id);
            var photosToDelete = _dbSetPhotos.Where(i => i.Goods.Count < 2 && i.Goods.Contains(currentGood)).ToList();
            // remove unused
            photosToDelete.ForEach(i => _dbSetPhotos.Remove(i));

            var oldSavedPhotoIds = entity.Photos.Select(c => c.Id);
            var oldSavedPhotos = _dbSetPhotos.Where(i => oldSavedPhotoIds.Contains(i.Id)).ToList();

            var newPhotos = entity.Photos.Where(i => i.Id == 0);

            List<Photo> currentPhotos = newPhotos.ToList();
            currentPhotos.AddRange(oldSavedPhotos);
            #endregion

            entity.Manufacturer = manufacturer;
            entity.Categories = categories;
            entity.Photos = currentPhotos;

            base.CreateOrUpdate(entity);
        }
    }
}
