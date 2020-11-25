using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodsStore.Domain.Entities
{
    public class Good
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public int Count { get; set; }

        [StringLength(500)]
        [DefaultValue("")]
        public string Description { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
