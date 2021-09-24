using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GoodsStore.Business.Models.Concrete
{
    public class GoodDTO : GenericDTO
    {
        [Required]
        [StringLength(200)]
        public override string Title { get; set; } = "";

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(1D, double.MaxValue)]
        public decimal Price { get; set; }

        /// <summary>
        /// Value Added Tax (Percentage?)
        /// </summary>
        public float Vat { get; set; }

        [Required]
        public int Count { get; set; }

        public IEnumerable<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();

        public ManufacturerDTO Manufacturer { get; set; }

        public IEnumerable<PhotoDTO> Photos { get; set; } = new List<PhotoDTO>();

        public IEnumerable<OrderDetailsDTO> OrderDetails { get; set; } = new List<OrderDetailsDTO>();
    }
}