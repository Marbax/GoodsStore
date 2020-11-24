using System.Collections.Generic;

namespace GoodsStore.Business.Models.Concrete
{
    public class CategoryDTO : GenericDTO
    {
        public IEnumerable<int> Goods { get; set; } = new List<int>();
    }
}