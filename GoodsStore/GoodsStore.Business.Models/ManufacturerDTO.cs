using System.Collections.Generic;

namespace GoodsStore.Business.Models
{
    public class ManufacturerDTO
    {
        public int Id { get; set; } = 0;
        public string Title { get; set; } = "Title";
        public IEnumerable<int> Goods { get; set; } = new List<int>();
    }

}
