namespace GoodsStore.Entities.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Photo")]
    public partial class Photo
    {
        public int PhotoId { get; set; }

        public int? GoodId { get; set; }

        [Required]
        [StringLength(200)]
        public string PhotoPath { get; set; }
    }
}
