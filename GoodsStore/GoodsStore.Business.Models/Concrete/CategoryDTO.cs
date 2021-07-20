using System.Collections.Generic;

namespace GoodsStore.Business.Models.Concrete
{
    /// <summary>
    /// Good's Category
    /// </summary>
    public class CategoryDTO : GenericDTO
    {
        /// <summary>
        /// Description of category
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Ids of goods to wich category represents
        /// </summary>
        public IEnumerable<int> Goods { get; set; } = new List<int>();
    }
}