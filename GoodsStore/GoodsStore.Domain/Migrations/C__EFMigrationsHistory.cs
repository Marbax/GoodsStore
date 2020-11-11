namespace GoodsStore.Migrations.Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("__EFMigrationsHistory")]
    public partial class C__EFMigrationsHistory
    {
        [Key]
        [StringLength(150)]
        public string MigrationId { get; set; }

        [Required]
        [StringLength(32)]
        public string ProductVersion { get; set; }
    }
}
