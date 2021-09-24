using System.Collections.Generic;

namespace GoodsStore.Business.Models.Concrete
{
    public class ManufacturerDTO : GenericDTO
    {
        /// <summary>
        /// Description of Manufacturer
        /// </summary>
        public string Description { get; set; }

        public IEnumerable<int> Goods { get; set; } = new List<int>();
    }

}
