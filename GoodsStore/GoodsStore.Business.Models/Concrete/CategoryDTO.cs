using System.Collections.Generic;

namespace GoodsStore.Business.Models.Concrete
{
    public class CategoryDTO : GenericDTO
    {
        public string Description { get; set; }
        public IEnumerable<int> Goods { get; set; } = new List<int>();
    }
}