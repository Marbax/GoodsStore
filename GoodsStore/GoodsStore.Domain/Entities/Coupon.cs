using System.ComponentModel.DataAnnotations.Schema;

namespace GoodsStore.Domain.Entities
{
    public class Coupon
    {
        [ForeignKey("OrderDetails")] // Relations One to Zero|One (OrderDetails to Coupon)
        public int Id { get; set; }

        public int OrderDetailsId { get; set; }

        public virtual OrderDetails OrderDetails { get; set; }

        /// <summary>
        /// Percentage?
        /// </summary>
        public decimal Discount { get; set; }
    }
}