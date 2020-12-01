using GoodsStore.Domain.Entities;
using System.Data.Entity;
using System.Linq;

namespace GoodsStore.Domain.Concrete
{
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
    }
}
