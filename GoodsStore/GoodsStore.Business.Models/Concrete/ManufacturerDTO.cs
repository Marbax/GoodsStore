using System.Collections.Generic;

namespace GoodsStore.Business.Models.Concrete
{
    public class ManufacturerDTO : GenericDTO
    {
        public IEnumerable<int> Goods { get; set; } = new List<int>();
    }

}
