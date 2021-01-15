using GoodsStore.Domain.Entities;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace GoodsStore.Domain.Concrete
{
    /// <summary>
    /// AutoMapper loose context , so , thats why this exists
    /// </summary>
    public class UserRepo : GenericRepository<User>
    {
        private DbSet<Role> _roles;

        public UserRepo(DbContext db) : base(db)
        {
            _roles = db.Set<Role>();
        }

        public override User Add(User entity)
        {
            var rIds = entity.Roles.Select(i => i.Id).ToList();
            var roles = _roles.Where(i => rIds.Contains(i.Id)).ToList();
            entity.Roles = roles;
            return _dbSet.Add(entity);
        }

        public override void CreateOrUpdate(User entity)
        {
            var contextEntity = Get(entity.Id);
            var rIds = entity.Roles.Select(i => i.Id).ToList();
            var roles = _roles.Where(i => rIds.Contains(i.Id)).ToList();
            contextEntity.Roles = roles;
            _dbSet.AddOrUpdate(entity);
        }
    }
}
