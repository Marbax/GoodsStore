using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoodsStore.Domain.Entities
{
    public class Manufacturer
    {
        public int Id { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(400)]
        [DefaultValue("")]
        public string Description { get; set; }

        public virtual ICollection<Good> Goods { get; set; }
    }
}
