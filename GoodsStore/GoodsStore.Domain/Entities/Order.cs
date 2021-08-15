using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodsStore.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        [StringLength(100)]
        public string Address { get; set; }

        [Required]
        [StringLength(40)]
        public string Phone { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Column(TypeName = "date")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue("getutcdate()")]
        public DateTime OrderDateUtc { get; set; }

        [Column(TypeName = "money")]
        public decimal Total { get; set; }

        /// <summary>
        /// Is it needed?
        /// </summary>
        public int PositionsAmount { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; }

        public virtual Payment Payment { get; set; }
    }
}
