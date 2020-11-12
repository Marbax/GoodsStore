namespace GoodsStore.Domain.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SalePos
    {
        public int SalePosId { get; set; }

        public int SaleId { get; set; }

        public int GoodId { get; set; }

        public int CountGood { get; set; }

        [Column(TypeName = "money")]
        public decimal Summa { get; set; }

        public virtual Good Good { get; set; }

        public virtual Sale Sale { get; set; }
    }
}
