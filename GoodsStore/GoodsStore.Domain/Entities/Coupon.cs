using System.ComponentModel.DataAnnotations.Schema;

namespace GoodsStore.Domain.Entities
{
    public class Coupon
    {
        [ForeignKey("OrderDetails")] // Relations One to Zero|One (OrderDetails to Coupon)
        public long Id { get; set; }

        public long OrderDetailsId { get; set; }

        public virtual OrderDetails OrderDetails { get; set; }

        /// <summary>
        /// Percentage?
        /// </summary>
        public decimal Discount { get; set; }
    }
}