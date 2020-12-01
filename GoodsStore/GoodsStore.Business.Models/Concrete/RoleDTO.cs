using System.Collections.Generic;

namespace GoodsStore.Business.Models.Concrete
{
    public class RoleDTO : GenericDTO
    {
        public IEnumerable<int> UserIds { get; set; } = new List<int>();
    }
}