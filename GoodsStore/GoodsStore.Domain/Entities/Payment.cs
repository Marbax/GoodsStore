using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodsStore.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        public virtual Order Order { get; set; }

        [Required]
        [StringLength(100)]
        public string PaymentMethod { get; set; }

        [Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [Column(TypeName = "datetime")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue("getutcdate()")]
        public DateTime PayedAtUtc { get; set; }
    }
}