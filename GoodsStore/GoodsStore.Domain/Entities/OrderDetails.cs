using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodsStore.Domain.Entities
{
    public class OrderDetails
    {
        public int Id { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Column(TypeName = "money")]
        public decimal Summ { get; set; }

        public int GoodId { get; set; }
        public virtual Good Good { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
